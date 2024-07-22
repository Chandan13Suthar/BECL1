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

public class RuntimeNetLogic2 : BaseNetLogic
{


    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var owner = (Comparision)LogicObject.Owner; 

        monthyearVariable = owner.MonthyearVariable;
        yearVariable = owner.YearVariable;
        //Utility
        jaceVariable = owner.JaceVariable;
        meterVariable = owner.MeterVariable;
        targetVariable = owner.TargetVariable;
        yearlowestVariable = owner.YearlowestVariable;
        monthlowestVariable = owner.MonthlowestVariable;
        averageVariable = owner.AverageVariable;
        consumptionVariable = owner.ConsumptionVariable;
        gbuttonVariable = owner.GBUTTONVariable;
        dateVariable = owner.DateVariable;
        date1Variable = owner.Date1Variable;
        // Stamping
        jacestampingVariable = owner.JaceStampingVariable;
        targetstampingVariable = owner.TargetStampingVariable;
        yearloweststampingVariable = owner.YearlowestStampingVariable;
        monthloweststampingVariable = owner.MonthlowestStampingVariable;
        averagestampingVariable = owner.AverageStampingVariable;
        consumptionstampingVariable = owner.ConsumptionStampingVariable;
        // TCF
        jacetcfVariable = owner.JaceTcfVariable;
        targettcfVariable = owner.TargetTcfVariable;
        yearlowesttcfVariable = owner.YearlowestTcfVariable;
        monthlowesttcfVariable = owner.MonthlowestTcfVariable;
        averagetcfVariable = owner.AverageTcfVariable;
        consumptiontcfVariable = owner.ConsumptionTcfVariable;

        // Bodyshop
        jacebodyshopVariable = owner.JaceBodyshopVariable;
        targetbodyshopVariable = owner.TargetBodyshopVariable;
        yearlowestbodyshopVariable = owner.YearlowestBodyshopVariable;
        monthlowestbodyshopVariable = owner.MonthlowestBodyshopVariable;
        averagebodyshopVariable = owner.AverageBodyshopVariable;
        consumptionbodyshopVariable = owner.ConsumptionBodyshopVariable;

        // Engineshop
        jaceengineshopVariable = owner.JaceEngineshopVariable;
        targetengineshopVariable = owner.TargetEngineshopVariable;
        yearlowestengineshopVariable = owner.YearlowestEngineshopVariable;
        monthlowestengineshopVariable = owner.MonthlowestEngineshopVariable;
        averageengineshopVariable = owner.AverageEngineshopVariable;
        consumptionengineshopVariable = owner.ConsumptionEngineshopVariable;

        // Paintshop
        jacepaintshopVariable = owner.JacePaintshopVariable;
        targetpaintshopVariable = owner.TargetPaintshopVariable;
        yearlowestpaintshopVariable = owner.YearlowestPaintshopVariable;
        monthlowestpaintshopVariable = owner.MonthlowestPaintshopVariable;
        averagepaintshopVariable = owner.AveragePaintshopVariable;
        consumptionpaintshopVariable = owner.ConsumptionPaintshopVariable;

        // Spp
        jacesppVariable = owner.JaceSppVariable;
        targetsppVariable = owner.TargetSppVariable;
        yearlowestsppVariable = owner.YearlowestSppVariable;
        monthlowestsppVariable = owner.MonthlowestSppVariable;
        averagesppVariable = owner.AverageSppVariable;
        consumptionsppVariable = owner.ConsumptionSppVariable;

        // Spare
        jacespareVariable = owner.JaceSpareVariable;
        targetspareVariable = owner.TargetSpareVariable;
        yearlowestspareVariable = owner.YearlowestSpareVariable;
        monthlowestspareVariable = owner.MonthlowestSpareVariable;
        averagespareVariable = owner.AverageSpareVariable;
        consumptionspareVariable = owner.ConsumptionSpareVariable;


        // 33KV
        jace33kvVariable = owner.Jace33KVVariable;
        target33kvVariable = owner.Target33KVVariable;
        yearlowest33kvVariable = owner.Yearlowest33KVVariable;
        monthlowest33kvVariable = owner.Monthlowest33KVVariable;
        average33kvVariable = owner.Average33KVVariable;
        consumption33kvVariable = owner.Consumption33KVVariable;

        ////Calculation Related/////////
        utilitypercentageVariable = owner.UtilitypercentageVariable;
        stampingpercentageVariable = owner.StampingpercentageVariable;
        tcfpercentageVariable = owner.TcfpercentageVariable;
        bodyshoppercentageVariable = owner.BodyshoppercentageVariable;
        engineshoppercentageVariable = owner.EngineshoppercentageVariable;
        paintshoppercentageVariable = owner.PaintshoppercentageVariable;
        sparepercentageVariable = owner.SparepercentageVariable;
        spppercentageVariable = owner.SpppercentageVariable;


        periodicTask = new PeriodicTask(IncrementDecrementTask, 1000, LogicObject);
        periodicTask.Start();


    }
    ////////////////////////////////*********************************************///////////////////////////////////////////////////////////////////////////
    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        periodicTask.Dispose();
        periodicTask = null;
    }

    public void IncrementDecrementTask()
    {
        DateTime date = dateVariable.Value;
        String date1 = date1Variable.Value;
        String monthyear = monthyearVariable.Value;
        String year = yearVariable.Value;

        //For Utility
        int jace = jaceVariable.Value;
        int meter = meterVariable.Value;
        int target = targetVariable.Value;
        int yearlowest = yearlowestVariable.Value;
        int monthlowest = monthlowestVariable.Value;
        int average = averageVariable.Value;
        int consumption = consumptionVariable.Value;
        bool gbutton = gbuttonVariable.Value;

        //For Stamping
        int jacestamping = jacestampingVariable.Value;
        int targetstamping = targetstampingVariable.Value;
        int yearloweststamping = yearloweststampingVariable.Value;
        int monthloweststamping = monthloweststampingVariable.Value;
        int averagestamping = averagestampingVariable.Value;
        float consumptionstamping = consumptionstampingVariable.Value;
        // For TCF
        int jacetcf = jacetcfVariable.Value;
        int targettcf = targettcfVariable.Value;
        int yearlowesttcf = yearlowesttcfVariable.Value;
        int monthlowesttcf = monthlowesttcfVariable.Value;
        int averagetcf = averagetcfVariable.Value;
        float consumptiontcf = consumptiontcfVariable.Value;

        // For BodyShop
        int jacebodyshop = jacebodyshopVariable.Value;
        int targetbodyshop = targetbodyshopVariable.Value;
        int yearlowestbodyshop = yearlowestbodyshopVariable.Value;
        int monthlowestbodyshop = monthlowestbodyshopVariable.Value;
        int averagebodyshop = averagebodyshopVariable.Value;
        float consumptionbodyshop = consumptionbodyshopVariable.Value;

        // For EngineShop
        int jaceengineshop = jaceengineshopVariable.Value;
        int targetengineshop = targetengineshopVariable.Value;
        int yearlowestengineshop = yearlowestengineshopVariable.Value;
        int monthlowestengineshop = monthlowestengineshopVariable.Value;
        int averageengineshop = averageengineshopVariable.Value;
        float consumptionengineshop = consumptionengineshopVariable.Value;

        // For Paintshop
        int jacepaintshop = jacepaintshopVariable.Value;
        int targetpaintshop = targetpaintshopVariable.Value;
        int yearlowestpaintshop = monthlowestpaintshopVariable.Value;
        int monthlowestpaintshop = monthlowestpaintshopVariable.Value;
        int averagepaintshop = averagepaintshopVariable.Value;
        float consumptionpaintshop = consumptionpaintshopVariable.Value;


        // For Spp
        int jacespp = jacesppVariable.Value;
        int targetspp = targetsppVariable.Value;
        int yearlowestspp = yearlowestsppVariable.Value;
        int monthlowestspp = monthlowestsppVariable.Value;
        int averagespp = averagesppVariable.Value;
        float consumptionspp = consumptionsppVariable.Value;


        // For Spare
        int jacespare = jacespareVariable.Value;
        int targetspare = targetspareVariable.Value;
        int yearlowestspare = yearlowestspareVariable.Value;
        int monthlowestspare = monthlowestspareVariable.Value;
        int averagespare = averagespareVariable.Value;
        float consumptionspare = consumptionspareVariable.Value;

        // For 33Kv
        int jace33kv = jace33kvVariable.Value;
        int target33kv = target33kvVariable.Value;
        int yearlowest33kv = yearlowest33kvVariable.Value;
        int monthlowest33kv = monthlowest33kvVariable.Value;
        int average33kv = average33kvVariable.Value;
        float consumption33kv = consumption33kvVariable.Value;

        // For Calculation
        float utilitypercentage = utilitypercentageVariable.Value;
        float stampingpercentage = stampingpercentageVariable.Value;
        float tcfpercentage = tcfpercentageVariable.Value;
        float bodyshoppercentage = bodyshoppercentageVariable.Value;
        float engineshoppercentage = engineshoppercentageVariable.Value;
        float paintshoppercentage = paintshoppercentageVariable.Value;
        float sparepercentage = sparepercentageVariable.Value;
        float spppercentage = spppercentageVariable.Value;

        ////////////////////////////////*********************************************///////////////////////////////////////////////////////////////////////////

        var project = FTOptix.HMIProject.Project.Current;
        // For Utility
        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Target
        var myStore2 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Yearlowest
        var myStore3 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//monthlowest
        var myStore4 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//average
        var myStore5 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//consumption
        // For Stamping
        var myStore6 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Target
        var myStore7 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Yearlowest
        var myStore8 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Monthlowest
        var myStore9 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//average
        var myStore10 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//consumption

        // For TCF
        var myStore11 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Target
        var myStore12 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Yearlowest
        var myStore13 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Monthlowest
        var myStore14 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//average
        var myStore15 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//consumption

        // For Bodyshop
        var myStore16 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Target
        var myStore17 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Yearlowest
        var myStore18 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Monthlowest
        var myStore19 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//average
        var myStore20 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//consumption


        // For Engineshop
        var myStore21 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Target
        var myStore22 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Yearlowest
        var myStore23 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Monthlowest
        var myStore24 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//average
        var myStore25 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//consumption

        // For Paintshop
        var myStore26 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Target
        var myStore27 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Yearlowest
        var myStore28 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Monthlowest
        var myStore29 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//average
        var myStore30 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//consumption

        // For Spp
        var myStore31 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Target
        var myStore32 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Yearlowest
        var myStore33 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Monthlowest
        var myStore34 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//average
        var myStore35 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//consumption

        // For Spare
        var myStore36 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Target
        var myStore37 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Yearlowest
        var myStore38 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Monthlowest
        var myStore39 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//average
        var myStore40 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//consumption


        // For 33KV
        var myStore41 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Target
        var myStore42 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Yearlowest
        var myStore43 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Monthlowest
        var myStore44 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//average
        var myStore45 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//consumption

        ////////////////////////////////*********************************************///////////////////////////////////////////////////////////////////////////
        // For Utility
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

        // For Stamping
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

        // For TCF
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


        // For Bodyshop
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

        // For Engineshop
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

        // For Paintshop
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

        // For Spp
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

        // For Spare
        object[,] resultSet36;
        string[] header36;
        object[,] resultSet37;
        string[] header37;
        object[,] resultSet38;
        string[] header38;
        object[,] resultSet39;
        string[] header39;
        object[,] resultSet40;
        string[] header40;


        // For 33KV
        object[,] resultSet41;
        string[] header41;
        object[,] resultSet42;
        string[] header42;
        object[,] resultSet43;
        string[] header43;
        object[,] resultSet44;
        string[] header44;
        object[,] resultSet45;
        string[] header45;


        ////////////////////////////////*********************************************///////////////////////////////////////////////////////////////////////////
        if (gbutton == true)
        {

            DateTime currentTime = DateTime.Now;
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            int currentHour = currentTime.Hour;

            // Calculate start and end times for the current day
            DateTime startTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 8, 0, 0);
            DateTime endTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 7, 59, 59).AddDays(1);

            if (currentHour < 8)
            {
                startTime = startTime.AddDays(-1);
                endTime = endTime.AddDays(-1);
            }

            string st = startTime.ToString("yyyy-MMM-dd");
            string et = endTime.ToString("yyyy-MMM-dd");
            string new123 = date.ToString("yyyy-MM-dd");
            monthyear = date.ToString("yyyy-MM");
            year = date.ToString("yyyy");
            string date2 = date.ToString("dd-MM-yyyy");



            // For Utility
            string query1 = $"SELECT Target FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'UTILITY' ";
            string query2 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE Year = '" + year + "' AND Jace = 'UTILITY' ";
            
            string query3 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'UTILITY' ";
            
            string query4 = $"SELECT AVG(Consumption) FROM ConsumptionDistribution WHERE MonthYear  = '" + monthyear + "' AND Jace = 'UTILITY' ";
            string query5 = $"SELECT MAX(Consumption) FROM ConsumptionDistribution WHERE Date = '" + date2 + "'  AND Jace = 'UTILITY' ";


            // For Stamping
            string query6 = $"SELECT Target FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'STAMPING' ";
            string query7 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE Year = '" + year + "' AND Jace = 'STAMPING' ";
            string query8 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'STAMPING' ";
            string query9 = $"SELECT AVG(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'STAMPING' ";
            string query10 = $"SELECT Consumption FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'STAMPING' ";


            // For TCF
            string query11 = $"SELECT Target FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'TCF' ";
            string query12 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE Year = '" + year + "' AND Jace = 'TCF' ";
            string query13 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'TCF' ";
            string query14 = $"SELECT AVG(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'TCF' ";
            string query15 = $"SELECT Consumption FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'TCf' ";


            // For Bodyshop
            string query16 = $"SELECT Target FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'BODYSHOP' ";
            string query17 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE Year = '" + year + "' AND Jace = 'BODYSHOP' ";
            string query18 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'BODYSHOP' ";
            string query19 = $"SELECT AVG(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'BODYSHOP' ";
            string query20 = $"SELECT Consumption FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'BODYSHOP' ";


            // For Engineshop
            string query21 = $"SELECT Target FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'UTILITY' ";
            string query22 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE Year = '" + year + "' AND Jace = 'Utility' ";
            string query23 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'Utility' ";
            string query24 = $"SELECT AVG(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'Utility' ";
            string query25 = $"SELECT Consumption FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'Utility' ";


            // For Paintshop
            string query26 = $"SELECT Target FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'PAINTSHOP' ";
            string query27 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE Year = '" + year + "' AND Jace = 'PAINTSHOP' ";
            string query28 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'PAINTSHOP' ";
            string query29 = $"SELECT AVG(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'PAINTSHOP' ";
            string query30 = $"SELECT Consumption FROM ConsumptionDistribution WHERE LocalTimestamp  BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'PAINTSHOP' ";


            // For Spp
            string query31 = $"SELECT Target FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + et + " 07:59:59' AND Jace = 'SPP' ";
            string query32 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE Year = '" + year + "' AND Jace = 'SPP' ";
            string query33 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'SPP' ";
            string query34 = $"SELECT AVG(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" +  monthyear + "' AND Jace = 'SPP' ";
            string query35 = $"SELECT Consumption FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'SPP' ";
            // For Spare
            string query36 = $"SELECT Target FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + st + " 07:59:59' AND Jace = 'Utility' ";
            string query37 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE Year = '" +   year + "' AND Jace = 'Utility' ";
            string query38 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'Utility' ";
            string query39 = $"SELECT AVG(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = 'Utility' ";
            string query40 = $"SELECT Consumption FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = 'Utility' ";

            // For 33KV
            string query41 = $"SELECT Target FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + st + " 08:00:00' AND '" + st + " 07:59:59' AND Jace = '33KV' ";
            string query42 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE Year = '" + year + "' AND Jace = '33KV' ";
            string query43 = $"SELECT MIN(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = '33KV' ";
            string query44 = $"SELECT AVG(Consumption) FROM ConsumptionDistribution WHERE MonthYear = '" + monthyear + "' AND Jace = '33KV' ";
            string query45 = $"SELECT Consumption FROM ConsumptionDistribution WHERE LocalTimestamp BETWEEN '" + new123 + " 08:00:00' AND '" + new123 + " 07:59:59' AND Jace = '33KV' ";

            ////////////////////////////////*********************************************/////////////////////////////////////////////////////////////////////////// 
            // For Utility
            myStore1.Query(query1, out header1, out resultSet1);
            myStore2.Query(query2, out header2, out resultSet2);
            myStore3.Query(query3, out header3, out resultSet3);
            myStore4.Query(query4, out header4, out resultSet4);
            myStore5.Query(query5, out header5, out resultSet5);
            // For Stamping
            myStore6.Query(query6, out header6, out resultSet6);
            myStore7.Query(query7, out header7, out resultSet7);
            myStore8.Query(query8, out header8, out resultSet8);
            myStore9.Query(query9, out header9, out resultSet9);
            myStore10.Query(query10, out header10, out resultSet10);

            // For Tcf
            myStore11.Query(query11, out header11, out resultSet11);
            myStore12.Query(query12, out header12, out resultSet12);
            myStore13.Query(query13, out header13, out resultSet13);
            myStore14.Query(query14, out header14, out resultSet14);
            myStore15.Query(query15, out header15, out resultSet15);


            // For Bodyshop
            myStore16.Query(query16, out header16, out resultSet16);
            myStore17.Query(query17, out header17, out resultSet17);
            myStore18.Query(query18, out header18, out resultSet18);
            myStore19.Query(query19, out header19, out resultSet19);
            myStore20.Query(query20, out header20, out resultSet20);



            // For Engineshop
            myStore21.Query(query21, out header21, out resultSet21);
            myStore22.Query(query22, out header22, out resultSet22);
            myStore23.Query(query23, out header23, out resultSet23);
            myStore24.Query(query24, out header24, out resultSet24);
            myStore25.Query(query25, out header25, out resultSet25);

            // For Paintshop
            myStore26.Query(query26, out header26, out resultSet26);
            myStore27.Query(query27, out header27, out resultSet27);
            myStore28.Query(query28, out header28, out resultSet28);
            myStore29.Query(query29, out header29, out resultSet29);
            myStore30.Query(query30, out header30, out resultSet30);


            // For Spp
            myStore31.Query(query31, out header31, out resultSet31);
            myStore32.Query(query32, out header32, out resultSet32);
            myStore33.Query(query33, out header33, out resultSet33);
            myStore34.Query(query34, out header34, out resultSet34);
            myStore35.Query(query35, out header35, out resultSet35);


            // For Spare
            myStore36.Query(query36, out header36, out resultSet36);
            myStore37.Query(query37, out header37, out resultSet37);
            myStore38.Query(query38, out header38, out resultSet38);
            myStore39.Query(query39, out header39, out resultSet39);
            myStore40.Query(query40, out header40, out resultSet40);



            // For 33KV
            myStore41.Query(query41, out header41, out resultSet41);
            myStore42.Query(query42, out header42, out resultSet42);
            myStore43.Query(query43, out header43, out resultSet43);
            myStore44.Query(query44, out header44, out resultSet44);
            myStore45.Query(query45, out header45, out resultSet45);


            ////////////////////////////////*********************************************///////////////////////////////////////////////////////////////////////////

            //For Utility
            var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount1 = header1 != null ? header1.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                var Target = column1;
                target = Target;
            }



            var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
            var columnCount2 = header2 != null ? header2.Length : 0;
            if (rowCount2 > 0 && columnCount2 > 0)
            {
                var column1 = Convert.ToInt32(resultSet2[0, 0]);
                var Yearlowest = column1;
                yearlowest = Yearlowest;
            }


            var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
            var columnCount3 = header3 != null ? header3.Length : 0;
            if (rowCount3 > 0 && columnCount3 > 0)
            {
                var column1 = Convert.ToInt32(resultSet3[0, 0]);
                var Monthlowest = column1;
                monthlowest = Monthlowest;
            }


            var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
            var columnCount4 = header4 != null ? header4.Length : 0;
            if (rowCount4 > 0 && columnCount4 > 0)
            {
                var column1 = Convert.ToInt32(resultSet4[0, 0]);
                var Average = column1;
                average = Average;
            }

            var rowCount5 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
            var columnCount5 = header5 != null ? header5.Length : 0;
            if (rowCount5 > 0 && columnCount5 > 0)
            {
                var column1 = Convert.ToInt32(resultSet5[0, 0]);
                var Consumption = column1;
                consumption = Consumption;
            }


            // For Stamping
            var rowCount6 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
            var columnCount6 = header6 != null ? header6.Length : 0;
            if (rowCount6 > 0 && columnCount6 > 0)
            {
                var column1 = Convert.ToInt32(resultSet6[0, 0]);
                var TargetStamping = column1;
                targetstamping = TargetStamping;
            }



            var rowCount7 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
            var columnCount7 = header7 != null ? header7.Length : 0;
            if (rowCount7 > 0 && columnCount7 > 0)
            {
                var column1 = Convert.ToInt32(resultSet7[0, 0]);
                var YearlowestStamping = column1;
                yearloweststamping = YearlowestStamping;
            }


            var rowCount8 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
            var columnCount8 = header8 != null ? header8.Length : 0;
            if (rowCount8 > 0 && columnCount8 > 0)
            {
                var column1 = Convert.ToInt32(resultSet8[0, 0]);
                var MonthlowestStamping = column1;
                monthloweststamping = MonthlowestStamping;
            }


            var rowCount9 = resultSet9 != null ? resultSet9.GetLength(0) : 0;
            var columnCount9 = header9 != null ? header9.Length : 0;
            if (rowCount9 > 0 && columnCount9 > 0)
            {
                var column1 = Convert.ToInt32(resultSet9[0, 0]);
                var AverageStamping = column1;
                averagestamping = AverageStamping;
            }

            var rowCount10 = resultSet10 != null ? resultSet10.GetLength(0) : 0;
            var columnCount10 = header10 != null ? header10.Length : 0;
            if (rowCount10 > 0 && columnCount10 > 0)
            {
                var column1 = Convert.ToInt32(resultSet10[0, 0]);
                var ConsumptionStamping = column1;
                consumptionstamping = ConsumptionStamping;
            }


            // For Tcf
            var rowCount11 = resultSet11 != null ? resultSet11.GetLength(0) : 0;
            var columnCount11 = header11 != null ? header11.Length : 0;
            if (rowCount11 > 0 && columnCount11 > 0)
            {
                var column1 = Convert.ToInt32(resultSet11[0, 0]);
                var TargetTcf = column1;
                targetstamping = TargetTcf;
            }



            var rowCount12 = resultSet12 != null ? resultSet12.GetLength(0) : 0;
            var columnCount12 = header12 != null ? header12.Length : 0;
            if (rowCount12 > 0 && columnCount12 > 0)
            {
                var column1 = Convert.ToInt32(resultSet12[0, 0]);
                var YearlowestTcf = column1;
                yearlowesttcf = YearlowestTcf;
            }


            var rowCount13 = resultSet13 != null ? resultSet13.GetLength(0) : 0;
            var columnCount13 = header13 != null ? header13.Length : 0;
            if (rowCount13 > 0 && columnCount13 > 0)
            {
                var column1 = Convert.ToInt32(resultSet13[0, 0]);
                var MonthlowestTcf = column1;
                monthlowesttcf = MonthlowestTcf;
            }


            var rowCount14 = resultSet14 != null ? resultSet14.GetLength(0) : 0;
            var columnCount14 = header14 != null ? header14.Length : 0;
            if (rowCount14 > 0 && columnCount14 > 0)
            {
                var column1 = Convert.ToInt32(resultSet14[0, 0]);
                var AverageTcf = column1;
                averagetcf = AverageTcf;
            }

            var rowCount15 = resultSet15 != null ? resultSet15.GetLength(0) : 0;
            var columnCount15 = header15 != null ? header15.Length : 0;
            if (rowCount15 > 0 && columnCount15 > 0)
            {
                var column1 = Convert.ToInt32(resultSet15[0, 0]);
                var ConsumptionTcf = column1;
                consumptiontcf = ConsumptionTcf;
            }

            // For Bodyshop
            var rowCount16 = resultSet16 != null ? resultSet16.GetLength(0) : 0;
            var columnCount16 = header16 != null ? header16.Length : 0;
            if (rowCount16 > 0 && columnCount16 > 0)
            {
                var column1 = Convert.ToInt32(resultSet16[0, 0]);
                var TargetBodyshop = column1;
                targetbodyshop = TargetBodyshop;
            }



            var rowCount17 = resultSet17 != null ? resultSet17.GetLength(0) : 0;
            var columnCount17 = header17 != null ? header17.Length : 0;
            if (rowCount17 > 0 && columnCount17 > 0)
            {
                var column1 = Convert.ToInt32(resultSet17[0, 0]);
                var YearlowestBodyshop = column1;
                yearlowestbodyshop = YearlowestBodyshop;
            }


            var rowCount18 = resultSet18 != null ? resultSet18.GetLength(0) : 0;
            var columnCount18 = header18 != null ? header18.Length : 0;
            if (rowCount18 > 0 && columnCount18 > 0)
            {
                var column1 = Convert.ToInt32(resultSet18[0, 0]);
                var MonthlowestBodyshop = column1;
                monthlowestbodyshop = MonthlowestBodyshop;
            }


            var rowCount19 = resultSet19 != null ? resultSet19.GetLength(0) : 0;
            var columnCount19 = header19 != null ? header19.Length : 0;
            if (rowCount19 > 0 && columnCount19 > 0)
            {
                var column1 = Convert.ToInt32(resultSet19[0, 0]);
                var AverageBodyshop = column1;
                averagebodyshop = AverageBodyshop;
            }

            var rowCount20 = resultSet20 != null ? resultSet20.GetLength(0) : 0;
            var columnCount20 = header20 != null ? header20.Length : 0;
            if (rowCount20 > 0 && columnCount20 > 0)
            {
                var column1 = Convert.ToInt32(resultSet20[0, 0]);
                var ConsumptionBodyshop = column1;
                consumptionbodyshop = ConsumptionBodyshop;
            }


            // For Engineshop
            var rowCount21 = resultSet21 != null ? resultSet21.GetLength(0) : 0;
            var columnCount21 = header21 != null ? header21.Length : 0;
            if (rowCount21 > 0 && columnCount21 > 0)
            {
                var column1 = Convert.ToInt32(resultSet21[0, 0]);
                var TargetEngineshop = column1;
                targetengineshop = TargetEngineshop;
            }



            var rowCount22 = resultSet22 != null ? resultSet22.GetLength(0) : 0;
            var columnCount22 = header22 != null ? header22.Length : 0;
            if (rowCount22 > 0 && columnCount22 > 0)
            {
                var column1 = Convert.ToInt32(resultSet22[0, 0]);
                var YearlowestEngineshop = column1;
                yearlowestengineshop = YearlowestEngineshop;
            }


            var rowCount23 = resultSet23 != null ? resultSet23.GetLength(0) : 0;
            var columnCount23 = header23 != null ? header23.Length : 0;
            if (rowCount23 > 0 && columnCount23 > 0)
            {
                var column1 = Convert.ToInt32(resultSet23[0, 0]);
                var MonthlowestEngineshop = column1;
                monthlowestengineshop = MonthlowestEngineshop;
            }


            var rowCount24 = resultSet24 != null ? resultSet24.GetLength(0) : 0;
            var columnCount24 = header24 != null ? header24.Length : 0;
            if (rowCount24 > 0 && columnCount24 > 0)
            {
                var column1 = Convert.ToInt32(resultSet24[0, 0]);
                var AverageEngineshop = column1;
                averageengineshop = AverageEngineshop;
            }

            var rowCount25 = resultSet25 != null ? resultSet25.GetLength(0) : 0;
            var columnCount25 = header25 != null ? header25.Length : 0;
            if (rowCount25 > 0 && columnCount25 > 0)
            {
                var column1 = Convert.ToInt32(resultSet25[0, 0]);
                var ConsumptionEngineshop = column1;
                consumptionengineshop = ConsumptionEngineshop;
            }



            // For Paintshop
            var rowCount26 = resultSet26 != null ? resultSet26.GetLength(0) : 0;
            var columnCount26 = header26 != null ? header26.Length : 0;
            if (rowCount26 > 0 && columnCount26 > 0)
            {
                var column1 = Convert.ToInt32(resultSet26[0, 0]);
                var TargetPaintshop = column1;
                targetpaintshop = TargetPaintshop;
            }



            var rowCount27 = resultSet27 != null ? resultSet27.GetLength(0) : 0;
            var columnCount27 = header27 != null ? header27.Length : 0;
            if (rowCount27 > 0 && columnCount27 > 0)
            {
                var column1 = Convert.ToInt32(resultSet27[0, 0]);
                var YearlowestPaintshop = column1;
                yearlowestpaintshop = YearlowestPaintshop;
            }


            var rowCount28 = resultSet28 != null ? resultSet28.GetLength(0) : 0;
            var columnCount28 = header28 != null ? header28.Length : 0;
            if (rowCount28 > 0 && columnCount28 > 0)
            {
                var column1 = Convert.ToInt32(resultSet28[0, 0]);
                var MonthlowestPaintshop = column1;
                monthlowestpaintshop = MonthlowestPaintshop;
            }


            var rowCount29 = resultSet29 != null ? resultSet29.GetLength(0) : 0;
            var columnCount29 = header29 != null ? header29.Length : 0;
            if (rowCount29 > 0 && columnCount29 > 0)
            {
                var column1 = Convert.ToInt32(resultSet29[0, 0]);
                var AveragePaintshop = column1;
                averagepaintshop = AveragePaintshop;
            }

            var rowCount30 = resultSet30 != null ? resultSet30.GetLength(0) : 0;
            var columnCount30 = header30 != null ? header30.Length : 0;
            if (rowCount30 > 0 && columnCount30 > 0)
            {
                var column1 = Convert.ToInt32(resultSet30[0, 0]);
                var ConsumptionPaintshop = column1;
                consumptionpaintshop = ConsumptionPaintshop;
            }



            // For Spp
            var rowCount31 = resultSet31 != null ? resultSet31.GetLength(0) : 0;
            var columnCount31 = header31 != null ? header31.Length : 0;
            if (rowCount31 > 0 && columnCount31 > 0)
            {
                var column1 = Convert.ToInt32(resultSet31[0, 0]);
                var TargetSpp = column1;
                targetspp = TargetSpp;
            }



            var rowCount32 = resultSet32 != null ? resultSet32.GetLength(0) : 0;
            var columnCount32 = header32 != null ? header32.Length : 0;
            if (rowCount32 > 0 && columnCount32 > 0)
            {
                var column1 = Convert.ToInt32(resultSet32[0, 0]);
                var YearlowestSpp = column1;
                yearlowestspp = YearlowestSpp;
            }


            var rowCount33 = resultSet33 != null ? resultSet33.GetLength(0) : 0;
            var columnCount33 = header33 != null ? header33.Length : 0;
            if (rowCount33 > 0 && columnCount33 > 0)
            {
                var column1 = Convert.ToInt32(resultSet33[0, 0]);
                var MonthlowestSpp = column1;
                monthlowestspp = MonthlowestSpp;
            }


            var rowCount34 = resultSet34 != null ? resultSet34.GetLength(0) : 0;
            var columnCount34 = header34 != null ? header34.Length : 0;
            if (rowCount34 > 0 && columnCount34 > 0)
            {
                var column1 = Convert.ToInt32(resultSet34[0, 0]);
                var AverageSpp = column1;
                averagespp = AverageSpp;
            }

            var rowCount35 = resultSet35 != null ? resultSet35.GetLength(0) : 0;
            var columnCount35 = header35 != null ? header35.Length : 0;
            if (rowCount35 > 0 && columnCount35 > 0)
            {
                var column1 = Convert.ToInt32(resultSet35[0, 0]);
                var ConsumptionSpp = column1;
                consumptionspp = ConsumptionSpp;
            }



            // For Spare
            var rowCount36 = resultSet36 != null ? resultSet36.GetLength(0) : 0;
            var columnCount36 = header36 != null ? header36.Length : 0;
            if (rowCount36 > 0 && columnCount36 > 0)
            {
                var column1 = Convert.ToInt32(resultSet36[0, 0]);
                var TargetSpare = column1;
                targetspare = TargetSpare;
            }



            var rowCount37 = resultSet37 != null ? resultSet37.GetLength(0) : 0;
            var columnCount37 = header37 != null ? header37.Length : 0;
            if (rowCount37 > 0 && columnCount37 > 0)
            {
                var column1 = Convert.ToInt32(resultSet37[0, 0]);
                var YearlowestSpare = column1;
                yearlowestspare = YearlowestSpare;
            }


            var rowCount38 = resultSet38 != null ? resultSet38.GetLength(0) : 0;
            var columnCount38 = header38 != null ? header38.Length : 0;
            if (rowCount38 > 0 && columnCount38 > 0)
            {
                var column1 = Convert.ToInt32(resultSet38[0, 0]);
                var MonthlowestSpare = column1;
                monthlowestspare = MonthlowestSpare;
            }


            var rowCount39 = resultSet39 != null ? resultSet39.GetLength(0) : 0;
            var columnCount39 = header39 != null ? header39.Length : 0;
            if (rowCount39 > 0 && columnCount39 > 0)
            {
                var column1 = Convert.ToInt32(resultSet39[0, 0]);
                var AverageSpare = column1;
                averagespare = AverageSpare;
            }

            var rowCount40 = resultSet40 != null ? resultSet40.GetLength(0) : 0;
            var columnCount40 = header40 != null ? header40.Length : 0;
            if (rowCount40 > 0 && columnCount40 > 0)
            {
                var column1 = Convert.ToInt32(resultSet40[0, 0]);
                var ConsumptionSpare = column1;
                consumptionspare = ConsumptionSpare;
            }

            // For 33KV
            var rowCount41 = resultSet41 != null ? resultSet41.GetLength(0) : 0;
            var columnCount41 = header41 != null ? header41.Length : 0;
            if (rowCount41 > 0 && columnCount41 > 0)
            {
                var column1 = Convert.ToInt32(resultSet41[0, 0]);
                var Target33KV = column1;
                target33kv = Target33KV;
            }



            var rowCount42 = resultSet42 != null ? resultSet42.GetLength(0) : 0;
            var columnCount42 = header42 != null ? header42.Length : 0;
            if (rowCount42 > 0 && columnCount42 > 0)
            {
                var column1 = Convert.ToInt32(resultSet42[0, 0]);
                var Yearlowest33KV = column1;
                yearlowest33kv = Yearlowest33KV;
            }


            var rowCount43 = resultSet43 != null ? resultSet43.GetLength(0) : 0;
            var columnCount43 = header43 != null ? header43.Length : 0;
            if (rowCount43 > 0 && columnCount43 > 0)
            {
                var column1 = Convert.ToInt32(resultSet43[0, 0]);
                var Monthlowest33KV = column1;
                monthlowest33kv = Monthlowest33KV;
            }


            var rowCount44 = resultSet44 != null ? resultSet44.GetLength(0) : 0;
            var columnCount44 = header44 != null ? header44.Length : 0;
            if (rowCount44 > 0 && columnCount44 > 0)
            {
                var column1 = Convert.ToInt32(resultSet44[0, 0]);
                var Average33KV = column1;
                average33kv = Average33KV;
            }

            var rowCount45 = resultSet45 != null ? resultSet45.GetLength(0) : 0;
            var columnCount45 = header45 != null ? header45.Length : 0;
            if (rowCount45 > 0 && columnCount45 > 0)
            {
                var column1 = Convert.ToInt32(resultSet45[0, 0]);
                var Consumption33KV = column1;
                consumption33kv = Consumption33KV;
            }


            ////////////////Calculation for Percentage/////////////////////////////
            float utilityP = (consumption * 100) / consumption33kv;
            float stampingP = (consumptionstamping * 100) / consumption33kv;
            float bodyshopP = (consumptionbodyshop * 100) / consumption33kv;
            float paintshopP = (consumptionpaintshop * 100) / consumption33kv;
            float sppP = (consumptionspp * 100) / consumption33kv;
            float spareP = (consumptionspare * 100) / consumption33kv;
            float tcfP = (consumptiontcf * 100) / consumption33kv;
            float engineP = (consumptionengineshop * 100) / consumption33kv;

            date1 = date2;
            utilitypercentage = utilityP;
            stampingpercentage = stampingP;
            tcfpercentage = tcfP;
            bodyshoppercentage = bodyshopP ;
            engineshoppercentage = engineP;
            paintshoppercentage = paintshopP;
            sparepercentage = spareP;
            spppercentage = sppP;



            /////////////////////////////////////////////////////*********************************************///////////////////////////////////////////////////////////////////////////
            monthyearVariable.Value = monthyear;
            yearVariable.Value = year;
            date1Variable.Value = date1;
            // For Utility
            targetVariable.Value = target;
            yearlowestVariable.Value = yearlowest;
            monthlowestVariable.Value = monthlowest;
            averageVariable.Value = average;
            consumptionVariable.Value = consumption;

            // For Stamping
            targetstampingVariable.Value = targetstamping;
            yearloweststampingVariable.Value = yearloweststamping;
            monthloweststampingVariable.Value = monthloweststamping;
            averagestampingVariable.Value = averagestamping;
            consumptionstampingVariable.Value = consumptionstamping;

            // For TCF
            targettcfVariable.Value = targettcf;
            yearlowesttcfVariable.Value = yearlowesttcf;
            monthlowesttcfVariable.Value = monthlowesttcf;
            averagetcfVariable.Value = averagetcf;
            consumptiontcfVariable.Value = consumptiontcf;

            // For Bodyshop
            targetbodyshopVariable.Value = targetbodyshop;
            yearlowestbodyshopVariable.Value = yearlowestbodyshop;
            monthlowestbodyshopVariable.Value = monthlowestbodyshop;
            averagebodyshopVariable.Value = averagebodyshop;
            consumptionbodyshopVariable.Value = consumptionbodyshop;


            // For Engineshop
            targetengineshopVariable.Value = targetengineshop;
            yearlowestengineshopVariable.Value = yearlowestengineshop;
            monthlowestengineshopVariable.Value = monthlowestengineshop;
            averageengineshopVariable.Value = averageengineshop;
            consumptionengineshopVariable.Value = consumptionengineshop;


            // For Paintshop
            targetpaintshopVariable.Value = targetpaintshop;
            yearlowestpaintshopVariable.Value = yearlowestpaintshop;
            monthlowestpaintshopVariable.Value = monthlowestpaintshop;
            averagepaintshopVariable.Value = averagepaintshop;
            consumptionpaintshopVariable.Value = consumptionpaintshop;

            // For Spp
            targetsppVariable.Value = targetspp;
            yearlowestsppVariable.Value = yearlowestspp;
            monthlowestsppVariable.Value = monthlowestspp;
            averagesppVariable.Value = averagespp;
            consumptionsppVariable.Value = consumptionspp;

            // For Spare
            targetspareVariable.Value = targetspare;
            yearlowestspareVariable.Value = yearlowestspare;
            monthlowestspareVariable.Value = monthlowestspare;
            averagespareVariable.Value = averagespare;
            consumptionspareVariable.Value = consumptionspare;


            // For 33KV
            target33kvVariable.Value = target33kv;
            yearlowest33kvVariable.Value = yearlowest33kv;
            monthlowest33kvVariable.Value = monthlowest33kv;
            average33kvVariable.Value = average33kv;
            consumption33kvVariable.Value = consumption33kv;


            // For Calculation
            utilitypercentageVariable.Value = utilitypercentage;
            stampingpercentageVariable.Value = stampingpercentage;
            tcfpercentageVariable.Value = tcfpercentage;
            bodyshoppercentageVariable.Value = bodyshoppercentage;
            engineshoppercentageVariable.Value = engineshoppercentage;
            paintshoppercentageVariable.Value = paintshoppercentage;
            sparepercentageVariable.Value = sparepercentage;
            spppercentageVariable.Value = spppercentage;

        }
    }
    ////////////////////////////////*********************************************///////////////////////////////////////////////////////////////////////////
    private IUAVariable dateVariable;
    private IUAVariable date1Variable;
    private IUAVariable jacestampingVariable;
    private IUAVariable targetstampingVariable;
    private IUAVariable yearloweststampingVariable;
    private IUAVariable monthloweststampingVariable;
    private IUAVariable averagestampingVariable;
    private IUAVariable consumptionstampingVariable;
    private IUAVariable jacetcfVariable;
    private IUAVariable targettcfVariable;
    private IUAVariable yearlowesttcfVariable;
    private IUAVariable monthlowesttcfVariable;
    private IUAVariable averagetcfVariable;
    private IUAVariable consumptiontcfVariable;
    private IUAVariable jacebodyshopVariable;
    private IUAVariable targetbodyshopVariable;
    private IUAVariable yearlowestbodyshopVariable;
    private IUAVariable monthlowestbodyshopVariable;
    private IUAVariable averagebodyshopVariable;
    private IUAVariable consumptionbodyshopVariable;
    private IUAVariable consumptionpaintshopVariable;
    private IUAVariable jacesppVariable;
    private IUAVariable targetsppVariable;
    private IUAVariable yearlowestsppVariable;
    private IUAVariable monthlowestsppVariable;
    private IUAVariable averagesppVariable;
    private IUAVariable consumptionsppVariable;
    private IUAVariable jacespareVariable;
    private IUAVariable targetspareVariable;
    private IUAVariable yearlowestspareVariable;
    private IUAVariable monthlowestspareVariable;
    private IUAVariable averagespareVariable;
    private IUAVariable consumptionspareVariable;
    private IUAVariable jace33kvVariable;
    private IUAVariable target33kvVariable;
    private IUAVariable yearlowest33kvVariable;
    private IUAVariable monthlowest33kvVariable;
    private IUAVariable average33kvVariable;
    private IUAVariable consumption33kvVariable;
    private IUAVariable bodyshoppercentageVariable;
    private IUAVariable engineshoppercentageVariable;
    private IUAVariable paintshoppercentageVariable;
    private IUAVariable sparepercentageVariable;
    private IUAVariable spppercentageVariable;
    private IUAVariable utilitypercentageVariable;
    private IUAVariable stampingpercentageVariable;
    private IUAVariable tcfpercentageVariable;
    private IUAVariable jaceengineshopVariable;
    private IUAVariable targetengineshopVariable;
    private IUAVariable yearlowestengineshopVariable;
    private IUAVariable monthlowestengineshopVariable;
    private IUAVariable averageengineshopVariable;
    private IUAVariable consumptionengineshopVariable;
    private IUAVariable jacepaintshopVariable;
    private IUAVariable targetpaintshopVariable;
    private IUAVariable yearlowestpaintshopVariable;
    private IUAVariable monthlowestpaintshopVariable;
    private IUAVariable averagepaintshopVariable;
    private IUAVariable jaceVariable;
    private IUAVariable meterVariable;
    private IUAVariable targetVariable;
    private IUAVariable yearlowestVariable;
    private IUAVariable monthlowestVariable;
    private IUAVariable averageVariable;
    private IUAVariable consumptionVariable;
    private IUAVariable gbuttonVariable;
    private PeriodicTask periodicTask;
    private IUAVariable monthyearVariable;
    private IUAVariable yearVariable;
}
