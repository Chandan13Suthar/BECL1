#region Using directives
using System;
using CoreBase = FTOptix.CoreBase;
using FTOptix.HMIProject;
using UAManagedCore;
using FTOptix.UI;
using FTOptix.NetLogic;
using FTOptix.WebUI;
using FTOptix.ODBCStore;
using FTOptix.System;
using FTOptix.OPCUAServer;
using FTOptix.OPCUAClient;
using FTOptix.MicroController;
using FTOptix.CommunicationDriver;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class CurrentDateTimeFormatted : BaseNetLogic
{
	public override void Start()
	{
		periodicTask = new PeriodicTask(UpdateTime, 1000, LogicObject);
		periodicTask.Start();
	}

	public override void Stop()
	{
		periodicTask.Dispose();
		periodicTask = null;
	}

	private void UpdateTime()
	{
		LogicObject.GetVariable("Time").Value = DateTime.Now;
		LogicObject.GetVariable("UTCTime").Value = DateTime.UtcNow;
		//Project.Current.GetVariable("Model/intDay").Value = DateTime.Now.Day;
		int YearVar = DateTime.Now.Year;
		int MonthVar = DateTime.Now.Month;
		int DayVar = DateTime.Now.Day;
		int HourVar = DateTime.Now.Hour;
		int MinVar = DateTime.Now.Minute;
		int SecVar = DateTime.Now.Second;
		String stMonth = "01";
		String stDay = "01";
		String stHour = "01";
		String stMin = "01";
		String stSec = "01";
		String dateFor = "DDMMYYYY";
		String DateFormatted = "01/02/2023";
		String TimeFormatted = "00:00:00";

		//Month formatting
		if (MonthVar < 10)
        {
            stMonth = "0" + MonthVar;
        }
        else
        {
            stMonth = MonthVar.ToString();
        }
		//Day formatting
		if (DayVar < 10)
        {
            stDay = "0" + DayVar;
        }
        else
        {
            stDay = DayVar.ToString();
        }
		//Hour formatting
		if (HourVar < 10)
        {
            stHour = "0" + HourVar;
        }
        else
        {
            stHour = HourVar.ToString();
        }
		//Minute formatting
		if (MinVar < 10)
        {
            stMin = "0" + MinVar;
        }
        else
        {
            stMin = MinVar.ToString();
        }
		//Second formatting
		if (SecVar < 10)
        {
            stSec= "0" + SecVar;
        }
        else
        {
            stSec = SecVar.ToString();
        }

		String DateOpt = Project.Current.GetVariable("Model/DateTime/DateOperator").Value;
		String TimeOpt = Project.Current.GetVariable("Model/DateTime/TimeOperator").Value;

		// Date Formatting
		dateFor = Project.Current.GetVariable("Model/DateTime/DateFormat").Value;
		if (dateFor == "DDMMYYYY")
		{
			DateFormatted = stDay + DateOpt + stMonth + DateOpt + YearVar;
		}
		else if (dateFor == "MMDDYYYY")
		{
			DateFormatted = stMonth + DateOpt + stDay + DateOpt + YearVar;
		}
		else if (dateFor == "YYYYMMDD")
		{
			DateFormatted = YearVar + DateOpt + stMonth + DateOpt + stDay;
		}
		else
		{
			DateFormatted = stDay + DateOpt + stMonth + DateOpt + YearVar;
		}

		// Time Formatting
		TimeFormatted = stHour + TimeOpt + stMin + TimeOpt + stSec;

		Project.Current.GetVariable("Model/DateTime/DateFormatted").Value = DateFormatted;
		Project.Current.GetVariable("Model/DateTime/TimeFormatted").Value = TimeFormatted;
		Project.Current.GetVariable("Model/DateTime/DateTimeFormatted").Value = DateFormatted + " " + TimeFormatted;

		Project.Current.GetVariable("Model/DateTime/CurrDateTime/Year").Value = YearVar.ToString();
		Project.Current.GetVariable("Model/DateTime/CurrDateTime/Month").Value = stMonth;
		Project.Current.GetVariable("Model/DateTime/CurrDateTime/Day").Value = stDay;
		Project.Current.GetVariable("Model/DateTime/CurrDateTime/Hour").Value = stHour;
		Project.Current.GetVariable("Model/DateTime/CurrDateTime/Minute").Value = stMin;
		Project.Current.GetVariable("Model/DateTime/CurrDateTime/Second").Value = stSec;
	}

	private PeriodicTask periodicTask;
}
