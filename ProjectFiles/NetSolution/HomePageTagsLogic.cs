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

public class HomePageTagsLogic : BaseNetLogic
{
    private IUAVariable todayConsumptionVariable;
    private IUAVariable todayConsumptionCostVariable;
    private IUAVariable averageFrequencyVariable;
    private IUAVariable avgPowerFactorVariable;
    private IUAVariable maxDemandVariable;
    private IUAVariable monthConsumptionVariable;
    private IUAVariable monthConsumptionCostVariable;
    private IUAVariable buttonVariable;
    private IUAVariable guageMaxVariable;
    private IUAVariable day1Variable;
    private IUAVariable day2Variable;
    private IUAVariable day3Variable;
    private IUAVariable day4Variable;
    private PeriodicTask periodicTask;
    private IUAVariable day5Variable;
    private IUAVariable day6Variable;
    private IUAVariable day7Variable;
    private IUAVariable day8Variable;
    private IUAVariable day9Variable;
    private IUAVariable day10Variable;
    private IUAVariable day11Variable;
    private IUAVariable day12Variable;
    private IUAVariable day13Variable;
    private IUAVariable day14Variable;
    private IUAVariable day15Variable;
    private IUAVariable day16Variable;
    private IUAVariable day17Variable;
    private IUAVariable day18Variable;
    private IUAVariable day19Variable;
    private IUAVariable day20Variable;
    private IUAVariable day21Variable;
    private IUAVariable day22Variable;
    private IUAVariable day23Variable;
    private IUAVariable day24Variable;
    private IUAVariable day25Variable;
    private IUAVariable day25Varibale;
    private IUAVariable day26Variable;
    private IUAVariable day27Variable;
    private IUAVariable day28Variable;
    private IUAVariable day29Variable;
    private IUAVariable day30Variable;
    private IUAVariable day31Variable;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var owner = (HomePageTags)LogicObject.Owner;

        todayConsumptionVariable = owner.TodayConsumptionVariable;
        todayConsumptionCostVariable = owner.TodayConsumptionCostVariable;
        averageFrequencyVariable = owner.AverageFrequencyVariable;
        avgPowerFactorVariable = owner.AvgPowerFactorVariable;
        maxDemandVariable = owner.MaxDemandVariable;
        monthConsumptionVariable = owner.MonthConsumptionVariable;
        monthConsumptionCostVariable = owner.MonthConsumptionCostVariable;
        buttonVariable = owner.ButtonVariable;
        guageMaxVariable = owner.GuageMaxVariable;
        day1Variable = owner.Day1Variable;
        day2Variable = owner.Day2Variable;
        day3Variable = owner.Day3Variable;
        day4Variable = owner.Day4Variable;
        day5Variable = owner.Day5Variable;
        day6Variable = owner.Day6Variable;
        day7Variable =  owner.Day7Variable;
        day8Variable = owner.Day8Variable;
        day9Variable =  owner.Day9Variable;
        day10Variable = owner.Day10Variable;
        day11Variable = owner.Day11Variable;
        day12Variable = owner.Day12Variable;
        day13Variable = owner.Day13Variable;
        day14Variable = owner.Day14Variable;
        day15Variable = owner.Day15Variable;
        day16Variable = owner.Day16Variable;
        day17Variable = owner.Day17Variable;
        day18Variable = owner.Day18Variable;
        day19Variable = owner.Day19Variable;
        day20Variable = owner.Day20Variable;
        day21Variable = owner.Day21Variable;
        day22Variable = owner.Day22Variable;
        day23Variable = owner.Day23Variable;
        day24Variable = owner.Day24Variable;
        day25Variable = owner.Day25Variable;
        day26Variable = owner.Day26Variable;
        day27Variable = owner.Day27Variable;
        day28Variable = owner.Day28Variable;
        day29Variable = owner.Day29Variable;
        day30Variable = owner.Day30Variable;
        day31Variable = owner.Day31Variable;

        periodicTask = new PeriodicTask(HomePageCalculationTask, 10000, LogicObject);
        periodicTask.Start();
    }

    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
    }

    public void HomePageCalculationTask()
    {
        float todayconsumption = todayConsumptionVariable.Value;
        float monthconsumption = monthConsumptionVariable.Value;
        float monthconsumptioncost = monthConsumptionCostVariable.Value;
        float todayconsumptioncost = todayConsumptionCostVariable.Value;
        float averagefrequency = averageFrequencyVariable.Value;
        float avgpowerfactor = avgPowerFactorVariable.Value;
        bool button = buttonVariable.Value;
        int guagemax = guageMaxVariable.Value;
        float IN1Consumption = 0; // Declare outside the if block
        float IN2Consumption = 0; // Declare outside the if block
        float MIN1Consumption = 0; // Declare outside the if block
        float MIN2Consumption = 0; // Declare outside the if block
        float day1 = day1Variable.Value;
        float day2 = day2Variable.Value;
        float day3 = day3Variable.Value;
        float day4 = day4Variable.Value;
        float day5 = day5Variable.Value;
        float day6 = day6Variable.Value;
        float day7 = day7Variable.Value;
        float day8 = day8Variable.Value;
        float day9 = day9Variable.Value;
        float day10 = day10Variable.Value;
        float day11 = day11Variable.Value;
        float day12 = day12Variable.Value;
        float day13 = day13Variable.Value;
        float day14 = day14Variable.Value;
        float day15 = day15Variable.Value;
        float day16 = day16Variable.Value;
        float day17 = day17Variable.Value;
        float day18 = day18Variable.Value;
        float day19 = day19Variable.Value;
        float day20 = day20Variable.Value;
        float day21 = day21Variable.Value;
        float day22 = day22Variable.Value;
        float day23 = day23Variable.Value;
        float day24 = day24Variable.Value;
        float day25 = day25Variable.Value;
        float day26 = day26Variable.Value;
        float day27 = day27Variable.Value;
        float day28 = day28Variable.Value;
        float day29 = day29Variable.Value;
        float day30 = day30Variable.Value;
        float day31 = day31Variable.Value;

            

        

        var project = FTOptix.HMIProject.Project.Current;
        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore2 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore3 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore4 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore5 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore6 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore7 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore8 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore9 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore10 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore11 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore12 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore13 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore14 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore15 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore16 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore17 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore18 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore19 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore20 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore21 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore22 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore23 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore24 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore25 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore26 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore27 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore28 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore29 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore30 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore31 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore32 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore33 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore34 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore35 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore36 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore37 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");


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

        object[,] resultSet15;
        string[] header15;

        object[,] resultSet16;
        string[] header16;
        object[,] resultSet17;
        string[] header17;

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

        object[,] resultSet26;
        string[] header26;

        object[,] resultSet27;
        string[] header27;

        object[,] resultSet28;
        string[] header28;

        object[,] resultSet29;
        string[] header29;

        object[,] resultSet30;
        string[] header30;

        object[,] resultSet31;
        string[] header31;

        object[,] resultSet32;
        string[] header32;

        object[,] resultSet33;
        string[] header33;

             object[,] resultSet34;
        string[] header34;

        object[,] resultSet35;
        string[] header35;

        object[,] resultSet36;
        string[] header36;

        object[,] resultSet37;
        string[] header37;




        if (button)
        {
            DateTime currentTime = DateTime.Now;
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            // Calculate start and end times for the current day
            DateTime startTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 0, 0, 0);
            DateTime endTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 7, 59, 59).AddDays(1);

            string new123 = startTime.ToString("yyyy-MM-dd");
            string new12 = startTime.ToString("yyyy-MM");
            string new1 = startTime.ToString("dd");

            string query1 = $"SELECT SUM(Consumption) FROM DailyConsumptionAgg WHERE LocalDate = '" + new123 + " 00:00:00' AND Jace = '33KV' AND Meter = 'J1_INCOMER1'";
            string query2 = $"SELECT SUM(Consumption) FROM DailyConsumptionAgg WHERE LocalDate = '" + new123 + " 00:00:00' AND Jace = '33KV' AND Meter = 'J1_INCOMER2'";
            string query3 = $"SELECT Frequency FROM DailyConsumptionAgg WHERE LocalDate = '" + new123 + " 00:00:00' AND Jace = '33KV' AND Meter = 'J1_INCOMER1'";
            string query4 = $"SELECT Avg_PF FROM DailyConsumptionAgg WHERE LocalDate = '" + new123 + " 00:00:00' AND Jace = '33KV' AND Meter = 'J1_INCOMER1'";
            string query5 = $"SELECT SUM(Consumption) FROM DailyConsumptionAgg WHERE MonthYear = '" + new12 + "' AND Jace = '33KV' AND Meter = 'J1_INCOMER1'";
            string query6 = $"SELECT SUM(Consumption) FROM DailyConsumptionAgg WHERE MonthYear = '" + new12 + "' AND Jace = '33KV' AND Meter = 'J1_INCOMER2'";
            string query7 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '1' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query8 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '2' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query9 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '3' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query10 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '4' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query11 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '5' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query12 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '6' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query13 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '7' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query14 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '8' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query15 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '9' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query16 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '10' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query17 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '11' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query18 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '12' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query19 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '13' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query20 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '14' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query21 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '15' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query22 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '16' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query23 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '17' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query24 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '18' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query25 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '19' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query26 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '20' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query27 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '21' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query28 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '22' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query29 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '23' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query30 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '24' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query31 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '25' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query32 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '26' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query33 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '27' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query34 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '28' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query35 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '29' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query36 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '30' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            string query37 = $"SELECT Consumption FROM DailyJaceDataLogger WHERE Day = '31' AND MonthYear = '" + new12 + "' AND Jace = '33KV'";
            

















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
            myStore26.Query(query26, out header26, out resultSet26);
            myStore27.Query(query27, out header27, out resultSet27);
            myStore28.Query(query28, out header28, out resultSet28);
            myStore29.Query(query29, out header29, out resultSet29);

            myStore30.Query(query30, out header30, out resultSet30);
            myStore31.Query(query31, out header31, out resultSet31);
            myStore32.Query(query32, out header32, out resultSet32);
            myStore33.Query(query33, out header33, out resultSet33);
            myStore34.Query(query34, out header34, out resultSet34);
            myStore35.Query(query35, out header35, out resultSet35);
            myStore36.Query(query36, out header36, out resultSet36);
            myStore37.Query(query37, out header37, out resultSet37);



            if (resultSet1 != null && resultSet1.GetLength(0) > 0 && header1 != null && header1.Length > 0)
            {
                float.TryParse(resultSet1[0, 0]?.ToString(), out IN1Consumption);
            }

            if (resultSet2 != null && resultSet2.GetLength(0) > 0 && header2 != null && header2.Length > 0)
            {
                float.TryParse(resultSet2[0, 0]?.ToString(), out IN2Consumption);
            }
            // Process resultSet3 (Frequency)
            if (resultSet3 != null && resultSet3.GetLength(0) > 0 && header3 != null && header3.Length > 0)
            {
                float.TryParse(resultSet3[0, 0]?.ToString(), out averagefrequency);
            }

            // Process resultSet4 (Avg_PF)
            if (resultSet4 != null && resultSet4.GetLength(0) > 0 && header4 != null && header4.Length > 0)
            {
                float.TryParse(resultSet4[0, 0]?.ToString(), out avgpowerfactor);
            }

            if (resultSet5 != null && resultSet5.GetLength(0) > 0 && header5 != null && header5.Length > 0)
            {
                float.TryParse(resultSet5[0, 0]?.ToString(), out MIN1Consumption);
            }

            if (resultSet6 != null && resultSet6.GetLength(0) > 0 && header6 != null && header6.Length > 0)
            {
                float.TryParse(resultSet6[0, 0]?.ToString(), out MIN2Consumption);
            }

            if (resultSet7 != null && resultSet7.GetLength(0) > 0 && header7 != null && header7.Length > 0)
            {
                float.TryParse(resultSet7[0, 0]?.ToString(), out day1);
            }
            if (resultSet8 != null && resultSet8.GetLength(0) > 0 && header8 != null && header8.Length > 0)
            {
                float.TryParse(resultSet8[0, 0]?.ToString(), out day2);
            }


            if (resultSet9 != null && resultSet9.GetLength(0) > 0 && header9 != null && header9.Length > 0)
            {
                float.TryParse(resultSet9[0, 0]?.ToString(), out day3);
            }
            if (resultSet10 != null && resultSet10.GetLength(0) > 0 && header10 != null && header10.Length > 0)
            {
                float.TryParse(resultSet10[0, 0]?.ToString(), out day4);
            }

            if (resultSet11 != null && resultSet11.GetLength(0) > 0 && header11 != null && header11.Length > 0)
            {
                float.TryParse(resultSet11[0, 0]?.ToString(), out day5);
            }
            if (resultSet12 != null && resultSet12.GetLength(0) > 0 && header12 != null && header12.Length > 0)
            {
                float.TryParse(resultSet12[0, 0]?.ToString(), out day6);
            }

            if (resultSet13 != null && resultSet13.GetLength(0) > 0 && header13 != null && header13.Length > 0)
            {
                float.TryParse(resultSet13[0, 0]?.ToString(), out day7);
            }
            if (resultSet14 != null && resultSet14.GetLength(0) > 0 && header14 != null && header14.Length > 0)
            {
                float.TryParse(resultSet14[0, 0]?.ToString(), out day8);
            }

            if (resultSet15 != null && resultSet15.GetLength(0) > 0 && header15 != null && header15.Length > 0)
            {
                float.TryParse(resultSet15[0, 0]?.ToString(), out day9);
            }
            if (resultSet16 != null && resultSet16.GetLength(0) > 0 && header16 != null && header16.Length > 0)
            {
                float.TryParse(resultSet16[0, 0]?.ToString(), out day10);
            }

            if (resultSet17 != null && resultSet17.GetLength(0) > 0 && header17 != null && header17.Length > 0)
            {
                float.TryParse(resultSet17[0, 0]?.ToString(), out day11);
            }
            if (resultSet18 != null && resultSet18.GetLength(0) > 0 && header18 != null && header18.Length > 0)
            {
                float.TryParse(resultSet18[0, 0]?.ToString(), out day12);
            }

            if (resultSet19 != null && resultSet19.GetLength(0) > 0 && header19 != null && header19.Length > 0)
            {
                float.TryParse(resultSet19[0, 0]?.ToString(), out day13);
            }
            if (resultSet20 != null && resultSet20.GetLength(0) > 0 && header20 != null && header20.Length > 0)
            {
                float.TryParse(resultSet20[0, 0]?.ToString(), out day14);
            }

            if (resultSet21 != null && resultSet21.GetLength(0) > 0 && header21 != null && header21.Length > 0)
            {
                float.TryParse(resultSet21[0, 0]?.ToString(), out day15);
            }
            if (resultSet22 != null && resultSet22.GetLength(0) > 0 && header22 != null && header22.Length > 0)
            {
                float.TryParse(resultSet22[0, 0]?.ToString(), out day16);
            }

            if (resultSet23 != null && resultSet23.GetLength(0) > 0 && header23 != null && header23.Length > 0)
            {
                float.TryParse(resultSet23[0, 0]?.ToString(), out day17);
            }
            if (resultSet24 != null && resultSet24.GetLength(0) > 0 && header24 != null && header24.Length > 0)
            {
                float.TryParse(resultSet24[0, 0]?.ToString(), out day18);
            }

            if (resultSet25 != null && resultSet25.GetLength(0) > 0 && header25 != null && header25.Length > 0)
            {
                float.TryParse(resultSet25[0, 0]?.ToString(), out day19);
            }
            if (resultSet26 != null && resultSet26.GetLength(0) > 0 && header26 != null && header26.Length > 0)
            {
                float.TryParse(resultSet26[0, 0]?.ToString(), out day20);
            }

            if (resultSet27 != null && resultSet27.GetLength(0) > 0 && header27 != null && header27.Length > 0)
            {
                float.TryParse(resultSet27[0, 0]?.ToString(), out day21);
            }
            if (resultSet28 != null && resultSet28.GetLength(0) > 0 && header28 != null && header28.Length > 0)
            {
                float.TryParse(resultSet28[0, 0]?.ToString(), out day22);
            }

            if (resultSet29 != null && resultSet29.GetLength(0) > 0 && header29 != null && header29.Length > 0)
            {
                float.TryParse(resultSet29[0, 0]?.ToString(), out day23);
            }
            if (resultSet30 != null && resultSet30.GetLength(0) > 0 && header30 != null && header30.Length > 0)
            {
                float.TryParse(resultSet30[0, 0]?.ToString(), out day24);
            }

            if (resultSet31 != null && resultSet31.GetLength(0) > 0 && header31 != null && header31.Length > 0)
            {
                float.TryParse(resultSet31[0, 0]?.ToString(), out day25);
            }
            if (resultSet32 != null && resultSet32.GetLength(0) > 0 && header32 != null && header32.Length > 0)
            {
                float.TryParse(resultSet32[0, 0]?.ToString(), out day26);
            }

            if (resultSet33 != null && resultSet33.GetLength(0) > 0 && header33 != null && header33.Length > 0)
            {
                float.TryParse(resultSet33[0, 0]?.ToString(), out day27);
            }
            if (resultSet34 != null && resultSet34.GetLength(0) > 0 && header34 != null && header34.Length > 0)
            {
                float.TryParse(resultSet34[0, 0]?.ToString(), out day28);
            }

            if (resultSet35 != null && resultSet35.GetLength(0) > 0 && header35 != null && header35.Length > 0)
            {
                float.TryParse(resultSet35[0, 0]?.ToString(), out day29);
            }
            if (resultSet36 != null && resultSet36.GetLength(0) > 0 && header36 != null && header36.Length > 0)
            {
                float.TryParse(resultSet36[0, 0]?.ToString(), out day30);
            }

            if (resultSet37 != null && resultSet37.GetLength(0) > 0 && header37 != null && header37.Length > 0)
            {
                float.TryParse(resultSet37[0, 0]?.ToString(), out day31);
            }
























            // Calculate today consumption based on IN1Consumption and IN2Consumption
            todayconsumption = IN1Consumption + IN2Consumption;
            monthconsumption = MIN1Consumption + MIN2Consumption;

            // Calculate today consumption cost
            todayconsumptioncost = todayconsumption * 752;
            todayconsumptioncost = todayconsumptioncost / 100 + 1608.4f;
            monthconsumptioncost = monthconsumption * 752;
            monthconsumptioncost = monthconsumptioncost / 100 + 1608.4f;

            if (monthconsumption < 99)
            {
                guagemax = 100;
            } 
            else if (monthconsumption > 99 && monthconsumption < 999)
            {
                guagemax = 1000;
            }
            else if (monthconsumption > 999 && monthconsumption < 9999)
            {
                guagemax = 10000;
            }
            else if (monthconsumption > 9999 && monthconsumption < 99999)
            {
                guagemax = 100000;
            }
            else if (monthconsumption > 99999 && monthconsumption < 999999)
            {
                guagemax = 1000000;
            }
            else if (monthconsumption > 999999 && monthconsumption < 9999999)
            {
                guagemax = 1000000;
            }
            else if (monthconsumption > 9999999 && monthconsumption < 99999999)
            {
                guagemax = 100000000;
            }
                  
        }


        // Update variable values
        todayConsumptionVariable.Value = todayconsumption;
        todayConsumptionCostVariable.Value = todayconsumptioncost;
        averageFrequencyVariable.Value = averagefrequency;
        avgPowerFactorVariable.Value = avgpowerfactor;
        monthConsumptionVariable.Value = monthconsumption;  
        monthConsumptionCostVariable.Value =monthconsumptioncost;
        guageMaxVariable.Value = guagemax;
        day1Variable.Value = day1;
        day2Variable.Value = day2;
        day3Variable.Value = day3;
        day4Variable.Value = day4;
        day5Variable.Value = day5;
        day6Variable.Value = day6;
        day7Variable.Value = day7;
        day8Variable.Value = day8;
        day9Variable.Value = day9;
        day10Variable.Value = day10;
        day11Variable.Value = day11;
        day12Variable.Value = day12;
        day13Variable.Value = day13;
        day14Variable.Value = day14;
        day15Variable.Value = day15;
        day16Variable.Value = day16;
        day17Variable.Value = day17;
        day18Variable.Value = day18;
        day19Variable.Value = day19;
        day20Variable.Value = day20;
        day21Variable.Value = day21;
        day22Variable.Value = day22;
        day23Variable.Value = day23;
        day24Variable.Value = day24;
        day25Variable.Value = day25;
        day26Variable.Value = day26;
        day27Variable.Value = day27;
        day28Variable.Value = day28;
        day29Variable.Value = day29;
        day30Variable.Value = day30;
        day31Variable.Value = day31;

    }

}


