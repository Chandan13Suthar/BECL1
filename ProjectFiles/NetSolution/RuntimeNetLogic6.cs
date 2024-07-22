#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.DataLogger;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.CoreBase;
using FTOptix.Store;
using FTOptix.ODBCStore;
using FTOptix.Report;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using Store = FTOptix.Store;
using System.Text.RegularExpressions;
using FTOptix.SQLiteStore;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Reflection.Emit;
using FTOptix.MicroController;
using System.Threading;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.Modbus;
#endregion

public class RuntimeNetLogic6 : BaseNetLogic
{
   // private IUAVariable nameVariable;
    private IUAVariable counterVariable;
    private IUAVariable dateVariable;

    //  private IUAVariable valueVariable;
    //  private IUAVariable incomer1nameVariable;
    // private IUAVariable incomer2nameVariable;
    // private IUAVariable incomer3nameVariable;
    //  private IUAVariable incomer4nameVariable;
    //  private IUAVariable incomer5nameVariable;
    //   private IUAVariable incomer1valueVariable;
    //  private IUAVariable incomer2valueVariable;
    //  private IUAVariable incomer3valueVariable;
    //  private IUAVariable incomer4valueVariable;
    //  private IUAVariable incomer5valueVariable;
    private IUAVariable buttonVariable;
  //  private IUAVariable gbuttonVariable;
    private PeriodicTask periodicTask;

    public override void Start()

    {



        var owner = (Object)LogicObject.Owner;
       // nameVariable = owner.NameVariable;
        counterVariable = owner.CounterVariable;
        dateVariable = owner.DateVariable;
       // valueVariable = owner.ValueVariable;
       // incomer1nameVariable = owner.Incomer1NameVariable;
       // incomer2nameVariable = owner.Incomer2NameVariable;
      //  incomer3nameVariable = owner.Incomer3NameVariable;
      //  incomer4nameVariable = owner.Incomer4NameVariable;
      //  incomer5nameVariable = owner.Incomer5NameVariable;
      //  incomer1valueVariable = owner.Income1ValueVariable;
      //  incomer2valueVariable = owner.Income2ValueVariable;
      //  incomer3valueVariable = owner.Incomer3ValueVariable;
      //  incomer4valueVariable = owner.Incomer4ValueVariable;
      //  incomer5valueVariable = owner.Incomer5ValueVariable;


        buttonVariable = owner.ButtonVariable;


        periodicTask = new PeriodicTask(IncrementDecrementTask,1000, LogicObject);
        periodicTask.Start();

        // Insert code to be executed when the user-defined logic is started
    }




    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
        // Insert code to be executed when the user-defined logic is stopped
    }

    public void IncrementDecrementTask()
    {
        int counter = counterVariable.Value;
        string date = dateVariable.Value;
       // int value = valueVariable.Value;
      //  int incomer1value = incomer1valueVariable.Value;
      //  int incomer2value = incomer2valueVariable.Value;
      //  int incomer3value = incomer3valueVariable.Value;
      //  int incomer4value = incomer4valueVariable.Value;
      //  int incomer5value = incomer5valueVariable.Value;

      //  string name = nameVariable.Value;
     //   string incomer1name = incomer1nameVariable.Value;
     //   string incomer2name = incomer2nameVariable.Value;
     //   string incomer3name = incomer3nameVariable.Value;
     //   string incomer4name = incomer4nameVariable.Value;
     //   string incomer5name = incomer5nameVariable.Value;
        bool button = buttonVariable.Value;



        var project = FTOptix.HMIProject.Project.Current;



        if (button == true)
        {

            if (counter <= 18)

            {
                DateTime currentTime = DateTime.Now;
                string currentDate = currentTime.ToString("yyyy-MM-dd");
                int currentHour = currentTime.Hour;

                // Calculate start and end times for the current day
                DateTime startTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 8, 0, 0);
               // DateTime endTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 7, 59, 59).AddDays(1);
                var date1 = startTime.ToString("dd-MM-yyyy");
                // Adjust the start time if the current hour is before 8 AM
              

                date = date1;
                counter = counter + 1;
            }

            else 
            {
                //Thread.Sleep(1 * 10 * 1000); // Pause for 5 minutes
                counter = 0;

            }

           




        }
        dateVariable.Value = date;
        counterVariable.Value = counter;


    }
}
