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
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.Modbus;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
{


    public override void Start()
    {
        var owner = (HistoDashboard)LogicObject.Owner;

        jaceVariable = owner.JaceVariable;
        meterVariable = owner.MeterVariable;
        maxconsumptionVariable = owner.MaxconsumptionVariable;
        minconsumptionVariable = owner.MinconsumptionVariable;
        avgpfVariable = owner.AvgpfVariable;
        avgfrequencyVariable = owner.AvgfrequencyVariable;
        consumptionVariable = owner.ConsumptionVariable;
        buttonVariable = owner.ButtonVariable;
        button1Variable = owner.Button1Variable;
        datefromVariable = owner.DatefromVariable;
        datetoVariable = owner.DatetoVariable;

        periodicTask = new PeriodicTask(IncrementDecrementTask, 2000 , LogicObject);
        periodicTask.Start();
    }

    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
    }

    public void IncrementDecrementTask()
    {
        string jace = jaceVariable.Value;
        string meter = meterVariable.Value;
        float consumption = consumptionVariable.Value;
        float maxconsumption = maxconsumptionVariable.Value;
        float minconsumption = minconsumptionVariable.Value;
        float avgfrequency = avgfrequencyVariable.Value;
        float avgpf = avgpfVariable.Value;
        bool button = buttonVariable.Value;
        bool button1 = button1Variable.Value;
        DateTime datefrom = datefromVariable.Value;
        DateTime dateto = datetoVariable.Value;

        var project = FTOptix.HMIProject.Project.Current;
        ///////////////////For Jace Selection////////////////////////////////////////////////////////////////////////////
        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//maxconsumption// Jace Selection
        //var myStore2 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//minconsumption// Jace Selection
        var myStore3 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgpf// Jace Selection
        var myStore4 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgfrequency// Jace Selection
        /////////////////For Meter Selection/////////////////////////////////////////////////////////////////////////////
        var myStore5 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//maxconsumption//meter selection
        //var myStore6 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//minconsumption//meter selection
        var myStore7 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgpf//meter selection
        var myStore8 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgfrequency//meter selection


        ///////////////////For Jace Selection////////////////////////////////////////////////////////////////////////////
        object[,] resultSet1;
        string[] header1;
        //object[,] resultSet2;
        //string[] header2;
        object[,] resultSet3;
        string[] header3;
        object[,] resultSet4;
        string[] header4;
        /////////////////For Meter Selection/////////////////////////////////////////////////////////////////////////////
        object[,] resultSet5;
        string[] header5;
       // object[,] resultSet6;
        //string[] header6;
        object[,] resultSet7;
        string[] header7;
        object[,] resultSet8;
        string[] header8;

        //////////////////////////////////////////////////////////////////////////////////For Jace Selection////////////////////////////////////////////////////////////////////////////
        if (button == true)
        {
            string new123 = datefrom.ToString("yyyy-MMM-dd");
            string new321 = dateto.ToString("yyyy-MMM-dd");
            string jace1 = jace.ToString();

            string query1 = $"SELECT  SUM(Consumption) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '"+ jace1 + "' ";
            //string query2 = $"SELECT  MIN(Consumption) FROM DailyConsumption WHERE  LocalTimestamp  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace1 + "' ";
            string query3 = $"SELECT  AVG(Avg_PF) FROM DailyConsumption  WHERE  LocalTimestamp BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace1 + "' ";
            string query4 = $"SELECT  AVG(Frequency) FROM DailyConsumption  WHERE  LocalTimestamp  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace1 + "' ";


            myStore1.Query(query1, out header1, out resultSet1);
           // myStore2.Query(query2, out header2, out resultSet2);
            myStore3.Query(query3, out header3, out resultSet3);
            myStore4.Query(query4, out header4, out resultSet4);



            var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount1 = header1 != null ? header1.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                var Consumption = column1;
                consumption = Consumption;
            }


           /*
            var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
            var columnCount2 = header2 != null ? header2.Length : 0;
            if (rowCount2 > 0 && columnCount2 > 0)
            {
                var column1 = Convert.ToInt32(resultSet2[0, 0]);
                var Minconsumption = column1;
                minconsumption = Minconsumption;
            }
           */

            var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
            var columnCount3 = header3 != null ? header3.Length : 0;
            if (rowCount3 > 0 && columnCount3 > 0)
            {
                var column1 = Convert.ToInt32(resultSet3[0, 0]);
                var Avgpf = column1;
                avgpf = Avgpf;
            }


            var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
            var columnCount4 = header4 != null ? header4.Length : 0;
            if (rowCount4 > 0 && columnCount4 > 0)
            {
                var column1 = Convert.ToInt32(resultSet4[0, 0]);
                var Avgfrequency = column1;
                avgfrequency = Avgfrequency;
            }


           

           // float consumptionP = (maxconsumption - minconsumption);

           // consumption = consumptionP;


            maxconsumptionVariable.Value = maxconsumption;
            minconsumptionVariable.Value = minconsumption;
            avgpfVariable.Value = avgpf;
            avgfrequencyVariable.Value = avgfrequency;
            consumptionVariable.Value = consumption;





        }

        ///////////////////////////////////////////////////////////////////////////For Meter Selection/////////////////////////////////////////////////////////////////////////////////////////
        else if (button1 == true)
        {
            string new456 = datefrom.ToString("yyyy-MM-dd");
            string new645 = dateto.ToString("yyyy-MM-dd");
            string meter1 = meter.ToString();
            string jace1 = jace.ToString();

            string query5 = $"SELECT  SUM(Consumption) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace1 + "'  AND Meter = '" + meter1 + "' ";
            //string query6 = $"SELECT  MIN(Consumption) FROM DailyConsumption  WHERE Date  BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace1 + "'  AND Meter = '" + meter1 + "' ";
            string query7 = $"SELECT  AVG(Avg_PF) FROM DailyConsumption  WHERE  LocalTimestamp BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace1 + "'  AND Meter = '" + meter1 + "' ";
            string query8 = $"SELECT  AVG(Frequency) FROM DailyConsumption  WHERE  LocalTimestamp  BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace1 + "'  AND Meter = '" + meter1 + "' ";


            myStore5.Query(query5, out header5, out resultSet5);
            //myStore6.Query(query6, out header6, out resultSet6);
            myStore7.Query(query7, out header7, out resultSet7);
            myStore8.Query(query8, out header8, out resultSet8);



            var rowCount5 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
            var columnCount5 = header5 != null ? header5.Length : 0;
            if (rowCount5 > 0 && columnCount5 > 0)
            {
                var column1 = Convert.ToInt32(resultSet5[0, 0]);
                var Consumption = column1;
                consumption = Consumption;
            }


            /*
            var rowCount6 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
            var columnCount6 = header6 != null ? header6.Length : 0;
            if (rowCount6 > 0 && columnCount6 > 0)
            {
                var column1 = Convert.ToInt32(resultSet6[0, 0]);
                var Minconsumption = column1;
                minconsumption = Minconsumption;
            }
            */

            var rowCount7 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
            var columnCount7 = header7 != null ? header7.Length : 0;
            if (rowCount7 > 0 && columnCount7 > 0)
            {
                var column1 = Convert.ToInt32(resultSet7[0, 0]);
                var Avgpf = column1;
                avgpf = Avgpf;
            }


            var rowCount8 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
            var columnCount8 = header8 != null ? header8.Length : 0;
            if (rowCount8 > 0 && columnCount8 > 0)
            {
                var column1 = Convert.ToInt32(resultSet8[0, 0]);
                var Avgfrequency = column1;
                avgfrequency = Avgfrequency;
            }


            //float consumptionP = (maxconsumption - minconsumption);

            //consumption = consumptionP;


            maxconsumptionVariable.Value = maxconsumption;
            minconsumptionVariable.Value = minconsumption;
            avgpfVariable.Value = avgpf;
            avgfrequencyVariable.Value = avgfrequency;
            consumptionVariable.Value = consumption;
            



        }




    }

    private IUAVariable jaceVariable;
    private IUAVariable meterVariable;
    private IUAVariable maxconsumptionVariable;
    private IUAVariable minconsumptionVariable;
    private IUAVariable avgpfVariable;
    private IUAVariable avgfrequencyVariable;
    private IUAVariable consumptionVariable;
    private IUAVariable buttonVariable;
    private IUAVariable button1Variable;
    private IUAVariable datefromVariable;
    private IUAVariable datetoVariable;
    private PeriodicTask periodicTask;
}
