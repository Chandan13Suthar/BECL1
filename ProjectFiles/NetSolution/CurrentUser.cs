#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.Alarm;
using FTOptix.CoreBase;
using FTOptix.Recipe;
using FTOptix.EventLogger;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.Report;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.AuditSigning;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.Modbus;

#endregion

public class CurrentUser : BaseNetLogic
{
    public override void Start()
    {
        
      var currentUser = Project.Current.GetVariable("Model/Retain_Variable/CurrentUser");
        TextBox _CurrentUser= (TextBox)LogicObject.Owner.Get("Header/TextBox3");
        currentUser.Value = _CurrentUser.Text;
        // Insert code to be executed when the user-defined logic is started
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void Method1()
    {

        var currentUser = Project.Current.GetVariable("Model/Retain_Variable/CurrentUser");
        TextBox _CurrentUser= (TextBox)LogicObject.Owner.Get("Header/TextBox3");
        currentUser.Value = _CurrentUser.Text;

        // Insert code to be executed by the method
    }
}
