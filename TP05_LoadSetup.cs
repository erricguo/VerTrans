using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Nini.Config;
using System.Net;

namespace VerTrans
{
    public partial class TP05_LoadSetup : Form
    {
        public TP05_LoadSetup()
        {
            InitializeComponent();
        }
        //string filename = @fc.Library2003Path;
        string filename = @fc.ConfigPath;
        //string FDefaultPath = "DefaultPath";
        string FDefaultPath = "Library2003";
        string FTempPath = "";
        string FMsg = "";
        private void btn_F2_Click(object sender, EventArgs e)
        {
            if (fc.isDirectory(fc.Mydocument))
            {
                fbd01.SelectedPath = fc.Mydocument;
            }            
            if (fc.isDirectory(tp05_td10.Text))
            {
                fbd01.SelectedPath = tp05_td10.Text;
            }
            if (fbd01.ShowDialog() == DialogResult.OK)
            {
                FTempPath = fbd01.SelectedPath;
                tp05_td10.Text = FTempPath + @"\Cosmos_patch\Cosmos安裝片\";
            }
        }

        private void TP05_LoadSetup_Load(object sender, EventArgs e)
        {

            if (filename.EndsWith(@"\"))
            {
                filename = filename.Substring(0, filename.Length - 1);
            }
            if (!File.Exists(filename))
            {
                File.Create(filename).Close();
            }

            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            IniConfigSource source = new IniConfigSource(filename);
            fc.SetAliasForNini(source);
            try
            {                
                //tp05_chk01.Checked = ini.ReadBool(FDefaultPath, "UseDefault", true);
                tp05_chk01.Checked = source.Configs[FDefaultPath].GetBoolean("UseDefault", true);
                //tp05_td10.Text = ini.ReadString(FDefaultPath, "CusPath", fc.DirVerTrans + @"\Cosmos_patch\Cosmos安裝片\");
                tp05_td10.Text = source.Configs[FDefaultPath].GetString("CusPath", fc.DirVerTrans + @"\Cosmos_patch\Cosmos安裝片\");
                tb_ftpip.Text = source.Configs[FDefaultPath].GetString("ftpip", "");
                tb_ftpid.Text = source.Configs[FDefaultPath].GetString("ftpid", "");
                string pw = source.Configs[FDefaultPath].GetString("ftppw", "");
                /*if (pw.Trim()!="")
                {
                    pw = fc.ERPDecrypt(pw); 
                }*/
                tb_ftppw.Text = pw;
            }
            catch (Exception ex)
            {                
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
        }

        private void tp05_chk13_CheckedChanged(object sender, EventArgs e)
        {
            tp05_chk02.Checked = !tp05_chk01.Checked;
            //SetPathEnable();
        }

        private void tp05_OK_Click(object sender, EventArgs e)
        {
            DownloadFiles1();
            return;
            IniConfigSource source = new IniConfigSource(filename);
            fc.SetAliasForNini(source);
            //SetupIni ini = new SetupIni();
            //ini.SetFileName(filename);
            try
            {
                {
                    //ini.WriteString(FDefaultPath, "CusPath", tp05_td10.Text);
                    source.Configs[FDefaultPath].Set("CusPath", tp05_td10.Text);                    
                    //ini.WriteBool(FDefaultPath, "UseDefault", tp05_chk01.Checked);
                    source.Configs[FDefaultPath].Set("UseDefault", tp05_chk01.Checked);
                    source.Configs[FDefaultPath].Set("ftpip", tb_ftpip.Text);
                    source.Configs[FDefaultPath].Set("ftpid", tb_ftpid.Text);
                    source.Configs[FDefaultPath].Set("ftppw", tb_ftppw.Text);
                    source.Save();
                }
            }
            catch (Exception ex)
            {
                fc.ShowBoxMessage(ex.Message.ToString());
                //throw;
            }
        }

        private void tp05_chk02_CheckedChanged(object sender, EventArgs e)
        {
            tp05_chk01.Checked = !tp05_chk02.Checked;
            if (tp05_td10.Text.Trim()=="")
            {
                tp05_td10.Text = fc.DirVerTrans;
            }
        }
        private void SetPathEnable()
        {
            if (tp05_chk01.Checked)
            {
                tp05_td10.Enabled = false;
                btn_F2.Enabled = false;
                tp05_Pack.Enabled = false;
            }
            else
            {
                tp05_td10.Enabled = true;
                btn_F2.Enabled = true;
                tp05_Pack.Enabled = true;
            }
        }

        private void tp05_Pack_Click(object sender, EventArgs e)
        {
            //btn_F2.PerformClick();

            string mCusPath = tp05_td10.Text;
            if (mCusPath == "")
            {
                fc.ShowConfirm("自訂路徑不可空白!");
                return;
            }
            if (!fc.isDirectory(mCusPath))
            {
                fc.ShowConfirm("自訂路徑不存在!");
                return;

            }
            if (fc.ShowConfirm("[打包位置] " + mCusPath + "\r\n是否確定要下載?") != DialogResult.OK)
            {
                return;
            }

            if (!System.IO.Directory.Exists(FTempPath + @"\Cosmos_patch"))
            {
                Directory.CreateDirectory(FTempPath + @"\Cosmos_patch");
                //UpdateMsg("",targetPath, true);
            }
            if (!System.IO.Directory.Exists(FTempPath + @"\Cosmos_patch\Cosmos安裝片"))
            {
                Directory.CreateDirectory(FTempPath + @"\Cosmos_patch\Cosmos安裝片");
                //UpdateMsg("",targetPath, true);
            }
            // mCusPath += @"\VerTrans\Cosmos_patch\Cosmos安裝片";

            if (rg01.SelectedIndex == 0) //0.對外區  1.FTP
            {
                DownloadFilesOUT();
                
            }
            else
            {
                DownloadFilesFTP();
            }

        }
        private void Get2003(string path2,string path3)
        {
            if (!Directory.Exists(path3))
            {
                Directory.CreateDirectory(path3);
            }
            //string[] files = System.IO.Directory.GetFiles(path2, "*.sdd");
            string[] files = GetFTPFilesName(path2, "sdd");
            int i = 1;
            foreach (string s in files)
            {
                //System.IO.File.Copy(s, path3 +"\\"+ Path.GetFileName(s), true);
                //System.IO.File.Copy(s, path3 + "\\" + s, true);
                DownloadFtpFile(path2, path3 + "\\" + s,s);

                Application.DoEvents();
               /* progressBarControl1.CreateGraphics().DrawString((100 * progressBarControl1.Position / progressBarControl1.Properties.Maximum).ToString() + "%", new Font("Arial",
                (float)10.25, FontStyle.Bold),
                Brushes.Black, new PointF(progressBarControl1.Width / 2 - 10, progressBarControl1.Height / 2 - 7));
                progressBarControl2.CreateGraphics().DrawString((100*progressBarControl2.Position/progressBarControl2.Properties.Maximum).ToString() + "%", new Font("Arial",
                (float)10.25, FontStyle.Bold),
                Brushes.Black, new PointF(progressBarControl2.Width / 2 - 10, progressBarControl2.Height / 2 - 7));*/

                pbc02.PerformStep();
            }
        }

        private void tp05_td10_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void DownloadFilesOUT()
        {
            string mCusPath = tp05_td10.Text;
            try
            {

                string path = @"\\10.40.40.127\Cosmos_patch\Cosmos安裝片";
                string path2 = "";
                string path3 = "";
                string path4 = "";
                int bar2Maxcount = 0;
                string[] dirs = Directory.GetDirectories(path);/*目錄(含路徑)的陣列*/
                List<string> sss = new List<string>();
                List<string> sss2 = new List<string>();

                foreach (string item in dirs)
                {
                    if (!item.EndsWith("POS"))
                    {
                        sss.Add(item);
                        string[] xx = fc.Split(@"\", item);
                        sss2.Add(xx[xx.Length - 1]);//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
                    }
                }
                //---------------------------------------------------
                //设置一个最小值
                pbc01.Properties.Minimum = 0;
                //设置一个最大值
                pbc01.Properties.Maximum = sss.Count;
                //设置步长，即每次增加的数
                pbc01.Properties.Step = 1;
                //设置进度条的样式
                pbc01.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                pbc01.Position = 0;
                //---------------------------------------------------
                for (int i = 0; i < sss.Count; i++)
                {
                    path2 = path + "\\" + sss2[i] + @"\Tools\TransDB\";
                    if (!fc.isDirectory(path + "\\" + sss2[i] + @"\Tools"))
                    {
                        FMsg += "資料夾:" + path + "\\" + sss2[i] + @"\Tools" + "不存在";
                        Application.DoEvents();
                        pbc01.PerformStep();
                        bar2Maxcount = 0;
                        continue;
                    }
                    if (!fc.isDirectory(path + "\\" + sss2[i] + @"\Tools\TransDB\"))
                    {
                        FMsg += "資料夾:" + path + "\\" + sss2[i] + @"\Tools\TransDB\" + "不存在";
                        Application.DoEvents();
                        pbc01.PerformStep();
                        bar2Maxcount = 0;
                        continue;
                    }
                    string[] dirs2 = Directory.GetDirectories(path2);/*目錄(含路徑)的陣列*/

                    //设置一个最小值
                    pbc02.Properties.Minimum = 0;
                    //设置一个最大值
                    //progressBarControl2.Properties.Maximum = bar2Maxcount;
                    //设置步长，即每次增加的数
                    pbc02.Properties.Step = 1;
                    //设置进度条的样式
                    pbc02.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    pbc02.Position = 0;
                    if (dirs2.Length > 0) //若底下有業態
                    {
                        //---------------------------------------------------

                        foreach (string item in dirs2)
                        {
                            bar2Maxcount += System.IO.Directory.GetFiles(path2 + Path.GetFileName(item), "*.sdd").Length;
                            pbc02.Properties.Maximum = bar2Maxcount;
                        }

                        //---------------------------------------------------
                        foreach (string item in dirs2)
                        {

                            path4 = mCusPath + "\\" + sss2[i] + @"\Tools\TransDB\" + Path.GetFileName(item);

                            Get2003(path2 + Path.GetFileName(item), path4);
                        }
                    }
                    else //若底下沒有業態
                    {
                        pbc02.Properties.Maximum = System.IO.Directory.GetFiles(path2, "*.sdd").Length;
                        path3 = mCusPath + "\\" + sss2[i] + @"\Tools\TransDB\";
                        Get2003(path2, path3);
                    }
                    Application.DoEvents();
                    pbc01.PerformStep();
                    bar2Maxcount = 0;
                }
                fc.ShowBoxMessage("打包完成!!\r\n" + FMsg);
            }
            catch (System.Exception ex)
            {
                fc.ShowBoxMessage("打包失敗!!\r\n" + FMsg + "\r\n" + ex.Message.ToString());
            }
        }

        private void DownloadFilesFTP()
        {
            //1.   File Download From FTP:
            string mCusPath = tp05_td10.Text;
            try
            {
                string path = @"ftp://"+ tb_ftpip.Text+"/Cosmos安裝片/";
                string path2 = "";
                string path3 = "";
                string path4 = "";
                int bar2Maxcount = 0;
                //string[] dirs = Directory.GetDirectories(path);/*目錄(含路徑)的陣列*/
                string[] dirs = GetFTPDirctory(path);/*目錄的陣列*/
                List<string> sss = new List<string>();
                List<string> sss2 = new List<string>();

                foreach (string item in dirs)
                {
                    if (!item.EndsWith("POS"))
                    {
                        sss.Add(item);
                        string[] xx = fc.Split(@"\", item);
                        sss2.Add(xx[xx.Length - 1]);//走訪每個元素只取得目錄名稱(不含路徑)並加入dirlist集合中
                    }
                }
                //---------------------------------------------------
                //设置一个最小值
                pbc01.Properties.Minimum = 0;
                //设置一个最大值
                pbc01.Properties.Maximum = sss.Count;
                //设置步长，即每次增加的数
                pbc01.Properties.Step = 1;
                //设置进度条的样式
                pbc01.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                pbc01.Position = 0;
                //---------------------------------------------------
                for (int i = 0; i < sss.Count; i++)
                {
                    path2 = path + "/" + sss2[i] + @"/Tools/TransDB/";
                    //if (!fc.isDirectory(path + "\\" + sss2[i] + @"\Tools"))
                    if (!FTP_isDirectoryExist(path + "/" + sss2[i] + "/", "Tools"))
                    {
                        FMsg += "資料夾:" + path + "\\" + sss2[i] + @"\Tools" + "不存在";
                        Application.DoEvents();
                        pbc01.PerformStep();
                        bar2Maxcount = 0;
                        continue;
                    }
                    //if (!fc.isDirectory(path + "\\" + sss2[i] + @"\Tools\TransDB\"))
                    if (!FTP_isDirectoryExist(path + "/" + sss2[i] + "/Tools/", "TransDB"))
                    {
                        FMsg += "資料夾:" + path + "\\" + sss2[i] + @"\Tools\TransDB\" + "不存在";
                        Application.DoEvents();
                        pbc01.PerformStep();
                        bar2Maxcount = 0;
                        continue;
                    }
                    //string[] dirs2 = Directory.GetDirectories(path2);/*目錄(含路徑)的陣列*/
                    string[] dirs2 = GetFTPDirctory(path2);

                    //设置一个最小值
                    pbc02.Properties.Minimum = 0;
                    //设置一个最大值
                    //progressBarControl2.Properties.Maximum = bar2Maxcount;
                    //设置步长，即每次增加的数
                    pbc02.Properties.Step = 1;
                    //设置进度条的样式
                    pbc02.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
                    pbc02.Position = 0;
                    if (dirs2.Length > 0) //若底下有業態
                    {
                        //---------------------------------------------------

                        foreach (string item in dirs2)
                        {
                            string[] a = GetFTPFilesName(path2 + Path.GetFileName(item), "sdd");
                            bar2Maxcount += a.Length;
                            //bar2Maxcount += System.IO.Directory.GetFiles(path2 + Path.GetFileName(item), "*.sdd").Length;
                            pbc02.Properties.Maximum = bar2Maxcount;
                        }

                        //---------------------------------------------------
                        foreach (string item in dirs2)
                        {

                            path4 = mCusPath + "\\" + sss2[i] + @"\Tools\TransDB\" + Path.GetFileName(item);

                            Get2003(path2 + Path.GetFileName(item), path4);
                        }
                    }
                    else //若底下沒有業態
                    {
                        string[] a = GetFTPFilesName(path2, "sdd");
                        pbc02.Properties.Maximum = a.Length;
                        path3 = mCusPath + "\\" + sss2[i] + @"\Tools\TransDB\";
                        Get2003(path2, path3);
                    }
                    Application.DoEvents();
                    pbc01.PerformStep();
                    bar2Maxcount = 0;
                }
                fc.ShowBoxMessage("打包完成!!\r\n" + FMsg);
            }
            catch (System.Exception ex)
            {
                fc.ShowBoxMessage("打包失敗!!\r\n" + FMsg + "\r\n" + ex.Message.ToString());
            }
        }
        public bool FTP_isDirectoryExist(string RequedstPath,string dirname)
        {
            string[] a = GetFTPDirctory(RequedstPath);
            return a.Contains(dirname);
        }
        public string[] GetFTPDirctory(string RequedstPath)
        {
            List<string> strs = new List<string>();
            try
            {
                string uri = RequedstPath;   //目标路径 path为服务器地址
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                // ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(tb_ftpid.Text, tb_ftppw.Text);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());//中文文件名

                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("d"))
                    {
                        strs.Add(line.Substring(49));
                    }

                }
                reader.Close();
                response.Close();
                return strs.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取目录出错：" + ex.Message);
            }
            return strs.ToArray();
        }

        public string[] GetFTPFilesName(string RequedstPath, string ext)
        {
            List<string> strs = new List<string>();
            try
            {
                string uri = RequedstPath;   //目标路径 path为服务器地址
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                // ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(tb_ftpid.Text, tb_ftppw.Text);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());//中文文件名

                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (!line.StartsWith("d"))
                    {
                        if (line.ToUpper().EndsWith(ext.ToUpper()))
                        {
                            strs.Add(line.Substring(49));
                        }
                    }
                }
                reader.Close();
                response.Close();
                return strs.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取文件出错：" + ex.Message);
            }
            return strs.ToArray();
        }

        public void FTP_MakeDic(string RequedstPath)
        {
            try
            {
                string uri = RequedstPath;   //目标路径 path为服务器地址
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                // ftp用户名和密码
                reqFTP.Credentials = new NetworkCredential(tb_ftpid.Text, tb_ftppw.Text);
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

                using (var response = (FtpWebResponse)reqFTP.GetResponse())
                {
                    //Console.WriteLine(response.StatusCode);
                    fc.WriteLog("FTP MakeDic:" + RequedstPath + ",response:" + response.StatusCode, true);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void DownloadFtpFile(string source, string localPath,string fileName)
        {

            FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(source + fileName);
            requestFileDownload.Credentials = new NetworkCredential(tb_ftpid.Text, tb_ftppw.Text);
            requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();

            Stream responseStream = responseFileDownload.GetResponseStream();
            FileStream writeStream = new FileStream(localPath + fileName, FileMode.Create);

            int Length = 2048;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();

            requestFileDownload = null;
            responseFileDownload = null;
        }

        private void DownloadFiles1()
        {
            //1.   File Download From FTP:
            string localPath = @"D:\FTPTrialLocalPath\";
            string fileName = "del_temp.bat";

            FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create("ftp://10.40.40.101/Cosmos安裝片/" + fileName);
            requestFileDownload.Credentials = new NetworkCredential("cosmos", "asdfghjkl;'");
            requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();

            Stream responseStream = responseFileDownload.GetResponseStream();
            FileStream writeStream = new FileStream(localPath + fileName, FileMode.Create);

            int Length = 2048;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);

            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            responseStream.Close();
            writeStream.Close();

            requestFileDownload = null;
            responseFileDownload = null;
        }
    }

}
