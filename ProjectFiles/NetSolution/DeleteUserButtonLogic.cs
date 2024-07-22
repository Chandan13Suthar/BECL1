#region Using directives
using System;
using FTOptix.HMIProject;
using UAManagedCore;
using FTOptix.NetLogic;
using FTOptix.UI;
using System.Linq;
using FTOptix.WebUI;
using FTOptix.Recipe;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class DeleteUserButtonLogic : BaseNetLogic
{

     public override void Start()
    {
        Label DeleteResult = Project.Current.Get<Label>("UI/NewUserEditor/EditUserDetailPanel/HorizontalLayout1/Details/DeleteUserMessage");
        DeleteResult.Text = "";
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {

        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void DeleteUser(NodeId userToDelete)
    {
        var userObjectToRemove = InformationModel.Get(userToDelete);
        if (userObjectToRemove == null)
        {
            Log.Error("UserEditor", "Cannot obtain the selected user.");
            return;
        }
        
        var userVariable = Owner.Owner.Owner.Owner.GetVariable("Users");
        if (userVariable == null)
        {
            Log.Error("UserEditor", "Missing user variable in UserEditor Panel.");
            return;
        }

        if (userVariable.Value == null || (NodeId)userVariable.Value == NodeId.Empty)
        {
            Log.Error("UserEditor", "Fill User variable in UserEditor.");
            return;
        }
        var usersFolder = InformationModel.Get(userVariable.Value);
        if (usersFolder == null)
        {
            Log.Error("UserEditor", "Cannot obtain Users folder.");
            return;
        }
        Log.Info("UserEditor","User to be deleted: " + userObjectToRemove.BrowseName + "Curr User: " + Session.User.BrowseName);

        if (userObjectToRemove.BrowseName == "Pima")
        {

            return;
            //usersFolder.Remove(userObjectToRemove);
            //-----------Customized Logic Start-----------------
            // Delete User Activity Logging into Audit Database
           // AuditTrailLogging DeleteUser = new AuditTrailLogging();
           // DeleteUser.LogIntoAudit("User deleted", userObjectToRemove.BrowseName, Session.User.BrowseName, "UserDeleteEvent");
            //-----------Customized Logic End-------------------
           // ShowMessage("User deleted successfully");
        }



        if (userObjectToRemove.BrowseName != Session.User.BrowseName)
        {
            usersFolder.Remove(userObjectToRemove);
            //-----------Customized Logic Start-----------------
            // Delete User Activity Logging into Audit Database
            AuditTrailLogging DeleteUser = new AuditTrailLogging();
            DeleteUser.LogIntoAudit("User deleted", userObjectToRemove.BrowseName, Session.User.BrowseName, "UserDeleteEvent");
            //-----------Customized Logic End-------------------
            ShowMessage("User deleted successfully");

            //var UserList = LogicObject.Owner.Owner.Owner.Find("UserList");
            //var _LogicObject = LogicObject.Owner.Owner.Owner.Find("ChildrenCounter"); 

            ////Clean files list

            //UserList.Children.ToList().ForEach((entry) => entry.Delete());

            //var UserDetails = _LogicObject.GetAlias("Users");

            //foreach (var child in UserDetails.Children.OfType<User_21CFR>())
            //{
            //    if (child.BrowseName != "Pima")
            //    {

            //        var User = InformationModel.MakeObject<User_21CFR>(child.BrowseName);
            //        UserList.Add(InformationModel.Get<User_21CFR>(child.NodeId));
            //    }
            //}
        }
        else
        {
            ShowMessage("LoggedIn user can not be deleted");
            Log.Error("DeleteUserButtonLogic", "LoggedIn user can not be deleted");
        }
        
        if (usersFolder.Children.Count > 0)
        {
            var usersList = (ListBox)Owner.Owner.Owner.Get<ListBox>("HorizontalLayout1/UsersList");
            usersList.SelectedItem = usersFolder.Children.First().NodeId;
        }
    }


    private void ShowMessage(string message)
	{
		Label DeleteResult = Project.Current.Get<Label>("UI/NewUserEditor/EditUserDetailPanel/HorizontalLayout1/Details/DeleteUserMessage");
        DeleteResult.Text = message;
        DeleteResult.Visible = true;
		if (delayedTask != null)
			delayedTask?.Dispose();
		
		delayedTask = new DelayedTask(DelayedAction, 1500, LogicObject);
		delayedTask.Start();
	}

	private void DelayedAction(DelayedTask task)
	{
        Label DeleteResult = Project.Current.Get<Label>("UI/NewUserEditor/EditUserDetailPanel/HorizontalLayout1/Details/DeleteUserMessage");
        DeleteResult.Text = "";
        DeleteResult.Visible = false;
		delayedTask?.Dispose();
		if (task.IsCancellationRequested)
			return;
        
        
	}

    private DelayedTask delayedTask;

}
