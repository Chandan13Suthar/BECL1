#region Using directives
using FTOptix.Core;
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.OPCUAServer;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class IdleTimeoutLogic : BaseNetLogic
{
    public override void Start()
    {
      

        duration = LogicObject.GetVariable("Duration");
        if (duration == null)
            throw new CoreConfigurationException("Unable to find Duration variable");

        enabled = LogicObject.GetVariable("Enabled");
        if (enabled == null)
            throw new CoreConfigurationException("Unable to find Enabled variable");

        onTimeout = (MethodInvocation)LogicObject.Get("OnTimeout");
        if (onTimeout == null)
            throw new CoreConfigurationException("Unable to find OnTimeout method invocation");

        LogoutWindow = (MethodInvocation)LogicObject.Get("LogoutWindow");
        if (LogoutWindow == null)
            throw new CoreConfigurationException("Unable to find OnTimeout method invocation");

        uiSession = Session as UISession;
        if (uiSession == null)
            throw new CoreConfigurationException("Idle Timeout logic must be placed inside a UI object");

        login = (MethodInvocation)LogicObject.Get("login");
        if (login == null)
            throw new CoreConfigurationException("unable to Invoke Login Method");

        enabled.VariableChange += Enabled_VariableChange;
        duration.VariableChange += Duration_VariableChange;

        uiSession.OnIdleTimeout += UiSession_OnIdleTimeout;
        uiSession.IdleTimeoutEnabled = enabled.Value;
        uiSession.IdleTimeoutDuration = TimeSpan.FromMilliseconds(duration.Value);
    }

    private void Enabled_VariableChange(object sender, VariableChangeEventArgs e)
    {
        uiSession.IdleTimeoutEnabled = e.NewValue;
    }

    private void Duration_VariableChange(object sender, VariableChangeEventArgs e)
    {
        uiSession.IdleTimeoutDuration = TimeSpan.FromMilliseconds(e.NewValue);
    }

    private void UiSession_OnIdleTimeout(object sender, IdleTimeoutEvent e)
    {

        DelayedTask myDelayedTask = new DelayedTask(Method1, 300, LogicObject);
        myDelayedTask.Start();

        var AutoLogOutTrigger = Project.Current.GetVariable("Model/AutoLogOutTrigger");
        AutoLogOutTrigger.Value = 0;
       
        onTimeout.Invoke();
        LogoutWindow.Invoke();
       // login.Invoke();
        
        Project.Current.GetVariable("Model/Screen_No").Value = 0;
        AutoLogOutTrigger.Value = 1;

    }

    public override void Stop()
    {
        if (uiSession != null)
            uiSession.OnIdleTimeout -= UiSession_OnIdleTimeout;
        if (enabled != null)
            enabled.VariableChange -= Enabled_VariableChange;
        if (duration != null)
            duration.VariableChange -= Duration_VariableChange;
    }
    [ExportMethod]
    public void Method1()
    {
        DialogType login = (DialogType)Project.Current.Get("UI/Screens/PopUpFolder/LoginDialog");
        Button button = (Button)LogicObject.Owner.Get("logindialog");
        button.OpenDialog(login);

    }
  


    private UISession uiSession;
    private IUAVariable duration;
    private IUAVariable enabled;
    private MethodInvocation onTimeout;
    private MethodInvocation login;
    private MethodInvocation LogoutWindow;

  
}
