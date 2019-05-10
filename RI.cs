using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;

namespace VerTrans
{
    public class RI
    {
        enum EditStatu
        {
            ESNone,
            ESAdd,
            ESEdit
        }
        public struct TTypeXLine
        {
            public int Lines;
            public string Type;
            public TTypeXLine(int xline, string xtype) { Lines = xline; Type = xtype; }
        }
        public struct TPOSLangStr
        {
            public string LangID;
            public string LangStr;
            public string MarkStr;
            public TPOSLangStr(string id, string str, string mark) { LangID = id; LangStr = str; MarkStr = mark; }
            //public TPOSLangStr( string str, string mark) { LangStr = str; MarkStr = mark; }
        }

        public RI()
        {
            TGSystemLangCode = new List<string>();
            TGInfoLangCode = new List<string>();
            TGErrorLangCode = new List<string>();
            gSLangStr = new Dictionary<TTypeXLine, TPOSLangStr>();
            gILangStr = new Dictionary<TTypeXLine, TPOSLangStr>();
            gELangStr = new Dictionary<TTypeXLine, TPOSLangStr>();
            gMarkStr = new Dictionary<TTypeXLine, TPOSLangStr>();
            //gAllStr   = new Dictionary<int, string>();
        }

        ~RI()
        {
            TGSystemLangCode.Clear();
            TGInfoLangCode.Clear();
            TGErrorLangCode.Clear();
            gSLangStr.Clear();
            gILangStr.Clear();
            gELangStr.Clear();
        }

        //public static bool SaveToInc(DataTable dt,DevExpress.XtraEditors.ComboBoxEdit cbo,string path)
        public static bool SaveToInc(DataTable dt, DevExpress.XtraEditors.ComboBoxEdit cbo, string path)
        {
            //string path = fc.RCInc + cbo2.Properties.Items[i].ToString() + ".inc";
            //string path = fc.RCInc + i.ToString() + ".inc";
            // 建立檔案串流（@ 可取消跳脫字元 escape sequence）\
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(@path, false, Encoding.Default);
                for (int i = 0; i < cbo.Properties.Items.Count; i++)
                {
                    //if (cbo.Properties.Items[i].ToString().StartsWith("[" + xtype + "]"))
                    {
                        sw.Write(@"//" + cbo.Properties.Items[i].ToString() + "\r\n");
                    }
                }
                sw.Write("\r\n");
                sw.Write("type\r\n");
                WritePart(sw, dt, cbo, 0, "S", mindex);
                WritePart(sw, dt, cbo, 2, "I", mindex);
                WritePart(sw, dt, cbo, 4, "E", mindex);
                mindex = 0;
                sw.Write("var\r\n");
                WritePart2(sw, dt, cbo, 6, "S", mindex);
                WritePart2(sw, dt, cbo, 8, "I", mindex);
                WritePart2(sw, dt, cbo, 10, "E", mindex);
                mindex = 0;
            }
            catch (System.Exception ex)
            {
                fc.ShowBoxMessage(ex.Message.ToString());
                sw.Close();
                return false;
            }
            finally
            {
                sw.Close();
            }
            return true;            
        }

        public static void WritePart(StreamWriter sw,DataTable dt, DevExpress.XtraEditors.ComboBoxEdit cbo, int Baseint,string xtype,int xindex)
        {
            
            sw.Write(gSpace0 + BaseString[Baseint] + "\r\n");   //EX: //System
            sw.Write(gSpace0 + BaseString[Baseint + 1] + "\r\n"); //EX: TGSystemLangCode = (
            //sw.Write("\r\n");
            for (; xindex < dt.Rows.Count; xindex++)
            {
                if (xindex + 1 < dt.Rows.Count)
                {
                    if (!dt.Rows[xindex + 1][2].ToString().StartsWith(xtype))
                    {
                        sym = "";
                        mindex = xindex + 1;
                    }
                }
                else if (xindex + 1 == dt.Rows.Count)
                {
                    sym = "";                    
                }
                if (dt.Rows[xindex][2].ToString().StartsWith(xtype + "M"))
                    sw.Write(TGSpace + @"//g" + xtype + dt.Rows[xindex][3] + sym + @" //" + dt.Rows[xindex][4] + " \t" + dt.Rows[xindex][5] + "\r\n");
                else if (dt.Rows[xindex][2].ToString().StartsWith(xtype))
                    sw.Write(TGSpace + @"g" + xtype + dt.Rows[xindex][3] + sym + @" //" + dt.Rows[xindex][4] + " \t" + dt.Rows[xindex][5] + "\r\n");
                if (sym == "")
                {
                    sym = ",";
                    break;
                }
            }
            sw.Write(TGSpace + endnote+"\r\n");
            sw.Write("\r\n");
        }

        public static void WritePart2(StreamWriter sw, DataTable dt, DevExpress.XtraEditors.ComboBoxEdit cbo, int Baseint, string xtype, int xindex)
        {
            string mmmm = "";
            sw.Write(gSpace0 + BaseString[Baseint] + "\r\n");
            sw.Write(gSpace0 + BaseString[Baseint + 1] + "\r\n");
            sw.Write(gSpace0+"(\r\n");
            for (; xindex < dt.Rows.Count; xindex++)
            {
                if (xindex + 1 < dt.Rows.Count)
                {
                    if (!dt.Rows[xindex + 1][2].ToString().StartsWith(xtype))
                    {
                        sym = "";
                        mindex = xindex + 1;
                    }
                }
                else if (xindex + 1 == dt.Rows.Count)
                {
                    sym = "";
                }
                if (xtype == "I" && dt.Rows[xindex][3].ToString() == "000299")
                {
                    mmmm = "";
                }
                if (dt.Rows[xindex][2].ToString().StartsWith(xtype + "M"))
                    sw.Write(gSpace + @"//(LangID: " + dt.Rows[xindex][3] + @";  LangStr: '" + dt.Rows[xindex][4] + "')" + sym + "\t" + dt.Rows[xindex][5] + "\r\n");
                else if (dt.Rows[xindex][2].ToString().StartsWith(xtype))
                    sw.Write(gSpace + @"(LangID: " + dt.Rows[xindex][3] + @";  LangStr: '" + dt.Rows[xindex][4] + "')" + sym + "\t" + dt.Rows[xindex][5] + "\r\n");
                if (sym == "")
                {
                    sym = ",";
                    break;
                }
            }
            sw.Write(gSpace0 + endnote + "\r\n");
            sw.Write("\r\n");
        }
        public List<string> TGSystemLangCode;
        public List<string> TGInfoLangCode;
        public List<string> TGErrorLangCode;
        
        public static int mindex = 0;
        public static string sym = ",";
        public static string TGSpace = "                     ";
        public static string gSpace = "      ";
        public static string gSpace0 = "    ";
        public static string endnote = ");";
        public static string[] BaseString = new string[] { @"//System", "TGSystemLangCode = (",
                                                    @"//Infomation","TGInfoLangCode = (",
                                                    @"//Error","TGErrorLangCode = (",
                                                    @"//SystemID","gSLangStr: array[TGSystemLangCode] of TPOSLangStr = ",
                                                    @"//InformationID","gILangStr: array[TGInfoLangCode] of TPOSLangStr = ",
                                                    @"//ErrorID","gELangStr: array[TGErrorLangCode] of TPOSLangStr = " };
        


        public Dictionary<TTypeXLine, TPOSLangStr> gSLangStr;
        public Dictionary<TTypeXLine, TPOSLangStr> gILangStr;
        public Dictionary<TTypeXLine, TPOSLangStr> gELangStr;
        public Dictionary<TTypeXLine, TPOSLangStr> gMarkStr;
        //public Dictionary<int, string> gAllStr;



    }
}
