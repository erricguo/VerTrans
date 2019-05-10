using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using LibDefine;
using ConvVariant;
using LibPOSFunc;
 // 20111124 Add By Yen For 排除非實體網卡(含有線網卡與無線網卡), Windows Vista之後才能辨識無線網卡"71(IF_TYPE_IEEE80211)", Win2003和WinXP會把無線網卡一樣辨識為 實體網卡"6(MIB_IF_TYPE_ETHERNET)"
namespace POSAuthoriseU
{
    public struct TPOSAuthoriseData
    {
        public string CustomID;
        // 客戶代號
        public string CustomerName;
        // 客戶名稱
        public byte SerialType;
        // 授權方式版號
        public ushort InstallType;
        // 授權類型(0: DEMO, 1: 正式, 2: 鼎新部門, 3: 鼎新個人)
        public ushort PosClient_UserCount;
        // POS前台購買人數
        public ushort PosERP_UserCount;
        // 後台購買人數
        public string AuthoriseDate;
        // 產生日期(不含日期分隔符號)
        public int Borrows;
        // 借貨  0: 未借  1: 借貨
        public ushort PosClient_BorrowsUserCount;
        // POS前台借貨人數
        public ushort PosERP_BorrowsUserCount;
        // 後台借貨人數
        public string BorrowsDate;
        // 借貨期限
        public string MemoNote;
    } // end TPOSAuthoriseData

    public struct TIP_ADDR_STRING
    {
        public TIP_ADDR_STRING Next;
        public char[] IpAddress;
        public char[] IpMask;
        public int Context;
    } // end TIP_ADDR_STRING

    public struct TIP_ADAPTER_INFO
    {
        public TIP_ADAPTER_INFO Next;
        public int ComboIndex;
        // 網卡順序
        public char[] AdapterName;
        // 網卡名稱
        public char[] Description;
        // 詳細說明
        public uint AddressLength;
        public byte[] Address;
        // 網卡號
        public int Index;
        public uint aType;
        public uint DHCPEnabled;
        public TIP_ADDR_STRING CurrentIPAddress;
        public TIP_ADDR_STRING IPAddressList;
        public TIP_ADDR_STRING GatewayList;
        public TIP_ADDR_STRING DHCPServer;
        public bool HaveWINS;
        public TIP_ADDR_STRING PrimaryWINSServer;
        public TIP_ADDR_STRING SecondaryWINSServer;
        public byte[] LeaseObtained;
        public byte[] LeaseExpires;
    } // end TIP_ADAPTER_INFO

}

namespace POSAuthoriseU.Units
{
    public class POSAuthoriseU
    {
        public const string C_MsgCaption = LibDefine.Units.LibDefine.C_ProductName + "授權管理員";
        public const int C_AdapterValue = 1;
        // 就是 SerialType COSMOS POS模組授權方式變動版號, 最多可改 255次
        public const int C_MaxINIIdent = 10;
        public const string C_System = "System";
        public static string[] C_SysIndent = {"POS01", "POS02", "POS03", "POS04", "POS05", "POS06", "POS07", "POS08", "POS09", "POS10"};
        public const string C_ININame = LibDefine.Units.LibDefine.C_ProductName + ".Ini";
        public const int C_Base_InstallType = 7;
        public const string C_COSMOSInstallInfo = "\\SOFTWARE\\DSC\\Conductor\\Install";
        public const string C_POSInstallInfo = LibDefine.Units.LibDefine.C_COSMOSPOS_REG + "\\Install";
        // 不用: O, o, g, q, l(小寫L), ^, \(左上右下斜線), | (Piple Line), `, ", ', 左小括號, 右小括號, 左大括號, 右大括號, _, :(冒號), ;(分號)
        // 0    1    2    3    4    5    6    7    8    9
        // 10  11   12   13   14   15   16   17   18   19
        // 20  21   22   23   24   25   26   27   28   29
        // 30  31   32   33   34   35   36   37   38   39
        // 40  41   42   43   44   45   46   47   48   49
        // 50  51   52   53   54   55   56   57   58   59
        // 60  61   62   63
        public static char[] C_Degree = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'a', 'b', 'c', 'd', 'e', 'f', 'h', 'i', 'j', 'k', 'm', 'n', 'p', 'r', 's', 't', 'u', '!', '@', '#', '$', '%', '&', '*', '-', '+', '=', '/', '~', '[', ']', '<', '>', '?'};
        public const string C_PasswordError = "DS_Say_Password!_Error";
        public const string C_NoPassWord = "DS_Say_NoPassWord_ThePassWordIsEmpty";
        public const string C_NeverHasPassWord = "DS_Say_Never_Has_Password";
        public const int MAX_ADAPTER_NAME_LENGTH = 256;
        public const int MAX_ADAPTER_DESCRIPTION_LENGTH = 128;
        public const int MAX_ADAPTER_ADDRESS_LENGTH = 8;
        public static ushort Yen_MessageDlg(string xMsg, string xCaption, MessageBoxIcon xDlgType, TMsgDlgButtons xButtons, ushort FontSize, string FontName)
        {
            ushort result;
            //@ Unsupported function or procedure: 'CreateMessageDialog'
             _wvar1 = CreateMessageDialog(xMsg, xDlgType, xButtons);
            try {
                //@ Unsupported property or method(D): 'Caption'
                _wvar1.Caption = xCaption;
                if (FontName != "")
                {
                    //@ Unsupported property or method(D): 'Font'
                    //@ Unsupported property or method(D): 'Name'
                    _wvar1.Font.Name = FontName;
                }
                if (FontSize > 0)
                {
                    //@ Unsupported property or method(D): 'Font'
                    //@ Unsupported property or method(D): 'Size'
                    _wvar1.Font.Size = FontSize;
                }
                //@ Unsupported property or method(C): 'Restore'
                Application.Restore;
                //@ Unsupported property or method(C): 'BringToFront'
                Application.BringToFront;
                //@ Unsupported property or method(D): 'ShowModal'
                result = _wvar1.ShowModal;
            } finally {
                //@ Unsupported property or method(D): 'Free'
                _wvar1.Free;
            }
            return result;
        }

        public static ushort Yen_MessageDlg(string xMsg, string xCaption, MessageBoxIcon xDlgType, TMsgDlgButtons xButtons)
        {
            return Yen_MessageDlg(xMsg, xCaption, xDlgType, xButtons, 0);
        }

        public static ushort Yen_MessageDlg(string xMsg, string xCaption, MessageBoxIcon xDlgType, TMsgDlgButtons xButtons, ushort FontSize)
        {
            return Yen_MessageDlg(xMsg, xCaption, xDlgType, xButtons, FontSize, "");
        }

        public static void SendClickMessage(Control xControl, int wParam, int lParam)
        {
            //@ Undeclared identifier(3): 'WM_LBUTTONDOWN'
            //@ Unsupported property or method(A): 'Perform'
            xControl.Perform(WM_LBUTTONDOWN, wParam, lParam);
            //@ Undeclared identifier(3): 'WM_LBUTTONUP'
            //@ Unsupported property or method(A): 'Perform'
            xControl.Perform(WM_LBUTTONUP, wParam, lParam);
        }

        // 取硬碟序號
        public static int GetHDSerialNumber(byte xDiskID)
        {
            int result;
            string mDiskRoot;
            int mDiskType;
#if WIN32
            double pdw;
            int mc;
            int fl;
#endif
            result = 0;
            if ((xDiskID == 0))
            {
                mDiskRoot = Path.GetPathRoot(System.Environment.GetCommandLineArgs()[0]) + '\\';
            }
            else
            {
                mDiskRoot = (char)(64 + xDiskID) + ":\\";
            }
            mDiskType = new DriveInfo((mDiskRoot as string)).DriveType;
            switch(mDiskType)
            {
                //@ Undeclared identifier(3): 'DRIVE_REMOVABLE'
                //@ Undeclared identifier(3): 'DRIVE_FIXED'
                case DRIVE_REMOVABLE:
                case DRIVE_FIXED:
#if WIN32
                    pdw = new double();
                    //@ Unsupported function or procedure: 'GetVolumeInformation'
                    GetVolumeInformation((mDiskRoot as string), null, 0, pdw, mc, fl, null, 0);
                    result = pdw;
                    //@ Unsupported function or procedure: 'Dispose'
                    Dispose(pdw);
#else
                    //@ Undeclared identifier(3): 'GetWinFlags'
                    result = GetWinFlags;
                    break;
#endif
                //@ Undeclared identifier(3): 'DRIVE_REMOTE'
                //@ Undeclared identifier(3): 'DRIVE_RAMDISK'
                case DRIVE_REMOTE:
                case DRIVE_RAMDISK:
                    // 單機版的程式放在網路或虛擬磁碟機上執行
#if WIN32
                    pdw = new double();
                    //@ Unsupported function or procedure: 'GetVolumeInformation'
                    GetVolumeInformation(("C:\\" as string), null, 0, pdw, mc, fl, null, 0);
                    result = pdw;
                    //@ Unsupported function or procedure: 'Dispose'
                    Dispose(pdw);
#else
                    //@ Undeclared identifier(3): 'GetWinFlags'
                    result = GetWinFlags;
                    break;
#endif
                // DRIVE_REMOTE: begin
                // showmessage('遠方磁碟機');
                // end;
                // DRIVE_RAMDISK: begin
                // showmessage('虛擬磁碟機');
                // end;
                //@ Undeclared identifier(3): 'DRIVE_CDROM'
                case DRIVE_CDROM:
                    // showmessage('光碟機');
                    Yen_MessageDlg("很抱歉, 不能在光碟機上執行!", "錯誤", System.Windows.Forms.MessageBoxIcon.Error, new System.Windows.Forms.MessageBoxButtons[] {System.Windows.Forms.MessageBoxButtons.OK});
                    break;
            }
            return result;
        }

        [ DllImport("IPHLPAPI.DLL")]
        public static extern int GetAdaptersInfo(TIP_ADAPTER_INFO pAdapterInfo, PULONG pOutBufLen);

        // Value: 作業方式
        // Value= 1 表示取出網卡號
        // Value= 2 表示依指定的 xMacAddress 取出該網卡使用的 IP, 此時, xCardNo最好= -1, 不然有可能會找不到, 因為你指定錯了
        // xCardNo: 抓第幾張網卡
        // xAdapterList: 回傳所有的網卡號清單, nil 表示不回傳
        // xMacAddress: 指定的網卡號
        public void GetAdapterData_Show_AdapterInfoError(int xCode)
        {
            string mErrorStr;
            switch(xCode)
            {
                //@ Undeclared identifier(3): 'ERROR_BUFFER_OVERFLOW'
                case ERROR_BUFFER_OVERFLOW:
                    mErrorStr = "作業內部提供的記憶空間太小";
                    break;
                //@ Undeclared identifier(3): 'ERROR_INVALID_DATA'
                case ERROR_INVALID_DATA:
                    mErrorStr = "錯誤的資訊, 無法接收";
                    break;
                //@ Undeclared identifier(3): 'ERROR_INVALID_PARAMETER'
                case ERROR_INVALID_PARAMETER:
                    mErrorStr = "參數錯誤";
                    break;
                //@ Undeclared identifier(3): 'ERROR_NO_DATA'
                case ERROR_NO_DATA:
                    mErrorStr = "無法取得資訊";
                    break;
                //@ Undeclared identifier(3): 'ERROR_NOT_SUPPORTED'
                case ERROR_NOT_SUPPORTED:
                    mErrorStr = "不支援的OS系統";
                    break;
                default:
                    mErrorStr = "未知的錯誤";
                    break;
            }
            Units.POSAuthoriseU.Yen_MessageDlg(mErrorStr, "系統控制", System.Windows.Forms.MessageBoxIcon.Error, new System.Windows.Forms.MessageBoxButtons[] {System.Windows.Forms.MessageBoxButtons.OK});
        }

        // 取網路卡序號, 網路線拔掉會找不到該網卡
        // function  GetAdapterNumber(var xCardNo: Integer; xAdapterList: TStringList= nil): String;
        // 取網路卡資訊, 1: 序號  其他的還沒寫, 網路線拔掉一樣可找到該網卡
        public static string GetAdapterData(byte Value, ref int xCardNo, bool xCanUseWireless, List<string> xAdapterList, string xMacAddress)
        {
            string result;
            int mSize;
            TIP_ADAPTER_INFO mpInfo;
            object mPData;
            int mErrorCode;
            int mIndex;
            string mAdapterNum;
            bool mDoit;
            int mCardNo;
            LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("取得網卡", "C:\\LOG");
            if (xCanUseWireless)
            {
                LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("允許在VISTA之後的OS下使用無線網卡", "C:\\LOG");
            }
            else
            {
                LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("不允許在VISTA之後的OS下使用無線網卡, 但非VISTA之後的OS不在此限", "C:\\LOG");
            }
            //@ Undeclared identifier(3): 'Win32MajorVersion'
            //@ Undeclared identifier(3): 'Win32MinorVersion'
            //@ Unsupported function or procedure: 'Format'
            LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("OS版本: " + Format("%d.%d", new object[] {Win32MajorVersion, Win32MinorVersion}), "C:\\LOG");
            mIndex = 0;
            result = "";
            //@ Unsupported function or procedure: 'Win32Platform'
            //@ Undeclared identifier(3): 'VER_PLATFORM_WIN32_NT'
            //@ Unsupported function or procedure: 'Win32Platform'
            //@ Undeclared identifier(3): 'VER_PLATFORM_WIN32_WINDOWS'
            mDoit = ((Win32Platform == VER_PLATFORM_WIN32_NT) || (Win32Platform == VER_PLATFORM_WIN32_WINDOWS));
            if (mDoit)
            {
                if ((xAdapterList != null))
                {
                    xAdapterList.Clear();
                }
                mSize = sizeof(mpInfo);
                try {
                    //@ Unsupported function or procedure: 'AllocMem'
                    mpInfo = AllocMem(mSize);
                    // 做一次初始化, 取得需要的 Size
                    mErrorCode = GetAdaptersInfo(mpInfo, mSize);
                    //@ Undeclared identifier(3): 'ERROR_BUFFER_OVERFLOW'
                    if ((mErrorCode == ERROR_BUFFER_OVERFLOW))
                    {
                        //@ Unsupported function or procedure: 'FreeMem'
                        FreeMem(mpInfo);
                        //@ Unsupported function or procedure: 'AllocMem'
                        mpInfo = AllocMem(mSize);
                        mErrorCode = GetAdaptersInfo(mpInfo, mSize);
                    }
                    mPData = mpInfo;
                    //@ Undeclared identifier(3): 'NO_ERROR'
                    if (mErrorCode == NO_ERROR)
                    {
                        mCardNo = xCardNo;
                        // 用來做判斷用的, 拿掉的話, 就不能抓到全部的網卡了
                        while ((mPData != null))
                        {
                            TIP_ADAPTER_INFO _wvar1 = ((mPData) as TIP_ADAPTER_INFO);
                            // 
                            // 
                            // type TIP_ADAPTER_INFO = record
                            // struct _IP_ADAPTER_INFO* Next;
                            // DWORD ComboIndex;
                            // Char AdapterName[MAX_ADAPTER_NAME_LENGTH + 4];
                            // char Description[MAX_ADAPTER_DESCRIPTION_LENGTH + 4];
                            // UINT AddressLength;
                            // BYTE Address[MAX_ADAPTER_ADDRESS_LENGTH];
                            // DWORD Index;
                            // UINT Type;
                            // UINT DhcpEnabled;
                            // PIP_ADDR_STRING CurrentIpAddress;
                            // IP_ADDR_STRING IpAddressList;
                            // IP_ADDR_STRING GatewayList;
                            // IP_ADDR_STRING DhcpServer;
                            // BOOL HaveWins;
                            // IP_ADDR_STRING PrimaryWinsServer;
                            // IP_ADDR_STRING SecondaryWinsServer;
                            // time_t LeaseObtained;
                            // time_t LeaseExpires;
                            // IP_ADAPTER_INFO, *PIP_ADAPTER_INFO;
                            // end;
                            LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("網卡號  : " + mAdapterNum, "C:\\LOG");
                            //@ Unsupported function or procedure: 'Format'
                            LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("網卡順序: " + Format("%d", new int[] {mIndex}), "C:\\LOG");
                            if ((mCardNo ==  -1) || (mCardNo >  -1) && (mIndex == xCardNo))
                            {
                                //@ Unsupported function or procedure: 'format'
                                mAdapterNum = format("%2.2x%2.2x%2.2x%2.2x%2.2x%2.2x", new byte[] {_wvar1.Address[1], _wvar1.Address[2], _wvar1.Address[3], _wvar1.Address[4], _wvar1.Address[5], _wvar1.Address[6]});
                                // 取網卡號
                                if ((Value & 1) == 1)
                                {
                                    // 檢查是否為符合的網卡
                                    // 20111124 Add By Yen For 排除非實體網卡
                                    // if CheckAdapterType(mAdapterNum) then
                                    if (CheckAdapterType(mAdapterNum, _wvar1.aType, xCanUseWireless))
                                    {
                                        LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("這是符合要求的網卡: " + mAdapterNum, "C:\\LOG");
                                        if (result == "")
                                        {
                                            result = mAdapterNum;
                                            xCardNo = mIndex;
                                        }
                                        if ((xAdapterList != null))
                                        {
                                            xAdapterList.Add(mAdapterNum);
                                        }
                                    }
                                // end of CheckAdapterType
                                // end of if (Value and 1)= 1 then
                                }
                                else if ((Value & 2) == 2)
                                {
                                    // 取指定網卡使用的IP
                                    if (xMacAddress == mAdapterNum)
                                    {
                                        result = _wvar1.CurrentIPAddress.IpAddress;
                                    }
                                }
                            }
                            // end of if (xCardNo= -1) or (xCardNo> -1) and (mIndex= xCardNo) then
                            mPData = _wvar1.Next;
                            mIndex ++;
                        }
                    // end of With
                    // end of if
                    }
                    else
                    {
                        GetAdapterData_Show_AdapterInfoError(mErrorCode);
                    }
                } finally {
                    //@ Unsupported function or procedure: 'FreeMem'
                    FreeMem(mpInfo);
                }
            }
            else
            {
                Yen_MessageDlg("OS 不符合! 無法執行本作業", "錯誤", System.Windows.Forms.MessageBoxIcon.Error, new System.Windows.Forms.MessageBoxButtons[] {System.Windows.Forms.MessageBoxButtons.OK});
            }
            return result;
        }

        public static string GetAdapterData(byte Value, ref int xCardNo)
        {
            return GetAdapterData(Value, xCardNo, false);
        }

        public static string GetAdapterData(byte Value, ref int xCardNo, bool xCanUseWireless)
        {
            return GetAdapterData(Value, xCardNo, xCanUseWireless, null);
        }

        public static string GetAdapterData(byte Value, ref int xCardNo, bool xCanUseWireless, List<string> xAdapterList)
        {
            return GetAdapterData(Value, xCardNo, xCanUseWireless, xAdapterList, "");
        }

        // 計算整個字串的ascii總和
        public static ushort GetASCIIAmount(string xString)
        {
            ushort result;
            int i;
            int mLen;
            result = 0;
            mLen = xString.Length;
            // 最多257 個字, 以確保回傳值一定是word, Word最大值 65535 / ASCII最大值 255 = 257個ASCII
            if (mLen > 257)
            {
                mLen = 257;
            }
            for (i = 1; i <= mLen; i ++ )
            {
                result = result + (int)(xString[i]);
            }
            return result;
        }

        // 將整個字串的ascii弄成一長串字串(每個ASCII固定為 2(16進位) 或 3 (10進位)碼)
        public static string GetASCIIStr(string xString, byte xScale)
        {
            string result;
            int i;
            string mMaskStr;
            result = "";
            switch(xScale)
            {
                case 10:
                    mMaskStr = "%.3d";
                    break;
                case 16:
                    mMaskStr = "%.2x";
                    break;
                default:
                    mMaskStr = "%d";
                    break;
            }
            for (i = 1; i <= xString.Length; i ++ )
            {
                //@ Unsupported function or procedure: 'Format'
                result = result + Format(mMaskStr, new int[] {(int)(xString[i])});
            }
            return result;
        }

        public static string GetASCIIStr(string xString)
        {
            return GetASCIIStr(xString, 10);
        }

        // 將ascii的字串轉成文字串
        public static string ASCIIToStr(string xASCIIStr, byte xScale)
        {
            string result;
            ushort mLen;
            int mTempInt;
            int i;
            byte mBaseLen;
            result = "";
            mBaseLen = 3;
            switch(xScale)
            {
                case 10:
                    mBaseLen = 3;
                    break;
                case 16:
                    mBaseLen = 2;
                    break;
            }
            mLen = xASCIIStr.Length;
            mTempInt = (mLen % mBaseLen);
            // 補足長度
            if (mTempInt > 0)
            {
                for (i = 1; i <= (mBaseLen - mTempInt); i ++ )
                {
                    xASCIIStr = '0' + xASCIIStr;
                }
            }
            mLen = (xASCIIStr.Length / mBaseLen);
            for (i = 0; i < mLen; i ++ )
            {
                mTempInt = CopyStrToWord(xASCIIStr, i * mBaseLen + 1, mBaseLen, xScale);
                if (mTempInt > 0)
                {
                    result = result + (char)(mTempInt);
                }
            }
            return result;
        }

        public static string ASCIIToStr(string xASCIIStr)
        {
            return ASCIIToStr(xASCIIStr, 10);
        }

        // 將字串的某部份轉成數字
        public static ushort CopyStrToWord(string xSource, ushort xIndex, ushort xCount, byte xScale)
        {
            ushort result;
            string mStr;
            int i;
            int mLen;
            mStr = xSource.Substring(xIndex - 1 ,xCount);
            mLen = mStr.Length;
            // if mLen= 0 then
            // Result:= 0
            // else
            // begin
            if (xScale == 16)
            {
                mStr = '$' + mStr;
            }
            result = Convert.ToInt32(mStr);
            // end;

            return result;
        }

        public static ushort CopyStrToWord(string xSource, ushort xIndex, ushort xCount)
        {
            return CopyStrToWord(xSource, xIndex, xCount, 10);
        }

        public static int CopyStrToDWord(string xSource, ushort xIndex, ushort xCount, byte xScale)
        {
            int result;
            string mStr;
            int i;
            int mLen;
            mStr = xSource.Substring(xIndex - 1 ,xCount);
            mLen = mStr.Length;
            // if mLen= 0 then
            // Result:= 0
            // else
            // begin
            if (xScale == 16)
            {
                mStr = '$' + mStr;
            }
            result = Convert.ToInt64(mStr);
            // end;

            return result;
        }

        public static int CopyStrToDWord(string xSource, ushort xIndex, ushort xCount)
        {
            return CopyStrToDWord(xSource, xIndex, xCount, 10);
        }

        // Bit 向左旋轉
        public static ushort RolByte(byte xSource, byte xbit)
        {
            ushort result = 0;
            // begin
            // asm
            // mov CL, xBit
            // rol xSource, CL
            // end;
            // Result:= xSource;
            // end;
            // asm
            // push SI
            // mov CL, DL
            // mov AH, 0
            // rol AL, CL
            // pop SI
            // end

            return result;
        }

        // Bit 向右旋轉
        public static ushort RorByte(byte xSource, byte xbit)
        {
            ushort result = 0;
            // asm
            // push SI
            // mov CL, DL
            // mov AH, 0
            // ror AL, CL
            // pop SI
            // end

            return result;
        }

        public static ushort RolWord(ushort xSource, byte xbit)
        {
            ushort result = 0;
            // asm
            // push SI
            // mov CL, DL
            // rol AX, CL
            // pop SI
            // end

            return result;
        }

        public static ushort RorWord(ushort xSource, byte xbit)
        {
            ushort result = 0;
            // asm
            // push SI
            // mov CL, DL
            // ror AX, CL
            // pop SI
            // end

            return result;
        }

        public static int RolInt(int xSource, byte xbit)
        {
            int result = 0;
            // asm
            // push SI
            // mov CL, DL
            // rol AX, CL
            // pop SI
            // end

            return result;
        }

        public static int RorInt(int xSource, byte xbit)
        {
            int result = 0;
            return result;
        }

        public static int RolDWord(int xSource, byte xbit)
        {
            int result = 0;
            return result;
        }

        public static int RorDWord(int xSource, byte xbit)
        {
            int result = 0;
            return result;
        }

        public static long RolInt64(long xSource, byte xbit)
        {
            long result = 0;
            return result;
        }

        public static long RorInt64(long xSource, byte xbit)
        {
            long result = 0;
            return result;
        }

        // 十進位轉二進位
        public static string DecimalToBinary(int xValue)
        {
            string result = String.Empty;
            return result;
        }

        // 二進位轉十進位
        public static int BinaryToDecimal(string xValue)
        {
            int result = 0;
            return result;
        }

        public static void DivMod(int xInt, ushort xDivisor, ref ushort xDivResult, ref ushort xModResult)
        {
        }

        public static string IntToBin(ushort xValue)
        {
            string result = String.Empty;
            return result;
        }

        public static string BinToScale(string xBinStr, byte xScale)
        {
            string result = String.Empty;
            return result;
        }

        public int ScaleToBin_GetSignValue(char xValueChar)
        {
            int result;
            return result;
        }

        public static string ScaleToBin(string xValue, byte xScale)
        {
            string result = String.Empty;
            return result;
        }

        public string MakeScale_GetFrontZero(ref string xValue)
        {
            string result;
            return result;
        }

        public static string MakeScale(string xValue, bool xEnScale)
        {
            string result = String.Empty;
            return result;
        }

        // 20111124 Add By Yen For 排除非實體網卡
        // function CheckAdapterType(xValue: String): Boolean;
        public static bool CheckAdapterType(string xValue, ushort xType, bool xCanUseWireless)
        {
            bool result = false;
            return result;
        }

        public static string EncodeSQLSa(string xSa)
        {
            string result = String.Empty;
            return result;
        }

        public static string DecodeSQLSa(string xSa)
        {
            string result = String.Empty;
            return result;
        }

        public static object Encode_AuthoriseData(TPOSAuthoriseData xAuthoriseData, object xSerialData, byte xRunType)
        {
            object result = null;
            return result;
        }

        public static object GetSerialCode(TPOSAuthoriseData xAuthoriseData, object xValue)
        {
            object result = null;
            return result;
        }

        public static object ConnectAuthoriseServer(TSocketConnection xSocketConn, object xValue)
        {
            object result = null;
            return result;
        }

        public static void DisConnectAuthoriseServer(TSocketConnection xSocketConn, object xAppServer)
        {
        }

        public static bool CheckAuthorise(object xValue, bool xShowmessage)
        {
            bool result = false;
            return result;
        }

        public static bool DelAuthorise(TSocketConnection xSocketConn, object xDest, object xValue, bool xShowmessage)
        {
            bool result = false;
            return result;
        }

        public static bool MakeAuthorise(TSocketConnection xSocketConn, object xDest, object xValue, bool xShowmessage)
        {
            bool result = false;
            return result;
        }

        public static int QueryAuthorise(TSocketConnection xSocketConn, object xDest, object xValue, bool xShowmessage)
        {
            int result = 0;
            return result;
        }

    } // end POSAuthoriseU

}

// asm
// push SI
// mov CL, DL
// ror AX, CL
// pop SI
// end
