using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using LibDefine;
using ConvVariant;
using LibPOSFunc;
 // 20111124 Add By Yen For �ư��D������d(�t���u���d�P�L�u���d), Windows Vista����~����ѵL�u���d"71(IF_TYPE_IEEE80211)", Win2003�MWinXP�|��L�u���d�@�˿��Ѭ� ������d"6(MIB_IF_TYPE_ETHERNET)"
namespace POSAuthoriseU
{
    public struct TPOSAuthoriseData
    {
        public string CustomID;
        // �Ȥ�N��
        public string CustomerName;
        // �Ȥ�W��
        public byte SerialType;
        // ���v�覡����
        public ushort InstallType;
        // ���v����(0: DEMO, 1: ����, 2: ���s����, 3: ���s�ӤH)
        public ushort PosClient_UserCount;
        // POS�e�x�ʶR�H��
        public ushort PosERP_UserCount;
        // ��x�ʶR�H��
        public string AuthoriseDate;
        // ���ͤ��(���t������j�Ÿ�)
        public int Borrows;
        // �ɳf  0: ����  1: �ɳf
        public ushort PosClient_BorrowsUserCount;
        // POS�e�x�ɳf�H��
        public ushort PosERP_BorrowsUserCount;
        // ��x�ɳf�H��
        public string BorrowsDate;
        // �ɳf����
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
        // ���d����
        public char[] AdapterName;
        // ���d�W��
        public char[] Description;
        // �Բӻ���
        public uint AddressLength;
        public byte[] Address;
        // ���d��
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
        public const string C_MsgCaption = LibDefine.Units.LibDefine.C_ProductName + "���v�޲z��";
        public const int C_AdapterValue = 1;
        // �N�O SerialType COSMOS POS�Ҳձ��v�覡�ܰʪ���, �̦h�i�� 255��
        public const int C_MaxINIIdent = 10;
        public const string C_System = "System";
        public static string[] C_SysIndent = {"POS01", "POS02", "POS03", "POS04", "POS05", "POS06", "POS07", "POS08", "POS09", "POS10"};
        public const string C_ININame = LibDefine.Units.LibDefine.C_ProductName + ".Ini";
        public const int C_Base_InstallType = 7;
        public const string C_COSMOSInstallInfo = "\\SOFTWARE\\DSC\\Conductor\\Install";
        public const string C_POSInstallInfo = LibDefine.Units.LibDefine.C_COSMOSPOS_REG + "\\Install";
        // ����: O, o, g, q, l(�p�gL), ^, \(���W�k�U�׽u), | (Piple Line), `, ", ', ���p�A��, �k�p�A��, ���j�A��, �k�j�A��, _, :(�_��), ;(����)
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

        // ���w�ЧǸ�
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
                    // ��������{����b�����ε����Ϻо��W����
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
                // showmessage('����Ϻо�');
                // end;
                // DRIVE_RAMDISK: begin
                // showmessage('�����Ϻо�');
                // end;
                //@ Undeclared identifier(3): 'DRIVE_CDROM'
                case DRIVE_CDROM:
                    // showmessage('���о�');
                    Yen_MessageDlg("�ܩ�p, ����b���о��W����!", "���~", System.Windows.Forms.MessageBoxIcon.Error, new System.Windows.Forms.MessageBoxButtons[] {System.Windows.Forms.MessageBoxButtons.OK});
                    break;
            }
            return result;
        }

        [ DllImport("IPHLPAPI.DLL")]
        public static extern int GetAdaptersInfo(TIP_ADAPTER_INFO pAdapterInfo, PULONG pOutBufLen);

        // Value: �@�~�覡
        // Value= 1 ��ܨ��X���d��
        // Value= 2 ��ܨ̫��w�� xMacAddress ���X�Ӻ��d�ϥΪ� IP, ����, xCardNo�̦n= -1, ���M���i��|�䤣��, �]���A���w���F
        // xCardNo: ��ĴX�i���d
        // xAdapterList: �^�ǩҦ������d���M��, nil ��ܤ��^��
        // xMacAddress: ���w�����d��
        public void GetAdapterData_Show_AdapterInfoError(int xCode)
        {
            string mErrorStr;
            switch(xCode)
            {
                //@ Undeclared identifier(3): 'ERROR_BUFFER_OVERFLOW'
                case ERROR_BUFFER_OVERFLOW:
                    mErrorStr = "�@�~�������Ѫ��O�ЪŶ��Ӥp";
                    break;
                //@ Undeclared identifier(3): 'ERROR_INVALID_DATA'
                case ERROR_INVALID_DATA:
                    mErrorStr = "���~����T, �L�k����";
                    break;
                //@ Undeclared identifier(3): 'ERROR_INVALID_PARAMETER'
                case ERROR_INVALID_PARAMETER:
                    mErrorStr = "�Ѽƿ��~";
                    break;
                //@ Undeclared identifier(3): 'ERROR_NO_DATA'
                case ERROR_NO_DATA:
                    mErrorStr = "�L�k���o��T";
                    break;
                //@ Undeclared identifier(3): 'ERROR_NOT_SUPPORTED'
                case ERROR_NOT_SUPPORTED:
                    mErrorStr = "���䴩��OS�t��";
                    break;
                default:
                    mErrorStr = "���������~";
                    break;
            }
            Units.POSAuthoriseU.Yen_MessageDlg(mErrorStr, "�t�α���", System.Windows.Forms.MessageBoxIcon.Error, new System.Windows.Forms.MessageBoxButtons[] {System.Windows.Forms.MessageBoxButtons.OK});
        }

        // �������d�Ǹ�, �����u�ޱ��|�䤣��Ӻ��d
        // function  GetAdapterNumber(var xCardNo: Integer; xAdapterList: TStringList= nil): String;
        // �������d��T, 1: �Ǹ�  ��L���٨S�g, �����u�ޱ��@�˥i���Ӻ��d
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
            LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("���o���d", "C:\\LOG");
            if (xCanUseWireless)
            {
                LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("���\�bVISTA���᪺OS�U�ϥεL�u���d", "C:\\LOG");
            }
            else
            {
                LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("�����\�bVISTA���᪺OS�U�ϥεL�u���d, ���DVISTA���᪺OS���b����", "C:\\LOG");
            }
            //@ Undeclared identifier(3): 'Win32MajorVersion'
            //@ Undeclared identifier(3): 'Win32MinorVersion'
            //@ Unsupported function or procedure: 'Format'
            LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("OS����: " + Format("%d.%d", new object[] {Win32MajorVersion, Win32MinorVersion}), "C:\\LOG");
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
                    // ���@����l��, ���o�ݭn�� Size
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
                        // �ΨӰ��P�_�Ϊ�, ��������, �N��������������d�F
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
                            LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("���d��  : " + mAdapterNum, "C:\\LOG");
                            //@ Unsupported function or procedure: 'Format'
                            LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("���d����: " + Format("%d", new int[] {mIndex}), "C:\\LOG");
                            if ((mCardNo ==  -1) || (mCardNo >  -1) && (mIndex == xCardNo))
                            {
                                //@ Unsupported function or procedure: 'format'
                                mAdapterNum = format("%2.2x%2.2x%2.2x%2.2x%2.2x%2.2x", new byte[] {_wvar1.Address[1], _wvar1.Address[2], _wvar1.Address[3], _wvar1.Address[4], _wvar1.Address[5], _wvar1.Address[6]});
                                // �����d��
                                if ((Value & 1) == 1)
                                {
                                    // �ˬd�O�_���ŦX�����d
                                    // 20111124 Add By Yen For �ư��D������d
                                    // if CheckAdapterType(mAdapterNum) then
                                    if (CheckAdapterType(mAdapterNum, _wvar1.aType, xCanUseWireless))
                                    {
                                        LibPOSFunc.Units.LibPOSFunc.WriteToAlertFile("�o�O�ŦX�n�D�����d: " + mAdapterNum, "C:\\LOG");
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
                                    // �����w���d�ϥΪ�IP
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
                Yen_MessageDlg("OS ���ŦX! �L�k���楻�@�~", "���~", System.Windows.Forms.MessageBoxIcon.Error, new System.Windows.Forms.MessageBoxButtons[] {System.Windows.Forms.MessageBoxButtons.OK});
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

        // �p���Ӧr�ꪺascii�`�M
        public static ushort GetASCIIAmount(string xString)
        {
            ushort result;
            int i;
            int mLen;
            result = 0;
            mLen = xString.Length;
            // �̦h257 �Ӧr, �H�T�O�^�ǭȤ@�w�Oword, Word�̤j�� 65535 / ASCII�̤j�� 255 = 257��ASCII
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

        // �N��Ӧr�ꪺascii�˦��@����r��(�C��ASCII�T�w�� 2(16�i��) �� 3 (10�i��)�X)
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

        // �Nascii���r���ন��r��
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
            // �ɨ�����
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

        // �N�r�ꪺ�Y�����ন�Ʀr
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

        // Bit �V������
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

        // Bit �V�k����
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

        // �Q�i����G�i��
        public static string DecimalToBinary(int xValue)
        {
            string result = String.Empty;
            return result;
        }

        // �G�i����Q�i��
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

        // 20111124 Add By Yen For �ư��D������d
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
