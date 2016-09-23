using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Microsoft.SharePoint;

// References:
// http://blog.dotnetstep.in/2009/01/batch-update-in-sharepoint.html
// http://www.codeproject.com/Articles/29813/Parallel-Computations-in-C

namespace PerformanceTest
{
    public partial class Form1 : Form
    {
        public const string _ListNamePerformanceTest = @"PerformanceTest";
        public const string _FieldName_1 = @"TextField1";
        public const string _FieldName_2 = @"TextField2";
        public const string _FieldName_3 = @"TextField3";
        public const string _FieldName_4 = @"TextField4";
        public const string _FieldName_5 = @"TextField5";
        public const string _FieldPrefix = @"urn:schemas-microsoft-com:office:office#";

        DateTime _PerformanceStartTime = DateTime.MinValue;
        DateTime _PerformanceEndTime = DateTime.MinValue;

        //private void writeLogDot()
        //{
            //if (maskedTextBoxThreadNumber.Text == "1")
            //{
            //    textBoxPerformanceLog.Text = "." + textBoxPerformanceLog.Text;
            //    Application.DoEvents();
            //}
        //}

        private void writeLog(string strLog)
        {
            if (string.IsNullOrEmpty(strLog))
            {
                textBoxPerformanceLog.Text = Environment.NewLine + textBoxPerformanceLog.Text;
            }
            else
            {
                textBoxPerformanceLog.Text = string.Format("{2}{0} - {1}{3}", DateTime.Now.ToString("hh:mm:ss"), strLog, Environment.NewLine, textBoxPerformanceLog.Text);
            }
            Application.DoEvents();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshStatus();
            }
            catch (Exception ex)
            {
                ;
            }
        }

        private int PerformanceGetRowNumber()
        {
            int iRowNumber = int.MinValue;
            int.TryParse(textBoxPerformanceRowNumber.Text, out iRowNumber);

            return iRowNumber;
        }

        private void PerformanceRefreshRowCount()
        {
            using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
            {
                using (SPWeb objSPWeb = objSPSite.OpenWeb())
                {
                    SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                    if (objSPListPerformanceTest != null)
                    {
                        int iRowCount = objSPListPerformanceTest.ItemCount;
                        textBoxPerformanceRowCount.Text = iRowCount.ToString();
                    }
                    else
                    {
                        textBoxPerformanceRowCount.Text = @"N/A";
                    }
                }
            }
        }

        private int GetStartID()
        {
            int iReturnID = -1;
            using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
            {
                using (SPWeb objSPWeb = objSPSite.OpenWeb())
                {
                    SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                    if (objSPListPerformanceTest != null)
                    {
                        iReturnID = objSPListPerformanceTest.Items[0].ID;
                    }
                }
            }

            return iReturnID;
        }

        private void PerformanceCalculate(DateTime objStart, DateTime objEnd, int iRowNumber)
        {
            TimeSpan objTimeSpan = new TimeSpan(objEnd.Ticks - objStart.Ticks);
            string strTmp = string.Empty;
            string strBatch = string.Empty;
            if (checkBoxBatch.Checked)
            {
                strBatch = "(batch)";
            }
            if (objTimeSpan.TotalMilliseconds > 0)
            {
                double doublePerformance = (double)(iRowNumber * 1000) / objTimeSpan.TotalMilliseconds;
                strTmp = string.Format(@"StartTime:{0}, EndTime:{1}, RowNumber:{2}, Performance:{3} (rows/second), TimeSpan:{4} seconds", objStart.ToLongTimeString(), objEnd.ToLongTimeString(), iRowNumber, doublePerformance.ToString("0.00"), objTimeSpan.TotalSeconds.ToString("0.00"));
                writeLog(strBatch + strTmp);
                writeLog(string.Empty);
            }
        }

        public void RefreshStatus()
        {
            bool boolExists = false;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (textBoxSiteURL.Text.Contains(@"{local}"))
                {
                    textBoxSiteURL.Text = textBoxSiteURL.Text.Replace(@"{local}", Environment.MachineName);
                }
                using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
                {
                    using (SPWeb objSPWeb = objSPSite.OpenWeb())
                    {
                        SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                        if (objSPListPerformanceTest != null)
                            boolExists = true;
                        else
                            boolExists = false;

                        checkBoxPerformanceExist.Checked = boolExists;
                        if (boolExists)
                            textBoxPerformanceRowCount.Text = objSPListPerformanceTest.ItemCount.ToString();
                        else
                            textBoxPerformanceRowCount.Text = @"0";
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                Cursor.Current = Cursors.Arrow;
            }
        }

        private void buttonPerformanceRecreate_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
                {
                    using (SPWeb objSPWeb = objSPSite.OpenWeb())
                    {
                        SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                        if (objSPListPerformanceTest != null)
                        {
                            writeLog(@"Deleting existing list (" + _ListNamePerformanceTest + ") begin...");
                            
                            objSPListPerformanceTest.Delete();
                            writeLog(@"...completed.");
                            writeLog(string.Empty);
                        }
                        writeLog(@"Creating list (" + _ListNamePerformanceTest + ") begin...");
                        
                        Guid newGuid = objSPWeb.Lists.Add(_ListNamePerformanceTest, _ListNamePerformanceTest, SPListTemplateType.GenericList);
                        SPList objSPList = objSPWeb.Lists[newGuid];
                        objSPList.Fields.Add(_FieldName_1, SPFieldType.Text, false);
                        objSPList.Fields.Add(_FieldName_2, SPFieldType.Text, false);
                        objSPList.Fields.Add(_FieldName_3, SPFieldType.Text, false);
                        objSPList.Fields.Add(_FieldName_4, SPFieldType.Text, false);
                        objSPList.Fields.Add(_FieldName_5, SPFieldType.Text, false);
                        objSPList.EnableAttachments = false;
                        objSPList.OnQuickLaunch = true;
                        objSPList.Update();

                        SPView view = objSPList.DefaultView;
                        view.ViewFields.Add(_FieldName_1);
                        view.ViewFields.Add("ID");
                        //view.ViewFields.Add("Created");
                        //view.ViewFields.Add("Modified");
                        view.RowLimit = 2000;
                        view.Aggregations = "<FieldRef Name='ID' Type='COUNT'/>";
                        view.AggregationsStatus = "On";
                        view.Update();

                        writeLog(@"...completed.");
                        writeLog(string.Empty);
                    }
                }

                RefreshStatus();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                Cursor.Current = Cursors.Arrow;
            }
        }

        public int insertSPListItemBatch(int iThreadId)
        {
            int iRowNumber = PerformanceGetRowNumber();
            if (iRowNumber < 0)
            {
                writeLog("Cannot insert zero items. Exit.");
                return 0;
            }

            string strValue = "insert-" + iThreadId.ToString() + @"-" + DateTime.Now.ToLongTimeString();
            StringBuilder query = new StringBuilder();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
                {
                    using (SPWeb objSPWeb = objSPSite.OpenWeb())
                    {
                        SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                        Guid strListGUID = objSPListPerformanceTest.ID;

                        for (int i = 0; i < iRowNumber; i++)
                        {
                            //string strValue = "insert(" + DateTime.Now.ToLongTimeString() + @"):: " + i.ToString();
                            query.AppendFormat("<Method ID=\"{0}\">" +
                                    "<SetList>{1}</SetList>" +
                                    "<SetVar Name=\"ID\">New</SetVar>" +
                                    "<SetVar Name=\"Cmd\">Save</SetVar>" +
                                    "<SetVar Name=\"{3}Title\">{2}</SetVar>" +
                                    "<SetVar Name=\"{3}TextField1\">{2}</SetVar>" +
                                    "<SetVar Name=\"{3}TextField2\">{2}</SetVar>" +
                                    "<SetVar Name=\"{3}TextField3\">{2}</SetVar>" +
                                    "<SetVar Name=\"{3}TextField4\">{2}</SetVar>" +
                                    "<SetVar Name=\"{3}TextField5\">{2}</SetVar>" +
                                 "</Method>", i, strListGUID, strValue, _FieldPrefix);
                        }
                        objSPWeb.ProcessBatchData(string.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                            "<ows:Batch OnError=\"Return\">{0}</ows:Batch>", query.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return iRowNumber;
        }

        public void insertSPListItemSingle(int iThreadId)
        {
            int iRowNumber = PerformanceGetRowNumber();
            string strValue = "insert-" + iThreadId.ToString() + @"-" + DateTime.Now.ToLongTimeString();

            using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
            {
                using (SPWeb objSPWeb = objSPSite.OpenWeb())
                {
                    SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                    //SPListItemCollection objSPListItemCollection = objSPListPerformanceTest.Items;

                    SPListItem objSPListItem = null;
                    for (int i = 0; i < iRowNumber; i++)
                    {
                        objSPListItem = objSPListPerformanceTest.AddItem();
                        objSPListItem[SPBuiltInFieldId.Title] = strValue;
                        objSPListItem[_FieldName_1] = strValue;
                        objSPListItem[_FieldName_2] = strValue;
                        objSPListItem[_FieldName_3] = strValue;
                        objSPListItem[_FieldName_4] = strValue;
                        objSPListItem[_FieldName_5] = strValue;
                        //objSPListItem.SystemUpdate(false);
                        objSPListItem.Update();

                        //if (i % 100 == 0)
                        //{
                        //    writeLogDot();
                        //}
                    }
                }
            }
        }

        private void buttonPerformanceInsert_Click(object sender, EventArgs e)
        {
            if (false == checkBoxPerformanceExist.Checked) return;

            int iThreadNumber = int.Parse(maskedTextBoxThreadNumber.Text);
            bool boolBatch = checkBoxBatch.Checked;

            //textBoxPerformanceLog.Text = string.Empty;
            int iRowNumber = PerformanceGetRowNumber();
            if (iRowNumber < 0)
            {
                writeLog("Cannot insert zero items. Exit.");
                return;
            }

            _PerformanceStartTime = DateTime.Now;
            writeLog(string.Format(@"Insert {0} rows, begin at {1}), in {2} threads...",
                iRowNumber * iThreadNumber, _PerformanceStartTime.ToLongTimeString(), iThreadNumber));

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                buttonPerformanceInsert.Enabled = false;

                AForge.Parallel.For(0, iThreadNumber, delegate(int i)
                {
                    if (boolBatch)
                    {
                        insertSPListItemBatch(i);
                    }
                    else
                    {
                        insertSPListItemSingle(i);
                    }
                });

                iRowNumber *= iThreadNumber;

                PerformanceRefreshRowCount();
                _PerformanceEndTime = DateTime.Now;
                writeLog(string.Format(@"Insert completed at {0}, {1} rows get inserted.", _PerformanceEndTime.ToLongTimeString(), iRowNumber));
                PerformanceCalculate(_PerformanceStartTime, _PerformanceEndTime, iRowNumber);

                RefreshStatus();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                buttonPerformanceInsert.Enabled = true;
                Cursor.Current = Cursors.Arrow;
            }
        }

        public int retrieveSPListItemSingle(int iThreadId)
        {
            int iBatchCount = PerformanceGetRowNumber();
            if (iBatchCount <= 0)
            {
                writeLog("Cannot retrieve zero items. Exit.");
                return -1;
            }

            int iPositionStart = iBatchCount * iThreadId;
            int iPositionEnd = iBatchCount * (iThreadId + 1);
            int iMethodId = 0;
            string strValue = string.Empty;

            using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
            {
                using (SPWeb objSPWeb = objSPSite.OpenWeb())
                {
                    SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                    SPListItemCollection objSPListItemCollection = objSPListPerformanceTest.Items;
                    SPListItem objSPListItem = null;
                    if (iPositionEnd > objSPListPerformanceTest.ItemCount)
                        iPositionEnd = objSPListPerformanceTest.ItemCount;
                    if (iPositionEnd < iPositionStart)
                    {
                        return 0;
                    }

                    for (int i = iPositionStart; i < iPositionEnd; i++)
                    {
                        iMethodId++;
                        objSPListItem = objSPListItemCollection[i];

                        strValue = objSPListItem[_FieldName_1].ToString();
                        strValue += objSPListItem[_FieldName_2].ToString();
                        strValue += objSPListItem[_FieldName_3].ToString();
                        strValue += objSPListItem[_FieldName_4].ToString();
                        strValue += objSPListItem[_FieldName_5].ToString();
                    }
                }
            }

            return iMethodId;
        }

        private void buttonPerformanceRetrieve_Click(object sender, EventArgs e)
        {
            if (false == checkBoxPerformanceExist.Checked) return;

            int iThreadNumber = int.Parse(maskedTextBoxThreadNumber.Text);
            bool boolBatch = checkBoxBatch.Checked;

            //textBoxPerformanceLog.Text = string.Empty;
            int iRowNumber = PerformanceGetRowNumber();
            if (iRowNumber <= 0)
            {
                writeLog("Cannot retrieve zero items. Exit.");
                return;
            }

            _PerformanceStartTime = DateTime.Now;
            writeLog(@"Retrieve (single mode only) " + textBoxPerformanceRowNumber.Text + " rows begin(" + _PerformanceStartTime.ToLongTimeString() + @")...");
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                buttonPerformanceRetrieve.Enabled = false;

                AForge.Parallel.For(0, iThreadNumber, delegate(int i)
                {
                    retrieveSPListItemSingle(i);
                });

                iRowNumber *= iThreadNumber;

                PerformanceRefreshRowCount();
                _PerformanceEndTime = DateTime.Now;
                writeLog(string.Format(@"Retrieve completed at {0}, {1} rows get retrieved.", _PerformanceEndTime.ToLongTimeString(), iRowNumber));
                PerformanceCalculate(_PerformanceStartTime, _PerformanceEndTime, iRowNumber);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                buttonPerformanceRetrieve.Enabled = true;
                Cursor.Current = Cursors.Arrow;
            }
        }

        public int updateSPListItemBatch(int iThreadId, int iStartID)
        {
            int iBatchCount = PerformanceGetRowNumber();
            if (iBatchCount <= 0)
            {
                writeLog("Cannot update zero items. Exit.");
                return -1;
            }

            int iPositionStart = iBatchCount * iThreadId;
            int iPositionEnd = iBatchCount * (iThreadId + 1);
            int iMethodId = 0;
            string strValue = "update-" + iThreadId.ToString() + @"-" + DateTime.Now.ToLongTimeString();

            StringBuilder sbDelete = new StringBuilder();
            sbDelete.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Batch>");

            using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
            {
                using (SPWeb objSPWeb = objSPSite.OpenWeb())
                {
                    SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                    SPListItemCollection objSPListItemCollection = objSPListPerformanceTest.Items;
                    string strListGUID = objSPListItemCollection.List.ID.ToString();
                    if (iPositionEnd > objSPListPerformanceTest.ItemCount)
                        iPositionEnd = objSPListPerformanceTest.ItemCount;
                    if (iPositionEnd < iPositionStart)
                    {
                        return 0;
                    }

                    for (int i = iPositionStart; i < iPositionEnd; i++)
                    {
                        iMethodId++;
                        sbDelete.AppendFormat("<Method ID=\"{0}\">" +
                            "<SetList>{1}</SetList>" +
                            "<SetVar Name=\"ID\">{2}</SetVar>" +
                            "<SetVar Name=\"Cmd\">Update</SetVar>" +
                            "<SetVar Name=\"{4}TextField1\">{3}</SetVar>" +
                            "<SetVar Name=\"{4}TextField2\">{3}</SetVar>" +
                            "<SetVar Name=\"{4}TextField3\">{3}</SetVar>" +
                            "<SetVar Name=\"{4}TextField4\">{3}</SetVar>" +
                            "<SetVar Name=\"{4}TextField5\">{3}</SetVar>" +
                            "</Method>", iMethodId, strListGUID, iStartID + i, strValue, _FieldPrefix);
                    }

                    sbDelete.Append("</Batch>");

                    try
                    {
                        objSPWeb.ProcessBatchData(sbDelete.ToString());
                    }
                    catch (Exception ex)
                    {
                        writeLog("updateSPListItemBatch, Delete failed: " + ex.Message);
                        throw;
                    }
                }
            }

            return iMethodId;
        }

        public int updateSPListItemSingle(int iThreadId, int iStartID)
        {
            int iBatchCount = PerformanceGetRowNumber();
            if (iBatchCount <= 0)
            {
                writeLog("Cannot update zero items. Exit.");
                return -1;
            }

            int iPositionStart = iBatchCount * iThreadId;
            int iPositionEnd = iBatchCount * (iThreadId + 1);
            int iMethodId = 0;
            string strValue = "update-" + iThreadId.ToString() + @"-" + DateTime.Now.ToLongTimeString();

            using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
            {
                using (SPWeb objSPWeb = objSPSite.OpenWeb())
                {
                    SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                    SPListItemCollection objSPListItemCollection = objSPListPerformanceTest.Items;
                    SPListItem objSPListItem = null;
                    if (iPositionEnd > objSPListPerformanceTest.ItemCount)
                        iPositionEnd = objSPListPerformanceTest.ItemCount;
                    if (iPositionEnd < iPositionStart)
                    {
                        return 0;
                    }

                    for (int i = iPositionStart; i < iPositionEnd; i++)
                    {
                        iMethodId++;
                        objSPListItem = objSPListPerformanceTest.GetItemById(iStartID + i);
                        objSPListItem[_FieldName_1] = strValue;
                        objSPListItem[_FieldName_2] = strValue;
                        objSPListItem[_FieldName_3] = strValue;
                        objSPListItem[_FieldName_4] = strValue;
                        objSPListItem[_FieldName_5] = strValue;
                        objSPListItem.SystemUpdate(false);

                        //if (i % 100 == 0)
                        //{
                        //    writeLogDot();
                        //}
                    }
                }
            }

            return iMethodId;
        }

        private void buttonPerformanceUpdate_Click(object sender, EventArgs e)
        {
            if (false == checkBoxPerformanceExist.Checked) return;

            //textBoxPerformanceLog.Text = string.Empty;
            int iRowNumber = PerformanceGetRowNumber();
            if (iRowNumber <= 0)
            {
                writeLog("No data found. Update exit.");
                return;
            }

            int iThreadNumber = int.Parse(maskedTextBoxThreadNumber.Text);
            bool boolBatch = checkBoxBatch.Checked;

            _PerformanceStartTime = DateTime.Now;
            writeLog(string.Format(@"Update {0} rows, begin at {1}), in {2} threads...",
                iRowNumber * iThreadNumber, _PerformanceStartTime.ToLongTimeString(), iThreadNumber));

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                buttonPerformanceUpdate.Enabled = false;

                int iStartID = GetStartID();

                AForge.Parallel.For(0, iThreadNumber, delegate(int i)
                {
                    if (boolBatch)
                    {
                        updateSPListItemBatch(i, iStartID);
                    }
                    else
                    {
                        updateSPListItemSingle(i, iStartID);
                    }
                });

                iRowNumber *= iThreadNumber;

                PerformanceRefreshRowCount();
                _PerformanceEndTime = DateTime.Now;
                writeLog(string.Format(@"Update completed at {0}, {1} rows get updated.", _PerformanceEndTime.ToLongTimeString(), iRowNumber));
                PerformanceCalculate(_PerformanceStartTime, _PerformanceEndTime, iRowNumber);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                buttonPerformanceUpdate.Enabled = true;
                Cursor.Current = Cursors.Arrow;
            }
        }

        public int deleteSPListItemBatch(int iThreadId, int iStartID)
        {
            int iBatchCount = PerformanceGetRowNumber();
            if (iBatchCount <= 0)
            {
                writeLog("Cannot delete zero items. Exit.");
                return -1;
            }

            int iPositionStart = iBatchCount * iThreadId;
            int iPositionEnd = iBatchCount * (iThreadId + 1);
            int iMethodId = 0;

            StringBuilder sbDelete = new StringBuilder();
            sbDelete.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Batch>");

            using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
            {
                using (SPWeb objSPWeb = objSPSite.OpenWeb())
                {
                    SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                    SPListItemCollection objSPListItemCollection = objSPListPerformanceTest.Items;
                    string strListGUID = objSPListItemCollection.List.ID.ToString();

                    if (iPositionEnd > objSPListPerformanceTest.ItemCount)
                        iPositionEnd = objSPListPerformanceTest.ItemCount;
                    if (iPositionEnd < iPositionStart)
                    {
                        return 0;
                    }

                    try
                    {
                        for (int i = iPositionStart; i < iPositionEnd; i++)
                        {
                            iMethodId++;
                            sbDelete.Append("<Method>");
                            sbDelete.Append("<SetList Scope=\"Request\">" + strListGUID + "</SetList>");
                            sbDelete.Append("<SetVar Name=\"ID\">" + Convert.ToString(iStartID + i) + "</SetVar>");
                            sbDelete.Append("<SetVar Name=\"Cmd\">Delete</SetVar>");
                            sbDelete.Append("</Method>");
                        }

                        sbDelete.Append("</Batch>");
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }

                    try
                    {
                        objSPWeb.ProcessBatchData(sbDelete.ToString());
                    }
                    catch (Exception ex)
                    {
                        writeLog("deleteSPListItemBatch, Delete failed: " + ex.Message);
                        throw;
                    }
                }
            }

            return iMethodId;
        }

        public int deleteSPListItemSingle(int iThreadId, int iStartID)
        {
            int iBatchCount = PerformanceGetRowNumber();
            if (iBatchCount <= 0)
            {
                writeLog("Cannot delete zero items. Exit.");
                return -1;
            }

            int iPositionStart = iBatchCount * iThreadId;
            int iPositionEnd = iBatchCount * (iThreadId + 1);
            int iMethodId = 0;

            using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
            {
                using (SPWeb objSPWeb = objSPSite.OpenWeb())
                {
                    SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                    //SPListItemCollection objSPListItemCollection = objSPListPerformanceTest.Items;
                    if (iPositionEnd > objSPListPerformanceTest.ItemCount)
                        iPositionEnd = objSPListPerformanceTest.ItemCount;
                    if (iPositionEnd < iPositionStart)
                    {
                        return 0;
                    }

                    try
                    {
                        for (int i = iPositionStart; i < iPositionEnd; i++)
                        {
                            iMethodId++;
                            objSPListPerformanceTest.GetItemById(iStartID + i).Delete();
                        }
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                }
            }

            return iMethodId;
        }

        private void buttonPerformanceDelete_Click(object sender, EventArgs e)
        {
            int iRowNumber = PerformanceGetRowNumber();
            if (false == checkBoxPerformanceExist.Checked) return;

            int iThreadNumber = int.Parse(maskedTextBoxThreadNumber.Text);
            bool boolBatch = checkBoxBatch.Checked;

            //textBoxPerformanceLog.Text = string.Empty;

            _PerformanceStartTime = DateTime.Now;
            writeLog(string.Format(@"Delete {0} rows, begin at {1}), in {2} threads...",
                iRowNumber * iThreadNumber, _PerformanceStartTime.ToLongTimeString(), iThreadNumber));

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                buttonPerformanceDelete.Enabled = false;

                int iStartID = GetStartID();

                AForge.Parallel.For(0, iThreadNumber, delegate(int i)
                {
                    if (boolBatch)
                    {
                        deleteSPListItemBatch(i, iStartID);
                    }
                    else
                    {
                        deleteSPListItemSingle(i, iStartID);
                    }
                });

                iRowNumber *= iThreadNumber;

                PerformanceRefreshRowCount();
                _PerformanceEndTime = DateTime.Now;
                writeLog(string.Format(@"Delete completed at {0}, {1} rows get deleted.", _PerformanceEndTime.ToLongTimeString(), iRowNumber));
                PerformanceCalculate(_PerformanceStartTime, _PerformanceEndTime, iRowNumber);

                RefreshStatus();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                buttonPerformanceDelete.Enabled = true;
                Cursor.Current = Cursors.Arrow;
            }
        }

        private void buttonPerformanceAll_Click(object sender, EventArgs e)
        {
            if (false == checkBoxPerformanceExist.Checked) return;

            buttonClearLog_Click(sender, e);

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                buttonPerformanceInsert_Click(sender, e);
                buttonPerformanceRetrieve_Click(sender, e);
                buttonPerformanceUpdate_Click(sender, e);
                buttonPerformanceDelete_Click(sender, e);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                Cursor.Current = Cursors.Arrow;
            }
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            textBoxPerformanceLog.Text = string.Empty;
        }

        private void textBoxSiteURL_TextChanged(object sender, EventArgs e)
        {
            RefreshStatus();
        }

        private void checkBoxPerformanceExist_CheckedChanged(object sender, EventArgs e)
        {
            bool boolExists = checkBoxPerformanceExist.Checked;

            buttonPerformanceInsert.Enabled = boolExists;
            buttonPerformanceDelete.Enabled = boolExists;
            buttonPerformanceUpdate.Enabled = boolExists;
            buttonPerformanceAll.Enabled = boolExists;
        }
    }
}
