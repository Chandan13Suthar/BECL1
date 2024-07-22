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
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Threading;
using FTOptix.Modbus;
#endregion


public class ConsumptionDistributionLogic : BaseNetLogic
{
   

    public override void Start()
    {
        var owner = (ConsumptionDistributionLogger)LogicObject.Owner;
        dateVariable = owner.DateVariable;
        buttonVariable = owner.ButtonVariable;
        countVariable = owner.CountVariable;
        monthyearVariable = owner.MonthYearVariable;
        yearVariable = owner.YearVariable;
        jaceVariable = owner.JaceVariable;
        jace1Variable = owner.Jace1Variable;
        consumption1Variable = owner.Consumption1Variable;
        consumption2Variable = owner.Consumption2Variable;
        consumptionVariable = owner.ConsumptionVariable;
        dummyVariable = owner.DummyVariable;



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
        String date = dateVariable.Value;
        int count = countVariable.Value;
        string jace = jaceVariable.Value;
        string jace1 = jace1Variable.Value;
        float consumption1 = consumption1Variable.Value;
        float consumption2 = consumption2Variable.Value;
        float consumption = consumptionVariable.Value;
        string dummy = dummyVariable.Value;



        string monthyear = monthyearVariable.Value;
        string year = yearVariable.Value;

        bool button = buttonVariable.Value;
        var project = FTOptix.HMIProject.Project.Current;

        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Delete 
        var myStore2 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Jace
        var myStore3 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption1
        var myStore4 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption2


        object[,] resultSet1;
        string[] header1;
        object[,] resultSet2;
        string[] header2;
        object[,] resultSet3;
        string[] header3;
        object[,] resultSet4;
        string[] header4;

        if (button == true)
        {

            if (count < 9)

            {
                DateTime currentTime = DateTime.Now;
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                int currentHour = currentTime.Hour;

                // Calculate start and end times for the current day
                DateTime startTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 8, 0, 0);
                DateTime endTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 7, 59, 59).AddDays(1);
                var date1 = startTime.ToString("dd-MM-yyyy");
                // Adjust the start time if the current hour is before 8 AM
                if (currentHour < 8)
                {
                    startTime = startTime.AddDays(-1);
                    endTime = endTime.AddDays(-1);
                }

               

                String jacee = jace1.ToString();
                
                
                string st = startTime.ToString("yyyy-MMM-dd");
                string et = endTime.ToString("yyyy-MMM-dd");
                monthyear = startTime.ToString("yyyy-MM");
                year = startTime.ToString("yyyy");

                if (count == 0)
                {

                    string query1 = $"DELETE FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Jace = '" + jacee + "'";
                    string query2 = $" SELECT Jace FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = 'J1_INCOMER1'"; ///Take JAce name from Seperate Jace Building
                    string query3 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_INCOMER1'";///Take Consumption from 33 KV building
                    string query4 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_INCOMER2'";///Take Consumption From 33 KV building 
                   
                    myStore1.Query(query1, out header1, out resultSet1);
                    myStore2.Query(query2, out header2, out resultSet2);
                    myStore3.Query(query3, out header3, out resultSet3);
                    myStore4.Query(query4, out header4, out resultSet4);

                    var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                    var columnCount2 = header2 != null ? header2.Length : 0;
                    if (rowCount2 > 0 && columnCount2 > 0)
                    {
                        var column1 = Convert.ToString(resultSet2[0, 0]);
                        jace = column1;

                    }

                    var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                    var columnCount3 = header3 != null ? header3.Length : 0;
                    if (rowCount3 > 0 && columnCount3 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet3[0, 0]);
                        consumption1 = column1;

                    }
                    var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                    var columnCount4 = header4 != null ? header4.Length : 0;
                    if (rowCount4 > 0 && columnCount4 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet4[0, 0]);
                        consumption2 = column1;

                    }



                    consumption = consumption1 + consumption2;

                    date = date1;
                    count = count + 1;

                }

               else if (count == 1)
                {

                    string query1 = $"DELETE FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Jace = '" + jacee + "'";
                    string query2 = $" SELECT Jace FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = 'J2_MCB_MVS_01_IN1'";///Took Name From Utility Building
                    string query3 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG2_IN1_UTILITY'"; // From 33 KV
                    string query4 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG13_IN2_UTILITY'";//From 33 KV

                    myStore1.Query(query1, out header1, out resultSet1);
                    myStore2.Query(query2, out header2, out resultSet2);
                    myStore3.Query(query3, out header3, out resultSet3);
                    myStore4.Query(query4, out header4, out resultSet4);

                    var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                    var columnCount2 = header2 != null ? header2.Length : 0;
                    if (rowCount2 > 0 && columnCount2 > 0)
                    {
                        var column1 = Convert.ToString(resultSet2[0, 0]);
                        jace = column1;

                    }

                    var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                    var columnCount3 = header3 != null ? header3.Length : 0;
                    if (rowCount3 > 0 && columnCount3 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet3[0, 0]);
                        consumption1 = column1;

                    }
                    var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                    var columnCount4 = header4 != null ? header4.Length : 0;
                    if (rowCount4 > 0 && columnCount4 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet4[0, 0]);
                        consumption2 = column1;

                    }



                    consumption = consumption1 + consumption2;

                    date = date1;
                    count = count + 1;

                }

                else if (count == 2)
                {

                    string query1 = $"DELETE FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Jace = '" + jacee + "'";
                    string query2 = $" SELECT Jace FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = 'J3_WTK-1-PMCC-0-01'";// From Pump Room 
                    string query3 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J3_WTK-1-PMCC-0-01'";//From Pump Room
                    string query4 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J3_WTK-1-PMCC-0-01'";///From PumpRoom

                    myStore1.Query(query1, out header1, out resultSet1);
                    myStore2.Query(query2, out header2, out resultSet2);
                    myStore3.Query(query3, out header3, out resultSet3);
                    myStore4.Query(query4, out header4, out resultSet4);

                    var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                    var columnCount2 = header2 != null ? header2.Length : 0;
                    if (rowCount2 > 0 && columnCount2 > 0)
                    {
                        var column1 = Convert.ToString(resultSet2[0, 0]);
                        jace = column1;

                    }

                    var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                    var columnCount3 = header3 != null ? header3.Length : 0;
                    if (rowCount3 > 0 && columnCount3 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet3[0, 0]);
                        consumption1 = column1;

                    }
                    var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                    var columnCount4 = header4 != null ? header4.Length : 0;
                    if (rowCount4 > 0 && columnCount4 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet4[0, 0]);
                        consumption2 = column1;

                    }



                    consumption = consumption1 ;  //////////////// Only Consumption 1 here because Pumproom has only one Incomer 

                    date = date1;
                    count = count + 1;

                }


               else if (count == 3)
                {

                    string query1 = $"DELETE FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Jace = '" + jacee + "'";
                    string query2 = $" SELECT Jace FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = 'J4_PNT_MVS_01_IN_F1'"; // From Paintshop Building 
                    string query3 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG7_IN1_PAINTSHOP'"; /// From 33KV
                    string query4 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG8_IN2_PAINTSHOP'";///From 33KV

                    myStore1.Query(query1, out header1, out resultSet1);
                    myStore2.Query(query2, out header2, out resultSet2);
                    myStore3.Query(query3, out header3, out resultSet3);
                    myStore4.Query(query4, out header4, out resultSet4);

                    var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                    var columnCount2 = header2 != null ? header2.Length : 0;
                    if (rowCount2 > 0 && columnCount2 > 0)
                    {
                        var column1 = Convert.ToString(resultSet2[0, 0]);
                        jace = column1;

                    }

                    var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                    var columnCount3 = header3 != null ? header3.Length : 0;
                    if (rowCount3 > 0 && columnCount3 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet3[0, 0]);
                        consumption1 = column1;

                    }
                    var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                    var columnCount4 = header4 != null ? header4.Length : 0;
                    if (rowCount4 > 0 && columnCount4 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet4[0, 0]);
                        consumption2 = column1;

                    }



                    consumption = consumption1 + consumption2;  
                    date = date1;
                    count = count + 1;

                }



                else if (count == 4)
                {

                    string query1 = $"DELETE FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Jace = '" + jacee + "'";
                    string query2 = $" SELECT Jace FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = 'J5_BDY_MVS_01_IN1'"; //// From Bodyshop
                    string query3 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG5_IN1_BODYSHOP1'"; ///From 33KV
                    string query4 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG10_IN2_BODYSHOP'";///From 33 KV

                    myStore1.Query(query1, out header1, out resultSet1);
                    myStore2.Query(query2, out header2, out resultSet2);
                    myStore3.Query(query3, out header3, out resultSet3);
                    myStore4.Query(query4, out header4, out resultSet4);

                    var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                    var columnCount2 = header2 != null ? header2.Length : 0;
                    if (rowCount2 > 0 && columnCount2 > 0)
                    {
                        var column1 = Convert.ToString(resultSet2[0, 0]);
                        jace = column1;

                    }

                    var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                    var columnCount3 = header3 != null ? header3.Length : 0;
                    if (rowCount3 > 0 && columnCount3 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet3[0, 0]);
                        consumption1 = column1;

                    }
                    var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                    var columnCount4 = header4 != null ? header4.Length : 0;
                    if (rowCount4 > 0 && columnCount4 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet4[0, 0]);
                        consumption2 = column1;

                    }



                    consumption = consumption1 + consumption2;
                    date = date1;
                    count = count + 1;

                }

                else if (count == 5)
                {

                    string query1 = $"DELETE FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Jace = '" + jacee + "'";
                    string query2 = $" SELECT Jace FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = 'J6_TCF_MVS_01_IN_F1'";////From TCF Building
                    string query3 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG4_IN1_TCF'";////From 33KV
                    string query4 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG11_IN2_TCF'";////From 33KV

                    myStore1.Query(query1, out header1, out resultSet1);
                    myStore2.Query(query2, out header2, out resultSet2);
                    myStore3.Query(query3, out header3, out resultSet3);
                    myStore4.Query(query4, out header4, out resultSet4);

                    var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                    var columnCount2 = header2 != null ? header2.Length : 0;
                    if (rowCount2 > 0 && columnCount2 > 0)
                    {
                        var column1 = Convert.ToString(resultSet2[0, 0]);
                        jace = column1;

                    }

                    var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                    var columnCount3 = header3 != null ? header3.Length : 0;
                    if (rowCount3 > 0 && columnCount3 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet3[0, 0]);
                        consumption1 = column1;

                    }
                    var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                    var columnCount4 = header4 != null ? header4.Length : 0;
                    if (rowCount4 > 0 && columnCount4 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet4[0, 0]);
                        consumption2 = column1;

                    }



                    consumption = consumption1 + consumption2;
                    date = date1;
                    count = count + 1;

                }

               else if (count == 6)
                {

                    string query1 = $"DELETE FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Jace = '" + jacee + "'";
                    string query2 = $" SELECT Jace FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = 'J7_STP_MVS_01_IN_F1'"; ///From Stamping Building
                    string query3 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG3_IN1_STAMPING'";///From 33KV
                    string query4 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG12_IN2_STAMPING'";///From 33KV

                    myStore1.Query(query1, out header1, out resultSet1);
                    myStore2.Query(query2, out header2, out resultSet2);
                    myStore3.Query(query3, out header3, out resultSet3);
                    myStore4.Query(query4, out header4, out resultSet4);

                    var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                    var columnCount2 = header2 != null ? header2.Length : 0;
                    if (rowCount2 > 0 && columnCount2 > 0)
                    {
                        var column1 = Convert.ToString(resultSet2[0, 0]);
                        jace = column1;

                    }

                    var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                    var columnCount3 = header3 != null ? header3.Length : 0;
                    if (rowCount3 > 0 && columnCount3 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet3[0, 0]);
                        consumption1 = column1;

                    }
                    var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                    var columnCount4 = header4 != null ? header4.Length : 0;
                    if (rowCount4 > 0 && columnCount4 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet4[0, 0]);
                        consumption2 = column1;

                    }



                    consumption = consumption1 + consumption2;
                    date = date1;
                    count = count + 1;

                }

                 else if (count == 7)
                {

                    string query1 = $"DELETE FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Jace = '" + jacee + "'";
                    string query2 = $" SELECT Jace FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = 'J8_ADM_DB_01_INF1'"; ///From Admin Building
                    string query3 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J8_ADM_DB_01_INF1'";///From Admin
                    string query4 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J8_ADM_EDB_01_INF2'";///From Admin

                    myStore1.Query(query1, out header1, out resultSet1);
                    myStore2.Query(query2, out header2, out resultSet2);
                    myStore3.Query(query3, out header3, out resultSet3);
                    myStore4.Query(query4, out header4, out resultSet4);

                    var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                    var columnCount2 = header2 != null ? header2.Length : 0;
                    if (rowCount2 > 0 && columnCount2 > 0)
                    {
                        var column1 = Convert.ToString(resultSet2[0, 0]);
                        jace = column1;

                    }

                    var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                    var columnCount3 = header3 != null ? header3.Length : 0;
                    if (rowCount3 > 0 && columnCount3 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet3[0, 0]);
                        consumption1 = column1;

                    }
                    var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                    var columnCount4 = header4 != null ? header4.Length : 0;
                    if (rowCount4 > 0 && columnCount4 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet4[0, 0]);
                        consumption2 = column1;

                    }



                    consumption = consumption1 + consumption2;
                    date = date1;
                    count = count + 1;

                }


               else if (count == 8)
                {

                    string query1 = $"DELETE FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Jace = '" + jacee + "'";
                    string query2 = $" SELECT Jace FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = 'J9_M1'"; ///From SPP Building
                    string query3 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG16_IN1_SPP1'";///From 33KV
                    string query4 = $" SELECT Consumption  FROM DailyConsumptionAgg WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '33KV' AND Meter = 'J1_OG15_IN2_SPP'";///From 33KV

                    myStore1.Query(query1, out header1, out resultSet1);
                    myStore2.Query(query2, out header2, out resultSet2);
                    myStore3.Query(query3, out header3, out resultSet3);
                    myStore4.Query(query4, out header4, out resultSet4);

                    var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                    var columnCount2 = header2 != null ? header2.Length : 0;
                    if (rowCount2 > 0 && columnCount2 > 0)
                    {
                        var column1 = Convert.ToString(resultSet2[0, 0]);
                        jace = column1;

                    }

                    var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                    var columnCount3 = header3 != null ? header3.Length : 0;
                    if (rowCount3 > 0 && columnCount3 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet3[0, 0]);
                        consumption1 = column1;

                    }
                    var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                    var columnCount4 = header4 != null ? header4.Length : 0;
                    if (rowCount4 > 0 && columnCount4 > 0)
                    {
                        var column1 = Convert.ToInt32(resultSet4[0, 0]);
                        consumption2 = column1;

                    }



                    consumption = consumption1 + consumption2;
                    date = date1;
                    count = count + 1;

                }


            }
        

            else
            {
                count = 0;
            }


        }

        dateVariable.Value = date;
        countVariable.Value = count;
        jaceVariable.Value = jace;
        consumptionVariable.Value = consumption;
        consumption1Variable.Value = consumption1;
        consumption2Variable.Value = consumption2;
        monthyearVariable.Value = monthyear;
        yearVariable.Value = year;




    }
    private IUAVariable dateVariable;
    private IUAVariable buttonVariable;
    private IUAVariable countVariable;
    private IUAVariable monthyearVariable;
    private IUAVariable yearVariable;
    private IUAVariable jaceVariable;
    private IUAVariable jace1Variable;
    private IUAVariable consumption1Variable;
    private IUAVariable consumption2Variable;
    private IUAVariable consumptionVariable;
    private IUAVariable dummyVariable;
    private PeriodicTask periodicTask;
}
