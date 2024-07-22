#region Using directives
using System;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NetLogic;
using FTOptix.Core;
using FTOptix.UI;
using FTOptix.Recipe;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.WebUI;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class LoginButtonLogic : BaseNetLogic
{
    public override void Start()
    {
        ComboBox comboBox = Owner.Owner.Get<ComboBox>("Username");
        TextBox UserName = (TextBox)LogicObject.Owner.Owner.Get("UserName");
        UserName.Text = "";


        if (Project.Current.Authentication.AuthenticationMode == AuthenticationMode.ModelOnly)
        {
            comboBox.Mode = ComboBoxMode.Normal;
        }
        else
        {
            comboBox.Mode = ComboBoxMode.Editable;
        }
    }

    public override void Stop()
    {

    }

    [ExportMethod]
    public void PerformLogin(string username, string password)
    {
        var usersAlias = LogicObject.GetAlias("Users");
        ComboBox comboBox = Owner.Owner.Get<ComboBox>("Username");
        TextBox UserName = (TextBox)LogicObject.Owner.Owner.Get("UserName");


        if (usersAlias == null || usersAlias.NodeId == NodeId.Empty)
        {
            Log.Error("LoginButtonLogic", "Missing Users alias");
            return;
        }

        var passwordExpiredDialogType = LogicObject.GetAlias("PasswordExpiredDialogType") as DialogType;
        if (passwordExpiredDialogType == null)
        {
            Log.Error("LoginButtonLogic", "Missing PasswordExpiredDialogType alias");
            return;
        }

        var LoginAttemptFailedDialogbox = LogicObject.GetAlias("LoginFailedDialogType") as DialogType;
        if (LoginAttemptFailedDialogbox == null)
        {
            Log.Error("LoginButtonLogic", "Missing LoginFailedDialogType alias");
            return;
        }

        var UserBlockedDialogbox = LogicObject.GetAlias("UserBlockedDialogType") as DialogType;
        if (UserBlockedDialogbox == null)
        {
            Log.Error("LoginButtonLogic", "Missing UserBlockedDialogType alias");
            return;
        }

        var PassExpiryDailogbox = LogicObject.GetAlias("PasswordExpiryDialogType") as DialogType;
        if (PassExpiryDailogbox == null)
        {
            Log.Error("LoginButtonLogic", "Missing PasswordExpiryDialogType alias");
            return;
        }

        var InvalidUserDailogbox = LogicObject.GetAlias("InvalidUserDialogType") as DialogType;
        if (InvalidUserDailogbox == null)
        {
            Log.Error("LoginButtonLogic", "Missing InvalidUserDailogbox alias");
            return;
        }

        var InvalidMasterPasswordDailogbox = LogicObject.GetAlias("InvalidMasterPasswordDialogType") as DialogType;
        if (InvalidMasterPasswordDailogbox == null)
        {
            Log.Error("LoginButtonLogic", "Missing InvalidMasterPassword alias");
            return;
        }
       

        //---------------User Validation Start---------------------------------------

        

       

       if (comboBox.Text == null)
        {
            var ownerButton1 = (Button)Owner;
            ownerButton1.OpenDialog(InvalidUserDailogbox);

            return;
        }

        //if (comboBox.Text == "Pima")
        //{
        //    var ownerButton1 = (Button)Owner;
        //    ownerButton1.OpenDialog(InvalidUserDailogbox);

        //    return;
        //}


        if (usersAlias.Get(comboBox.Text) == null)
        {
            var ownerButton1 = (Button)Owner;
            ownerButton1.OpenDialog(InvalidUserDailogbox);

            return;
        }




        if (string.IsNullOrEmpty(username))
        {
            var ownerButton1 = (Button)Owner;
            ownerButton1.OpenDialog(InvalidUserDailogbox);
            Log.Error("EditUserDetailPanelLogic", "Cannot Login user with empty username");
            return;
           
        
        }

        //---------------User Validation End---------------------------------------

        Button loginButton = (Button)Owner;
        loginButton.Enabled = false;

        // Check If User is Active
        var UserDetails = usersAlias.Get<User_21CFR>(username);
        if (!UserDetails.User_Active)
        {
            var outputMessageLabel = Owner.Owner.GetObject("LoginFormOutputMessage");
            var outputMessageLogic = outputMessageLabel.GetObject("LoginFormOutputMessageLogic");
            outputMessageLogic.ExecuteMethod("SetOutputMessage", new object[] { (int)21 });
            loginButton.Enabled = true;
            return;
        }

        // Check If User is Blocked
        if (UserDetails.User_Blocked)
        {
            var outputMessageLabel = Owner.Owner.GetObject("LoginFormOutputMessage");
            var outputMessageLogic = outputMessageLabel.GetObject("LoginFormOutputMessageLogic");
            outputMessageLogic.ExecuteMethod("SetOutputMessage", new object[] { (int)20 });
            loginButton.Enabled = true;
            return;
        }
        
        //var checkUserPass = usersAlias.Get<User_21CFR>(username);

        try
        {
            var loginResult = Session.Login(username, password);
            
            if (loginResult.ResultCode == ChangeUserResultCode.PasswordExpired)
            {
                loginButton.Enabled = true;

                //-----------Customized Logic Start-----------------
                // User Password Expired Event Logging into Audit Database
                AuditTrailLogging UserPassExp = new AuditTrailLogging();
                UserPassExp.LogIntoAudit("User password expired", username, Session.User.BrowseName, "UserLoginEvent");
                //-----------Customized Logic End-------------------

                var user = usersAlias.Get<User_21CFR>(username);
                var ownerButton1 = (Button)Owner;
                passwordExpiredDialogType.GetVariable("ShowCurrentUser").Value = false;
                passwordExpiredDialogType.GetVariable("ShowFirstLogonText").Value = false;
                passwordExpiredDialogType.GetVariable("ShowLableText").Value = true;
                ownerButton1.OpenDialog(passwordExpiredDialogType, user.NodeId);
                return;
            }
            else if (loginResult.ResultCode != ChangeUserResultCode.Success)
            {
                loginButton.Enabled = true;

                 if (loginResult.ResultCode != ChangeUserResultCode.LoginAttemptBlocked)
               // if (loginResult.ResultCode == ChangeUserResultCode.WrongPassword)
                {
                    Log.Error("LoginButtonLogic", "Authentication failed");

                    //-----------Customized Logic Start-----------------
                    // User Login Failed Event Logging into Audit Database
                    AuditTrailLogging UserLoginFailed = new AuditTrailLogging();
                    UserLoginFailed.LogIntoAudit("Invalid login attempt", username, Session.User.BrowseName, "UserLoginEvent");
                    //-----------Customized Logic End-------------------

                    if (username == "Pima")
                    {
                        var ownerButton1 = (Button)Owner;
                        ownerButton1.OpenDialog(InvalidMasterPasswordDailogbox);

                        return;
                    }



                    UserDetails.Invalid_Login_Attempts = UserDetails.Invalid_Login_Attempts + 1;
                    Int32 LoginAttempLimit = Project.Current.GetVariable("UI/UserObjects/PasswordPolicy/InvalidLoginAttemptLimit").Value;
                    if (UserDetails.Invalid_Login_Attempts >= LoginAttempLimit)
                    {
                        UserDetails.User_Blocked = true;
                        AuditTrailLogging UserBlockedLog = new AuditTrailLogging();
                        UserBlockedLog.LogIntoAudit("User modified", "'" + username + "'" + " blocked", Session.User.BrowseName, "UserStatusChangeEvent");
                        UserDetails.Invalid_Login_Attempts = 0;

                        var ownerButton2 = (Button)Owner;
                        UserBlockedDialogbox.GetVariable("BlockedUser").Value = username;
                        ownerButton2.OpenDialog(UserBlockedDialogbox);
                    }
                    else
                    {
                        var ownerButton3 = (Button)Owner;
                        LoginAttemptFailedDialogbox.GetVariable("InvalidLoginUser").Value = username;
                        LoginAttemptFailedDialogbox.GetVariable("RemainingAttempt").Value = LoginAttempLimit - UserDetails.Invalid_Login_Attempts;
                        ownerButton3.OpenDialog(LoginAttemptFailedDialogbox);
                    }
                }
                else
                {


                    Log.Error("LoginButtonLogic", "Login Attempt Blocked, Try again after 30 sec");
                }
                
            }

            if (loginResult.ResultCode != ChangeUserResultCode.Success)
            {
                var outputMessageLabel = Owner.Owner.GetObject("LoginFormOutputMessage");
                var outputMessageLogic = outputMessageLabel.GetObject("LoginFormOutputMessageLogic");
                if (UserDetails.User_Blocked)
                    outputMessageLogic.ExecuteMethod("SetOutputMessage", new object[] { (int)20 });
                else
                {
                    if (username == "Pima")
                    {
                        var ownerButton1 = (Button)Owner;
                        ownerButton1.OpenDialog(InvalidMasterPasswordDailogbox);
                        
                        return;
                    }
                    outputMessageLogic.ExecuteMethod("SetOutputMessage", new object[] { (int)loginResult.ResultCode });

                }
            }

            //-----------Customized Logic Start-----------------
            if ((loginResult.ResultCode == ChangeUserResultCode.Success))
            {
                UserDetails.Invalid_Login_Attempts = 0;
                if (UserDetails.Change_Password_On_Next_Login == true)
                {
                    loginButton.Enabled = true;
                    //var user = usersAlias.Get<User_21CFR>(username);
                    // User's First Login Attempt Event Logging into Audit Database
                    AuditTrailLogging FirstLoginAtt = new AuditTrailLogging();
                    FirstLoginAtt.LogIntoAudit("First login attempt", username + "'s first login attempt after creation", Session.User.BrowseName, "UserLoginEvent");
                    
                    var logoutuser = Session.ChangeUser("Anonymous","");
                    ComboBox loginSelectedUser = Project.Current.Get<ComboBox>("UI/UserLoginForm/Login/Username");
                    loginSelectedUser.SelectedValue = username;
                    
                    var ownerButton4 = (Button)Owner;
                    passwordExpiredDialogType.GetVariable("ShowCurrentUser").Value = false;
                    passwordExpiredDialogType.GetVariable("ShowFirstLogonText").Value = true;
                    passwordExpiredDialogType.GetVariable("ShowLableText").Value = false;
                    ownerButton4.OpenDialog(passwordExpiredDialogType, UserDetails.NodeId);
                }
                else
                {
                    DateTime passdate = UserDetails.Password_Creation_Date;
                    TimeSpan CurrPassAge = DateTime.Now.Subtract(passdate);
                    //Log.Info("LoginButtonLogic", "Curr Pass Age " + CurrPassAge.ToString());
                    int MaxPassAgeDays = Project.Current.GetVariable("PasswordPolicy/MaximumPasswordAge").Value;
                    int ExpAlertDays = Project.Current.GetVariable("UI/UserObjects/PasswordPolicy/PasswordExpiryAlertDays").Value;
                    if ((MaxPassAgeDays > 0) && (ExpAlertDays > 0))
                    {
                        int AlertAfterDays = MaxPassAgeDays - ExpAlertDays;
                        
                        if (CurrPassAge.Days >= AlertAfterDays)
                        {
                            string expRemTimeMsg = "";
                            Int32 CurrPassAgeHours = (CurrPassAge.Days * 24) + CurrPassAge.Hours;
                            Int32 MaxPassAgeHours = MaxPassAgeDays * 24;
                            Int32 ExpRemHours = MaxPassAgeHours - CurrPassAgeHours;
                            //Log.Info("LoginButtonLogic", "Curr Pass Age " + CurrPassAgeHours + ", Max Pass Age " + MaxPassAgeHours + ", Rem Hour " + ExpRemHours);
                            int ExpRemDays =  MaxPassAgeDays - CurrPassAge.Days;
                            if (ExpRemHours > 24)
                            {
                                if (ExpRemDays > 1)
                                {
                                    expRemTimeMsg = ExpRemDays + " days.";
                                }
                                else
                                {
                                    expRemTimeMsg = ExpRemDays + " day.";
                                }
                            }
                            else
                            {
                                if (ExpRemHours > 0)
                                {
                                    if (ExpRemHours > 1)
                                    {
                                        expRemTimeMsg = ExpRemHours + " hours.";
                                    }
                                    else
                                    {
                                        expRemTimeMsg = ExpRemHours + " hour.";
                                    }
                                }
                                else
                                {
                                    expRemTimeMsg = " less than 1 hour.";
                                }
                            }
                            var ownerButton5 = (Button)Owner;
                            PassExpiryDailogbox.GetVariable("PassExpRemainTime").Value = expRemTimeMsg;
                            ownerButton5.OpenDialog(PassExpiryDailogbox);
                        }
                    }
                    
                }
            }
            //-----------Customized Logic End--------------------
            
        }
        catch (Exception e)
        {
            Log.Error("LoginButtonLogic", e.Message);
        }

        loginButton.Enabled = true;
    }
}
