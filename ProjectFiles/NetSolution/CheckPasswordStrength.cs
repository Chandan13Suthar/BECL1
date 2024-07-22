#region Using directives
using System;
using UAManagedCore;
using FTOptix.HMIProject;
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

public class CheckPasswordStrength : BaseNetLogic
{
    // Check Password for Desired Strength
    [ExportMethod]
	public bool CheckPassword(string EnteredPassword)
	{
		// Password Policy
		bool UpperCase_Ch = Project.Current.GetVariable("UI/UserObjects/PasswordPolicy/UppercaseCharacter").Value;
        bool LowerCase_Ch = Project.Current.GetVariable("UI/UserObjects/PasswordPolicy/LowercaseCharacter").Value;
        bool Special_Ch = Project.Current.GetVariable("UI/UserObjects/PasswordPolicy/SpecialCharacter").Value;
        bool Numeric_Ch = Project.Current.GetVariable("UI/UserObjects/PasswordPolicy/NumericCharacter").Value;

		// Check For Special Character in Password
        string specialCh = "~`!@#$%^&*()_-+={[}]|:;'<,>.?/";
        char[] CharArray = specialCh.ToCharArray();
        int SplChrCheck = 0;
        foreach (char ch1 in CharArray) 
        {
            if (EnteredPassword.Contains(ch1))
            {
                SplChrCheck += 1;
            }
        }
        
		// Check For Upper Case Character in Password
        string upercasechar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char[] upCharArray = upercasechar.ToCharArray();
        int upercasecheck = 0;
        foreach (char ch2 in upCharArray) 
        {
            if (EnteredPassword.Contains(ch2))
            {
                upercasecheck += 1;
            }
        }

		// Check For Lower Case Character in Password
        string lowercasechar = "abcdefghijklmnopqrstuvwxyz";
        char[] loCharArray = lowercasechar.ToCharArray();
        int lowercasecheck = 0;
        foreach (char ch3 in loCharArray) 
        {
            if (EnteredPassword.Contains(ch3))
            {
                lowercasecheck += 1;
            }
        }

		// Check For Numeric Character in Password
        string numchar = "0123456789";
        char[] numCharArray = numchar.ToCharArray();
        int numcheck = 0;
        foreach (char ch4 in numCharArray) 
        {
            if (EnteredPassword.Contains(ch4))
            {
                numcheck += 1;
            }
        }

        if (((SplChrCheck <= 0) && Special_Ch) || ((upercasecheck <= 0) && UpperCase_Ch) || ((lowercasecheck <= 0) && LowerCase_Ch) || ((numcheck <= 0) && Numeric_Ch))
        {
            return false;
        }
        else
        {
            return true;
        }

	}

}
