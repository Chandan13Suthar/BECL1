#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.WebUI;
using FTOptix.UI;
using FTOptix.CoreBase;
using FTOptix.DataLogger;
using FTOptix.EventLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.ODBCStore;
using FTOptix.Report;
using FTOptix.MicroController;
using FTOptix.Retentivity;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;

#endregion

public class LOGON : BaseNetLogic
{
    public override void Start()
    {
        DelayedTask myDelayedTask = new DelayedTask(Method1, 1000, LogicObject);
        myDelayedTask.Start();
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }
    [ExportMethod]
    public void Method1()
    {
        DialogType login = (DialogType)Project.Current.Get("UI/Screens/PopUpFolder/LoginDialog");
        Button button = (Button)LogicObject.Owner.Get("logindialog");
        button.OpenDialog(login);

    }
}
