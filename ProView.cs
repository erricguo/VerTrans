using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Threading;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;

namespace VerTrans
{
    public partial class ProView : Form
    {
        List<string> List_ver = new List<string>();
        Dictionary<Cell, short> cells = new Dictionary<Cell, short>();
        string min = "";
        static string basicpath = @"\\10.40.40.127\Cosmos_patch\對外區\";
        static Computer myComputer = new Computer();
        static string Mydocument = myComputer.FileSystem.SpecialDirectories.MyDocuments;
        Dictionary<string, List<string>> ListDir = new Dictionary<string, List<string>>();
        Dictionary<string, List<DateTime>> ListDate = new Dictionary<string, List<DateTime>>();
        private Thread threadcount;   //定義線程
        delegate void setpro(ProgressBar pb ,int count, int index,System.Windows.Forms.Label lb );  //定義委託
        setpro SetPro;
        delegate void info(string xvalue);  //定義委託
        info Setinfo;
        delegate void closewindow(ProView pv);  //定義委託
        closewindow closewin;
        int[] CheckCount = new int[]{0,0};
        string filename = "";
        string savefilename = "";
        public enum ColorIndex //EXCEL 背景色
        {
            無色 = -4142, 自動 = -4105,
            紫羅蘭 = 13, 淺青綠 = 34, 玫瑰紅 = 38, 水綠色 = 42, 酸橙色 = 43, 淺橙色 = 45,   
            灰色25 = 15, 灰色40 = 48, 灰色50 = 16, 灰色80 = 56, 
            
            黑色 = 1, 白色 = 2, 紅色 = 3, 鮮綠 = 4, 藍色 = 5,
            黃色 = 6, 粉紅 = 7, 青綠 = 8, 深紅 = 9, 綠色 = 10,
            深藍 = 11, 深黃 = 12, 青色 = 14,

            海綠 = 50, 褐色 = 53, 橄欖 = 52, 深綠 = 51, 深青 = 49,
             靛藍 = 55,   橙色 = 46, 
                 藍灰 = 47,

            淺藍 = 41, 
            金色   = 44,      天藍 = 33, 
            梅紅   = 54,  茶色 = 40, 淺黃 = 36, 淺綠 = 35,  
            淡藍 = 37, 淡紫 = 39,
            
        }
        public ProView()
        {
            InitializeComponent();
        }
        //SET，GET
        public List<string> SetListVer
        {
            set
            {
                List_ver = value;
            }
        }
        public string SetMinute
        {
            set
            {
                min = value;
            }
        }
        public int[] GetResult
        {
            get
            {
                return CheckCount;
            }
        }
        public string GetFilename
        {
            get
            {
                return filename;
            }
        }
        public string GetSaveFilename
        {
            get
            {
                return savefilename;
            }
        }
        //副程式---------------------------------------------------------
        public static bool isDirectory(string p)//目錄是否存在
        {
            if (p == "")
            {
                return false;
            }
            return System.IO.Directory.Exists(p);
        }
        public void GetAllDirList(string strBaseDir)
        {
            ListDir.Clear();
            ListDate.Clear();
            DirectoryInfo di = new DirectoryInfo(strBaseDir);
            DirectoryInfo[] diA = di.GetDirectories();
          /*  if (aaa == 0)
            {
                FileInfo[] fis2 = di.GetFiles();   //有关目录下的文件   
                for (int i2 = 0; i2 < fis2.Length; i2++)
                {
                    ListFile.Add(fis2[i2].Name);
                    //fis2[i2].FullName是根目录中文件的绝对地址，把它记录在ArrayList中
                }
            }*/
            for (int i = 0; i < diA.Length; i++)
            {
                //aaa++;
                //ListDir.Add(diA[i].Name + "\t<目录>");
                //ListDir.Add(diA[i].Name);
                //diA[i].FullName是某个子目录的绝对地址，把它记录在ArrayList中
                string dirname = diA[i].Name;
                List<string> ListFile = new List<string>();
                List<DateTime> Dates = new List<DateTime>();
                DirectoryInfo di1; 
                if (!isDirectory(diA[i].FullName+@"\MODI"))
                    //continue;
                    di1 = new DirectoryInfo(diA[i].FullName);                
                else
                    di1 = new DirectoryInfo(diA[i].FullName + @"\MODI");
                DirectoryInfo[] diA1 = di1.GetDirectories();
                FileInfo[] fis1 = di1.GetFiles("*.dll");   //有关目录下的文件   
                for (int ii = 0; ii < fis1.Length; ii++)
                {
                    ListFile.Add(fis1[ii].Name);
                    Dates.Add(fis1[ii].LastWriteTime);
                    //fis1[ii].FullName是某个子目录中文件的绝对地址，把它记录在ArrayList中
                }
                ListDir.Add(dirname, ListFile);
                ListDate.Add(dirname, Dates);
                //GetAllDirList(diA[i].FullName);
                //注意：递归了。逻辑思维正常的人应该能反应过来
            }

        }
        public static string GetFileLastWriteDate(string FileName)//取得檔案最後修改日期
        {
            FileInfo fs = new FileInfo(FileName);
            if (!fs.Exists) return " Not Found!";
            else return fs.LastWriteTime.ToString("yyyyMMdd");
        }
        private void UpdateCpuProgress(ProgressBar pb,System.Windows.Forms.Label lb)
        {
            string strText = (100*pb.Value/pb.Maximum).ToString() + "%";
            lb.Text = strText;
            /*System.Drawing.Font font = new System.Drawing.Font("微软雅黑", (float)10, FontStyle.Regular);
            PointF pointF = new PointF(pb.Width / 2 - 10, pb.Height / 2 - 10);
            pb.CreateGraphics().DrawString(strText, font, Brushes.Black, pointF);*/
        }
        private void Updateinfo(string xvalue)
        {
            lb_info.Text = xvalue;
            lb_info.Left = this.Width / 2 - lb_info.Width / 2;
        }
        private void changepar(ProgressBar pb, int count1, int value,System.Windows.Forms.Label lb)
        {
            pb.Maximum = count1;
            pb.Value = value;
            pb.Refresh();
            UpdateCpuProgress(pb,lb);
        }
        private void closepv(ProView pv)
        {
            pv.Close();
        }
        private void WriteXls()
        {
            int g = 0;
            int h = 0;
            int i = 0;
            int j = 0;
            CheckCount[0] = 0;
            CheckCount[1] = 0;
            
            ////需要使用的變數。Workbook表示Excel檔，Worksheet表示一個Excel檔裡面的sheet(一個Excel檔可以有很多sheet)，Range表示Excel裡面單元格的範圍。
            //Microsoft.Office.Interop.Excel.Application xlApp = null;
            //Workbook wb = null;
            HSSFWorkbook wb = new HSSFWorkbook();
            HSSFSheet ws = (HSSFSheet)wb.CreateSheet(DateTime.Today.ToString("yyyyMMdd"));
            //Worksheet ws = null;
            //Range aRange = null;
            //Range aRange2 = null;
           /* object mObj_opt = System.Reflection.Missing.Value;
            //啟動Excel應用程式
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.Visible = false;
            if (xlApp == null)
            {
                Console.WriteLine("Error! xlApp");
                return;
            }
            //用Excel應用程式建立一個Excel物件，也就是Workbook。並取得Workbook中的第一個sheet。這就是我們要操作資料的地方。
            wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            ws = (Worksheet)wb.Worksheets[1];
            if (ws == null)
            {
                Console.WriteLine("Error! ws");
            }*/
            //利用Cells屬性，取得單一儲存格，並進行操作。
            //ws.Name = DateTime.Today.ToString("yyyyMMdd");
            string[] header = { "版本", "個案", "檔案名稱", "修改時間", "落差值(分)", "落差值(天)" };
            ws.CreateRow(0);
            for ( int ki = 0; ki < header.Length;ki++ )
            {
                ws.GetRow(0).CreateCell(ki).SetCellValue(header[ki]);
            }
            //ws.get_Range("A1", "F1").Value2 = header;
            //ListFile.Clear();
            //pb_main.Maximum = List_ver.Count;
            //pb_main.Value = 0;
            int colorcount = 0;
            //string ra0 = "A2";
            int ra0 = 1;
            int ra1 = 0;
            int last = 0;
            for (int mi = 0; mi < List_ver.Count; mi++)
            {

                GetAllDirList(basicpath + List_ver[mi] + @"\FOR客製客戶");
                //最後，呼叫SaveAs function儲存這個Excel物件到硬碟。
                /* for (int i = 0; i < ListFile.Count;i++ )
                 {
                     ws.Cells[2+i, 1] = ListFile[i];
                 }*/
                //版本
                //個案
                //pb_file.Maximum = ListDir.Count;
                //pb_file.Value = 0;
                int index = 0;
                foreach (KeyValuePair<string, List<string>> item in ListDir)
                {
                    if (item.Value.Count > 0)
                    {
                        for (int p = 0; p < item.Value.Count; p++)
                        {
                            ws.CreateRow(1 + h);
                            //ws.Cells[2 + h, 2] = item.Key;
                            //ws.CreateRow(2 + h).CreateCell(2).SetCellValue(item.Key);
                            ws.GetRow(1 + h).CreateCell(1).SetCellValue(item.Key);
                            //ws.Cells[2 + h, 1] = List_ver[mi];
                            //ws.CreateRow(2 + h).CreateCell(1).SetCellValue(List_ver[mi]);
                            ws.GetRow(1 + h).CreateCell(0).SetCellValue(List_ver[mi]);
                            h++;
                        }
                    }
                }
                //修改時間
                foreach (KeyValuePair<string, List<DateTime>> item in ListDate)
                {

                    if (item.Value.Count > 0)
                    {
                        DateTime dt = item.Value[0];
                        for (int p = 0; p < item.Value.Count; p++)
                        {

                            //ws.Cells[2 + j, 4] = item.Value[p];
                            //ws.CreateRow(2 + j).CreateCell(4).SetCellValue(item.Value[p]);
                            DateTime dt1 = item.Value[p];
                            ws.GetRow(1 + j).CreateCell(3).SetCellValue(dt1.ToString());
                            TimeSpan Total = item.Value[p].Subtract(dt); //日期相減
                            //Cell cell = ws.GetRow(2 + j).GetCell(2 + j);


                            //ws.Cells[2 + j, 5] = Math.Round(Total.TotalMinutes,3).ToString();
                            int seconds = Math.Abs(Int32.Parse(Math.Round(Total.TotalSeconds, 0).ToString()));
                            int hours = Convert.ToInt32((seconds / 3600) - (seconds % 3600) / 3600);
                            seconds -= hours * 3600;
                            int minutes = Convert.ToInt32((seconds / 60) - (seconds % 60) / 60); ;
                            seconds -= minutes * 60;
                            //ws.Cells[2 + j, 5] = string.Format("{0:00}", hours) + ":" + string.Format("{0:00}", minutes) + ":" + string.Format("{0:00}", seconds);
                            //ws.GetRow(2 + j).CreateCell(5).SetCellValue(string.Format("{0:00}", hours) + ":" + string.Format("{0:00}", minutes) + ":" + string.Format("{0:00}", seconds));
                            string edittime = string.Format("{0:00}", hours) + ":" + string.Format("{0:00}", minutes) + ":" + string.Format("{0:00}", seconds);
                            ws.GetRow(1 + j).CreateCell(4).SetCellValue(edittime);
                            //ws.Cells[2 + j, 6] = Convert.ToInt32((float.Parse(Total.TotalDays.ToString())));
                            //ws.GetRow(2 + j).CreateCell(6).SetCellValue(Convert.ToInt32((float.Parse(Total.TotalDays.ToString()))));
                            ws.GetRow(1 + j).CreateCell(5).SetCellValue(Convert.ToInt32((float.Parse(Total.TotalDays.ToString()))));

                            Cell cell = ws.GetRow(1 + j).GetCell(4);
                            if (float.Parse(Total.TotalMinutes.ToString()) > Int32.Parse(min))
                            {
                                //string palce = "E" + (2 + j).ToString();
                                //font_color(wb, ws.GetRow(2 + j).GetCell(2 + j),NPOI.HSSF.Util.HSSFColor.BLUE.index2);
                                cells.Add(cell, NPOI.HSSF.Util.HSSFColor.BLUE.index);
                                //font_color(wb, ws.GetRow(1 + j).GetCell(4), NPOI.HSSF.Util.HSSFColor.BLUE.index);
                                //Range ras1 = ws.get_Range(palce, palce);
                                //ras1.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(0, 0, 255));
                                CheckCount[0]++;
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(ras1);
                                //ras1 = null;
                            }
                            else if ((float.Parse(Total.TotalMinutes.ToString()) < (-1)*Int32.Parse(min)) )
                            {
                                //string palce = "E" + (2 + j).ToString();
                                //fill_background(wb, cell, NPOI.HSSF.Util.HSSFColor.RED.index);
                                //font_color(wb, ws.GetRow(2 + j).GetCell(2 + j),NPOI.HSSF.Util.HSSFColor.RED.index);
                                cells.Add(cell, NPOI.HSSF.Util.HSSFColor.RED.index);
                                //font_color(wb, ws.GetRow(1 + j).GetCell(4), NPOI.HSSF.Util.HSSFColor.RED.index);
                                //Range ras2 = ws.get_Range(palce, palce);
                                //ras2.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(255, 0, 0));
                                CheckCount[0]++;
                               // System.Runtime.InteropServices.Marshal.ReleaseComObject(ras2);
                               // ras2 = null;
                            }

                            cell = ws.GetRow(1 + j).GetCell(5);
                            if (Convert.ToInt32(float.Parse(Total.TotalDays.ToString())) >= 1 )
                            {
                                //string palce = "F" + (2 + j).ToString();
                                //Range ras3 = ws.get_Range(palce, palce);
                                //font_color(wb,ws.GetRow(2 + j).GetCell(2 + j), NPOI.HSSF.Util.HSSFColor.BLUE.index2);
                                cells.Add(cell, NPOI.HSSF.Util.HSSFColor.BLUE.index2);
                                //font_color(wb, ws.GetRow(1 + j).GetCell(4), NPOI.HSSF.Util.HSSFColor.BLUE.index2);
                                //ras3.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(0, 0, 255));
                                CheckCount[1]++;
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(ras3);
                                //ras3 = null;
                            }
                            else if ((Convert.ToInt32(float.Parse(Total.TotalDays.ToString())) <= -1))
                            {
                                //string palce = "F" + (2 + j).ToString();
                                //Range ras4 = ws.get_Range(palce, palce);
                                //fill_background(wb, cell, NPOI.HSSF.Util.HSSFColor.RED.index);
                                //font_color(wb,ws.GetRow(2 + j).GetCell(2 + j), NPOI.HSSF.Util.HSSFColor.RED.index);
                                cells.Add(cell, NPOI.HSSF.Util.HSSFColor.RED.index);
                                //font_color(wb, ws.GetRow(1 + j).GetCell(4), NPOI.HSSF.Util.HSSFColor.RED.index);
                                //ras4.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(255, 0, 0));
                                CheckCount[1]++;
                                //System.Runtime.InteropServices.Marshal.ReleaseComObject(ras4);
                                //ras4 = null;
                            }

                            j++;
                        }
                    }
                }
                //檔案
                foreach (KeyValuePair<string, List<string>> item in ListDir)
                {
                    if (item.Value.Count > 0)
                    {
                        for (int p = 0; p < item.Value.Count; p++)
                        {
                            //ws.Cells[2 + i, 3] = item.Value[p];
                            //ws.GetRow(2 + i).CreateCell(3).SetCellValue(item.Value[p]);
                            ws.GetRow(1 + i).CreateCell(2).SetCellValue(item.Value[p]);
                            i++;
                            //Updateinfo(List_ver[mi] + ".." + item.Key + ".." + item.Value[p]);
                            lb_info.Invoke(Setinfo, new object[] { List_ver[mi] + ".." + item.Key + ".." + item.Value[p] });
                            //UpdateCpuProgress(pb_file);
                        }

                        index++;
                        pb_file.Invoke(SetPro, new object[] { pb_file, ListDir.Count, index, lb_file });
                        Thread.CurrentThread.Join(100);
                    }
                }
                pb_main.Invoke(SetPro, new object[] { pb_main, List_ver.Count, mi + 1, lb_main });
                //Thread.CurrentThread.Join(1);
                if (mi < List_ver.Count)
                {
                    ra1 = 1+i;
                   /* ra1 = "F" + (1 + i).ToString();
                    Range rax = ws.get_Range(ra0, ra1);*/
                    if (colorcount % 2 == 0)
                    {
                        //rax.Interior.Color =
                        //System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromName("AliceBlue")); //背景顏色
                        for (int r1 = 0; r1 < 6; r1++)
                        {
                            for (int a2 = last; a2 < ra1; a2++)
                            {
                                fill_background(wb, ws.GetRow(a2).GetCell(r1), NPOI.HSSF.Util.HSSFColor.SKY_BLUE.index);
                            }
                        }
                    }
                    else
                    {
                        for (int r1 = 0; r1 < 6; r1++)
                        {
                            for (int a2 = last; a2 < ra1; a2++)
                            {
                                fill_background(wb, ws.GetRow(a2).GetCell(r1), NPOI.HSSF.Util.HSSFColor.LIGHT_GREEN.index);
                            }
                        }
                        //rax.Interior.Color =
                        //System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromName("Honeydew")); //背景顏色

                        //ra0 = "A" + (2 + i).ToString();
                        //ra0 = 1 + i;                       
                    }
                    last = ra1;
                    colorcount++;
                }
            }
                //UpdateCpuProgress(pb_main);
                //string ra_row = "A1";
                int ra_row = 1;
                //string ra_col = "F" + (1 + j).ToString();
                int ra_col = 1 + j;
                //Range ra = ws.get_Range(ra_row, ra_col);
                //自動調整蘭寬
                //ra.Columns.AutoFit();

                for (ra_col = 0; ra_col < 6; ra_col++)
                {
                    ws.AutoSizeColumn(ra_col);
                }
                //ra = ws.get_Range("A1", "F1");
                //ra.Interior.Color =
                //System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(192,192,255)); //背景顏色
                for (int ai=0;ai<6;ai++)
                {
                    fill_background(wb, ws.GetRow(0).GetCell(ai), NPOI.HSSF.Util.HSSFColor.ORANGE.index);
                }
                
                //置中
                //ra.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                //ra.VerticalAlignment = XlVAlign.xlVAlignCenter;
                /*
                Process[] runningProcs = Process.GetProcessesByName("excel");
                foreach (Process p in runningProcs)
                {
                    p.Close();
                }*/
                //string row = "A2";
                int row = 2;
                //string col = "F" + (1 + j).ToString();
                int col = 1 + j;
                //ra = ws.get_Range(row, col);
                //ra.Select();
                //xlApp.ActiveWindow.FreezePanes = true;
                ws.CreateFreezePane(1, 0, 1, 0);
                if (!isDirectory(Mydocument+@"\ModiCheck"))
                {
                    Directory.CreateDirectory(Mydocument + @"\ModiCheck");
                }
                if (!isDirectory(Mydocument + @"\ModiCheck\" + DateTime.Now.ToString("yyyyMM")))
                {
                    Directory.CreateDirectory(Mydocument + @"\ModiCheck\" + DateTime.Now.ToString("yyyyMM") );
                }
                if (!isDirectory(Mydocument + @"\ModiCheck\" + DateTime.Now.ToString("yyyyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd")))
                {
                    Directory.CreateDirectory(Mydocument + @"\ModiCheck\" + DateTime.Now.ToString("yyyyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd"));
                }
                filename = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                //wb.SaveAs(Mydocument + @"\ModiCheck\" + DateTime.Now.ToString("yyyyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + filename + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet, mObj_opt, mObj_opt, mObj_opt, mObj_opt, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, mObj_opt, mObj_opt, mObj_opt, mObj_opt, mObj_opt);
                savefilename = Mydocument + @"\ModiCheck\" + DateTime.Now.ToString("yyyyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + filename + ".xls";
                WriteToFile(wb, savefilename);
               /* Console.WriteLine("save");
                wb.Close(false, mObj_opt, mObj_opt);
                xlApp.Workbooks.Close();
                xlApp.Quit();
                //刪除 Windows工作管理員中的Excel.exe 進程，  
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ra);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(aRange);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(aRange2);
                xlApp = null;
                wb = null;
                ws = null;
                ra = null;*/
                //aRange = null;
                //aRange2 = null;
                //呼叫垃圾回收  
                GC.Collect();
                this.Invoke(closewin, new object[] { this }); 
        }

        private void ProView_Shown(object sender, EventArgs e)
        {
            SetPro = new setpro(changepar);
            Setinfo = new info(Updateinfo);
            closewin = new closewindow(closepv);
            threadcount = new Thread(new ThreadStart(WriteXls));
            threadcount.IsBackground = true;
            threadcount.Start();
        }
        private void fill_background(HSSFWorkbook hssfworkbook,Cell cell,short color)
        {
            CellStyle style1 = hssfworkbook.CreateCellStyle();
            //style1.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.BLUE.index2;
            if (cells.ContainsKey(cell))
            {
               /* NPOI.SS.UserModel.Font font1 = hssfworkbook.CreateFont();
                font1.Color = cells[cell];
                style1.SetFont(font1);*/
                style1.FillForegroundColor = cells[cell];
            }
            else
            style1.FillForegroundColor = color;
            style1.FillPattern = FillPatternType.SOLID_FOREGROUND;
            cell.CellStyle = style1;
        }
        private void font_color(HSSFWorkbook hssfworkbook,Cell cell, short color)
        {
            CellStyle cellStyleFontColor = hssfworkbook.CreateCellStyle();
            NPOI.SS.UserModel.Font font1 = hssfworkbook.CreateFont();
            font1.Color = color;
            cellStyleFontColor.SetFont(font1);
            cell.CellStyle = cellStyleFontColor;
        }
        static void WriteToFile(HSSFWorkbook hssfworkbook,string filepath)
        {
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(filepath, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

    }
}
