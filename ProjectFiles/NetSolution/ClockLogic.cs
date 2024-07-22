#region Using directives
using FTOptix.NetLogic;
using System;
using UAManagedCore;
using FTOptix.AuditSigning;
using FTOptix.MicroController;
using FTOptix.CommunicationDriver;
using FTOptix.ODBCStore;
using FTOptix.OPCUAServer;
using FTOptix.UI;
using FTOptix.Alarm;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class ClockLogic : BaseNetLogic {
    public override void Start() {
        periodicTask = new PeriodicTask(UpdateTime, 1000, LogicObject);
        periodicTask.Start();
    }

    public override void Stop() {
        periodicTask.Dispose();
        periodicTask = null;
    }

    private void UpdateTime() {
        LogicObject.GetVariable("Time").Value = DateTime.Now;
        LogicObject.GetVariable("UTCTime").Value = DateTime.UtcNow;


        DateTime currentTime = DateTime.Now;
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        int currentHour = currentTime.Hour;

        // Calculate start and end times for the current day
        DateTime startTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 8, 0, 0);
        DateTime endTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 7, 59, 59).AddDays(1);
        DateTime todaydate = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 0, 0, 0);
        var date1 = startTime.ToString("dd-MM-yyyy");
        
        // Adjust the start time if the current hour is before 8 AM
        if (currentHour < 8)
        {
            startTime = startTime.AddDays(-1);
            endTime = endTime.AddDays(-1);
        }
        LogicObject.GetVariable("TodayStartTime").Value = startTime;
        LogicObject.GetVariable("TodayEndTime").Value = endTime;
        LogicObject.GetVariable("TodayDate").Value = todaydate;
        LogicObject.GetVariable("Day").Value = currentTime.Day.ToString();
        LogicObject.GetVariable("MonthYear").Value = currentTime.ToString("MM-yyyy");
    }

    private PeriodicTask periodicTask;
}

