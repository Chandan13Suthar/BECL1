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

public class RuntimeNetLogic4 : BaseNetLogic
{


    public override void Start()
    {
        var owner = (_33KVAlarm)LogicObject.Owner;

        buttonVariable = owner.ButtonVariable;
        dateVariable = owner.DateVariable;

        ///////////////////////////For Old Avg Tag For Consumption/////////////////////////
        avgconsumptionincomer1Variable = owner.AvgConsumptionIncomer1Variable;
        avgconsumptionincomer2Variable = owner.AvgConsumptionIncomer2Variable;                                    
        avgconsumptionsparein1og1Variable = owner.AvgConsumptionSPAREIN1OG1Variable;                                    
        avgconsumptionutilityin1og2Variable = owner.AvgConsumptionUTILITYIN1OG2Variable;
        avgconsumptionstampingin1og3Variable = owner.AvgConsumptionSTAMPINGIN1OG3Variable;
        avgconsumptiontcfin1og4Variable = owner.AvgConsumptionTCFIN1OG4Variable;
        avgconsumptionbodyshopin1og5Variable = owner.AvgConsumptionBODYSHOPIN1OG5Variable;
        avgconsumptionengineshopin1og6Variable = owner.AvgConsumptionENGINESHOPIN1OG6Variable;
        avgconsumptionpaintshopin1og7Variable = owner.AvgConsumptionPAINTSHOPIN1OG7Variable;
        avgconsumptionpaintshopin1og8Variable = owner.AvgConsumptionPAINTSHOPIN2OG8Variable;
        avgconsumptionengineshopin2og9Variable = owner.AvgConsumptionENGINESHOPIN2OG9Variable;
        avgconsumptionbodyshopin2og10Variable = owner.AvgConsumptionBODYSHOPIN2G10Variable;
        avgconsumptiontcf2og11Variable = owner.AvgConsumptionTCFIN2OG11Variable;
        avgconsumptionstampingin2og12Variable = owner.AvgConsumptionSTAMPINGIN2OG12Variable;
        avgconsumptionutility2og13Variable = owner.AvgConsumptionUTILITYIN2OG13Variable;
        avgconsumptionsparein2og14Variable = owner.AvgConsumptionSPAREIN2OG14Variable;
        avgconsumptionsppshopin2og15Variable = owner.AvgConsumptionSPPShopIN2OG15Variable;
        avgconsumptionsppshopin1og16Variable = owner.AvgConsumptionSPPShopIN1OG16Variable;
        avgconsumptionebin1Variable = owner.AvgConsumptionEBIN1Variable;
        avgconsumptiondg1in1Variable = owner.AvgConsumptionDGIN1Variable;

        ///////////////////////////For new Avg Tags For  Consumption/////////////////////////
        avgconsumptionincomer1newVariable = owner.AvgConsumptionIncomer1newVariable;
        avgconsumptionincomer2newVariable = owner.AvgConsumptionIncomer2newVariable;
        avgconsumptionsparein1og1newVariable = owner.AvgConsumptionSPAREIN1OG1newVariable;
        avgconsumptionutilityin1og2newVariable = owner.AvgConsumptionUTILITYIN1OG2newVariable;
        avgconsumptionstampingin1og3newVariable = owner.AvgConsumptionSTAMPINGIN1OG3newVariable;
        avgconsumptiontcfin1og4newVariable = owner.AvgConsumptionTCFIN1OG4newVariable;
        avgconsumptionbodyshopin1og5newVariable = owner.AvgConsumptionBODYSHOPIN1OG5newVariable;
        avgconsumptionengineshopin1og6newVariable = owner.AvgConsumptionENGINESHOPIN1OG6newVariable;
        avgconsumptionengineshopin1og1newVariable = owner.AvgConsumptionENGINESHOPIN1OG1newVariable;
        avgconsumptionpaintshopin1og7newVariable = owner.AvgConsumptionPAINTSHOPIN1OG7newVariable;
        avgconsumptionpaintshopin1og8newVariable = owner.AvgConsumptionPAINTSHOPIN2OG8newVariable;
        avgconsumptionengineshopin2og9newVariable = owner.AvgConsumptionENGINESHOPIN2OG9newVariable;
        avgconsumptionbodyshopin2og10newVariable = owner.AvgConsumptionBODYSHOPIN2G10newVariable;
        avgconsumptiontcf2og11newVariable = owner.AvgConsumptionTCFIN2OG11newVariable;
        avgconsumptionstampingin2og12newVariable = owner.AvgConsumptionSTAMPINGIN2OG12newVariable;
        avgconsumptionutility2og13newVariable = owner.AvgConsumptionUTILITYIN2OG13newVariable;
        avgconsumptionsparein2og14newVariable = owner.AvgConsumptionSPAREIN2OG14newVariable;
        avgconsumptionsppshopin2og15newVariable = owner.AvgConsumptionSPPShopIN2OG15newVariable;
        avgconsumptionsppshopin1og16newVariable = owner.AvgConsumptionSPPShopIN1OG16newVariable;
        avgconsumptionebin1newVariable = owner.AvgConsumptionEBIN1newVariable;
        avgconsumptiondg1in1newVariable = owner.AvgConsumptionDGIN1newVariable;


        ///////////////////////////Alarm Tags For Consumption/////////////////////////
        alarmincomer1Variable = owner.AlarmIncomer1Variable;
        alarmincomer2Variable = owner.AlarmIncomer2Variable;
        alarmsparein1og1Variable = owner.AlarmSPAREIN1OG1Variable;
        alarmutilityin1og2Variable = owner.AlarmUTILITYIN1OG2Variable;
        alarmstampingin1og3Variable = owner.AlarmSTAMPINGIN1OG3Variable;
        alarmtcfin1og4Variable = owner.AlarmTCFIN1OG4Variable;
        alarmbodyshopin1og5Variable = owner.AlarmBODYSHOPIN1OG5Variable;
        alarmengineshopin1og6Variable = owner.AlarmENGINESHOPIN1OG6Variable;
        alarmpaintshopin1og7Variable = owner.AlarmPAINTSHOPIN1OG7Variable;
        alarmpaintshopin1og8Variable = owner.AlarmPAINTSHOPIN2OG8Variable;
        alarmengineshopin2og9Variable = owner.AlarmENGINESHOPIN2OG9Variable;
        alarmbodyshopin2og10Variable = owner.AlarmBODYSHOPIN2G10Variable;
        alarmtcf2og11Variable = owner.AlarmTCFIN2OG11Variable;
        alarmstampingin2og12Variable = owner.AlarmSTAMPINGIN2OG12Variable;
        alarmutility2og13Variable = owner.AlarmUTILITYIN2OG13Variable;
        alarmsparein2og14Variable = owner.AlarmSPAREIN2OG14Variable;
        alarmsppshopin2og15Variable = owner.AlarmSPPShopIN2OG15Variable;
        alarmsppshopin1og16Variable = owner.AlarmSPPShopIN1OG16Variable;
        alarmebin1Variable = owner.AlarmEBIN1Variable;
        alarmdg1in1Variable = owner.AlarmDGIN1Variable;


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
        ///////////////////////////For Old Avg Tag For Consumption/////////////////////////
        bool button = buttonVariable.Value;
        DateTime date = dateVariable.Value;

        float avgconsumptionincomer1 = avgconsumptionincomer1Variable.Value;
        float avgconsumptionincomer2 = avgconsumptionincomer2Variable.Value;
        float avgconsumptionsparein1og1 = avgconsumptionsparein1og1Variable.Value;
        float avgconsumptionutilityin1og2 = avgconsumptionutilityin1og2Variable.Value;
        float avgconsumptionstampingin1og3 = avgconsumptionstampingin1og3Variable.Value;
        float avgconsumptiontcfin1og4 = avgconsumptiontcfin1og4Variable.Value;
        float avgconsumptionbodyshopin1og5 = avgconsumptiontcfin1og4Variable.Value;
        float avgconsumptionengineshopin1og6 = avgconsumptionengineshopin1og6Variable.Value;
        float avgconsumptionpaintshopin1og7 = avgconsumptionpaintshopin1og7Variable.Value;
        float avgconsumptionpaintshopin1og8 = avgconsumptionpaintshopin1og8Variable.Value;
        float avgconsumptionengineshopin2og9 = avgconsumptionengineshopin2og9Variable.Value;
        float avgconsumptionbodyshopin2og10 = avgconsumptionbodyshopin2og10Variable.Value;
        float avgconsumptiontcf2og11 = avgconsumptiontcf2og11Variable.Value;
        float avgconsumptionstampingin2og12 = avgconsumptionstampingin2og12Variable.Value;
        float avgconsumptionutility2og13 = avgconsumptionutility2og13Variable.Value;
        float avgconsumptionsparein2og14 = avgconsumptionsparein2og14Variable.Value;
        float avgconsumptionsppshopin2og15 = avgconsumptionsppshopin2og15Variable.Value;
        float avgconsumptionsppshopin1og16 = avgconsumptionsppshopin1og16Variable.Value;
        float avgconsumptionebin1 = avgconsumptionebin1Variable.Value;
        float avgconsumptiondg1in1 = avgconsumptiondg1in1Variable.Value;

        ///////////////////////////For new Avg Tags For  Consumption/////////////////////////
        float avgconsumptionincomer1new = avgconsumptionincomer1newVariable.Value;
        float avgconsumptionincomer2new = avgconsumptionincomer2newVariable.Value;
        float avgconsumptionsparein1og1new = avgconsumptionsparein1og1newVariable.Value;
        float avgconsumptionutilityin1og2new = avgconsumptionutilityin1og2newVariable.Value;
        float avgconsumptionstampingin1og3new = avgconsumptionstampingin1og3newVariable.Value;
        float avgconsumptiontcfin1og4new = avgconsumptiontcfin1og4newVariable.Value;
        float avgconsumptionbodyshopin1og5new = avgconsumptiontcfin1og4newVariable.Value;
        float avgconsumptionengineshopin1og6new = avgconsumptionengineshopin1og6newVariable.Value;
        float avgconsumptionengineshopin1og1new = avgconsumptionengineshopin1og1newVariable.Value;
        float avgconsumptionpaintshopin1og7new = avgconsumptionpaintshopin1og7newVariable.Value;
        float avgconsumptionpaintshopin1og8new = avgconsumptionpaintshopin1og8newVariable.Value;
        float avgconsumptionengineshopin2og9new = avgconsumptionengineshopin2og9newVariable.Value;
        float avgconsumptionbodyshopin2og10new = avgconsumptionbodyshopin2og10newVariable.Value;
        float avgconsumptiontcf2og11new = avgconsumptiontcf2og11newVariable.Value;
        float avgconsumptionstampingin2og12new = avgconsumptionstampingin2og12newVariable.Value;
        float avgconsumptionutility2og13new = avgconsumptionutility2og13newVariable.Value;
        float avgconsumptionsparein2og14new = avgconsumptionsparein2og14newVariable.Value;
        float avgconsumptionsppshopin2og15new = avgconsumptionsppshopin2og15newVariable.Value;
        float avgconsumptionsppshopin1og16new = avgconsumptionsppshopin1og16newVariable.Value;
        float avgconsumptionebin1new = avgconsumptionebin1newVariable.Value;
        float avgconsumptiondg1in1new = avgconsumptiondg1in1newVariable.Value;

        bool alarmincomer1 = alarmincomer1Variable.Value;
        bool alarmincomer2 = alarmincomer2Variable.Value;
        bool alarmsparein1og1 = alarmsparein1og1Variable.Value;
        bool alarmutilityin1og2 = alarmutilityin1og2Variable.Value;
        bool alarmstampingin1og3 = alarmstampingin1og3Variable.Value;
        bool alarmtcfin1og4 = alarmtcfin1og4Variable.Value;
        bool alarmbodyshopin1og5 = alarmbodyshopin1og5Variable.Value;
        bool alarmengineshopin1og6 = alarmengineshopin1og6Variable.Value;
        bool alarmpaintshopin1og7 = alarmpaintshopin1og7Variable.Value;
        bool alarmpaintshopin1og8 = alarmpaintshopin1og8Variable.Value;
        bool alarmengineshopin2og9 = alarmengineshopin2og9Variable.Value;
        bool alarmbodyshopin2og10 = alarmbodyshopin2og10Variable.Value;
        bool alarmtcf2og11 = alarmtcf2og11Variable.Value;
        bool alarmstampingin2og12 = alarmstampingin2og12Variable.Value;
        bool alarmutility2og13 = alarmutility2og13Variable.Value;
        bool alarmsparein2og14 = alarmsparein2og14Variable.Value;
        bool alarmsppshopin2og15 = alarmsppshopin2og15Variable.Value;
        bool alarmsppshopin1og16 = alarmsppshopin1og16Variable.Value;
        bool alarmebin1 = alarmebin1Variable.Value;
        bool alarmdg1in1 = alarmdg1in1Variable.Value;


        var project = FTOptix.HMIProject.Project.Current;

        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Incomer_1
        var myStore2 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Incomer_2
        var myStore3 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//SPARE_IN1_OG1
        var myStore4 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//UTILITY_IN1_OG2
        var myStore5 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//STAMPING_IN1_OG3
        var myStore6 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//TCF_IN1_OG4
        var myStore7 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//BODY_SHOP_IN1_OG5
        var myStore8 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//ENGINE_SHOP_IN1_OG6
        var myStore9 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//PAINT_SHOP_IN1_OG7
        var myStore10 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//PAINT_SHOP_IN2_OG8
        var myStore11 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//ENGINE_SHOP_IN2_OG9
        var myStore12 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//BODY_SHOP_IN2_G10
        var myStore13 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//TCF_IN2_OG11
        var myStore14 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//STAMPING_IN2_OG12
        var myStore15 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//UTILITY_IN2_OG13   
        var myStore16 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//SPARE_IN2_OG14
        var myStore17 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//SPP_Shop_IN2_OG15  
        var myStore18 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//SPP_Shop_IN1_OG16 EB_IN1
        var myStore19 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//Eg
        var myStore20 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");//EBD


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

        // Initially alarm turns off ////
        alarmincomer1 = false;
        alarmincomer2 = false;
        alarmsparein1og1 = false;
        alarmutilityin1og2 = false;
        alarmstampingin1og3 = false;
        alarmtcfin1og4 = false;
        alarmbodyshopin1og5 = false;
        alarmengineshopin1og6 = false;
        alarmpaintshopin1og7 = false;
        alarmpaintshopin1og8 = false;
        alarmengineshopin2og9 = false;
        alarmbodyshopin2og10 = false;
        alarmtcf2og11 = false;
        alarmstampingin2og12 = false;
        alarmutility2og13 = false;
        alarmsparein2og14 = false;
        alarmsppshopin2og15 = false;
        alarmsppshopin1og16 = false;
        alarmebin1 = false;
        alarmdg1in1 = false;





        if (button == false)
        {

            avgconsumptionincomer1 = avgconsumptionincomer1new;
            avgconsumptionincomer2 = avgconsumptionincomer2new;
            avgconsumptionsparein1og1 = avgconsumptionsparein1og1new;
            avgconsumptionutilityin1og2 = avgconsumptionutilityin1og2new;
            avgconsumptionstampingin1og3 = avgconsumptionstampingin1og3new;
            avgconsumptiontcfin1og4 = avgconsumptiontcfin1og4new;
            avgconsumptionbodyshopin1og5 = avgconsumptionbodyshopin1og5new;
            avgconsumptionengineshopin1og6 = avgconsumptionengineshopin1og6new;
            avgconsumptionpaintshopin1og7 = avgconsumptionpaintshopin1og7new;
            avgconsumptionpaintshopin1og8 = avgconsumptionpaintshopin1og8new;
            avgconsumptionengineshopin2og9 = avgconsumptionengineshopin2og9new;
            avgconsumptionbodyshopin2og10 = avgconsumptionbodyshopin2og10new;
            avgconsumptiontcf2og11 = avgconsumptiontcf2og11new;
            avgconsumptionstampingin2og12 = avgconsumptionstampingin2og12new;
            avgconsumptionutility2og13 = avgconsumptionutility2og13new;
            avgconsumptionsparein2og14 = avgconsumptionsparein2og14new;
            avgconsumptionsppshopin2og15 = avgconsumptionsppshopin2og15new;
            avgconsumptionsppshopin1og16 = avgconsumptionsppshopin1og16new;
            avgconsumptionebin1 = avgconsumptionebin1new;
            avgconsumptiondg1in1 = avgconsumptiondg1in1new;




            string new123 = DateTime.Now.ToString("yyyy-MM-dd");

            string query1 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = 'UTILITY'  AND Meter = 'MCB_MVS_01_IN1' ";
            string query2 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'Incomer_2' ";
            string query3 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'SPARE_IN1_OG1' ";
            string query4 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'UTILITY_IN1_OG2' ";
            string query5 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'STAMPING_IN1_OG3' ";
            string query6 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'TCF_IN1_OG4' ";
            string query7 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'BODY_SHOP_IN1_OG5' ";
            string query8 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'ENGINE_SHOP_IN1_OG6' ";
            string query9 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'PAINT_SHOP_IN1_OG7' ";
            string query10 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'PAINT_SHOP_IN2_OG8' ";
            string query11 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'ENGINE_SHOP_IN2_OG9' ";
            string query12 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'BODY_SHOP_IN2_G10' ";
            string query13 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'TCF_IN2_OG11' ";
            string query14 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'STAMPING_IN2_OG12' ";
            string query15 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'UTILITY_IN2_OG13 ' ";
            string query16 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'SPARE_IN2_OG14' ";
            string query17 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'SPP_Shop_IN2_OG15 ' ";
            string query18 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'SPP_Shop_IN1_OG16 EB_IN1' ";
            string query19 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'EB_IN1' ";
            string query20 = $"SELECT  AVG(Consumption) FROM ConsumptionDistribution WHERE Date  BETWEEN '" + new123 + " 00:00:00' AND '" + new123 + " 23:59:59' AND Jace = '33KV SEGR ROOM'  AND Meter = 'DG_IN1' ";


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


            var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount1 = header1 != null ? header1.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                var AvgConsumptionIncomer1new = column1;
                avgconsumptionincomer1new = AvgConsumptionIncomer1new;
            }


            var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
            var columnCount2 = header2 != null ? header2.Length : 0;
            if (rowCount2 > 0 && columnCount2 > 0)
            {
                var column1 = Convert.ToInt32(resultSet2[0, 0]);
                var AvgConsumptionIncomer2new = column1;
                avgconsumptionincomer2new = AvgConsumptionIncomer2new;
            }

            var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
            var columnCount3 = header3 != null ? header3.Length : 0;
            if (rowCount3 > 0 && columnCount3 > 0)
            {
                var column1 = Convert.ToInt32(resultSet3[0, 0]);
                var AvgConsumptionSPAREIN1OG1new = column1;
                avgconsumptionsparein1og1new = AvgConsumptionSPAREIN1OG1new;
            }

            var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
            var columnCount4 = header4 != null ? header4.Length : 0;
            if (rowCount4 > 0 && columnCount4 > 0)
            {
                var column1 = Convert.ToInt32(resultSet4[0, 0]);
                var AvgConsumptionUTILITYIN1OG2new = column1;
                avgconsumptionutilityin1og2new = AvgConsumptionUTILITYIN1OG2new;
            }

            var rowCount5 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
            var columnCount5 = header5 != null ? header5.Length : 0;
            if (rowCount5 > 0 && columnCount5 > 0)
            {
                var column1 = Convert.ToInt32(resultSet5[0, 0]);
                var AvgConsumptionSTAMPINGIN1OG3new = column1;
                avgconsumptionstampingin1og3new = AvgConsumptionSTAMPINGIN1OG3new;
            }

            var rowCount6 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
            var columnCount6 = header6 != null ? header6.Length : 0;
            if (rowCount6 > 0 && columnCount6 > 0)
            {
                var column1 = Convert.ToInt32(resultSet6[0, 0]);
                var AvgConsumptionTCFIN1OG4new = column1;
                avgconsumptiontcfin1og4new = AvgConsumptionTCFIN1OG4new;
            }

            var rowCount7 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
            var columnCount7 = header7 != null ? header7.Length : 0;
            if (rowCount7 > 0 && columnCount7 > 0)
            {
                var column1 = Convert.ToInt32(resultSet7[0, 0]);
                var AvgConsumptionBODYSHOPIN1OG5new = column1;
                avgconsumptionbodyshopin1og5new = AvgConsumptionBODYSHOPIN1OG5new;
            }

            var rowCount8 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
            var columnCount8 = header8 != null ? header8.Length : 0;
            if (rowCount8 > 0 && columnCount8 > 0)
            {
                var column1 = Convert.ToInt32(resultSet8[0, 0]);
                var AvgConsumptionENGINESHOPIN1OG6new = column1;
                avgconsumptionengineshopin1og6new = AvgConsumptionENGINESHOPIN1OG6new;
            }

            var rowCount9 = resultSet9 != null ? resultSet9.GetLength(0) : 0;
            var columnCount9 = header9 != null ? header9.Length : 0;
            if (rowCount9 > 0 && columnCount9 > 0)
            {
                var column1 = Convert.ToInt32(resultSet9[0, 0]);
                var AvgConsumptionPAINTSHOPIN1OG7new = column1;
                avgconsumptionpaintshopin1og7new = AvgConsumptionPAINTSHOPIN1OG7new;
            }

            var rowCount10 = resultSet10 != null ? resultSet10.GetLength(0) : 0;
            var columnCount10 = header10 != null ? header10.Length : 0;
            if (rowCount10 > 0 && columnCount10 > 0)
            {
                var column1 = Convert.ToInt32(resultSet10[0, 0]);
                var AvgConsumptionPAINTSHOPIN2OG8new = column1;
                avgconsumptionpaintshopin1og8new = AvgConsumptionPAINTSHOPIN2OG8new;
            }

            var rowCount11 = resultSet11!= null ? resultSet11.GetLength(0) : 0;
            var columnCount11 = header11 != null ? header11.Length : 0;
            if (rowCount11 > 0 && columnCount11 > 0)
            {
                var column1 = Convert.ToInt32(resultSet11[0, 0]);
                var AvgConsumptionENGINESHOPIN2OG9new = column1;
                avgconsumptionengineshopin2og9new = AvgConsumptionENGINESHOPIN2OG9new;
            }

            var rowCount12 = resultSet12 != null ? resultSet12.GetLength(0) : 0;
            var columnCount12 = header12 != null ? header12.Length : 0;
            if (rowCount12 > 0 && columnCount12 > 0)
            {
                var column1 = Convert.ToInt32(resultSet12[0, 0]);
                var AvgConsumptionBODYSHOPIN2G10new = column1;
                avgconsumptionbodyshopin2og10new = AvgConsumptionBODYSHOPIN2G10new;
            }

            var rowCount13 = resultSet13 != null ? resultSet13.GetLength(0) : 0;
            var columnCount13 = header13 != null ? header13.Length : 0;
            if (rowCount13 > 0 && columnCount13 > 0)
            {
                var column1 = Convert.ToInt32(resultSet13[0, 0]);
                var AvgConsumptionTCFIN2OG11new = column1;
                avgconsumptiontcf2og11new = AvgConsumptionTCFIN2OG11new;
            }

            var rowCount14 = resultSet14 != null ? resultSet14.GetLength(0) : 0;
            var columnCount14 = header14 != null ? header14.Length : 0;
            if (rowCount14 > 0 && columnCount14 > 0)
            {
                var column1 = Convert.ToInt32(resultSet14[0, 0]);
                var AvgConsumptionSTAMPINGIN2OG12new = column1;
                avgconsumptionstampingin2og12new = AvgConsumptionSTAMPINGIN2OG12new;
            }

            var rowCount15 = resultSet15 != null ? resultSet15.GetLength(0) : 0;
            var columnCount15 = header15 != null ? header15.Length : 0;
            if (rowCount15 > 0 && columnCount15 > 0)
            {
                var column1 = Convert.ToInt32(resultSet15[0, 0]);
                var AvgConsumptionUTILITYIN2OG13new = column1;
                avgconsumptionutility2og13new = AvgConsumptionUTILITYIN2OG13new;
            }

            var rowCount16 = resultSet16 != null ? resultSet16.GetLength(0) : 0;
            var columnCount16 = header16 != null ? header16.Length : 0;
            if (rowCount16 > 0 && columnCount16 > 0)
            {
                var column1 = Convert.ToInt32(resultSet16[0, 0]);
                var AvgConsumptionSPAREIN2OG14new = column1;
                avgconsumptionsparein2og14new = AvgConsumptionSPAREIN2OG14new;
            }

            var rowCount17 = resultSet17 != null ? resultSet17.GetLength(0) : 0;
            var columnCount17 = header17 != null ? header17.Length : 0;
            if (rowCount17 > 0 && columnCount17 > 0)
            {
                var column1 = Convert.ToInt32(resultSet17[0, 0]);
                var AvgConsumptionSPPShopIN2OG15new = column1;
                avgconsumptionsppshopin2og15new = AvgConsumptionSPPShopIN2OG15new;
            }

            var rowCount18 = resultSet18 != null ? resultSet18.GetLength(0) : 0;
            var columnCount18 = header18 != null ? header18.Length : 0;
            if (rowCount18 > 0 && columnCount18 > 0)
            {
                var column1 = Convert.ToInt32(resultSet18[0, 0]);
                var AvgConsumptionSPPShopIN1OG16new = column1;
                avgconsumptionsppshopin1og16new = AvgConsumptionSPPShopIN1OG16new;
            }

            var rowCount19 = resultSet19 != null ? resultSet19.GetLength(0) : 0;
            var columnCount19 = header19 != null ? header19.Length : 0;
            if (rowCount19 > 0 && columnCount19 > 0)
            {
                var column1 = Convert.ToInt32(resultSet19[0, 0]);
                var AvgConsumptionEBIN1new = column1;
                avgconsumptionebin1new = AvgConsumptionEBIN1new;
            }

            var rowCount20 = resultSet20 != null ? resultSet20.GetLength(0) : 0;
            var columnCount20 = header20 != null ? header20.Length : 0;
            if (rowCount20 > 0 && columnCount20 > 0)
            {
                var column1 = Convert.ToInt32(resultSet20[0, 0]);
                var AvgConsumptionDGIN1new = column1;
                avgconsumptiondg1in1new = AvgConsumptionDGIN1new;
            }


            float diff1 = (avgconsumptionincomer1new - avgconsumptionincomer1 );
            float diff2 = (avgconsumptionincomer2new - avgconsumptionincomer2);
            float diff3 = (avgconsumptionsparein1og1new - avgconsumptionsparein1og1);
            float diff4 = (avgconsumptionutilityin1og2new - avgconsumptionutilityin1og2);
            float diff5 = (avgconsumptionstampingin1og3new - avgconsumptionstampingin1og3);
            float diff6 = (avgconsumptiontcfin1og4new - avgconsumptiontcfin1og4);
            float diff7 = (avgconsumptionbodyshopin1og5new - avgconsumptionbodyshopin1og5);
            float diff8 = (avgconsumptionengineshopin1og6new - avgconsumptionengineshopin1og6);
            float diff9 = (avgconsumptionpaintshopin1og7new - avgconsumptionpaintshopin1og7);
            float diff10 = (avgconsumptionpaintshopin1og8new - avgconsumptionpaintshopin1og8);
            float diff11 = (avgconsumptionengineshopin2og9new - avgconsumptionengineshopin2og9);
            float diff12 = (avgconsumptionbodyshopin2og10new - avgconsumptionbodyshopin2og10);
            float diff13 = (avgconsumptiontcf2og11new - avgconsumptiontcf2og11);
            float diff14 = (avgconsumptionstampingin2og12new - avgconsumptionstampingin2og12);
            float diff15 = (avgconsumptionutility2og13new - avgconsumptionutility2og13);
            float diff16 = (avgconsumptionsparein2og14new - avgconsumptionsparein2og14);
            float diff17 = (avgconsumptionsppshopin2og15new - avgconsumptionsppshopin2og15);
            float diff18 = (avgconsumptionsppshopin1og16new - avgconsumptionsppshopin1og16);
            float diff19 = (avgconsumptionebin1new - avgconsumptionebin1);
            float diff20 = (avgconsumptiondg1in1new - avgconsumptiondg1in1);

          
            
            if (diff1 > 100 )
            {
                alarmincomer1 = true ;

            }
           


            if (diff2 > 100 )
            {
                alarmincomer2 = true ;
            }
            

            if (diff3 > 100)
            {
                alarmsparein1og1 = true ;
            }
            

            if (diff4 > 100)
            {
                alarmutilityin1og2 = true ;

            }
            

             if (diff5 > 100)
            {
                alarmstampingin1og3 = true ;
            }
             


            if (diff6 > 100)
            {
                alarmtcfin1og4 = true ;
            }
             

             if (diff7 > 100)
            {
                alarmbodyshopin1og5 = true ;
            }
            

            if (diff8 > 100)
            {
                alarmengineshopin1og6 = true ;
            }
            

            if (diff9 > 100)
            {
                alarmpaintshopin1og7 = true ;
            }
            

             if (diff10 > 100)
             {
                alarmpaintshopin1og8 = true ;
             }
             

             if (diff11 > 100)
             {
                alarmengineshopin2og9 = true ;
             }
             

             if (diff12 > 100)
             {
                alarmbodyshopin2og10 = true ;
             }
             

             if (diff13 > 100)
             {
                alarmtcf2og11 = true ;
             }
            

             if (diff14 > 100)
             {
                alarmstampingin2og12 = true ;
             }


             if (diff15 > 100)
             {
                alarmutility2og13 = true ;
             }
             

             if (diff16 > 100)
             {
                alarmsparein2og14 = true ;
             }
            


             if (diff17 > 100)
             {
                alarmsppshopin2og15 = true ;
             }
             

             if (diff18 > 100)
             {
                alarmsppshopin1og16 = true ;
             }


             if (diff19 > 100)
             {
                alarmebin1 = true ;
             }
            

             if (diff20 > 100)
             {
                alarmdg1in1 = true;
             }
             


            avgconsumptionincomer1newVariable.Value = avgconsumptionincomer1new;
            avgconsumptionincomer2newVariable.Value = avgconsumptionincomer2new;
            avgconsumptionsparein1og1newVariable.Value = avgconsumptionsparein1og1new;
            avgconsumptionutilityin1og2newVariable.Value = avgconsumptionutilityin1og2new;
            avgconsumptionstampingin1og3newVariable.Value = avgconsumptionstampingin1og3new;
            avgconsumptiontcfin1og4newVariable.Value = avgconsumptiontcfin1og4new;
            avgconsumptionbodyshopin1og5newVariable.Value = avgconsumptionbodyshopin1og5new;
            avgconsumptionengineshopin1og6newVariable.Value = avgconsumptionengineshopin1og6new;
            avgconsumptionengineshopin1og1newVariable.Value = avgconsumptionengineshopin1og1new;
            avgconsumptionpaintshopin1og7newVariable.Value = avgconsumptionpaintshopin1og7new;
            avgconsumptionpaintshopin1og8newVariable.Value = avgconsumptionpaintshopin1og8new;
            avgconsumptionengineshopin2og9newVariable.Value = avgconsumptionengineshopin2og9new;
            avgconsumptionbodyshopin2og10newVariable.Value = avgconsumptionbodyshopin2og10new;
            avgconsumptiontcf2og11newVariable.Value = avgconsumptiontcf2og11new;
            avgconsumptionstampingin2og12newVariable.Value = avgconsumptionstampingin2og12new;
            avgconsumptionutility2og13newVariable.Value = avgconsumptionutility2og13new;
            avgconsumptionsparein2og14newVariable.Value = avgconsumptionsparein2og14new;
            avgconsumptionsppshopin2og15newVariable.Value = avgconsumptionsppshopin2og15new;
            avgconsumptionsppshopin1og16newVariable.Value = avgconsumptionsppshopin1og16new;
            avgconsumptionebin1newVariable.Value = avgconsumptionebin1new;
            avgconsumptiondg1in1newVariable.Value = avgconsumptiondg1in1new;
 

            avgconsumptionincomer1Variable.Value = avgconsumptionincomer1;
            avgconsumptionincomer2Variable.Value = avgconsumptionincomer2;
            avgconsumptionsparein1og1Variable.Value = avgconsumptionsparein1og1;
            avgconsumptionutilityin1og2Variable.Value = avgconsumptionutilityin1og2;
            avgconsumptionstampingin1og3Variable.Value = avgconsumptionstampingin1og3;
            avgconsumptiontcfin1og4Variable.Value = avgconsumptiontcfin1og4;
            avgconsumptiontcfin1og4Variable.Value = avgconsumptiontcfin1og4;
            avgconsumptionengineshopin1og6Variable.Value = avgconsumptionengineshopin1og6;
            avgconsumptionpaintshopin1og7Variable.Value = avgconsumptionpaintshopin1og7;
            avgconsumptionpaintshopin1og8Variable.Value = avgconsumptionpaintshopin1og8;
            avgconsumptionengineshopin2og9Variable.Value = avgconsumptionengineshopin2og9;
            avgconsumptionbodyshopin2og10Variable.Value = avgconsumptionbodyshopin2og10;
            avgconsumptiontcf2og11Variable.Value = avgconsumptiontcf2og11;
            avgconsumptionstampingin2og12Variable.Value = avgconsumptionstampingin2og12;
            avgconsumptionutility2og13Variable.Value = avgconsumptionutility2og13;
            avgconsumptionsparein2og14Variable.Value = avgconsumptionsparein2og14;
            avgconsumptionsppshopin2og15Variable.Value = avgconsumptionsppshopin2og15;
            avgconsumptionsppshopin1og16Variable.Value = avgconsumptionsppshopin1og16;
            avgconsumptionebin1Variable.Value = avgconsumptionebin1;
            avgconsumptiondg1in1Variable.Value = avgconsumptiondg1in1;

            
            alarmincomer1Variable.Value = alarmincomer1;
            alarmincomer2Variable.Value = alarmincomer2;
            alarmsparein1og1Variable.Value = alarmsparein1og1;
            alarmutilityin1og2Variable.Value = alarmutilityin1og2;
            alarmstampingin1og3Variable.Value = alarmstampingin1og3;
            alarmtcfin1og4Variable.Value = alarmtcfin1og4;
            alarmbodyshopin1og5Variable.Value = alarmbodyshopin1og5;
            alarmengineshopin1og6Variable.Value = alarmengineshopin1og6;
            alarmpaintshopin1og7Variable.Value = alarmpaintshopin1og7;
            alarmpaintshopin1og8Variable.Value = alarmpaintshopin1og8;
            alarmengineshopin2og9Variable.Value = alarmengineshopin2og9;
            alarmbodyshopin2og10Variable.Value = alarmbodyshopin2og10;
            alarmtcf2og11Variable.Value = alarmtcf2og11;
            alarmstampingin2og12Variable.Value = alarmstampingin2og12;
            alarmutility2og13Variable.Value = alarmutility2og13;
            alarmsparein2og14Variable.Value = alarmsparein2og14;
            alarmsppshopin2og15Variable.Value  = alarmsppshopin2og15;
            alarmsppshopin1og16Variable.Value = alarmsppshopin1og16;
            alarmebin1Variable.Value = alarmebin1;
            alarmdg1in1Variable.Value = alarmdg1in1; 






        }
    }


    private PeriodicTask periodicTask;
    private IUAVariable buttonVariable;
    private IUAVariable dateVariable;
    private IUAVariable avgconsumptionincomer1Variable;
    private IUAVariable avgconsumptionincomer2Variable;
    private IUAVariable avgconsumptionsparein1og1Variable;
    private IUAVariable avgconsumptionutilityin1og2Variable;
    private IUAVariable avgconsumptionstampingin1og3Variable;
    private IUAVariable avgconsumptiontcfin1og4Variable;
    private IUAVariable avgconsumptionbodyshopin1og5Variable;
    private IUAVariable avgconsumptionengineshopin1og6Variable;
    private IUAVariable avgconsumptionpaintshopin1og7Variable;
    private IUAVariable avgconsumptionpaintshopin1og8Variable;
    private IUAVariable avgconsumptionengineshopin2og9Variable;
    private IUAVariable avgconsumptionbodyshopin2og10Variable;
    private IUAVariable avgconsumptiontcf2og11Variable;
    private IUAVariable avgconsumptionstampingin2og12Variable;
    private IUAVariable avgconsumptionutility2og13Variable;
    private IUAVariable avgconsumptionsparein2og14Variable;
    private IUAVariable avgconsumptionsppshopin2og15Variable;
    private IUAVariable avgconsumptionsppshopin1og16Variable;
    private IUAVariable avgconsumptionebin1Variable;
    private IUAVariable avgconsumptiondg1in1Variable;



    private IUAVariable avgconsumptionincomer1newVariable;
    private IUAVariable avgconsumptionincomer2newVariable;
    private IUAVariable avgconsumptionsparein1og1newVariable;
    private IUAVariable avgconsumptionutilityin1og2newVariable;
    private IUAVariable avgconsumptionstampingin1og3newVariable;
    private IUAVariable avgconsumptiontcfin1og4newVariable;
    private IUAVariable avgconsumptionbodyshopin1og5newVariable;
    private IUAVariable avgconsumptionengineshopin1og6newVariable;
    private IUAVariable avgconsumptionengineshopin1og1newVariable;
    private IUAVariable avgconsumptionpaintshopin1og7newVariable;
    private IUAVariable avgconsumptionpaintshopin1og8newVariable;
    private IUAVariable avgconsumptionengineshopin2og9newVariable;
    private IUAVariable avgconsumptionbodyshopin2og10newVariable;
    private IUAVariable avgconsumptiontcf2og11newVariable;
    private IUAVariable avgconsumptionstampingin2og12newVariable;
    private IUAVariable avgconsumptionutility2og13newVariable;
    private IUAVariable avgconsumptionsparein2og14newVariable;
    private IUAVariable avgconsumptionsppshopin2og15newVariable;
    private IUAVariable avgconsumptionsppshopin1og16newVariable;
    private IUAVariable avgconsumptionebin1newVariable;
    private IUAVariable avgconsumptiondg1in1newVariable;

    private IUAVariable alarmincomer1Variable;
    private IUAVariable alarmincomer2Variable;
    private IUAVariable alarmsparein1og1Variable;
    private IUAVariable alarmutilityin1og2Variable;
    private IUAVariable alarmstampingin1og3Variable;
    private IUAVariable alarmtcfin1og4Variable;
    private IUAVariable alarmbodyshopin1og5Variable;
    private IUAVariable alarmengineshopin1og6Variable;
    private IUAVariable alarmpaintshopin1og7Variable;
    private IUAVariable alarmpaintshopin1og8Variable;
    private IUAVariable alarmengineshopin2og9Variable;
    private IUAVariable alarmbodyshopin2og10Variable;
    private IUAVariable alarmtcf2og11Variable;
    private IUAVariable alarmstampingin2og12Variable;
    private IUAVariable alarmutility2og13Variable;
    private IUAVariable alarmsparein2og14Variable;
    private IUAVariable alarmsppshopin2og15Variable;
    private IUAVariable alarmsppshopin1og16Variable;
    private IUAVariable alarmebin1Variable;
    private IUAVariable alarmdg1in1Variable;
}





