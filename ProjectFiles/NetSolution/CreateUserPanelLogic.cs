#region Using directives
using System;
using System.Linq;
using FTOptix.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.Recipe;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.WebUI;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class CreateUserPanelLogic : BaseNetLogic
{
	[ExportMethod]
    public void CreateUser(string username, string password, string locale, out NodeId result)
    {
		result = NodeId.Empty;

		if (string.IsNullOrEmpty(username))
		{
			ShowMessage(1);
			Log.Error("EditUserDetailPanelLogic", "Cannot create user with empty username");
			return;
		}

		result = GenerateUser(username, password, locale);
    }

    private NodeId GenerateUser(string username, string password, string locale)
    {
		var users = GetUsers();
		if (users == null)
		{
			ShowMessage(2);
			Log.Error("EditUserDetailPanelLogic", "Unable to get users");
			return NodeId.Empty;
		}

		foreach (var child in users.Children.OfType<User_21CFR>())
		{
			if (child.BrowseName.Equals(username, StringComparison.OrdinalIgnoreCase))
			{
				ShowMessage(10);
				Log.Error("EditUserDetailPanelLogic", "Username already exists");
				return NodeId.Empty;
			}
		}

		//-----------Customized Logic Start-----------------
		// User Password Strength Check
		CheckPasswordStrength NewUserPassCheck = new CheckPasswordStrength();
		bool NewPassStrengthCheck = false;
		NewPassStrengthCheck = NewUserPassCheck.CheckPassword(password);
		if (!NewPassStrengthCheck)
		{
			ShowMessage(20);
			Log.Error("EditUserDetailPanelLogic", "Entered Password is not Complex");
			return NodeId.Empty;
		}
		//-----------Customized Logic End-------------------
		
		var user = InformationModel.MakeObject<User_21CFR>(username);
		users.Add(user);

		//Apply LocaleId
		if (!string.IsNullOrEmpty(locale))
			user.LocaleId = locale;

		//Apply groups
		ApplyGroups(user);
		
		//Apply password
		var result = Session.ChangePassword(username, password, string.Empty);

		switch (result.ResultCode)
		{
			case FTOptix.Core.ChangePasswordResultCode.Success:
				user.Password_Creation_Date = DateTime.Now;
				break;
			case FTOptix.Core.ChangePasswordResultCode.WrongOldPassword:
				//Not applicable
				break;
			case FTOptix.Core.ChangePasswordResultCode.PasswordAlreadyUsed:
				//Not applicable
				break;
			case FTOptix.Core.ChangePasswordResultCode.PasswordChangedTooRecently:
				//Not applicable
				break;
			case FTOptix.Core.ChangePasswordResultCode.PasswordTooShort:
				ShowMessage(6);
				users.Remove(user);
				return NodeId.Empty;
			case FTOptix.Core.ChangePasswordResultCode.UserNotFound:
				//Not applicable
				break;
			case FTOptix.Core.ChangePasswordResultCode.UnsupportedOperation:
				ShowMessage(8);
				users.Remove(user);
				return NodeId.Empty;

		}



		//var UserList = LogicObject.Owner.Owner.Owner.Children["UsersList"].Children["UserList"];
		//var _LogicObject = LogicObject.Owner.Owner.Owner.Children["UsersList"].Children["ChildrenCounter"];

		////Clean files list


		//UserList.Children.ToList().ForEach((entry) => entry.Delete());

		//var UserDetails = _LogicObject.GetAlias("Users");

		//foreach (var child in UserDetails.Children.OfType<User_21CFR>())
		//{
		//	if (child.BrowseName != "Pima")
		//	{

		//		var User = InformationModel.MakeObject<User_21CFR>(child.BrowseName);
		//		UserList.Add(InformationModel.Get<User_21CFR>(child.NodeId));
		//	}
		//}


		return user.NodeId;
	}

	private void ApplyGroups(User_21CFR user)
	{
		Panel groupsPanel = Owner.Get<Panel>("HorizontalLayout1/GroupsPanel1");
		IUAVariable editable = groupsPanel.GetVariable("Editable");
		IUANode groups = groupsPanel.GetAlias("Groups");
		var panel = groupsPanel.Children.Get("ScrollView").Get("Container");

		if (editable.Value == false)
			return;

		if (user == null || groups == null || panel == null)
			return;

		var userNode = InformationModel.Get(user.NodeId);
		if (userNode == null)
			return;

		var groupCheckBoxes = panel.Refs.GetObjects(OpcUa.ReferenceTypes.HasOrderedComponent, false);
		string usergroupname = "";

		foreach (var groupCheckBoxNode in groupCheckBoxes)
		{
			var group = groups.Get(groupCheckBoxNode.BrowseName);
			if (group == null)
				return;

			bool userHasGroup = UserHasGroup(user, group.NodeId);

			if (groupCheckBoxNode.GetVariable("Checked").Value && !userHasGroup)
			{
				userNode.Refs.AddReference(FTOptix.Core.ReferenceTypes.HasGroup, group);
			}
			else if (!groupCheckBoxNode.GetVariable("Checked").Value && userHasGroup)
			{
				userNode.Refs.RemoveReference(FTOptix.Core.ReferenceTypes.HasGroup, group.NodeId, false);
			}
			if (groupCheckBoxNode.GetVariable("Checked").Value)
			{
				usergroupname = usergroupname + group.BrowseName + ",";
			}
		}

		//-----------Customized Logic Start-----------------
		int crNoOfGroups = Project.Current.GetVariable("UI/UserObjects/UserGroups/UserGroupCounts").Value;
		string[] crUGroupName = Project.Current.GetVariable("UI/UserObjects/UserGroups/UserGroupName").Value;
		int cnt2 = crNoOfGroups;
		while (cnt2 > 0)
		{
			if (usergroupname.Contains(crUGroupName[cnt2-1]))
			{
				usergroupname = crUGroupName[cnt2-1];
				break;
			}
			else if (cnt2 == 1)
			{
				usergroupname = "not assigned";
				break;
			}
			cnt2 -= 1;
		}
		
		// User Group Change Activity Logging into Audit Database
		AuditTrailLogging UserCreateGroup = new AuditTrailLogging();
		UserCreateGroup.LogIntoAudit("New user created", "'" + user.BrowseName + "'" + " with '" + usergroupname + "' group", Session.User.BrowseName, "UserCreateEvent");

		//-----------Customized Logic End-------------------
	}

	private bool UserHasGroup(IUANode user, NodeId groupNodeId)
	{
		if (user == null)
			return false;
		return user.Refs.GetObjects(FTOptix.Core.ReferenceTypes.HasGroup, false).Any(u => u.NodeId == groupNodeId);
	}

	private IUANode GetUsers()
	{
		var pathResolverResult = LogicObject.Context.ResolvePath(LogicObject, "{Users}");
		if (pathResolverResult == null)
			return null;
		if (pathResolverResult.ResolvedNode == null)
			return null;

		return pathResolverResult.ResolvedNode;
	}

	private void ShowMessage(int message)
	{
		var errorMessageVariable = LogicObject.GetVariable("ErrorMessage");
		if (errorMessageVariable != null)
			errorMessageVariable.Value = message;

		delayedTask?.Dispose();

		delayedTask = new DelayedTask(DelayedAction, 5000, LogicObject);
		delayedTask.Start();
	}

	private void DelayedAction(DelayedTask task)
	{
		if (task.IsCancellationRequested)
			return;

		var errorMessageVariable = LogicObject.GetVariable("ErrorMessage");
		if (errorMessageVariable != null)
		{
			errorMessageVariable.Value = 0;
		}
		delayedTask?.Dispose();
	}

	private DelayedTask delayedTask;
}
