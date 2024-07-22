#region Using directives
using System;
using UAManagedCore;
using FTOptix.Core;
using FTOptix.NetLogic;
using System.IO;
using System.Reflection;
using FTOptix.ODBCStore;
using FTOptix.OPCUAServer;
using FTOptix.MicroController;
using FTOptix.CommunicationDriver;
using FTOptix.UI;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.Modbus;
using FTOptix.RAEtherNetIP;
#endregion

public class ManageRemotePdf : BaseNetLogic
{
    private LongRunningTask longRunningTask;
    private const string PDF_FOLDER_NAME = "%PROJECTDIR%\\pdf_from_remote";
    private const string PDF_VIEWER_PDF_SERVER_PATH = "PdfServerPath";
    private const string PDF_VIEWER_IS_NATIVE_UI = "IsNativeUI";
    private DirectoryInfo pdfLocalFolder;
    private string pdfFilename;

    public override void Start()
    {
        RemoveUndeletedPdf();
        LoadPdf();
    }

    public override void Stop()
    {
        DeleteLocalPdf();
        if (longRunningTask == null) return;
        longRunningTask.Dispose();
    }

    [ExportMethod]
    public void LoadPdf()
    {
        try
        {
            var isNativeUI = bool.Parse(Owner.GetVariable(PDF_VIEWER_IS_NATIVE_UI).Value);
            if (isNativeUI) return;
            longRunningTask = new LongRunningTask(DownloadPdfFromRemoteAndShow, LogicObject);
            longRunningTask.Start();
        }
        catch (System.Exception ex)
        {
            Log.Error(MethodBase.GetCurrentMethod().Name, ex.Message);
        }
    }

    private void DownloadPdfFromRemoteAndShow()
    {
        try
        {
            var pdfViewer = (RemotePDFViewer)Owner;

            CreatePdfLocalFolder();

            var pdfRemotePath = new ResourceUri(Owner.GetVariable(PDF_VIEWER_PDF_SERVER_PATH).Value);
            pdfFilename = DateTime.Now.Ticks + Path.GetFileName(pdfRemotePath.Uri);
            var pdfLocalPath = new ResourceUri(PDF_FOLDER_NAME + "/" + pdfFilename).Uri;

            if (!CopyPdfToProjectDir(pdfRemotePath, pdfLocalPath)) return;
            pdfViewer.Path = "ns=7;" + PDF_FOLDER_NAME + "\\" + pdfFilename;
        }
        catch (System.Exception ex)
        {
            Log.Error(MethodBase.GetCurrentMethod().Name, ex.Message);
        }
    }

    private void CreatePdfLocalFolder()
    {
        try
        {
            var pdfLocalFolderUri = new ResourceUri(PDF_FOLDER_NAME).Uri;
            pdfLocalFolder = System.IO.Directory.CreateDirectory(pdfLocalFolderUri);
        }
        catch (System.Exception ex)
        {
            Log.Error(MethodBase.GetCurrentMethod().Name, ex.Message);
        }
    }

    private bool CopyPdfToProjectDir(ResourceUri sourcePath, string destinationPath)
    {
        try
        {
            File.Copy(sourcePath.Uri, destinationPath);
            return true;
        }
        catch (System.Exception ex)
        {
            Log.Error(MethodBase.GetCurrentMethod().Name, ex.Message);
            return false;
        }
    }

    private void DeleteLocalPdf()
    {
        try
        {
            foreach (var item in pdfLocalFolder.GetFiles())
            {
                if (item.Name.Equals(pdfFilename))
                {
                    item.Delete();
                    break;
                }
            }
        }
        catch (System.Exception ex)
        {
            Log.Error(MethodBase.GetCurrentMethod().Name, ex.Message);
        }
    }

    private void RemoveUndeletedPdf()
    {
        try
        {
            CreatePdfLocalFolder();
            foreach (var item in pdfLocalFolder.GetFiles())
            {
                if (item.LastAccessTime < DateTime.Now.AddHours(-1))
                {
                    item.Delete();
                }
            }
        }
        catch (System.Exception ex)
        {
            Log.Error(MethodBase.GetCurrentMethod().Name, ex.Message);
        }
    }

}
