#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.WebUI;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class GetUserStatus : BaseNetLogic
{
    
    public override void Start()
    {
        userVar = Owner.GetVariable("User");
        userVar.VariableChange += userVar_VariableChange;
        
        UpdateUserBlockedState();
    }
    
    private void userVar_VariableChange(object sender, VariableChangeEventArgs e)
    {
        UpdateUserBlockedState();
    }

    private void UpdateUserBlockedState()
    {
        CheckBox BlockedCheckBox = Owner.Get<CheckBox>("UserStatus");
        CheckBox ActiveCheckBox = Owner.Get<CheckBox>("UserActive");

        var selectedusernode = Owner.GetAlias("User");
        string selectedusername = selectedusernode.BrowseName;

        var useralias = LogicObject.GetAlias("Users");
        bool UserFound = false;
        
        foreach (var child in useralias.Children)
        {
            if (child.BrowseName.Equals(selectedusername, StringComparison.OrdinalIgnoreCase))
            {
                UserFound = true;
            }
        }
        if (UserFound)
        {
            var selecteduser = useralias.Get<User_21CFR>(selectedusername);
            BlockedCheckBox.Checked = selecteduser.User_Blocked;
            ActiveCheckBox.Checked = selecteduser.User_Active;
        }
    }

    private IUAVariable userVar;
    
}
