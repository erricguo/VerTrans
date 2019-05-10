using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.Runtime.InteropServices;
using LibDefine;
using LibJSON;
namespace LibPOSFunc
{
    // 寫法一
    // var
    // mProgName: array[0..MAX_PATH] of char;
    // mCreateTime: TFileTime;
    // mAccessTime: TFileTime;
    // mModifyTime: TFileTime;
    // mFileHND: THandle;
    // mTime: TSystemTime;
    // mFormat: TTimeZoneInformation;
    // begin
    // // 取檔名
    // FillChar(mProgName, sizeof(mProgName), #0);
    // GetModuleFileName(hInstance, mProgName, SizeOf(mProgName));
    // try
    // mFileHND:= FileOpen(mProgName, fmOpenRead or fmShareDenyNone);
    // if mFileHND<> 0 then
    // if getfiletime(mFileHND, @mCreateTime, @mAccessTime, @mModifyTime) then
    // begin
    // GetTimeZoneInformation(mFormat);
    // FileTimeToSystemTime(mModifyTime, mTime);
    // SystemTimeToTzSpecificLocalTime(@mFormat, mTime, mTime);
    // Result:=Result+ ' 修改日期: '+ FormatDateTime('YYYY/MM/DD HH:NN:SS', SystemTimeToDateTime(mTime));
    // end;
    // FileClose(mFileHND);
    // except
    // end;
    // end;
    // 寫法二
    // 取檔名
    // end of 20020611 Yen Add S60-9106001
    public struct TTranslation
    {
        public ushort langID;
        public ushort charset;
    } // end TTranslation

    public struct TExData
    {
        public string Path;
        public string Caption;
    } // end TExData

}

namespace LibPOSFunc.Units
{
    public class LibPOSFunc
    {
    //@ Undeclared identifier(3): 'INFINITE'
    //@ Undeclared identifier(3): 'SW_SHOW'
    //@ Undeclared identifier(3): 'SW_SHOW'
    // 1.一般字串(第2碼起遮蔽3碼)
    // 2.日期
    // 3.證號
    // 4.E-MAIL
    // 5.姓名
    // 6.電話
    // 7.地址
    // 8.銀行帳戶
    // 9.信用卡號
    // 10.其他財務情況, 社會活動
        public static string[, ] DefaultMASKArray = {{'1', '2', '3', '0', '*'}, {'2', '0', '0', '0', '*'}, {'3', '0', '0', '3', '*'}, {'4', '@', '0', '0', '*'}, {'5', '0', '0', '0', '*'}, {'1', '5', '3', '0', '*'}, {'1', '7', '0', '0', '*'}, {'1', '7', '4', '0', '*'}, {'1', '6', '5', '0', '*'}, {'1', '7', '0', '0', '*'}};
        public const int MXColors = 255;
        public const string SurLib = "DSCPOSL01.DLL";
        // 檢查統一編號
        // , Blowfish
        public static bool CheckisInteger(string UnifiedNumber)
        {
            bool result;
            int i;
            result = false;
            for (i = 1; i <= UnifiedNumber.Length; i ++ )
            {
                if (!(UnifiedNumber[i] >= '0' && UnifiedNumber[i]<= '9'))
                {
                    return result;
                }
            }
            result = true;
            return result;
        }

        // 字串解密
        public static bool CheckNum(string UnifiedNumber)
        {
            bool result;
            int[] cx;
            int i;
            int mi;
            result = false;
            if (UnifiedNumber.Length != 8)
            {
                return result;
            }
            mi = 0;
            cx = new int[8];
            if (!CheckisInteger(UnifiedNumber))
            {
                return result;
            }
            for (i = cx.GetLowerBound(0); i <= cx.GetUpperBound(0); i ++ )
            {
                if (i == 7)
                {
                    cx[i] = Convert.ToInt32(UnifiedNumber[i + 1]) * 4;
                }
                else if (i == 8)
                {
                    cx[i] = Convert.ToInt32(UnifiedNumber[i + 1]);
                }
                else if (i % 2 == 1)
                {
                    cx[i] = Convert.ToInt32(UnifiedNumber[i + 1]) * 2;
                }
                else
                {
                    cx[i] = Convert.ToInt32(UnifiedNumber[i + 1]);
                }
                if ((cx[i]).ToString().Length == 2)
                {
                    cx[i] = Convert.ToInt32(((cx[i]).ToString())[1]) + Convert.ToInt32(((cx[i]).ToString())[2]);
                }
            }
            for (i = cx.GetLowerBound(0); i <= cx.GetUpperBound(0); i ++ )
            {
                mi = mi + cx[i];
            }
            if ((mi % 10) == 0)
            {
                result = true;
            }
            else
            {
                if (UnifiedNumber[7] == '7')
                {
                    if (((mi + 1) % 10) == 0)
                    {
                        result = true;
                    }
                }
            }
            cx = null;
            return result;
        }

        public static string Encrypt(string S, string KeyWord)
        {
            string result;
            const int C1 = 52845;
            const int C2 = 22719;
            int I;
            int Key;
            string mStr;
            if (KeyWord == "")
            {
                KeyWord = "12375447";
            }
            mStr = S;
            result = mStr;
            Key = 0;
            for (I = 1; I <= KeyWord.Length; I ++ )
            {
                Key = Key + ((byte)KeyWord[I]);
            }
            for (I = 1; I <= mStr.Length; I ++ )
            {
                result[I] = ((char)((byte)mStr[I]) ^ (Key >> 8));
                Key = (((byte)result[I]) + Key) * C1 + C2;
            }
            mStr = result;
            result = "";
            for (I = 1; I <= mStr.Length; I ++ )
            {
                //@ Unsupported function or procedure: 'Format'
                result = result + Format("%.*d", new int[] {3, ((int)mStr[I])});
            }
            return result;
        }

        // 字串壓密;
        public static string Decrypt(string S, string KeyWord)
        {
            string result;
            const int C1 = 52845;
            const int C2 = 22719;
            int I;
            int Key;
            string mTmp;
            if (KeyWord == "")
            {
                KeyWord = "12375447";
            }
            I = 1;
            while (I <= S.Length)
            {
                mTmp = mTmp + ((char)Convert.ToInt32(S.Substring(I - 1 ,3)));
                I = I + 3;
            }
            result = mTmp;
            Key = 0;
            for (I = 1; I <= KeyWord.Length; I ++ )
            {
                Key = Key + ((byte)KeyWord[I]);
            }
            for (I = 1; I <= mTmp.Length; I ++ )
            {
                result[I] = ((char)((byte)mTmp[I]) ^ (Key >> 8));
                Key = (((byte)mTmp[I]) + Key) * C1 + C2;
            }
            return result;
        }

        // 檢查是否是數字
        public static void WaitForIdle(int Millisecond)
        {
            int i;
            for (i = 1; i <= (Millisecond / 10); i ++ )
            {
                //@ Unsupported property or method(C): 'ProcessMessages'
                Application.ProcessMessages;
                Thread.CurrentThread.Sleep(10);
            }
        }

        public static void WaitForVar(ref bool Obj, bool Target, uint TimeOut)
        {
            int Time;
            //@ Undeclared identifier(3): 'INFINITE'
            if (TimeOut == INFINITE)
            {
                do
                {
                    WaitForIdle(10);
                } while (!((Obj == Target)));
            }
            else
            {
                Time = 0;
                do
                {
                    if (Obj == Target)
                    {
                        break;
                    }
                    Time = Time + 10;
                    WaitForIdle(10);
                } while (!((Time >= TimeOut)));
            }
        }

        [ DllImport("User32.dll")]
        public static extern object ERPEncrypt();

        [ DllImport("User32.dll")]
        public static extern object ERPDecrypt();

        // 解析陣列最大值 相當於 VarArrayHighBound
        public static string AnsiFormat(string xFormat, object[] xArgs)
        {
            string result;
            char[] mBuffer = new char[2048 + 1];
            //@ Unsupported function or procedure: 'StrFmt'
            result = StrFmt(mBuffer, (xFormat as string), xArgs);
            return result;
        }

        public static string AnsiCopy(string xStr, int xBG, int xED)
        {
            string result;
            result = (xStr.Substring(xBG - 1 ,xED) as string);
            return result;
        }

        public static object iif(bool xCheck, object xValue1, object xValue2)
        {
            object result;
            if (xCheck)
            {
                result = xValue1;
            }
            else
            {
                result = xValue2;
            }
            return result;
        }

        // 安裝時是否指定使用Terminal模式
        // function IsUseTerminal: Boolean;
        // 記錄 Exception 到 LOG 檔, 沒有 Exception時, 傳入 nil 即可
        public static void HandleError(Exception xExcept, string xFromProcName, string xProgramerMessage, string xLogFilePath)
        {/*
            int i;
            int mErrorCode;
            string mErrorMsg;
            int mErrorCount;
            xFromProcName = iif(xFromProcName.Trim() != "", " on " + xFromProcName + ' ', "");
            WriteToAlertFile("----------------------------------" + xFromProcName + "----------------------------------", xLogFilePath);
            //@ Unsupported function or procedure: 'FormatDateTime'
            WriteToAlertFile("發生時間: " + FormatDateTime("yyyy/mm/dd hh:nn:ss:zzz", DateTime.Now), xLogFilePath);
            mErrorCount = 0;
            if (xExcept != null)
            {
                WriteToAlertFile("訊息資料:", xLogFilePath);
                //@ Undeclared identifier(3): 'EDBEngineError'
                if (xExcept is EDBEngineError)
                {
                    //@ Undeclared identifier(3): 'EDBEngineError'
                    //@ Unsupported property or method(D): 'ErrorCount'
                    for (i = 0; i < EDBEngineError(xExcept).ErrorCount; i ++ )
                    {
                        mErrorCount ++;
                        //@ Undeclared identifier(3): 'EDBEngineError'
                        //@ Unsupported property or method(B): 'Errors'
                        //@ Unsupported property or method(D): 'ErrorCode'
                        mErrorCode = EDBEngineError(xExcept).Errors[i].ErrorCode;
                        //@ Undeclared identifier(3): 'EDBEngineError'
                        //@ Unsupported property or method(B): 'Errors'
                        //@ Unsupported property or method(D): 'Message'
                        mErrorMsg = EDBEngineError(xExcept).Errors[i].Message;
                        //@ Unsupported function or procedure: 'Format'
                        WriteToAlertFile(Format("EDBEngineError Error %.2d: Code: %d, Message: %s", new string[] {mErrorCount, mErrorCode, mErrorMsg}), xLogFilePath);
                    }
                }
                else if (xExcept is Exception)
                {
                    mErrorCount ++;
                    //@ Unsupported property or method(C): 'ErrorCode'
                    mErrorCode = ((xExcept) as Exception).ErrorCode;
                    mErrorMsg = ((xExcept) as Exception).Message;
                    //@ Unsupported function or procedure: 'Format'
                    WriteToAlertFile(Format("EWin32Error Error %.2d: Code: %d, Message: %s", new string[] {mErrorCount, mErrorCode, mErrorMsg}), xLogFilePath);
                }
                else if (xExcept is Exception)
                {*/
        }

        public static void HandleError(Exception xExcept, string xFromProcName, string xProgramerMessage)
        {
            HandleError(xExcept, xFromProcName, xProgramerMessage, "");
        }

        public static bool WriteToAlertFile(string xMessage, string xLogFilePath)
        {
            bool result = false;
            return result;
        }

        public static object GetInstallInfo(int xIndex)
        {
            object result = null;
            return result;
        }

        public static string GetInstallPath(bool xMustResult)
        {
            string result = String.Empty;
            return result;
        }

        public static string GetPrivatePath()
        {
            string result = String.Empty;
            return result;
        }

        // 行業別代號
        public static byte GetProductModuleID()
        {
            byte result = 0;
            return result;
        }

        // 行業別名稱
        public static string GetProductModuleName()
        {
            string result = String.Empty;
            return result;
        }

        // 安裝模式
        public static bool GetUseTerminal()
        {
            bool result = false;
            return result;
        }

        // 取得客服代號 //20100329 modi by 3188 for 增加個案處理
        public static string GetCTICustNo()
        {
            string result = String.Empty;
            return result;
        }

        // function IsUseTerminal: Boolean;
        // begin
        // Result:= False;
        // with TRegistry.Create do
        // try
        // RootKey:= HKEY_LOCAL_MACHINE;
        // if KeyExists(C_COSMOSPOS_REG) then
        // if OpenKeyReadOnly(C_COSMOSPOS_REG) then
        // if ValueExists(C_COSMOSPOS_USETERMINAL) then
        // Result:= ReadBool(C_COSMOSPOS_USETERMINAL);
        // finally
        // CloseKey;
        // Free;
        // end;
        // end;
        public static string GetLocalMachineIPAddress()
        {
            string result = String.Empty;
            return result;
        }

        public static bool IsLocalAddress(string xAddress)
        {
            bool result = false;
            return result;
        }

        public static string GetHostbyAddress(string xAddress)
        {
            string result = String.Empty;
            return result;
        }

        public static string SafeGetComputerName()
        {
            string result = String.Empty;
            return result;
        }

        public static string GetLocalComputerName()
        {
            string result = String.Empty;
            return result;
        }

        public static string GetOSUserID()
        {
            string result = String.Empty;
            return result;
        }

        public static string SwapStr(string xString, string xOldSplit, string xNewSplit, bool xTrim)
        {
            string result = String.Empty;
            return result;
        }

        public static void CallSetMSSQL(TADOConnection xADOConnection)
        {
        }

        // start for 20020611 Yen Add S60-9106001
        public static string GetFileInfo()
        {
            string result = String.Empty;
            return result;
        }

        public static string GetDllVersion(string xFileName)
        {
            string result = String.Empty;
            return result;
        }

        // *********************************************
        // 取出子字串, 並從原字串中刪除該子字串
        // *********************************************
        public static string CutToken(ref string sString, string sDelim)
        {
            string result = String.Empty;
            return result;
        }

        // 取出有間隔符號的子字串總數
        public static int NumToken(string AString, string SepStr)
        {
            int result = 0;
            return result;
        }

        // 取出有間隔符號的長字串中的子字串
        public static string GetToken(string AString, string SepStr, int TokenNum)
        {
            string result = String.Empty;
            return result;
        }

        // 取代 Token 字串 ...明
        public static string ReplaceToken(string AString, string SepStr, int TokenNum, string xNew)
        {
            string result = String.Empty;
            return result;
        }

        // *********************************************
        // 傳回一個固定長度且在數字前面補零的字串
        // *********************************************
        // 最長只能到 15 而已....
        public static string ZeroAtFirst(object xValue, int xLen)
        {
            string result = String.Empty;
            return result;
        }

        public static bool IsVarEmptyOrNull(object xVariant)
        {
            bool result = false;
            return result;
        }

        // 前置空白WideString
        public static string StrSpaceW(string xStr, int xLen)
        {
            string result = String.Empty;
            return result;
        }

        // 前置空白
        public static string StrSpace(string xStr, int xLen)
        {
            string result = String.Empty;
            return result;
        }

        // 數值(小數)格式化包含前置符號
        public static string NumFmt(int xLen, int xDecLen, double xValue, string xSign, string xFmt)
        {
            string result = String.Empty;
            return result;
        }

        // 檢查公司統一編號
        public static bool CheckCompanyID(string Value)
        {
            bool result = false;
            return result;
        }

        // 過濾英文字
        public static void FilterNum(ref string xValue)
        {
        }

        // 判斷範圍內
        public static bool IsInRange(object xValue, double xBg, double xEd)
        {
            bool result = false;
            return result;
        }

        // 前置補滿字元
        public static string FillCharStr(string xStr, char xChar, int xTotLen)
        {
            string result = String.Empty;
            return result;
        }

        // 置中字串
        public static string CenterStr(string xStr, int xWidth)
        {
            string result = String.Empty;
            return result;
        }

        // 字串轉浮點數可傳內定值
        public static double StrToFloatDef(string xValue, double xDefault)
        {
            double result = 0;
            return result;
        }

        // 字串遮罩
        public static string StrMask(string xFormat, string xString)
        {
            string result = String.Empty;
            return result;
        }

        // 四捨五入
        public static double GetRound(int xPrecision, double xValue)
        {
            double result = 0;
            return result;
        }

        // 開啟目錄瀏覽
        public static int BrowseCallbackProc(HWND xhwnd, int xMsg, int xlParam, int xlpData)
        {
            int result = 0;
            return result;
        }

        // 開啟目錄瀏覽
        public static bool SelectDirectory(string xTitle, ref string xSelectDir, bool xPrinterOnly)
        {
            bool result = false;
            return result;
        }

        // 把 YYYYMMDD 的日期格式字串轉成 DATETIME
        public static DateTime TransDateIn(string Value)
        {
            DateTime result;
            return result;
        }

        public static string DateToString(DateTime xDate, int xType, string xDelim)
        {
            string result = String.Empty;
            return result;
        }

        public static DateTime StringToDate(string xDate, int xType, string xDelim)
        {
            DateTime result;
            return result;
        }

        // 系統強制關機
        public static void ShutDownSystem(bool xReboot)
        {
        }

        public void VirtualKey_Send_event(ushort xVKey)
        {
        }

        // 模擬按鍵
        public static void VirtualKey(TVirtualKey xKey)
        {
        }

        public static int IncX(ref int X)
        {
            int result = 0;
            return result;
        }

        // 是否空白
        public static bool IsEmpty(object xValue)
        {
            bool result = false;
            return result;
        }

        public static void RUN_DOS_COMMAND(string xExeFileName, string xExeDirectory, string xParameters, int xShow)
        {
        }

        // 32-bit version
        public static uint WinExecAndWait32(string xFileName, int xVisibility)
        {
            uint result = 0;
            return result;
        }

        public static void WaitFor(string xWaitFileName)
        {
        }

        public static string GetLastErrorString(int xErrorID)
        {
            string result = String.Empty;
            return result;
        }

        public static bool CheckDate(string Value)
        {
            bool result = false;
            return result;
        }

        public static bool SetFileReadonly(string xFileName, bool xReadOnly)
        {
            bool result = false;
            return result;
        }

        public static string ConnectADO(object xValue)
        {
            string result = String.Empty;
            return result;
        }

        public static string CheckInvoiceNo(string Value)
        {
            string result = String.Empty;
            return result;
        }

        // 檢查VariantArray
        public static bool ValidVarArray(object V)
        {
            bool result = false;
            return result;
        }

        public static bool SetPrivilege(string xPrivilegeName, bool xEnabled)
        {
            bool result = false;
            return result;
        }

        public static bool WinExitInNT(int xFlags)
        {
            bool result = false;
            return result;
        }

        public static void SendClickMessage(Control xControl, int wParam, int lParam)
        {
        }

        public static string GetTmpAdminPass()
        {
            string result = String.Empty;
            return result;
        }

        public static DateTime AssignMonthFirst(DateTime date)
        {
            DateTime result;
            return result;
        }

        public static DateTime AssignMonthEnd(DateTime date)
        {
            DateTime result;
            return result;
        }

        public static object Tax(object xSum, string xTaxKind, object xTax)
        {
            object result = null;
            return result;
        }

        // 取得目前路徑
        public static string GetCurrPath()
        {
            string result = String.Empty;
            return result;
        }

        // 示警聲響
        public static void SndAlert(TSndType xType)
        {
        }

        // 設定ADO Connection String
        public static string SetADOStr(string xDBInfo, string xTimeOut)
        {
            string result = String.Empty;
            return result;
        }

        // 刪除ini中的Section
        public static void EraseIniSection(string xIniName, string xSectionName)
        {
        }

        public static void ErrorLog(string xKind, string xMessage, string xUser, string xInputBuff)
        {
        }

        // 取得國別碼
        public static LCID GetLocaleID(TLocaleID xLocaleID)
        {
            LCID result = null;
            return result;
        }

        // 取得高亮度顏色
        public static Color GetHighLighColor(Color col1, Color col2, int xRange)
        {
            Color result = null;
            return result;
        }

        // 金額百分比 xAmt: 金額, xNumerator(分子), xDenominator(分母)
        public static double CalcPercentAmt(double xAmt, double xNumerator, double xDenominator)
        {
            double result = 0;
            return result;
        }

        // 使用BlowFish壓密
        // function ERPEncrypt(xValue: string): string;
        // begin
        // with TBlowFish.Create do //密碼加入編碼
        // begin
        // try
        // Result := Encrypt(xValue);
        // finally
        // Free;
        // end;
        // end;
        // 
        // end;
        // 
        // //使用BlowFish解密
        // function ERPDecrypt(xValue: string): string;
        // begin
        // with TBlowFish.Create do //密碼加入編碼
        // begin
        // try
        // Result := Decrypt(xValue);
        // finally
        // Free;
        // end;
        // end;
        // end;
        // 計算電子發票號碼檢核碼
        public static string Get_eInvNoCheckNo(string xInvNo)
        {
            string result = String.Empty;
            return result;
        }

        // 數字轉大寫
        public static string Num2Chinese(string xValue)
        {
            string result = String.Empty;
            return result;
        }

        // Package--建立Form Class
        public static Form CreateFormByClassName(string ClassName)
        {
            Form result = null;
            return result;
        }

        // Package--建立DataModule Class
        public static TDataModule CreateDataModuleByClassName(string ClassName)
        {
            TDataModule result = null;
            return result;
        }

        // Package--釋放Package Module
        public static void UnLoadAddInPackage(long ModuleInstance)
        {
        }

        // 數字轉中文大寫
        public static string GetChineseDigit(string xValue)
        {
            string result = String.Empty;
            return result;
        }

        // 載入DLL
        public static long LoadDLL(string xDLLName, bool xLoadModi)
        {
            long result = 0;
            return result;
        }

        // 釋放DLL;
        public static bool ReleaseDLL(long xHandle)
        {
            bool result = false;
            return result;
        }

        // 20080410 Add By Yen For Vista
        // 取得 特殊資料夾 的位置
        public static string GetOSSpecialPath(ushort xType)
        {
            string result = String.Empty;
            return result;
        }

        public static string GetERPExportPath()
        {
            string result = String.Empty;
            return result;
        }

        public static string PadL(string str, int[] others)
        {
            string result = String.Empty;
            return result;
        }

        public static string PadR(string str, int[] others)
        {
            string result = String.Empty;
            return result;
        }

        public static string DateGetText(TField Sender)
        {
            string result = String.Empty;
            return result;
        }

        public static string TimeGetText(TField Sender)
        {
            string result = String.Empty;
            return result;
        }

        // 偵測IP存在
        public static bool CheckIPExist(string IPAddr, ref string MacName)
        {
            bool result = false;
            return result;
        }

        public static long DsTrunc(double Value)
        {
            long result = 0;
            return result;
        }

        // 四捨五入
        public static double FourOutFiveIn(int xPrecision, double xValue)
        {
            double result = 0;
            return result;
        }

        // 算日期函數
        public static string CountDate(object xSourceDate, string xKind, int xMove)
        {
            string result = String.Empty;
            return result;
        }

        public static int MonthDay(int xYear, int xMonth)
        {
            int result = 0;
            return result;
        }

        // (* V是垂直 H水平  *)
        public static int PMTswitchCount(string s)
        {
            int result = 0;
            return result;
        }

        public static double PMTswitchFloat(string xPmtStr, int v, int h)
        {
            double result = 0;
            return result;
        }

        public static int PMTswitchInt(string xPmtStr, int v, int h)
        {
            int result = 0;
            return result;
        }

        public static string PMTswitchStr(string xPmtStr, int v, int h)
        {
            string result = String.Empty;
            return result;
        }

        public static string PIPMASK(string xMA003, string xMA004, string xMA005, string xMA006, string xMA007, string xbeforStr, string xType)
        {
            string result = String.Empty;
            return result;
        }

        public static string ReplaceMaskstr(string xbeforStr, int xreplacebegin, int xreplaceend, string xmask)
        {
            string result = String.Empty;
            return result;
        }

        public static string ReplaceMasknum(string xbeforStr, int xreplacebegin, int xreplaceend, int xreplaceNum, string xmask)
        {
            string result = String.Empty;
            return result;
        }

        // xMaskKind:
        // 1.一般字串(第2碼起遮蔽3碼)
        // 2.日期
        // 3.證號
        // 4.E-MAIL
        // 5.姓名
        // 6.電話
        // 7.地址
        // 8.銀行帳戶
        // 9.信用卡號
        // 10.其他財務情況, 社會活動(第7碼起全部遮蔽)
        public static string DefaultMask(int xMaskKind, string xbeforStr, string xType)
        {
            string result = String.Empty;
            return result;
        }

        // 判斷字串最後一碼是否DoubleType的LeadByte,若是則以空白置換
        public static string CheckLeadingCode(string xValue, char xReplaceChar)
        {
            string result = String.Empty;
            return result;
        }

        // 判斷最後char是否為DoubleByte的LeadingByte
        public static bool IsLeadingCode(string xValue)
        {
            bool result = false;
            return result;
        }

        public static bool IsMainFormActive()
        {
            bool result = false;
            return result;
        }

        public static bool IsPicFileExists(ref string xPicFileName)
        {
            bool result = false;
            return result;
        }

        // 取得版號
        public static string GetFileVersion()
        {
            string result = String.Empty;
            return result;
        }

        public static int CheckInvType(string xInvType)
        {
            int result = 0;
            return result;
        }

        public static TArray<string> MergeStr(string[] xVar1, string[] xVar2)
        {
            TArray<string> result = null;
            return result;
        }

        // 取得付款別識別碼
        public static int CheckPayIndex(string xPayID)
        {
            int result = 0;
            return result;
        }

        // 取得內定付款別
        public static bool CheckDefaultPay(ref string xFuncCode, ref string xPayID)
        {
            bool result = false;
            return result;
        }

        public static Object GetObject(string xObjName)
        {
            Object result = null;
            return result;
        }

        public static void SetPubValue(TDataPoolNode xNode, string[] xStratum, TJSONValue xValue)
        {
        }

        public static void SetPubValue(TDataPoolNode xNode, string xName, object xValue)
        {
        }

        public static void SetPubValue(TDataPoolNode xNode, string xSection, string xName, object xValue)
        {
        }

        public static void SetPubValue(TDataPoolNode xNode, string xSection, string xName, string xSubName, object xValue)
        {
        }

        public static void SetPubValue(TDataPoolNode xNode, string[] xStratum, object xValue)
        {
        }

        // function GetPubValue(xName: string; xDefValue: Variant): Variant;
        // begin
        // try
        // Result := TVarPool.Pool.GetSimpleVariant(xName, xDefValue);
        // except on E: Exception do
        // ShowMessage(xName + ':' + e.message);
        // end;
        // end;
        // 
        // function GetPubValue(xSection: string; xName: string; xValue: variant): variant;
        // begin
        // try
        // Result := TVarPool.Pool.GetStratumVariant([xSection, xName], xValue);
        // except on E: Exception do
        // ShowMessage(xName + ':' + e.message);
        // end;
        // end;
        // 
        // function GetPubValue(xSection: string; xName: string; xSubName: string; xValue: variant): Variant;
        // begin
        // try
        // Result := TVarPool.Pool.GetStratumVariant([xSection, xName, xSubName], xValue);
        // except on E: Exception do
        // ShowMessage(xName + '.' + xSubName + ':' + e.message);
        // end;
        // end;
        // 
        // function GetPubValue(xStratum: array of string; xValue: variant):Variant;
        // begin
        // try
        // Result := TVarPool.Pool.GetStratumVariant(xStratum, xValue);
        // except on E: Exception do
        // ShowMessage(xStratum[High(xStratum)]  + ':' + e.message);
        // end;
        // end;
        public static void DelPubValue(TDataPoolNode xNode, string xName)
        {
        }

        public static void DelPubValue(TDataPoolNode xNode, string xSection, string xName)
        {
        }

        public static void DelPubValue(TDataPoolNode xNode, string xSection, string xName, string xSubName, object xValue)
        {
        }

        public static void DelPubValue(TDataPoolNode xNode, string[] xStratum)
        {
        }

        // 1111111111111111111111111111111111111111111111111111111111111111111 //
        public static string GetPub_S(TDataPoolNode xNode, string xName)
        {
            string result = String.Empty;
            return result;
        }

        public static int GetPub_I(TDataPoolNode xNode, string xName)
        {
            int result = 0;
            return result;
        }

        public static double GetPub_E(TDataPoolNode xNode, string xName)
        {
            double result = 0;
            return result;
        }

        public static bool GetPub_B(TDataPoolNode xNode, string xName)
        {
            bool result = false;
            return result;
        }

        public static DateTime GetPub_D(TDataPoolNode xNode, string xName)
        {
            DateTime result;
            return result;
        }

        // 1111111111111111111111111111111111111111111111111111111111111111111 //
        // 2222222222222222222222222222222222222222222222222222222222222222222 //
        public static string GetPub_S(TDataPoolNode xNode, string xSection, string xName)
        {
            string result = String.Empty;
            return result;
        }

        public static int GetPub_I(TDataPoolNode xNode, string xSection, string xName)
        {
            int result = 0;
            return result;
        }

        public static double GetPub_E(TDataPoolNode xNode, string xSection, string xName)
        {
            double result = 0;
            return result;
        }

        public static bool GetPub_B(TDataPoolNode xNode, string xSection, string xName)
        {
            bool result = false;
            return result;
        }

        public static DateTime GetPub_D(TDataPoolNode xNode, string xSection, string xName)
        {
            DateTime result;
            return result;
        }

        // 2222222222222222222222222222222222222222222222222222222222222222222 //
        // 3333333333333333333333333333333333333333333333333333333333333333333 //
        public static string GetPub_S(TDataPoolNode xNode, string xSection, string xName, string xSubName)
        {
            string result = String.Empty;
            return result;
        }

        public static int GetPub_I(TDataPoolNode xNode, string xSection, string xName, string xSubName)
        {
            int result = 0;
            return result;
        }

        public static double GetPub_E(TDataPoolNode xNode, string xSection, string xName, string xSubName)
        {
            double result = 0;
            return result;
        }

        public static bool GetPub_B(TDataPoolNode xNode, string xSection, string xName, string xSubName)
        {
            bool result = false;
            return result;
        }

        public static DateTime GetPub_D(TDataPoolNode xNode, string xSection, string xName, string xSubName)
        {
            DateTime result;
            return result;
        }

        public static string GetPub_S(TDataPoolNode xNode, string xSection, int xCount, string xSubName)
        {
            string result = String.Empty;
            return result;
        }

        public static int GetPub_I(TDataPoolNode xNode, string xSection, int xCount, string xSubName)
        {
            int result = 0;
            return result;
        }

        public static double GetPub_E(TDataPoolNode xNode, string xSection, int xCount, string xSubName)
        {
            double result = 0;
            return result;
        }

        public static bool GetPub_B(TDataPoolNode xNode, string xSection, int xCount, string xSubName)
        {
            bool result = false;
            return result;
        }

        public static DateTime GetPub_D(TDataPoolNode xNode, string xSection, int xCount, string xSubName)
        {
            DateTime result;
            return result;
        }

        // 3333333333333333333333333333333333333333333333333333333333333333333 //
        // 4444444444444444444444444444444444444444444444444444444444444 //
        public static string GetPub_S(TDataPoolNode xNode, string xSection, int xCount, string xSubName, string xVar1)
        {
            string result = String.Empty;
            return result;
        }

        public static int GetPub_I(TDataPoolNode xNode, string xSection, int xCount, string xSubName, string xVar1)
        {
            int result = 0;
            return result;
        }

        public static double GetPub_E(TDataPoolNode xNode, string xSection, int xCount, string xSubName, string xVar1)
        {
            double result = 0;
            return result;
        }

        public static bool GetPub_B(TDataPoolNode xNode, string xSection, int xCount, string xSubName, string xVar1)
        {
            bool result = false;
            return result;
        }

        public static DateTime GetPub_D(TDataPoolNode xNode, string xSection, int xCount, string xSubName, string xVar1)
        {
            DateTime result;
            return result;
        }

        public static string GetPub_S(TDataPoolNode xNode, string xSection, int xCount, string xSubName, int xVar1)
        {
            string result = String.Empty;
            return result;
        }

        public static int GetPub_I(TDataPoolNode xNode, string xSection, int xCount, string xSubName, int xVar1)
        {
            int result = 0;
            return result;
        }

        public static double GetPub_E(TDataPoolNode xNode, string xSection, int xCount, string xSubName, int xVar1)
        {
            double result = 0;
            return result;
        }

        public static bool GetPub_B(TDataPoolNode xNode, string xSection, int xCount, string xSubName, int xVar1)
        {
            bool result = false;
            return result;
        }

        public static DateTime GetPub_D(TDataPoolNode xNode, string xSection, int xCount, string xSubName, int xVar1)
        {
            DateTime result;
            return result;
        }

        // 4444444444444444444444444444444444444444444444444444444444444 //
        // 5555555555555555555555555555555555555555555555555555555555555 //
        public static string GetPub_S(TDataPoolNode xNode, string xSection, int xCount, string xSubName, int xVar1, int xVar2)
        {
            string result = String.Empty;
            return result;
        }

        public static int GetPub_I(TDataPoolNode xNode, string xSection, int xCount, string xSubName, int xVar1, int xVar2)
        {
            int result = 0;
            return result;
        }

        public static double GetPub_E(TDataPoolNode xNode, string xSection, int xCount, string xSubName, int xVar1, int xVar2)
        {
            double result = 0;
            return result;
        }

        public static bool GetPub_B(TDataPoolNode xNode, string xSection, int xCount, string xSubName, int xVar1, int xVar2)
        {
            bool result = false;
            return result;
        }

        public static DateTime GetPub_D(TDataPoolNode xNode, string xSection, int xCount, string xSubName, int xVar1, int xVar2)
        {
            DateTime result;
            return result;
        }

        // 5555555555555555555555555555555555555555555555555555555555555 //
        // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA //
        public static string GetPub_S(TDataPoolNode xNode, string[] xStratum)
        {
            string result = String.Empty;
            return result;
        }

        public static int GetPub_I(TDataPoolNode xNode, string[] xStratum)
        {
            int result = 0;
            return result;
        }

        public static double GetPub_E(TDataPoolNode xNode, string[] xStratum)
        {
            double result = 0;
            return result;
        }

        public static bool GetPub_B(TDataPoolNode xNode, string[] xStratum)
        {
            bool result = false;
            return result;
        }

        public static DateTime GetPub_D(TDataPoolNode xNode, string[] xStratum)
        {
            DateTime result;
            return result;
        }

        public static TJSONValue GetPub_J(TDataPoolNode xNode, string[] xStratum)
        {
            TJSONValue result = null;
            return result;
        }

        // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA //
        public static string GetArrayInfo(string[] xStratum)
        {
            string result = String.Empty;
            return result;
        }

        // 置換資料庫名稱
        public static string TransSQLCommand(string xDBName, string xSQL, object[] Args)
        {
            string result = String.Empty;
            return result;
        }

        public static string VarToAnsiStr(object xVariant, int xDim)
        {
            string result = String.Empty;
            return result;
        }

        public string AnsiStrToVar_ChkValue(TJSONValue xValue)
        {
            string result;
            return result;
        }

        public bool AnsiStrToVar_GetBool(string xValue)
        {
            bool result;
            return result;
        }

        public object AnsiStrToVar_GetValue(TJSONValue xValue)
        {
            object result;
            return result;
        }

        public static object AnsiStrToVar(string xAnsiString, int xDim)
        {
            object result = null;
            return result;
        }

        public static string VarToAnsiStr2(object xVariant, int xDim)
        {
            string result = String.Empty;
            return result;
        }

        public string AnsiStrToVar2_ChkValue(TJSONValue xValue)
        {
            string result;
            return result;
        }

        public bool AnsiStrToVar2_GetBool(string xValue)
        {
            bool result;
            return result;
        }

        public object AnsiStrToVar2_GetValue(TJSONValue xValue)
        {
            object result;
            return result;
        }

        public static object AnsiStrToVar2(string xAnsiString, int xDim)
        {
            object result = null;
            return result;
        }

        public static bool IncPub(TDataPoolNode xNode, string[] xStratum)
        {
            bool result = false;
            return result;
        }

        public static bool PlusPub(TDataPoolNode xNode, string[] xStratum, int xValue)
        {
            bool result = false;
            return result;
        }

        public static bool PlusPub(TDataPoolNode xNode, string[] xStratum, double xValue)
        {
            bool result = false;
            return result;
        }

        public static bool PlusPub(TDataPoolNode xNode, string[] xStratum, string xValue)
        {
            bool result = false;
            return result;
        }

        public static bool PlusPub(TDataPoolNode xNode, string[] xStratum, object Value)
        {
            bool result = false;
            return result;
        }

        public static int GetSize(TDataPoolNode xNode, string[] xVar)
        {
            int result = 0;
            return result;
        }

        public static int GetMaxPaymanetInfo()
        {
            int result = 0;
            return result;
        }

        public static int GetMaxPOSTCInfo()
        {
            int result = 0;
            return result;
        }

        // 還原TVipBonusType
        public static int CheckVipBonusType(string xValue)
        {
            int result = 0;
            return result;
        }

        public static int CheckItemType(string xValue)
        {
            int result = 0;
            return result;
        }

        public static Rectangle SetRect(int xLeft, int xTop, int xWidth, int xHight)
        {
            Rectangle result = null;
            return result;
        }

        public static bool TerminateApp(string xProcessName, bool xCloseProcess)
        {
            bool result = false;
            return result;
        }

    } // end LibPOSFunc

}

