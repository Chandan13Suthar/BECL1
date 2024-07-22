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

public class RuntimeNetLogic3 : BaseNetLogic
{


    public override void Start()
    {
        var owner = (ComparisionDashboard)LogicObject.Owner;

        jace1Variable = owner.Jace1Variable;
        jace2Variable = owner.Jace2Variable;
        meter1Variable = owner.Meter1Variable;
        meter2Variable = owner.Meter2Variable;
        maxconsumption1Variable = owner.Maxconsumption1Variable;
        maxconsumption2Variable = owner.Maxconsumption2Variable;
        minconsumption1Variable = owner.Minconsumption1Variable;
        minconsumption2Variable = owner.Minconsumption2Variable;
        avgpf1Variable = owner.Avgpf1Variable;
        avgpf2Variable = owner.Avgpf2Variable;
        avgfrequency1Variable = owner.Avgfrequency1Variable;
        avgfrequency2Variable = owner.Avgfrequency2Variable;
        consumption1Variable = owner.Consumption1Variable;
        consumption2Variable = owner.Consumption2Variable;
        button1Variable = owner.Button1Variable;
        button2Variable = owner.Button2Variable;
        datefromVariable = owner.DatefromVariable;
        datetoVariable = owner.DatetoVariable;

        periodicTask = new PeriodicTask(IncrementDecrementTask, 5000, LogicObject);
        periodicTask.Start();
    }

    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
    }

    public void IncrementDecrementTask()
    {
        string jace1 = jace1Variable.Value;
        String jace2 = jace2Variable.Value;
        string meter1 = meter1Variable.Value;
        string meter2 = meter2Variable.Value;
        float consumption1 = consumption1Variable.Value;
        float consumption2 = consumption2Variable.Value;
        float maxconsumption1 = maxconsumption1Variable.Value;
        float maxconsumption2 = maxconsumption2Variable.Value;
        float minconsumption1 = minconsumption1Variable.Value;
        float minconsumption2 = minconsumption2Variable.Value;
        float avgfrequency1 = avgfrequency1Variable.Value;
        float avgfrequency2 = avgfrequency2Variable.Value;
        float avgpf1 = avgpf1Variable.Value;
        float avgpf2 = avgpf2Variable.Value;
        bool button1 = button1Variable.Value;
        bool button2 = button2Variable.Value;
        DateTime datefrom = datefromVariable.Value;
        DateTime dateto = datetoVariable.Value;

        var project = FTOptix.HMIProject.Project.Current;
        ///////////////////For Jace Selection////////////////////////////////////////////////////////////////////////////
        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//maxconsumption// Jace1 Selection
       // var myStore2 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//minconsumption// Jace1 Selection
        var myStore3 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgpf// Jace1 Selection
        var myStore4 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgfrequency// Jace1 Selection
        var myStore5 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//maxconsumption// Jace2 Selection
       // var myStore6 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//minconsumption// Jace2 Selection
        var myStore7 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgpf// Jace2 Selection
        var myStore8 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgfrequency// Jace2 Selection
        /////////////////For Meter Selection/////////////////////////////////////////////////////////////////////////////
        var myStore9 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//maxconsumption//meter1 selection
       // var myStore10 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//minconsumption//meter1 selection
        var myStore11 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgpf//meter1 selection
        var myStore12 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgfrequency//meter1 selection
        var myStore13 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//maxconsumption//meter2 selection
       // var myStore14 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//minconsumption//meter2 selection
        var myStore15 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgpf//meter2 selection
        var myStore16 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//avgfrequency//meter2 selection

        ///////////////////For Jace1 Selection////////////////////////////////////////////////////////////////////////////
        object[,] resultSet1;
        string[] header1;
      //  object[,] resultSet2;
      //  string[] header2;
        object[,] resultSet3;
        string[] header3;
        object[,] resultSet4;
        string[] header4;

        ///////////////////For Jace2 Selection////////////////////////////////////////////////////////////////////////////
        object[,] resultSet5;
        string[] header5;
       // object[,] resultSet6;
      //  string[] header6;
        object[,] resultSet7;
        string[] header7;
        object[,] resultSet8;
        string[] header8;
        /////////////////For Meter1 Selection/////////////////////////////////////////////////////////////////////////////
        object[,] resultSet9;
        string[] header9;
      //  object[,] resultSet10;
      //  string[] header10;
        object[,] resultSet11;
        string[] header11;
        object[,] resultSet12;
        string[] header12;
        /////////////////For Meter1 Selection/////////////////////////////////////////////////////////////////////////////
        object[,] resultSet13;
        string[] header13;
       // object[,] resultSet14;
       // string[] header14;
        object[,] resultSet15;
        string[] header15;
        object[,] resultSet16;
        string[] header16;


        //////////////////////////////////////////////////////////////////////////////////For Jace Selection////////////////////////////////////////////////////////////////////////////
        if (button1 == true)
        {
            string new123 = datefrom.ToString("yyyy-MM-dd");
            string new321 = dateto.ToString("yyyy-MM-dd");
            string jace3 = jace1.ToString();
            string jace4 = jace2.ToString();


            ///////////For Jace1/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string query1 = $"SELECT  SUM(Consumption) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace3 + "' ";
            //string query2 = $"SELECT  MIN(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace3 + "' ";
            string query3 = $"SELECT  AVG(Avg_PF) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace3 + "' ";
            string query4 = $"SELECT  AVG(Frequency) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace3 + "' ";
            ///////////For Jace2/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string query5 = $"SELECT  MAX(Consumption) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace4 + "' ";
           // string query6 = $"SELECT  MIN(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace4 + "' ";
            string query7 = $"SELECT  AVG(Avg_PF) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace4 + "' ";
            string query8 = $"SELECT  AVG(Frequency) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new123 + " 00:00:00' AND '" + new321 + " 23:59:59' AND Jace = '" + jace4 + "' ";


            ///////////For Jace1///////////////////////////////
            myStore1.Query(query1, out header1, out resultSet1);
           // myStore2.Query(query2, out header2, out resultSet2);
            myStore3.Query(query3, out header3, out resultSet3);
            myStore4.Query(query4, out header4, out resultSet4);
            ///////////For Jace2///////////////////////////////
            myStore5.Query(query5, out header5, out resultSet5);
           // myStore6.Query(query6, out header6, out resultSet6);
            myStore7.Query(query7, out header7, out resultSet7);
            myStore8.Query(query8, out header8, out resultSet8);


            ///////////For Jace1///////////////////////////
            var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount1 = header1 != null ? header1.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                var Consumption1 = column1;
                consumption1 = Consumption1;
            }


/*
            var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
            var columnCount2 = header2 != null ? header2.Length : 0;
            if (rowCount2 > 0 && columnCount2 > 0)
            {
                var column1 = Convert.ToInt32(resultSet2[0, 0]);
                var Minconsumption1 = column1;
                minconsumption1 = Minconsumption1;
            }
*/

            var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
            var columnCount3 = header3 != null ? header3.Length : 0;
            if (rowCount3 > 0 && columnCount3 > 0)
            {
                var column1 = Convert.ToInt32(resultSet3[0, 0]);
                var Avgpf1 = column1;
                avgpf1 = Avgpf1;
            }


            var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
            var columnCount4 = header4 != null ? header4.Length : 0;
            if (rowCount4 > 0 && columnCount4 > 0)
            {
                var column1 = Convert.ToInt32(resultSet4[0, 0]);
                var Avgfrequency1 = column1;
                avgfrequency1 = Avgfrequency1;
            }
            ////////////////////////////////For Jace 2 ///////////////////////////////////////////////
            var rowCount5 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
            var columnCount5 = header5 != null ? header5.Length : 0;
            if (rowCount5 > 0 && columnCount5 > 0)
            {
                var column1 = Convert.ToInt32(resultSet5[0, 0]);
                var Consumption2 = column1;
                consumption2 = Consumption2;
            }
/*
            var rowCount6 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
            var columnCount6 = header6 != null ? header6.Length : 0;
            if (rowCount6 > 0 && columnCount6 > 0)
            {
                var column1 = Convert.ToInt32(resultSet6[0, 0]);
                var Minconsumption2 = column1;
                minconsumption2 = Minconsumption2;
            }
*/

            var rowCount7 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
            var columnCount7 = header7 != null ? header7.Length : 0;
            if (rowCount7 > 0 && columnCount7 > 0)
            {
                var column1 = Convert.ToInt32(resultSet7[0, 0]);
                var Avgpf2 = column1;
                avgpf2 = Avgpf2;
            }


            var rowCount8 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
            var columnCount8 = header8 != null ? header8.Length : 0;
            if (rowCount8 > 0 && columnCount8 > 0)
            {
                var column1 = Convert.ToInt32(resultSet8[0, 0]);
                var Avgfrequency2 = column1;
                avgfrequency2 = Avgfrequency2;
            }



           // float consumptionP1 = (maxconsumption1 - minconsumption1);
           // float consumptionP2 = (maxconsumption2 - minconsumption2);
           // consumption1 = consumptionP1;
           // consumption2 = consumptionP2;

            ///////////For Jace1///////////////////////////
            maxconsumption1Variable.Value = maxconsumption1;
            minconsumption1Variable.Value = minconsumption1;
            avgpf1Variable.Value = avgpf1;
            avgfrequency1Variable.Value = avgfrequency1;
            consumption1Variable.Value = consumption1;
            ///////////For Jace2///////////////////////////
            maxconsumption2Variable.Value = maxconsumption2;
            minconsumption2Variable.Value = minconsumption2;
            avgpf2Variable.Value = avgpf2;
            avgfrequency2Variable.Value = avgfrequency2;
            consumption2Variable.Value = consumption2;





        }

        ///////////////////////////////////////////////////////////////////////////For Meter Selection/////////////////////////////////////////////////////////////////////////////////////////
        else if (button2 == true)
        {
            string new456 = datefrom.ToString("yyyy-MM-dd");
            string new645 = dateto.ToString("yyyy-MM-dd");
            string meter5 = meter1.ToString();
            string meter6 = meter2.ToString();
            string jace5 = jace1.ToString();
            string jace6 = jace2.ToString();

            ////////////////////////////////////////////////////////////////////////// For Meter 1 ////////////////////////////////////////////////////////////////////////////////////// 

            string query9 = $"SELECT  SUM(Consumption) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace5 + "'  AND Meter = '" + meter5 + "' ";
            //string query10 = $"SELECT  MIN(Consumption) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace5 + "'  AND Meter = '" + meter5 + "' ";
            string query11 = $"SELECT  AVG(Avg_PF) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace5 + "'  AND Meter = '" + meter5 + "' ";
            string query12 = $"SELECT  AVG(Frequency) FROM DailyConsumption WHERE LocalTimestamp BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace5 + "'  AND Meter = '" + meter5 + "' ";
            
            ////////////////////////////////////////////////////////////////////////// For Meter 2 ////////////////////////////////////////////////////////////////////////////////////// 
            string query13 = $"SELECT  SUM(Consumption) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace6 + "'  AND Meter = '" + meter6 + "' ";
            //string query14 = $"SELECT  MIN(Consumption) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace6 + "'  AND Meter = '" + meter6 + "' ";
            string query15 = $"SELECT  AVG(Avg_PF) FROM DailyConsumption WHERE LocalTimestamp BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace6 + "'  AND Meter = '" + meter6 + "' ";
            string query16 = $"SELECT  AVG(Frequency) FROM DailyConsumption WHERE LocalTimestamp BETWEEN '" + new456 + " 00:00:00' AND '" + new645 + " 23:59:59' AND Jace =  '" + jace6 + "'  AND Meter = '" + meter6 + "' ";

            ///////////////////////For MEter 1//////////////////////
            myStore9.Query(query9, out header9, out resultSet9);
           // myStore10.Query(query10, out header10, out resultSet10);
            myStore11.Query(query11, out header11, out resultSet11);
            myStore12.Query(query12, out header12, out resultSet12);

            ///////////////////////For MEter 2//////////////////////
            myStore13.Query(query13, out header13, out resultSet13);
           // myStore14.Query(query14, out header14, out resultSet14);
            myStore15.Query(query15, out header15, out resultSet15);
            myStore16.Query(query16, out header16, out resultSet16);


            ///////////////////////////For Meter 1////////////////////////////////////////////////
            
            var rowCount9 = resultSet9 != null ? resultSet9.GetLength(0) : 0;
            var columnCount9 = header9 != null ? header9.Length : 0;
            if (rowCount9 > 0 && columnCount9 > 0)
            {
                var column1 = Convert.ToInt32(resultSet9[0, 0]);
                var Consumption1 = column1;
                consumption1 = Consumption1;
            }

/*

            var rowCount10 = resultSet10 != null ? resultSet10.GetLength(0) : 0;
            var columnCount10= header10 != null ? header10.Length : 0;
            if (rowCount10 > 0 && columnCount10 > 0)
            {
                var column1 = Convert.ToInt32(resultSet10[0, 0]);
                var Minconsumption1 = column1;
                minconsumption1 = Minconsumption1;
            }
*/

            var rowCount11 = resultSet11 != null ? resultSet11.GetLength(0) : 0;
            var columnCount11 = header11 != null ? header11.Length : 0;
            if (rowCount11 > 0 && columnCount11 > 0)
            {
                var column1 = Convert.ToInt32(resultSet11[0, 0]);
                var Avgpf1 = column1;
                avgpf1 = Avgpf1;
            }


            var rowCount12 = resultSet12 != null ? resultSet12.GetLength(0) : 0;
            var columnCount12 = header12 != null ? header12.Length : 0;
            if (rowCount12 > 0 && columnCount12 > 0)
            {
                var column1 = Convert.ToInt32(resultSet12[0, 0]);
                var Avgfrequency1 = column1;
                avgfrequency1 = Avgfrequency1;
            }


            /////////////////////////////////////////////////////For Meter 2////////////////////////////////////////////////////////////////////////////////////////////////////



            var rowCount13 = resultSet13 != null ? resultSet13.GetLength(0) : 0;
            var columnCount13 = header13 != null ? header13.Length : 0;
            if (rowCount13 > 0 && columnCount13 > 0)
            {
                var column1 = Convert.ToInt32(resultSet13[0, 0]);
                var Consumption2 = column1;
                consumption2 = Consumption2;
            }

/*

            var rowCount14 = resultSet14 != null ? resultSet14.GetLength(0) : 0;
            var columnCount14 = header14 != null ? header14.Length : 0;
            if (rowCount14 > 0 && columnCount14 > 0)
            {
                var column1 = Convert.ToInt32(resultSet14[0, 0]);
                var Minconsumption2 = column1;
                minconsumption2 = Minconsumption2;
            }
*/

            var rowCount15 = resultSet15 != null ? resultSet15.GetLength(0) : 0;
            var columnCount15 = header15 != null ? header15.Length : 0;
            if (rowCount15 > 0 && columnCount15 > 0)
            {
                var column1 = Convert.ToInt32(resultSet15[0, 0]);
                var Avgpf2 = column1;
                avgpf2 = Avgpf2;
            }


            var rowCount16 = resultSet16 != null ? resultSet16.GetLength(0) : 0;
            var columnCount16 = header16 != null ? header16.Length : 0;
            if (rowCount16 > 0 && columnCount16 > 0)
            {
                var column1 = Convert.ToInt32(resultSet16[0, 0]);
                var Avgfrequency2 = column1;
                avgfrequency2 = Avgfrequency2;
            }



            ///////////////////For Meter 1/////////////////////////////
           // float consumptionP1 = (maxconsumption1 - minconsumption1);
          //  consumption1 = consumptionP1;
            maxconsumption1Variable.Value = maxconsumption1;
            minconsumption1Variable.Value = minconsumption1;
            avgpf1Variable.Value = avgpf1;
            avgfrequency1Variable.Value = avgfrequency1;
            consumption1Variable.Value = consumption1;


            ///////////////////For Meter 2/////////////////////////////
           // float consumptionP2 = (maxconsumption2 - minconsumption2);
           // consumption2 = consumptionP2;
            maxconsumption2Variable.Value = maxconsumption2;
            minconsumption2Variable.Value = minconsumption2;
            avgpf2Variable.Value = avgpf2;
            avgfrequency2Variable.Value = avgfrequency2;
            consumption2Variable.Value = consumption2;


        }




    }


    private IUAVariable datefromVariable;
    private IUAVariable datetoVariable;
    private IUAVariable jace1Variable;
    private IUAVariable jace2Variable;
    private IUAVariable meter1Variable;
    private IUAVariable meter2Variable;
    private IUAVariable maxconsumption1Variable;
    private IUAVariable maxconsumption2Variable;
    private IUAVariable minconsumption1Variable;
    private IUAVariable minconsumption2Variable;
    private IUAVariable avgpf1Variable;
    private IUAVariable avgpf2Variable;
    private IUAVariable avgfrequency1Variable;
    private IUAVariable avgfrequency2Variable;
    private IUAVariable consumption1Variable;
    private IUAVariable consumption2Variable;
    private IUAVariable button1Variable;
    private IUAVariable button2Variable;
    private PeriodicTask periodicTask;
}
