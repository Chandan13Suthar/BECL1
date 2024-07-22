#region Using directives
using System;
using UAManagedCore;
//using OpcUa = UAManagedCore.OpcUa;
//using FTOptix.DataLogger;
//using FTOptix.HMIProject;
using FTOptix.NetLogic;
//using FTOptix.NativeUI;
//using FTOptix.UI;
//using FTOptix.CoreBase;
//using FTOptix.Store;
//using FTOptix.ODBCStore;
//using FTOptix.Report;
//using FTOptix.RAEtherNetIP;
//using FTOptix.Retentivity;
//using FTOptix.CommunicationDriver;
//using FTOptix.Core;
using Store = FTOptix.Store;
//using System.Text.RegularExpressions;
//using FTOptix.SQLiteStore;
//using System.Data;
//using System.Linq;
//using System.Data.SqlClient;
//using System.Reflection.Emit;
//using FTOptix.MicroController;
//using System.Collections.Generic;
//using System.Collections;
//using System.Diagnostics.Metrics;
//using FTOptix.AuditSigning;
//using FTOptix.Alarm;
using FTOptix.Modbus;
#endregion
public class RuntimeNetLogic5 : BaseNetLogic
{


    public override void Start()
    {
        var owner = (HourlyDataAgg)LogicObject.Owner;


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
        minconsumptionVariable = owner.MinconsumptionVariable;

        /////////////////Power///////////////////////////
        minconsumptionVariable = owner.MinconsumptionVariable;
        maxactivepowerVariable = owner.MaxActivePowerVariable;
        minactivepowerVariable = owner.MinActivePowerVariable;
        maxapparentpowerVariable = owner.MaxApparentPowerVariable;
        minapparentpowerVariable = owner.MinApparentPowerVariable;
        maxreactivepowerVariable = owner.MaxReactivePowerVariable;
        minreactivepowerVariable = owner.MinReactivePowerVariable;

        ////////////////Energy/////////////////////////
        minconsumptionVariable = owner.MinconsumptionVariable;
        maxactiveenergyVariable = owner.MaxActiveEnergyVariable;
        minactiveenergyVariable = owner.MinActiveEnergyVariable;
        maxapparentenergyVariable = owner.MaxApparentEnergyVariable;
        minapparentenergyVariable = owner.MinApparentEnergyVariable;
        maxreactiveenergyVariable = owner.MaxReactiveEnergyVariable;
        minreactiveenergyVariable = owner.MinReactiveEnergyVariable;

        periodicTask = new PeriodicTask(IncrementDecrementTask, 1000, LogicObject);
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
        int count = countVariable.Value;
        string jace = jaceVariable.Value;
        string meter1 = meter1Variable.Value;
        string jace1 = jace1Variable.Value;
        float consumption = consumptionVariable.Value;
        float activepowertotal = activepowertotalVariable.Value;
        float apparentpowertotal = apparentpowertotalVariable.Value;
        float reactivepowertotal = reactivepowertotalVariable.Value;
        float activeenergytotal = activeenergytotalVariable.Value;
        float apparentenergytotal = apparentenergytotalVariable.Value;
        float reactiveenergytotal = reactiveenergytotalVariable.Value;
        float avgpf = avgpfVariable.Value;
        float phasercurrent = phasercurrentVariable.Value;
        float phaseycurrent = phaseycurrentVariable.Value;
        float phasebcurrent = phasebcurrentVariable.Value;
        float avgcurrent = avgcurrentVariable.Value;
        float avgvoltagell = avgvoltagellVariable.Value;
        float avgvoltageln = avgvoltagelnVariable.Value;
        float frequency = frequencyVariable.Value;
        float voltagery = voltageryVariable.Value;
        float voltageyb = voltageybVariable.Value;
        float voltagebr = voltagebrVariable.Value;
        float voltagern = voltagernVariable.Value;
        float voltageyn = voltageynVariable.Value;
        float voltagebn = voltagebnVariable.Value;
        float maxactivepower = maxactivepowerVariable.Value;
        float maxapparentpower = maxapparentpowerVariable.Value;
        float maxreactivepower = maxreactivepowerVariable.Value;
        float maxactiveenergy = maxactiveenergyVariable.Value;
        float maxapparentenergy = maxapparentenergyVariable.Value;
        float maxreactiveenergy = maxreactiveenergyVariable.Value;
        float minactivepower = minactivepowerVariable.Value;
        float minapparentpower = minapparentpowerVariable.Value;
        float minreactivepower = minreactivepowerVariable.Value;
        float minactiveenergy = minactiveenergyVariable.Value;
        float minapparentenergy = minapparentenergyVariable.Value;
        float minreactiveenergy = minreactiveenergyVariable.Value;
        bool button = buttonVariable.Value;
        var project = FTOptix.HMIProject.Project.Current;
        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");///Delete Incomer2


        if (button == true)
        {

            if (count <= 18)

            {
                string currentHour = DateTime.Now.ToString("HH");
                string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                String jacee = jace1.ToString();
                string meterr = meter1.ToString();

           
                myStore1.Query("DELETE FROM DailyConsumption WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Meter = '" + meterr + "'", out string[] header1, out object[,] resultSet1);
                myStore1.Query(" SELECT     Meter FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header2, out object[,] resultSet2);
                myStore1.Query(" SELECT    Jace FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header24, out object[,] resultSet24);
                myStore1.Query(" SELECT     MAX(Active_Energy_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header3, out object[,] resultSet3);
                myStore1.Query(" SELECT     MAX(Active_Power_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header4, out object[,] resultSet4);
                myStore1.Query(" SELECT     MAX(Apparent_Power_Total_Power_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header5, out object[,] resultSet5);
                myStore1.Query(" SELECT     MAX(Reactive_Power_Total_Power_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header6, out object[,] resultSet6);
                myStore1.Query(" SELECT     MAX(Active_Energy_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header7, out object[,] resultSet7);
                myStore1.Query(" SELECT     MAX(Apparent_Energy_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header8, out object[,] resultSet8);
                myStore1.Query(" SELECT     MAX(Reactive_Energy_Total)  FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header9, out object[,] resultSet9);
                myStore1.Query(" SELECT    AVG(Avg_Pf) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header10, out object[,] resultSet10);
                myStore1.Query(" SELECT    AVG(Phase_R_Current) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header11, out object[,] resultSet11);
                myStore1.Query(" SELECT    AVG(Phase_Y_Current) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header12, out object[,] resultSet12);
                myStore1.Query(" SELECT    AVG(Phase_B_Current) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header13, out object[,] resultSet13);
                myStore1.Query(" SELECT    AVG(Avg_Current)FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header14, out object[,] resultSet14);
                myStore1.Query(" SELECT    AVG(Avg_Voltage_LL) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header15, out object[,] resultSet15);
                myStore1.Query(" SELECT    AVG(Avg_Voltage_LN) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header16, out object[,] resultSet16);
                myStore1.Query(" SELECT    AVG(Frequency) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header17, out object[,] resultSet17);
                myStore1.Query(" SELECT    AVG(Voltage_RY) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header18, out object[,] resultSet18);
                myStore1.Query(" SELECT    AVG(Voltage_YB )FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header19, out object[,] resultSet19);
                myStore1.Query(" SELECT    AVG(Voltage_BR) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header20, out object[,] resultSet20);
                myStore1.Query(" SELECT    AVG(Voltage_RN) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "'AND Meter = '" + meterr + "'", out string[] header21, out object[,] resultSet21);
                myStore1.Query(" SELECT    AVG(Voltage_YN) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header22, out object[,] resultSet22);
                myStore1.Query(" SELECT    AVG(Voltage_BN) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header23, out object[,] resultSet23);
                myStore1.Query(" SELECT     MIN(Active_Energy_Total)  FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header25, out object[,] resultSet25);
                myStore1.Query(" SELECT    MIN(Active_Energy_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header26, out object[,] resultSet26);
                myStore1.Query(" SELECT      MIN(Apparent_Power_Total_Power_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header27, out object[,] resultSet27);
                myStore1.Query(" SELECT      MIN(Reactive_Power_Total_Power_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header28, out object[,] resultSet28);
                myStore1.Query(" SELECT     MIN(Active_Energy_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header29, out object[,] resultSet29);
                myStore1.Query(" SELECT     MIN(Apparent_Energy_Total) FROM HomePage WHERE LocalTimestamp BETWEEN '" + currentDate + " " + currentHour + ":00:00' AND '" + currentDate + " " + currentHour + ":59:59' AND Jace = '" + jacee + "' AND Meter = '" + meterr + "'", out string[] header30, out object[,] resultSet30);





                // var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
                // var columnCount1 = header1 != null ? header1.Length : 0;
                // if (rowCount1 > 0 && columnCount1 > 0)
                //  {
                //     var column1 = Convert.ToString(resultSet1[0, 0]);
                //     meter = column1;

                //  }


                var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
                var columnCount2 = header2 != null ? header2.Length : 0;
                if (rowCount2 > 0 && columnCount2 > 0)
                {
                    var column1 = Convert.ToString(resultSet2[0, 0]);
                    meter = column1;

                }


                var rowCount24 = resultSet24 != null ? resultSet24.GetLength(0) : 0;
                var columnCount24 = header24 != null ? header24.Length : 0;
                if (rowCount24 > 0 && columnCount24 > 0)
                {
                    var column1 = Convert.ToString(resultSet24[0, 0]);
                    jace = column1;

                }

                var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
                var columnCount3 = header3 != null ? header3.Length : 0;
                if (rowCount3 > 0 && columnCount3 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet3[0, 0]);
                   // var column2 = Convert.ToString(resultSet3[0, 1]);
                    consumption = column1;
                    //jace = column2;
                }



                var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
                var columnCount4 = header4 != null ? header4.Length : 0;
                if (rowCount4 > 0 && columnCount4 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet4[0, 0]);
                    maxactivepower = column1;

                }

                var rowCount5 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
                var columnCount5 = header5 != null ? header5.Length : 0;
                if (rowCount5 > 0 && columnCount5 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet5[0, 0]);
                    maxapparentpower = column1;

                }

                var rowCount6 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
                var columnCount6 = header6 != null ? header6.Length : 0;
                if (rowCount6 > 0 && columnCount6 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet6[0, 0]);
                    maxreactivepower = column1;

                }

                var rowCount7 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
                var columnCount7 = header7 != null ? header7.Length : 0;
                if (rowCount7 > 0 && columnCount7 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet7[0, 0]);
                    maxactiveenergy = column1;

                }

                var rowCount8 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
                var columnCount8 = header8 != null ? header8.Length : 0;
                if (rowCount8 > 0 && columnCount8 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet8[0, 0]);
                    maxapparentenergy = column1;

                }

                var rowCount9 = resultSet9 != null ? resultSet9.GetLength(0) : 0;
                var columnCount9 = header9 != null ? header9.Length : 0;
                if (rowCount9 > 0 && columnCount9 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet9[0, 0]);
                    maxreactiveenergy = column1;

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


                var rowCount25 = resultSet25 != null ? resultSet25.GetLength(0) : 0;
                var columnCount25 = header25 != null ? header25.Length : 0;
                if (rowCount25 > 0 && columnCount25 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet25[0, 0]);
                    minactivepower = column1;

                }

                var rowCount26 = resultSet26 != null ? resultSet26.GetLength(0) : 0;
                var columnCount26 = header26 != null ? header26.Length : 0;
                if (rowCount26 > 0 && columnCount26 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet26[0, 0]);
                    minapparentpower = column1;

                }

                var rowCount27 = resultSet27 != null ? resultSet27.GetLength(0) : 0;
                var columnCount27 = header27 != null ? header27.Length : 0;
                if (rowCount27 > 0 && columnCount27 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet27[0, 0]);
                    minreactivepower = column1;

                }

                var rowCount28 = resultSet28 != null ? resultSet28.GetLength(0) : 0;
                var columnCount28 = header28 != null ? header28.Length : 0;
                if (rowCount28 > 0 && columnCount28 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet28[0, 0]);
                    minactiveenergy = column1;

                }

                var rowCount29 = resultSet29 != null ? resultSet29.GetLength(0) : 0;
                var columnCount29 = header29 != null ? header29.Length : 0;
                if (rowCount29 > 0 && columnCount29 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet29[0, 0]);
                    minapparentenergy = column1;

                }

                var rowCount30 = resultSet30 != null ? resultSet30.GetLength(0) : 0;
                var columnCount30 = header30 != null ? header30.Length : 0;
                if (rowCount30 > 0 && columnCount30 > 0)
                {
                    var column1 = Convert.ToInt32(resultSet30[0, 0]);
                    minreactiveenergy = column1;

                }


                count = count + 1;

            }
            else
            {
                count = 0;
            }



        }


        meterVariable.Value = meter;
        countVariable.Value = count;
        jaceVariable.Value = jace;
        consumptionVariable.Value = consumption;
        activepowertotalVariable.Value = activepowertotal;
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


        maxactivepowerVariable.Value = maxactivepower;
        maxapparentpowerVariable.Value = maxapparentpower;
        maxreactivepowerVariable.Value = maxreactivepower;
        maxactiveenergyVariable.Value = maxactiveenergy;
        maxapparentenergyVariable.Value = maxapparentenergy;
        maxreactiveenergyVariable.Value = maxreactiveenergy;

        minactivepowerVariable.Value = minactivepower;
        minapparentpowerVariable.Value = minapparentpower;
        minreactivepowerVariable.Value = minreactivepower;
        minactiveenergyVariable.Value = minactiveenergy;
        minapparentenergyVariable.Value = minapparentenergy;
        minreactiveenergyVariable.Value = minreactiveenergy;



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
    private IUAVariable maxactivepowerVariable;
    private IUAVariable minactivepowerVariable;
    private IUAVariable maxapparentpowerVariable;
    private IUAVariable minapparentpowerVariable;
    private IUAVariable maxreactivepowerVariable;
    private IUAVariable minreactivepowerVariable;
    private IUAVariable maxactiveenergyVariable;
    private IUAVariable minactiveenergyVariable;
    private IUAVariable maxapparentenergyVariable;
    private IUAVariable minapparentenergyVariable;
    private IUAVariable maxreactiveenergyVariable;
    private IUAVariable minreactiveenergyVariable;

    private PeriodicTask periodicTask;

}



