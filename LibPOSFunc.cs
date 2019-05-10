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
    // �g�k�@
    // var
    // mProgName: array[0..MAX_PATH] of char;
    // mCreateTime: TFileTime;
    // mAccessTime: TFileTime;
    // mModifyTime: TFileTime;
    // mFileHND: THandle;
    // mTime: TSystemTime;
    // mFormat: TTimeZoneInformation;
    // begin
    // // ���ɦW
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
    // Result:=Result+ ' �ק���: '+ FormatDateTime('YYYY/MM/DD HH:NN:SS', SystemTimeToDateTime(mTime));
    // end;
    // FileClose(mFileHND);
    // except
    // end;
    // end;
    // �g�k�G
    // ���ɦW
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
    // 1.�@��r��(��2�X�_�B��3�X)
    // 2.���
    // 3.�Ҹ�
    // 4.E-MAIL
    // 5.�m�W
    // 6.�q��
    // 7.�a�}
    // 8.�Ȧ�b��
    // 9.�H�Υd��
    // 10.��L�]�ȱ��p, ���|����
        public static string[, ] DefaultMASKArray = {{'1', '2', '3', '0', '*'}, {'2', '0', '0', '0', '*'}, {'3', '0', '0', '3', '*'}, {'4', '@', '0', '0', '*'}, {'5', '0', '0', '0', '*'}, {'1', '5', '3', '0', '*'}, {'1', '7', '0', '0', '*'}, {'1', '7', '4', '0', '*'}, {'1', '6', '5', '0', '*'}, {'1', '7', '0', '0', '*'}};
        public const int MXColors = 255;
        public const string SurLib = "DSCPOSL01.DLL";
        // �ˬd�Τ@�s��
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

        // �r��ѱK
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

        // �r�����K;
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

        // �ˬd�O�_�O�Ʀr
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

        // �ѪR�}�C�̤j�� �۷�� VarArrayHighBound
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

        // �w�ˮɬO�_���w�ϥ�Terminal�Ҧ�
        // function IsUseTerminal: Boolean;
        // �O�� Exception �� LOG ��, �S�� Exception��, �ǤJ nil �Y�i
        public static void HandleError(Exception xExcept, string xFromProcName, string xProgramerMessage, string xLogFilePath)
        {/*
            int i;
            int mErrorCode;
            string mErrorMsg;
            int mErrorCount;
            xFromProcName = iif(xFromProcName.Trim() != "", " on " + xFromProcName + ' ', "");
            WriteToAlertFile("----------------------------------" + xFromProcName + "----------------------------------", xLogFilePath);
            //@ Unsupported function or procedure: 'FormatDateTime'
            WriteToAlertFile("�o�ͮɶ�: " + FormatDateTime("yyyy/mm/dd hh:nn:ss:zzz", DateTime.Now), xLogFilePath);
            mErrorCount = 0;
            if (xExcept != null)
            {
                WriteToAlertFile("�T�����:", xLogFilePath);
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

        // ��~�O�N��
        public static byte GetProductModuleID()
        {
            byte result = 0;
            return result;
        }

        // ��~�O�W��
        public static string GetProductModuleName()
        {
            string result = String.Empty;
            return result;
        }

        // �w�˼Ҧ�
        public static bool GetUseTerminal()
        {
            bool result = false;
            return result;
        }

        // ���o�ȪA�N�� //20100329 modi by 3188 for �W�[�Ӯ׳B�z
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
        // ���X�l�r��, �ñq��r�ꤤ�R���Ӥl�r��
        // *********************************************
        public static string CutToken(ref string sString, string sDelim)
        {
            string result = String.Empty;
            return result;
        }

        // ���X�����j�Ÿ����l�r���`��
        public static int NumToken(string AString, string SepStr)
        {
            int result = 0;
            return result;
        }

        // ���X�����j�Ÿ������r�ꤤ���l�r��
        public static string GetToken(string AString, string SepStr, int TokenNum)
        {
            string result = String.Empty;
            return result;
        }

        // ���N Token �r�� ...��
        public static string ReplaceToken(string AString, string SepStr, int TokenNum, string xNew)
        {
            string result = String.Empty;
            return result;
        }

        // *********************************************
        // �Ǧ^�@�өT�w���ץB�b�Ʀr�e���ɹs���r��
        // *********************************************
        // �̪��u��� 15 �Ӥw....
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

        // �e�m�ť�WideString
        public static string StrSpaceW(string xStr, int xLen)
        {
            string result = String.Empty;
            return result;
        }

        // �e�m�ť�
        public static string StrSpace(string xStr, int xLen)
        {
            string result = String.Empty;
            return result;
        }

        // �ƭ�(�p��)�榡�ƥ]�t�e�m�Ÿ�
        public static string NumFmt(int xLen, int xDecLen, double xValue, string xSign, string xFmt)
        {
            string result = String.Empty;
            return result;
        }

        // �ˬd���q�Τ@�s��
        public static bool CheckCompanyID(string Value)
        {
            bool result = false;
            return result;
        }

        // �L�o�^��r
        public static void FilterNum(ref string xValue)
        {
        }

        // �P�_�d��
        public static bool IsInRange(object xValue, double xBg, double xEd)
        {
            bool result = false;
            return result;
        }

        // �e�m�ɺ��r��
        public static string FillCharStr(string xStr, char xChar, int xTotLen)
        {
            string result = String.Empty;
            return result;
        }

        // �m���r��
        public static string CenterStr(string xStr, int xWidth)
        {
            string result = String.Empty;
            return result;
        }

        // �r����B�I�ƥi�Ǥ��w��
        public static double StrToFloatDef(string xValue, double xDefault)
        {
            double result = 0;
            return result;
        }

        // �r��B�n
        public static string StrMask(string xFormat, string xString)
        {
            string result = String.Empty;
            return result;
        }

        // �|�ˤ��J
        public static double GetRound(int xPrecision, double xValue)
        {
            double result = 0;
            return result;
        }

        // �}�ҥؿ��s��
        public static int BrowseCallbackProc(HWND xhwnd, int xMsg, int xlParam, int xlpData)
        {
            int result = 0;
            return result;
        }

        // �}�ҥؿ��s��
        public static bool SelectDirectory(string xTitle, ref string xSelectDir, bool xPrinterOnly)
        {
            bool result = false;
            return result;
        }

        // �� YYYYMMDD ������榡�r���ন DATETIME
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

        // �t�αj������
        public static void ShutDownSystem(bool xReboot)
        {
        }

        public void VirtualKey_Send_event(ushort xVKey)
        {
        }

        // ��������
        public static void VirtualKey(TVirtualKey xKey)
        {
        }

        public static int IncX(ref int X)
        {
            int result = 0;
            return result;
        }

        // �O�_�ť�
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

        // �ˬdVariantArray
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

        // ���o�ثe���|
        public static string GetCurrPath()
        {
            string result = String.Empty;
            return result;
        }

        // ��ĵ�n�T
        public static void SndAlert(TSndType xType)
        {
        }

        // �]�wADO Connection String
        public static string SetADOStr(string xDBInfo, string xTimeOut)
        {
            string result = String.Empty;
            return result;
        }

        // �R��ini����Section
        public static void EraseIniSection(string xIniName, string xSectionName)
        {
        }

        public static void ErrorLog(string xKind, string xMessage, string xUser, string xInputBuff)
        {
        }

        // ���o��O�X
        public static LCID GetLocaleID(TLocaleID xLocaleID)
        {
            LCID result = null;
            return result;
        }

        // ���o���G���C��
        public static Color GetHighLighColor(Color col1, Color col2, int xRange)
        {
            Color result = null;
            return result;
        }

        // ���B�ʤ��� xAmt: ���B, xNumerator(���l), xDenominator(����)
        public static double CalcPercentAmt(double xAmt, double xNumerator, double xDenominator)
        {
            double result = 0;
            return result;
        }

        // �ϥ�BlowFish���K
        // function ERPEncrypt(xValue: string): string;
        // begin
        // with TBlowFish.Create do //�K�X�[�J�s�X
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
        // //�ϥ�BlowFish�ѱK
        // function ERPDecrypt(xValue: string): string;
        // begin
        // with TBlowFish.Create do //�K�X�[�J�s�X
        // begin
        // try
        // Result := Decrypt(xValue);
        // finally
        // Free;
        // end;
        // end;
        // end;
        // �p��q�l�o�����X�ˮֽX
        public static string Get_eInvNoCheckNo(string xInvNo)
        {
            string result = String.Empty;
            return result;
        }

        // �Ʀr��j�g
        public static string Num2Chinese(string xValue)
        {
            string result = String.Empty;
            return result;
        }

        // Package--�إ�Form Class
        public static Form CreateFormByClassName(string ClassName)
        {
            Form result = null;
            return result;
        }

        // Package--�إ�DataModule Class
        public static TDataModule CreateDataModuleByClassName(string ClassName)
        {
            TDataModule result = null;
            return result;
        }

        // Package--����Package Module
        public static void UnLoadAddInPackage(long ModuleInstance)
        {
        }

        // �Ʀr�त��j�g
        public static string GetChineseDigit(string xValue)
        {
            string result = String.Empty;
            return result;
        }

        // ���JDLL
        public static long LoadDLL(string xDLLName, bool xLoadModi)
        {
            long result = 0;
            return result;
        }

        // ����DLL;
        public static bool ReleaseDLL(long xHandle)
        {
            bool result = false;
            return result;
        }

        // 20080410 Add By Yen For Vista
        // ���o �S���Ƨ� ����m
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

        // ����IP�s�b
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

        // �|�ˤ��J
        public static double FourOutFiveIn(int xPrecision, double xValue)
        {
            double result = 0;
            return result;
        }

        // �������
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

        // (* V�O���� H����  *)
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
        // 1.�@��r��(��2�X�_�B��3�X)
        // 2.���
        // 3.�Ҹ�
        // 4.E-MAIL
        // 5.�m�W
        // 6.�q��
        // 7.�a�}
        // 8.�Ȧ�b��
        // 9.�H�Υd��
        // 10.��L�]�ȱ��p, ���|����(��7�X�_�����B��)
        public static string DefaultMask(int xMaskKind, string xbeforStr, string xType)
        {
            string result = String.Empty;
            return result;
        }

        // �P�_�r��̫�@�X�O�_DoubleType��LeadByte,�Y�O�h�H�ťոm��
        public static string CheckLeadingCode(string xValue, char xReplaceChar)
        {
            string result = String.Empty;
            return result;
        }

        // �P�_�̫�char�O�_��DoubleByte��LeadingByte
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

        // ���o����
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

        // ���o�I�ڧO�ѧO�X
        public static int CheckPayIndex(string xPayID)
        {
            int result = 0;
            return result;
        }

        // ���o���w�I�ڧO
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

        // �m����Ʈw�W��
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

        // �٭�TVipBonusType
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

