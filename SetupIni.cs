using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace VerTrans
{
    public class SetupIni
    {
        public string path;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal,
        int size, string filePath);
        public void IniWriteValue(string Section, string Key, string Value, string inipath)
        {
            WritePrivateProfileString(Section, Key, Value, inipath);
        }
        public string IniReadValue(string Section, string Key, string inipath)
        {
            StringBuilder temp = new StringBuilder(2048);
            int i = GetPrivateProfileString(Section, Key, "", temp, 2048, inipath);
            return temp.ToString();
        }
        public void WriteString(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }
        public void WriteInteger(string Section, string Key, int Value)
        {
            WritePrivateProfileString(Section, Key, Value.ToString(), path);
        }
        public void WriteBool(string Section, string Key, bool Value)
        {
            string tmp = "";
            if (Value) tmp = "1"; else tmp = "0";            
            WritePrivateProfileString(Section, Key, tmp, path);
        }
        public void SetFileName(string xfilename)
        {
            path = xfilename;
        }
        public string ReadString(string Section, string Key, string Default)
        {
            StringBuilder temp = new StringBuilder(2048);
            int i = GetPrivateProfileString(Section, Key, Default, temp, 2048, path);
            return temp.ToString();
        }
        public int ReadInteger(string Section, string Key, int Default)
        {
            StringBuilder temp = new StringBuilder(2048);
            string s = ReadString(Section, Key, "");
            int i = Default;
            if (s != "")
            {
                i = Int32.Parse(s);
            }
            try
            {
                return i;
            }
            catch (System.Exception ex)
            {
                return Default;
            }
        }
        public bool ReadBool(string Section, string Key, bool Default)
        {
            StringBuilder temp = new StringBuilder(2048);
            int n = 0;
            if (Default) n = 1; else n=0;
            int i = ReadInteger(Section, Key, n);
            try
            {
                return (i != 0);
            }
            catch (System.Exception ex)
            {
                return Default;
            }
        }
    }
    public class DBINFO
    {
        public string DBType;
        public string ServerIP;
        public string DBName;
        public string UserID;
        public string UserPass;
        public string DSCSYS;
    }
    public class MACHINEPARAM
    {
       public string BAS_CompNo;
       public string BAS_CompShortName;
       public string BAS_StoreNo;
       public string BAS_StoreShortName;
       public string BAS_POSNO;
       public string BAS_Stamp_StoreName;
       public string BAS_Stamp_Manager;
       public string BAS_Stamp_CompName;
       public string BAS_Stamp_CompAddress;
       public string BAS_Stamp_CompPhone;
       public string BAS_Stamp_CompUniID;
    }
}
