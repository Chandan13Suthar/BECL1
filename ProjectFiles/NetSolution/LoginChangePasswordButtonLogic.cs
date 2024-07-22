#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.HMIProject;
using FTOptix.UI;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.Retentivity;
using FTOptix.Recipe;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.WebUI;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class LoginChangePasswordButtonLogic : BaseNetLogic
{
    [ExportMethod]
    public void PerformChangePassword(string oldPassword, string newPassword, string confirmPassword)
    {
        var outputMessageLabel = Owner.Owner.GetObject("ChangePasswordFormOutputMessage");
        var outputMessageLogic = outputMessageLabel.GetObject("LoginChangePasswordFormOutputMessageLogic");

        //-----------Customized Logic Start-----------------
		// User Password Strength Check
		CheckPasswordStrength ChgPassCheck = new CheckPasswordStrength();
		bool ChgassStrengthCheck = false;
		ChgassStrengthCheck = ChgPassCheck.CheckPassword(newPassword);
		//-----------Customized Logic End-------------------

        if (!ChgassStrengthCheck)
        {
            outputMessageLogic.ExecuteMethod("SetOutputMessage", new object[] { 20 });
        }
        else if (newPassword != confirmPassword)
        {
            outputMessageLogic.ExecuteMethod("SetOutputMessage", new object[] { 7 });
        }
        else
        {
            var username = Session.User.BrowseName;
            try
            {
                var userWithExpiredPassword = Owner.GetAlias("UserWithExpiredPassword");
                
                if (userWithExpiredPassword != null)
                    username = userWithExpiredPassword.BrowseName;
            }
            catch
            {
            }

            var result = Session.ChangePassword(username, newPassword, oldPassword);
            if (result.ResultCode == ChangePasswordResultCode.Success)
            {
                
                var UserAlias = LogicObject.GetAlias("Users");
                var userpasschanged = UserAlias.Get<User_21CFR>(username);
                userpasschanged.Password_Creation_Date = DateTime.Now;
                userpasschanged.Invalid_Login_Attempts = 0;
                userpasschanged.Change_Password_On_Next_Login = false;
                
                //-----------Customized Logic Start-----------------
                // User Password Changed Event Logging into Audit Database
                AuditTrailLogging UserPassChgDiag = new AuditTrailLogging();
                UserPassChgDiag.LogIntoAudit("User modified", "'" + username + "'" + " password changed", Session.User.BrowseName, "UserPasswordChangeEvent");
                //-----------Customized Logic End-------------------

                var parentDialog = Owner.Owner?.Owner?.Owner as Dialog;
                if (parentDialog != null && result.Success)
                    parentDialog.Close();
            }
            else
            {
                outputMessageLogic.ExecuteMethod("SetOutputMessage", new object[] { (int)result.ResultCode });
            }
        }
    }
}
