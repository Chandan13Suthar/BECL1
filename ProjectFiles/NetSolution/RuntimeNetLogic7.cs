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
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.Modbus;
#endregion
public class RuntimeNetLogic7 : BaseNetLogic
{


    public override void Start()
    {
        var owner = (DailyDataAgg)LogicObject.Owner;


        dateVariable = owner.DateVariable;
        buttonVariable = owner.ButtonVariable;
        meter1Variable = owner.Meter1Variable;
        jace1Variable = owner.Jace1Variable;
        countVariable = owner.CountVariable;

        ////////////Tags////////////////////
        meterVariable = owner.MeterVariable;
        consumptionVariable = owner.ConsumptionVariable;
        jaceVariable = owner.JaceVariable;
        activepowertotalVariable = owner.ActivePowerTotalVariable;
        apparentpowertotalVariable = owner.ApparentPowerTotalVariable;
        reactivepowertotalVariable = owner.ReactivePowerTotalVariable;
        activeenergytotalVariable = owner.ActiveEnergyTotalVariable;
        apparentenergytotalVariable = owner.ApparentEnergyTotalVariable;
        reactiveenergytotalVariable = owner.ReactiveEnergyTotalVariable;
        avgpfVariable = owner.AvgPFVariable;
        phasercurrentVariable = owner.PhaseRCurrentVariable;
        phaseycurrentVariable = owner.PhaseYCurrentVariable;
        phasebcurrentVariable = owner.PhaseBCurrentVariable;
        avgcurrentVariable = owner.AvgCurrentVariable;
        avgvoltagellVariable = owner.AvgVoltageLLVariable;
        avgvoltagelnVariable = owner.AvgVoltageLNVariable;
        frequencyVariable = owner.FrequencyVariable;
        voltageryVariable = owner.VoltageRYVariable;
        voltageybVariable = owner.VoltageYBVariable;
        voltagebrVariable = owner.VoltageBRVariable;
        voltagernVariable = owner.VoltageRNVariable;
        voltageynVariable = owner.VoltageYNVariable;
        voltagebnVariable = owner.VoltageBNVariable;
        maxconsumptionVariable = owner.MaxconsumptionVariable;
        // minconsumptionVariable = owner.MinconsumptionVariable;
        minconsumptionVariable = owner.MinconsumptionVariable;
        monthVariable = owner.MonthVariable;
         monthyearVariable = owner.MonthYearVariable;
         yearVariable = owner.YearVariable;


        periodicTask = new PeriodicTask(IncrementDecrementTask, 3000, LogicObject);
        periodicTask.Start();
    }

    public override void Stop()
    {

        periodicTask.Dispose();
        periodicTask = null;
    }

    public void IncrementDecrementTask()

    {
        string meter = meterVariable.Value;
        string date = dateVariable.Value;
        int count = countVariable.Value;
        string jace = jaceVariable.Value;
        string meter1 = meter1Variable.Value;
        string jace1 = jace1Variable.Value;
        int consumption = consumptionVariable.Value;
        int activepowertotal = activepowertotalVariable.Value;
        int apparentpowertotal = apparentpowertotalVariable.Value;
        int reactivepowertotal = reactivepowertotalVariable.Value;
        int activeenergytotal = activeenergytotalVariable.Value;
        int apparentenergytotal = apparentenergytotalVariable.Value;
        int reactiveenergytotal = reactiveenergytotalVariable.Value;
        int avgpf = avgpfVariable.Value;
        int phasercurrent = phasercurrentVariable.Value;
        int phaseycurrent = phaseycurrentVariable.Value;
        int phasebcurrent = phasebcurrentVariable.Value;
        int avgcurrent = avgcurrentVariable.Value;
        int avgvoltagell = avgvoltagellVariable.Value;
        int avgvoltageln = avgvoltagelnVariable.Value;
        int frequency = frequencyVariable.Value;
        int voltagery = voltageryVariable.Value;
        int voltageyb = voltageybVariable.Value;
        int voltagebr = voltagebrVariable.Value;
        int voltagern = voltagernVariable.Value;
        int voltageyn = voltageynVariable.Value;
        int voltagebn = voltagebnVariable.Value;
        int minconsumption = minconsumptionVariable.Value;
        int maxconsumption = maxconsumptionVariable.Value;
        string month = monthVariable.Value;
        string monthyear = monthyearVariable.Value;
         string year = yearVariable.Value;




        bool button = buttonVariable.Value;
        var project = FTOptix.HMIProject.Project.Current;
        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Delete Incomer2
        var myStore2 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Meter
        var myStore3 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore4 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore5 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore6 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore7 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore8 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore9 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore10 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore11 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore12 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore13 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore14 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore15 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore16 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore17 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore18 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore19 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore20 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore21 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore22 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore23 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore24 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        var myStore25 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Consumption
        //var myStore25 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///jace






        ////////33KV////////
        object[,] resultSet1;
        string[] header1;
        object[,] resultSet2;
        string[] header2;
        object[,] resultSet3;
        string[] header3;
        object[,] resultSet4;
        string[] header4;
        object[,] resultSet5;
        string[] header5;
        object[,] resultSet6;
        string[] header6;
        object[,] resultSet7;
        string[] header7;
        object[,] resultSet8;
        string[] header8;
        object[,] resultSet9;
        string[] header9;
        object[,] resultSet10;
        string[] header10;
        object[,] resultSet11;
        string[] header11;
        object[,] resultSet12;
        string[] header12;
        object[,] resultSet13;
        string[] header13;
        object[,] resultSet14;
        string[] header14;
        object[,] resultSet16;
        string[] header16;
        object[,] resultSet17;
        string[] header17;

        object[,] resultSet15;
        string[] header15;

        object[,] resultSet18;
        string[] header18;

        object[,] resultSet19;
        string[] header19;

        object[,] resultSet20;
        string[] header20;

        object[,] resultSet21;
        string[] header21;


        object[,] resultSet22;
        string[] header22;



        object[,] resultSet23;
        string[] header23;

        object[,] resultSet24;
        string[] header24;

        object[,] resultSet25;
        string[] header25;










        if (button == true)
        {

            if (count <= 18)

            {
                DateTime currentTime = DateTime.Now;
                string currentDate = currentTime.ToString("yyyy-MM-dd");
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

                string timeRange = $" BETWEEN '{startTime:yyyy-MM-dd HH:mm:ss}' AND '{endTime:yyyy-MM-dd HH:mm:ss}'";

                //string currentHour = DateTime.Now.ToString("HH");
                //string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                String jacee = jace1.ToString();
                string meterr = meter1.ToString();

                string st = startTime.ToString("yyyy-MMM-dd");
                string et = endTime.ToString("yyyy-MMM-dd");
                month = startTime.ToString("MM");
                monthyear = startTime.ToString("yyyy-MM");
                 year = startTime.ToString("yyyy");



                string query1 = $"DELETE FROM DailyConsumptionAgg WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Meter = '" + meterr + "'";
                string query2 = $" SELECT Meter FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query3 = $" SELECT MAX(Consumption)  FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query4 = $" SELECT AVG(Active_Power_Total) FROM  DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query5 = $" SELECT AVG(Apparent_Power_Total) FROM DailyConsumption WHERE LocalTimestamp   BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query6 = $" SELECT AVG(Reactive_Power_Total)  FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query7 = $" SELECT AVG(Active_Energy_Total) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query8 = $" SELECT AVG(Apparent_Energy_Total) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query9 = $" SELECT AVG(Reactive_Energy_Total) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query10 = $" SELECT AVG(Avg_PF) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query11 = $" SELECT AVG(Phase_R_Current) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query12 = $" SELECT AVG(Phase_Y_Current) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query13 = $" SELECT AVG(Phase_B_Current) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query14 = $" SELECT AVG(Avg_Current)FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query15 = $" SELECT AVG(Voltage_LL) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query16 = $" SELECT AVG(Voltage_LN) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query17 = $" SELECT AVG(Frequency) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query18 = $" SELECT AVG(Voltage_RY) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query19 = $" SELECT AVG(Voltage_YB)FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query20 = $" SELECT AVG(Voltage_BR) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query21 = $" SELECT AVG(Voltage_RN) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "'AND Meter = '" + meterr + "'";
                string query22 = $" SELECT AVG(Voltage_YN) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query23 = $" SELECT AVG(Voltage_BN) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query24 = $" SELECT MIN(Consumption) FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";
                string query25 = $" SELECT Jace FROM DailyConsumption WHERE LocalTimestamp  BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59'  AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'";




                myStore1.Query(query1, out header1, out resultSet1);
                myStore2.Query(query2, out header2, out resultSet2);
                myStore3.Query(query3, out header3, out resultSet3);
                myStore4.Query(query4, out header4, out resultSet4);
                myStore5.Query(query5, out header5, out resultSet5);
                myStore6.Query(query6, out header6, out resultSet6);
                myStore7.Query(query7, out header7, out resultSet7);
                myStore8.Query(query8, out header8, out resultSet8);
                myStore9.Query(query9, out header9, out resultSet9);
                myStore10.Query(query10, out header10, out resultSet10);
                myStore11.Query(query11, out header11, out resultSet11);
                myStore12.Query(query12, out header12, out resultSet12);
                myStore13.Query(query13, out header13, out resultSet13);
                myStore14.Query(query14, out header14, out resultSet14);
                myStore15.Query(query15, out header15, out resultSet15);
                myStore16.Query(query16, out header16, out resultSet16);
                myStore17.Query(query17, out header17, out resultSet17);
                myStore18.Query(query18, out header18, out resultSet18);
                myStore19.Query(query19, out header19, out resultSet19);
                myStore20.Query(query20, out header20, out resultSet20);
                myStore21.Query(query21, out header21, out resultSet21);
                myStore22.Query(query22, out header22, out resultSet22);
                myStore23.Query(query23, out header23, out resultSet23);
                myStore24.Query(query24, out header24, out resultSet24);
                myStore25.Query(query25, out header25, out resultSet25);




                var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
                var columnCount1 = header1 != null ? header1.Length : 0;
                if (rowCount1 > 0 && columnCount1 > 0)
                {
                    var column1 = Convert.ToString(resultSet1[0, 0]);
                    meter = column1;

                }


                var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                var columnCount2 = header2 != null ? header2.Length : 0;
                if (rowCount2 > 0 && columnCount2 > 0)
                {
                    var column1 = Convert.ToString(resultSet2[0, 0]);
                    meter = column1;

                }

                var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                var columnCount3 = header3 != null ? header3.Length : 0;
                if (rowCount3 > 0 && columnCount3 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet3[0, 0]);
                    consumption = column1;

                }

                var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                var columnCount4 = header4 != null ? header4.Length : 0;
                if (rowCount4 > 0 && columnCount4 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet4[0, 0]);
                    activepowertotal = column1;

                }

                var rowCount5 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
                var columnCount5 = header5 != null ? header5.Length : 0;
                if (rowCount5 > 0 && columnCount5 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet5[0, 0]);
                    apparentpowertotal = column1;

                }

                var rowCount6 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
                var columnCount6 = header6 != null ? header6.Length : 0;
                if (rowCount6 > 0 && columnCount6 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet6[0, 0]);
                    reactivepowertotal = column1;

                }

                var rowCount7 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
                var columnCount7 = header7 != null ? header7.Length : 0;
                if (rowCount7 > 0 && columnCount7 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet7[0, 0]);
                    activeenergytotal = column1;

                }

                var rowCount8 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
                var columnCount8 = header8 != null ? header8.Length : 0;
                if (rowCount8 > 0 && columnCount8 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet8[0, 0]);
                    apparentenergytotal = column1;

                }

                var rowCount9 = resultSet9 != null ? resultSet9.GetLength(0) : 0;
                var columnCount9 = header9 != null ? header9.Length : 0;
                if (rowCount9 > 0 && columnCount9 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet9[0, 0]);
                    reactiveenergytotal = column1;

                }

                var rowCount10 = resultSet10 != null ? resultSet10.GetLength(0) : 0;
                var columnCount10 = header10 != null ? header10.Length : 0;
                if (rowCount10 > 0 && columnCount10 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet10[0, 0]);
                    avgpf = column1;

                }

                var rowCount11 = resultSet11 != null ? resultSet11.GetLength(0) : 0;
                var columnCount11 = header11 != null ? header11.Length : 0;
                if (rowCount11 > 0 && columnCount11 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet11[0, 0]);
                    phasercurrent = column1;

                }

                var rowCount12 = resultSet12 != null ? resultSet12.GetLength(0) : 0;
                var columnCount12 = header12 != null ? header12.Length : 0;
                if (rowCount12 > 0 && columnCount12 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet12[0, 0]);
                    phaseycurrent = column1;

                }

                var rowCount13 = resultSet13 != null ? resultSet13.GetLength(0) : 0;
                var columnCount13 = header13 != null ? header13.Length : 0;
                if (rowCount13 > 0 && columnCount13 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet13[0, 0]);
                    phasebcurrent = column1;

                }

                var rowCount14 = resultSet14 != null ? resultSet14.GetLength(0) : 0;
                var columnCount14 = header14 != null ? header14.Length : 0;
                if (rowCount14 > 0 && columnCount14 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet14[0, 0]);
                    avgcurrent = column1;

                }

                var rowCount15 = resultSet15 != null ? resultSet15.GetLength(0) : 0;
                var columnCount15 = header15 != null ? header15.Length : 0;
                if (rowCount15 > 0 && columnCount15 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet15[0, 0]);
                    avgvoltagell = column1;

                }

                var rowCount16 = resultSet16 != null ? resultSet16.GetLength(0) : 0;
                var columnCount16 = header16 != null ? header16.Length : 0;
                if (rowCount16 > 0 && columnCount16 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet16[0, 0]);
                    avgvoltageln = column1;
                }

                var rowCount17 = resultSet17 != null ? resultSet17.GetLength(0) : 0;
                var columnCount17 = header17 != null ? header17.Length : 0;
                if (rowCount17 > 0 && columnCount17 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet17[0, 0]);
                    frequency = column1;

                }

                var rowCount18 = resultSet18 != null ? resultSet18.GetLength(0) : 0;
                var columnCount18 = header18 != null ? header18.Length : 0;
                if (rowCount18 > 0 && columnCount18 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet18[0, 0]);
                    voltagery = column1;

                }

                var rowCount19 = resultSet19 != null ? resultSet19.GetLength(0) : 0;
                var columnCount19 = header19 != null ? header19.Length : 0;
                if (rowCount19 > 0 && columnCount19 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet19[0, 0]);
                    voltageyb = column1;

                }

                var rowCount20 = resultSet20 != null ? resultSet20.GetLength(0) : 0;
                var columnCount20 = header20 != null ? header20.Length : 0;
                if (rowCount20 > 0 && columnCount20 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet20[0, 0]);
                    voltagebr = column1;

                }

                var rowCount21 = resultSet21 != null ? resultSet21.GetLength(0) : 0;
                var columnCount21 = header21 != null ? header21.Length : 0;
                if (rowCount21 > 0 && columnCount21 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet21[0, 0]);
                    voltagern = column1;

                }

                var rowCount22 = resultSet22 != null ? resultSet22.GetLength(0) : 0;
                var columnCount22 = header22 != null ? header22.Length : 0;
                if (rowCount22 > 0 && columnCount22 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet22[0, 0]);
                    voltageyn = column1;

                }

                var rowCount23 = resultSet23 != null ? resultSet23.GetLength(0) : 0;
                var columnCount23 = header23 != null ? header23.Length : 0;
                if (rowCount23 > 0 && columnCount23 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet23[0, 0]);
                    voltagebn = column1;

                }

                var rowCount24 = resultSet24 != null ? resultSet24.GetLength(0) : 0;
                var columnCount24 = header24 != null ? header24.Length : 0;
                if (rowCount24 > 0 && columnCount24 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet24[0, 0]);
                    minconsumption = column1;

                }


                var rowCount25 = resultSet25 != null ? resultSet25.GetLength(0) : 0;
                var columnCount25 = header25 != null ? header25.Length : 0;
                if (rowCount25 > 0 && columnCount25 > 0)
                {
                    var column1 = Convert.ToString(resultSet25[0, 0]);
                    jace = column1;

                }

                date = date1;
                count = count + 1;



            }
            else
            {
                count = 0;
            }


        }

        //date = timeRange;
        dateVariable.Value = date;
        meterVariable.Value = meter;
        countVariable.Value = count;
        jaceVariable.Value = jace;
        consumptionVariable.Value = consumption;
        apparentpowertotalVariable.Value = apparentpowertotal;
        reactivepowertotalVariable.Value = reactivepowertotal;
        activeenergytotalVariable.Value = activeenergytotal;
        apparentenergytotalVariable.Value = apparentenergytotal;
        reactiveenergytotalVariable.Value = reactiveenergytotal;
        avgpfVariable.Value = avgpf;
        phasercurrentVariable.Value = phasercurrent;
        phaseycurrentVariable.Value = phaseycurrent;
        phasebcurrentVariable.Value = phasebcurrent;
        avgcurrentVariable.Value = avgcurrent;
        avgvoltagellVariable.Value = avgvoltagell;
        avgvoltagelnVariable.Value = avgvoltageln;
        frequencyVariable.Value = frequency;
        voltageryVariable.Value = voltagery;
        voltageybVariable.Value = voltageyb;
        voltagebrVariable.Value = voltagebr;
        voltagernVariable.Value = voltagern;
        voltageynVariable.Value = voltageyn;
        voltagebnVariable.Value = voltagebn;
        monthVariable.Value = month;
        monthyearVariable.Value = monthyear;
         yearVariable.Value = year;




    }

    private IUAVariable dateVariable;
    private IUAVariable buttonVariable;
    private IUAVariable meter1Variable;
    private IUAVariable jace1Variable;
    private IUAVariable countVariable;
    private IUAVariable meterVariable;
    private IUAVariable consumptionVariable;
    private IUAVariable jaceVariable;
    private IUAVariable activepowertotalVariable;
    private IUAVariable apparentpowertotalVariable;
    private IUAVariable reactivepowertotalVariable;
    private IUAVariable activeenergytotalVariable;
    private IUAVariable apparentenergytotalVariable;
    private IUAVariable reactiveenergytotalVariable;
    private IUAVariable avgpfVariable;
    private IUAVariable phasercurrentVariable;
    private IUAVariable phaseycurrentVariable;
    private IUAVariable phasebcurrentVariable;
    private IUAVariable avgcurrentVariable;
    private IUAVariable avgvoltagellVariable;
    private IUAVariable avgvoltagelnVariable;
    private IUAVariable frequencyVariable;
    private IUAVariable voltageryVariable;
    private IUAVariable voltageybVariable;
    private IUAVariable voltagebrVariable;
    private IUAVariable voltagernVariable;
    private IUAVariable voltageynVariable;
    private IUAVariable voltagebnVariable;
    private IUAVariable maxconsumptionVariable;
    private IUAVariable minconsumptionVariable;
    private IUAVariable monthVariable;
     private IUAVariable monthyearVariable;
      private IUAVariable yearVariable;
    private PeriodicTask periodicTask;
}
