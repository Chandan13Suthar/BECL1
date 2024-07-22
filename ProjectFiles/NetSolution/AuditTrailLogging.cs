#region Using directives
using System;
using UAManagedCore;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.Store;
using FTOptix.Recipe;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.WebUI;
using FTOptix.UI;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class AuditTrailLogging : BaseNetLogic
{
    // Logging into Audit Database
    [ExportMethod]
	public void LogIntoAudit(string clSourceName, string clMessage, string curruser, string clEventName)
	{
		Store myStore = Project.Current.Get<Store>("DataStores/EDB_AuditTrail");
        Table myTable = myStore.Tables.Get<Table>("SigningEventLogger");

        object[,] rawValues = new object [1,10]; // [Raw, Column]; Column = number columns in Table of Audit event logger  database 
        rawValues[0,0] = DateTime.Now;
        rawValues[0,1] = clSourceName;
        rawValues[0,2] = clMessage;
        rawValues[0,3] = "";
        rawValues[0,4] = "";
        rawValues[0,5] = curruser;
        rawValues[0,6] = "";
        rawValues[0,7] = "";
        rawValues[0,8] = "";
        rawValues[0,9] = clEventName;
        string[] columns = new string[10] {"LocalTimeStamp", "SourceName", "Message", "WorkflowType", "SignResult", "ClientUserId", "ClientUserNote", "SecondClientUserId", "SecondClientUserNote", "EventType"};
        myTable.Insert(columns,rawValues);
	}

}
