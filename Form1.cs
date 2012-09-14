using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using System.Threading;

using Microsoft.SharePoint;

namespace PerformanceTest
{
    public partial class Form1 : Form
    {
        private string _ListNamePerformanceTest = @"PerformanceTest";
        DateTime _PerformanceStartTime = DateTime.MinValue;
        DateTime _PerformanceEndTime = DateTime.MinValue;

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

        private void writeLogDot()
        {
            //if (maskedTextBoxThreadNumber.Text == "1")
            //{
            //    textBoxPerformanceLog.Text = "." + textBoxPerformanceLog.Text;
            //    Application.DoEvents();
            //}
        }

        private void writeLog(string strLog)
        {
            textBoxPerformanceLog.Text = string.Format("{2}{0} - {1}{3}", DateTime.Now.ToString("hh:mm:ss"), strLog, Environment.NewLine, textBoxPerformanceLog.Text);
            Application.DoEvents();
        }

        private int PerformanceGetRowNumber()
        {
            int iRowNumber = int.MinValue;
            int.TryParse(maskedTextBoxPerformanceRowNumber.Text, out iRowNumber);

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
            }
        }

        public void RefreshStatus()
        {
            bool boolExists = false;

            try
            {
                Cursor.Current = Cursors.WaitCursor;

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
                        buttonPerformanceInsert.Enabled = boolExists;
                        buttonPerformanceDelete.Enabled = boolExists;
                        buttonPerformanceUpdate.Enabled = boolExists;
                        buttonPerformanceAll.Enabled = boolExists;
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
                            writeLog(@"Delete existing list (" + _ListNamePerformanceTest + ") begin...");
                            Application.DoEvents();
                            objSPListPerformanceTest.Delete();
                            writeLog(@"...complete.");
                        }
                        writeLog(@"create list (" + _ListNamePerformanceTest + ") begin...");
                        Application.DoEvents();
                        Guid newGuid = objSPWeb.Lists.Add(_ListNamePerformanceTest, _ListNamePerformanceTest, SPListTemplateType.GenericList);
                        SPList objSPList = objSPWeb.Lists[newGuid];
                        objSPList.Fields.Add("TextField1", SPFieldType.Text, false);
                        objSPList.Fields.Add("TextField2", SPFieldType.Text, false);
                        objSPList.Fields.Add("TextField3", SPFieldType.Text, false);
                        objSPList.Fields.Add("TextField4", SPFieldType.Text, false);
                        objSPList.Fields.Add("TextField5", SPFieldType.Text, false);
                        objSPList.Update();
                        writeLog(@"...complete");
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

        public void insertSPListItemBatch()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
                {
                    using (SPWeb objSPWeb = objSPSite.OpenWeb())
                    {
                        SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                        insertSPListItemBatch(objSPWeb, objSPListPerformanceTest);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            //return iRowNumber;
        }

        public int insertSPListItemBatch(SPWeb objSPWeb, SPList objSPList)
        {
            int iRowNumber = PerformanceGetRowNumber();
            if (iRowNumber < 0)
            {
                writeLog("No data found. Insert exit.");
                return 0;
            }

            StringBuilder query = new StringBuilder();
            Guid guidListID = objSPList.ID;
            for (int i = 0; i < iRowNumber; i++)
            {
                //string strValue = "insert(" + DateTime.Now.ToLongTimeString() + @"):: " + i.ToString();
                query.AppendFormat("<Method ID=\"{0}\">" +
                        "<SetList>{1}</SetList>" +
                        "<SetVar Name=\"ID\">New</SetVar>" +
                        "<SetVar Name=\"Cmd\">Save</SetVar>" +
                        "<SetVar Name=\"{3}TextField1\">{2}</SetVar>" +
                        "<SetVar Name=\"{3}TextField2\">{2}</SetVar>" +
                        "<SetVar Name=\"{3}TextField3\">{2}</SetVar>" +
                        "<SetVar Name=\"{3}TextField4\">{2}</SetVar>" +
                        "<SetVar Name=\"{3}TextField5\">{2}</SetVar>" +
                     "</Method>", i, guidListID, "strValue", "urn:schemas-microsoft-com:office:office#");
            }
            objSPWeb.ProcessBatchData(string.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<ows:Batch OnError=\"Return\">{0}</ows:Batch>", query.ToString()));

            return 0;
        }

        public void insertSPListItemSingle()
        {
            int iRowNumber = PerformanceGetRowNumber();

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
                        string strValue = "insert(" + DateTime.Now.ToLongTimeString() + @"):: " + i.ToString();
                        objSPListItem["TextField1"] = strValue;
                        objSPListItem["TextField2"] = strValue;
                        objSPListItem["TextField3"] = strValue;
                        objSPListItem["TextField4"] = strValue;
                        objSPListItem["TextField5"] = strValue;
                        //objSPListItem.SystemUpdate(false);
                        objSPListItem.Update();

                        if (i % 100 == 0)
                        {
                            writeLogDot();
                        }
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
                writeLog("No data found. Insert exit.");
                return;
            }

            _PerformanceStartTime = DateTime.Now;
            writeLog(string.Format(@"Insert {0} rows begin at {1}), in {2} threads...",
                iRowNumber, _PerformanceStartTime.ToLongTimeString(), iThreadNumber));
            Application.DoEvents();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                AForge.Parallel.For(0, iThreadNumber, delegate(int i)
                {
                    if (boolBatch)
                    {
                        insertSPListItemBatch();
                    }
                    else
                    {
                        insertSPListItemSingle();
                    }
                });

                iRowNumber *= iThreadNumber;

                PerformanceRefreshRowCount();
                _PerformanceEndTime = DateTime.Now;
                writeLog(@"Complete(" + _PerformanceEndTime.ToLongTimeString() + @")");
                PerformanceCalculate(_PerformanceStartTime, _PerformanceEndTime, iRowNumber);

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

        private void buttonPerformanceRetrieve_Click(object sender, EventArgs e)
        {
            if (false == checkBoxPerformanceExist.Checked) return;

            //textBoxPerformanceLog.Text = string.Empty;
            int iRowNumber = PerformanceGetRowNumber();
            if (iRowNumber <= 0)
            {
                writeLog("No data found. Retrieve exit.");
                return;
            }

            _PerformanceStartTime = DateTime.Now;
            writeLog(@"Retrieve " + maskedTextBoxPerformanceRowNumber.Text + " rows begin(" + _PerformanceStartTime.ToLongTimeString() + @")...");
            Application.DoEvents();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
                {
                    using (SPWeb objSPWeb = objSPSite.OpenWeb())
                    {
                        SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                        SPListItemCollection objSPListItemCollection = objSPListPerformanceTest.Items;
                        if (iRowNumber > objSPListItemCollection.Count) iRowNumber = objSPListItemCollection.Count;
                        int i = 1;
                        foreach (SPListItem objSPListItem in objSPListItemCollection)
                        {
                            string strValue = "retrieve(" + DateTime.Now.ToLongTimeString() + @"):: " + i.ToString();
                            i++;
                            strValue = objSPListItem["TextField1"].ToString();
                            strValue += objSPListItem["TextField2"].ToString();
                            strValue += objSPListItem["TextField3"].ToString();
                            strValue += objSPListItem["TextField4"].ToString();
                            strValue += objSPListItem["TextField5"].ToString();

                            if (i % 100 == 0)
                            {
                                writeLogDot();
                                Application.DoEvents();
                            }
                        }

                        PerformanceRefreshRowCount();
                        _PerformanceEndTime = DateTime.Now;
                        writeLog(@"Complete(" + _PerformanceEndTime.ToLongTimeString() + @")");
                        PerformanceCalculate(_PerformanceStartTime, _PerformanceEndTime, iRowNumber);
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

            _PerformanceStartTime = DateTime.Now;
            writeLog(@"Update " + maskedTextBoxPerformanceRowNumber.Text + " rows begin(" + _PerformanceStartTime.ToLongTimeString() + @")...");
            Application.DoEvents();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
                {
                    using (SPWeb objSPWeb = objSPSite.OpenWeb())
                    {
                        SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                        SPListItemCollection objSPListItemCollection = objSPListPerformanceTest.Items;
                        if (iRowNumber > objSPListItemCollection.Count) iRowNumber = objSPListItemCollection.Count;
                        int i = 1;
                        foreach (SPListItem objSPListItem in objSPListItemCollection)
                        {
                            string strValue = "update(" + DateTime.Now.ToLongTimeString() + @"):: " + i.ToString();
                            i++;
                            objSPListItem["TextField1"] = strValue;
                            objSPListItem["TextField2"] = strValue;
                            objSPListItem["TextField3"] = strValue;
                            objSPListItem["TextField4"] = strValue;
                            objSPListItem["TextField5"] = strValue;
                            objSPListItem.SystemUpdate(false);

                            if (i % 100 == 0)
                            {
                                writeLogDot();
                                Application.DoEvents();
                            }
                        }

                        PerformanceRefreshRowCount();
                        _PerformanceEndTime = DateTime.Now;
                        writeLog(@"Complete(" + _PerformanceEndTime.ToLongTimeString() + @")");
                        PerformanceCalculate(_PerformanceStartTime, _PerformanceEndTime, iRowNumber);
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

        public int deleteSPListItemBatch(SPWeb objSPWeb, SPListItemCollection objSPListItemCollection)
        {
            int iCount = 0;
            StringBuilder sbDelete = new StringBuilder();
            sbDelete.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Batch>");

            string strListGUID = objSPListItemCollection.List.ID.ToString();

            foreach (SPListItem item in objSPListItemCollection)
            {
                sbDelete.Append("<Method>");
                sbDelete.Append("<SetList Scope=\"Request\">" + strListGUID + "</SetList>");
                sbDelete.Append("<SetVar Name=\"ID\">" + Convert.ToString(item.ID) + "</SetVar>");
                sbDelete.Append("<SetVar Name=\"Cmd\">Delete</SetVar>");
                sbDelete.Append("</Method>");
                iCount++;
            }

            sbDelete.Append("</Batch>");

            try
            {
                objSPWeb.ProcessBatchData(sbDelete.ToString());
            }
            catch (Exception ex)
            {
                writeLog("deleteSPListItemBatch, Delete failed: " + ex.Message);
                throw;
            }
            finally
            {
                objSPWeb.Dispose();
            }

            return iCount;
        }

        private void buttonPerformanceDelete_Click(object sender, EventArgs e)
        {
            if (false == checkBoxPerformanceExist.Checked) return;

            //textBoxPerformanceLog.Text = string.Empty;
            int iRowNumber = PerformanceGetRowNumber();
            if (iRowNumber <= 0)
            {
                writeLog("No data found. Update exit.");
                return;
            }

            _PerformanceStartTime = DateTime.Now;
            writeLog(@"Delete " + iRowNumber.ToString() + " rows begin(" + _PerformanceStartTime.ToLongTimeString() + @")...");
            Application.DoEvents();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                using (SPSite objSPSite = new SPSite(textBoxSiteURL.Text.Trim()))
                {
                    using (SPWeb objSPWeb = objSPSite.OpenWeb())
                    {
                        SPList objSPListPerformanceTest = objSPWeb.Lists.TryGetList(_ListNamePerformanceTest);
                        SPListItemCollection objSPListItemCollection = objSPListPerformanceTest.Items;
                        iRowNumber = objSPListItemCollection.Count;

                        if (checkBoxBatch.Checked)
                            deleteSPListItemBatch(objSPWeb, objSPListItemCollection);
                        else
                        {
                            for (int i = iRowNumber - 1; i >= 0; i--)
                            {
                                objSPListItemCollection.Delete(i);

                                if (i % 100 == 0)
                                {
                                    writeLogDot();
                                }
                            }
                        }

                        PerformanceRefreshRowCount();
                        _PerformanceEndTime = DateTime.Now;
                        writeLog(@"Complete(" + _PerformanceEndTime.ToLongTimeString() + @")");
                        PerformanceCalculate(_PerformanceStartTime, _PerformanceEndTime, iRowNumber);
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

    }
}
