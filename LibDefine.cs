using System;
namespace LibDefine
{
    // NET02�ϥ�
    public struct PARAMINFO
    {
        public string BAS_CompNo;
        public string BAS_StoreNo;
        public string BAS_POSNo;
    } // end PARAMINFO

    public struct DBINFO
    {
        public TDBType DBType;
        // ��Ʈw���O
        // ��ƮwIP
        // ��Ʈw�W��
        // �n�J�b��
        // �n�J�K�X
        public string ServerIP;
        public string DBName;
        public string UserID;
        public string UserPass;
        public string DSCSYS;
    } // end DBINFO

    public struct USERINFO
    {
        // �b��
        // �m�W
        // �n�J�K�X
        // �Z�O
        public string ID;
        public string Name;
        public string Password;
        public string Note;
        public string LastLogin;
        // �e���n�J���
        public int Level;
        // ����
        public TLoginMode LoginMode;
        // �n�J�Ҧ�
        public string USR_GROUP;
        // �s�եN�X
        // �W�ŨϥΪ�       //MU015
        // �Ӹ��v��         //MU016
        // ����             //MU017
        public string Supervisor;
        public string PIP;
        public string NonUse;
        public string LimitStore;
    } // end USERINFO

    public struct VIPINFO
    {
        // �|���s��
        // �|������
        // �|���d��
        // IC�����d�d��
        // �m�W
        // �q��
        // ��ʹq��
        // �����Ҹ�
        // �ͤ�
        // �a�}
        // ���O
        public string No;
        public string Level;
        public string CardNo;
        public string ICCardNo;
        public string Name;
        public string TelNo;
        public string MobileNo;
        public string IDNo;
        public string BirthDay;
        public string Address;
        public string VipType;
        public string Note;
        // ���O
        // �馩�v
        // �֭p�P�O���B
        // �i�οn�I��
        public double DiscRate;
        public double TotAmt;
        public double TotBonusLeft;
        public double TotBonusUsed;
        // �w�I���I��
        public string Gender;
        // �ʧO
        public double MultiplePnt;
        // �~�ץͤ�§���İ϶�-�I�ƥ[��
        public string BirthPmtActNo;
        // �ͤ鬡�ʥN��
        public double NewBonusPoint;
        // ��������s�W�I��
        public double Freegive;
        // �ذe�I��
        // 20130620 add by 01622 for SC01-20130430001 GP20���|����X ��
        // �֭p�������O���B
        public double TotNetAmt;
        public double TotNetPOINT;
        // �`�i�κ����I��
        public string netVIPno;
        // �����|���s��,             ���P�����|���H�y;�z���j
        public string TotBonusLeftNet;
        // �i�κ����I��,             ���P�����|���H�y;�z���j
        public string ChangePointNet;
        // ��������I�������I��,     ���P�����|���H�y;�z���j
        public string PntMoneyDisc_NET;
    } // end VIPINFO

    public enum TClientType
    {
        ct_POSClient,
        ct_BackupClient,
        ct_BorrowsClient,
        ct_DataSync,
        ct_UnKnow
    } // end TClientType

    // ���x          �ƥ�             �ɥ�              �ǿ�D��     ���]�w
    public enum TProgramType
    {
        pt_POS,
        pt_Set,
        pt_Sync,
        pt_UnKnow
    } // end TProgramType

    // ���x�{��  �]�w�{�� �ǿ�{��    ���]�w
    public enum TBeClose
    {
        bc_None,
        bc_Will,
        bc_Close
    } // end TBeClose

    // ���`���A, �Y�N����, �i�H����
    public enum TDBAdaptertype
    {
        dbaLOCAL,
        dbaERP,
        dbaUPLOAD
    } // end TDBAdaptertype

    // �����AERP�A�ǿ�D��
    public enum TVirtualKey
    {
        vkENTER,
        vkTAB,
        vkSHIFTTAB,
        vkBACKSPACE,
        vkCLEAR,
        vkSPACE,
        vkESC,
        vkPRIOR,
        vkNEXT,
        vkLEFT,
        vkRIGHT,
        vkUP,
        vkDOWN,
        vkRESIZE,
        vkCANCEL,
        vkACCEPT
    } // end TVirtualKey

    // POS�e�x�������䲣��
    public enum TPrintInvType
    {
        pitPOSDouble,
        pitPOSTriple,
        pitPOSEInv,
        pitManualDouble,
        pitManualTriple,
        pitNoPrint
    } // end TPrintInvType

    // 5.���L�o��
    // �H�Υd�O
    public enum TCardType
    {
        crdMASTER,
        crdVISA,
        crdJCB,
        crdAE,
        crdDINERS,
        crdOTHER,
        crdNonCard
    } // end TCardType

    // �D�H�Υd��
    public enum TLocaleID
    {
        LCID_DEFAULT,
        LCID_TAIWAN,
        LCID_RPC,
        LCID_ENGLISH
    } // end TLocaleID

    public enum TSndType
    {
        stNo,
        stMessage,
        stError,
        stSuccess
    } // end TSndType

    public enum TDBType
    {
        dbSQL,
        dbOracle,
        dbPostgre,
        dbMySQL
    } // end TDBType

    public enum TLoginMode
    {
        lmNormal,
        lmSupervisor,
        lmPratice
    } // end TLoginMode

    // DataPool���I���A
    public enum TDataPoolNode
    {
        dpnRef,
        dpnPub,
        dpnTX,
        dpnTMP,
        dpnRec
    } // end TDataPoolNode

}

namespace LibDefine.Units
{
    public class LibDefine
    {
    // �I�ƭӮ�****************************
    // 512�� �t�ά��� EX:�n�J�n�X�A��L��J��
    //@ Undeclared identifier(3): 'WM_User'
    // �����t��
    // 512�� �\����� EX: �P�PĲ�o�A�|����J�A�~����J��
    //@ Undeclared identifier(3): 'WM_User'
    // �ͤ�Ϯ� �}��
    // 1024�� �Ӯ׬���
    //@ Undeclared identifier(3): 'WM_User'
        // �t�ΰѼ�, �����ܼ�, �����, �Ӯ�
        public const string C_ProductName = "COSMOS_POS";
        // ���~�W��
        public const string C_ProgramName = "POS";
        // �{���W��
        public const string C_COSMOSPOS_REG = "\\SOFTWARE\\DSC\\" + C_ProductName;
        // COSMOS POS��T�bRegistry�����|
        public const string C_COSMOSPOS_INSTALLPATH = "ProgPath";
        // �{���w�˸��|
        public const string C_COSMOSPOS_MODULE_ID = "MODULE_ID";
        // �����O�N��
        public const string C_COSMOSPOS_MODULE_NAME = "MODULE_NAME";
        // �����O�W��
        public const string C_COSMOSPOS_VERSION_NO = "VERSION_NO";
        // ����
        public const string C_COSMOSPOS_VERSION_DATE = "VERSION_DATE";
        // �������
        public const string C_COSMOSPOS_USETERMINAL = "USETERMINAL";
        // �O�_�]�w���ϥ�Terminal
        public const string C_DefInstallPath = "C:\\" + C_ProductName;
        // �w�]�w�˸��|
        public const string C_DefPOSDBName = C_ProductName;
        // �w�]����Ʈw�W��
        public const string C_DataPath = "DATA";
        public const string C_LOGPath = "LOG";
        public const string C_PKGPath = "PKG";
        public const string c_MODIPath = "MODI";
        public const string C_PICPath = "PIC";
        // 20090618 add by 2919 for �Ӯ׽s��1014C4_001 �W�[�Ϥ����ɦW�W��
        public const string C_Token = "^`";
        public const string C_DataSyncName = "DSC_DataSync";
        // �ǿ�{����APPName
        public const string C_POSAppName = "DSC_POSMainAppName";
        // POS�D�{����APPName;
        public const string C_POSTransFrmName = "DSC_POSTransFrmName";
        // POS�ߧY�ǿ骺FormName;
        public const string C_WM_DataSync = "WM_DATASYNC";
        // �ǿ�{��Message_id
        public const string C_WM_PosKey = "WM_POSKEYBOARD";
        // POS��L��Message_id
        public const string C_POSBackupTxFileName = "BackupTx.dat";
        // POS�Ȧs��������
        public const double C_SECOND = 0.00001157407;
        public const double C_MINUTE = 0.00069444443;
        public const double C_HOUR = 0.04166666666;
        public const int C_RetryTimes = 3;
        public const int C_DataSyncInt = 30;
        public const string C_IniFileName = "DSCPOSSetup.ini";
        public const string C_SampleStr = "Sample";
        public const string C_PrivateKey = "12375447";
        // ��Key�ť��N���
        public const int C_DeptDefColor = 0x00A7D7FF;
        public const int C_PluDefColor = 0x00AFDFC7;
        public const int C_FuncDefColor = 0x00F7E7E7;
        public const System.Drawing.Color C_HighLightColor = System.Drawing.Color.Yellow;
        public const int C_HighLightRange = 150;
        public const System.Drawing.Color C_FontDefColor = System.Drawing.Color.Black;
        public const int C_FontDefSize = 10;
        public const string C_FontDefName = "�ө���";
        public const int C_MsgClrError = 0x0099A3E6;
        public const int C_MsgClrNormal = 0x00A3CDB4;
        public const int C_MsgClrHint = 0x0093BEE1;
        public const string C_DIGIT = "�s���L�Ѹv��m�èh";
        // �c�餤��CHT $404
        // ²�餤��CHS $804
        public static int[] C_LocaleID = {0, 1028, 2052, 1033};
        // �^�y(����)ENU $409
        public static string[] C_InvTypeID = {'3', '4', '5', '1', '2', ""};
        // 1.�G�p 2.�T�p 3.�G�p�����Ⱦ��o�� 4.�T�p�����Ⱦ��o�� 5.�q�l�p����o��
        // Server IP /Host Name;
        // DB Name
        // User ID
        // User Pas
        // Size=4096;
        // Local Host Name
        // Application Name;
        // DB Connect Timeout
        public const string C_DEFAULT_ADO_CONNECT_SQL_STR = "Provider=SQLOLEDB.1;" + "Persist Security Info=False;" + "Use Procedure for Prepare=1;" + "Use Encryption for Data=False;" + "Data Source=%s;" + "Initial Catalog=%s;" + "User ID=%s;" + "Password=%s;" + "Packet Size=%d;" + "Workstation ID=%s;" + "Application Name=%s;" + "Connect Timeout=%s; " + "Tag with column collation when possible=False";
        public static string[] C_CardType = {"MASTER Card", "VISA Card", "JCB Card", "AE Card", "DINERS Card", "OTHER Card", "Non Credit Card"};
        public static string[] C_PicExtension = {".jpg", ".jpeg", ".ico", ".emf", ".wmf"};
        // 20090618 add by 2919 for �Ӯ׽s��1014C4_001 �W�[�Ϥ����ɦW�W��
        public static string[] C_CurrModeType = {'0', '1', '2', '3', '4', '5', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '6', '0', '0', '0', '0'};
        public static string[] C_VipBonusStr = {'0', '1', '2', '3', '4', '5', '6', '7', '8'};
        // 1.�P��     //2.�h�f     //3.�ث~     //4.�q��  //5.�R�q�� //6.���w�� //7.�ߧY��
        // 8.§��P�� //9.������� //A.�q���P�h //B.�R�q���P�h       //C.�x�ȥd�[��   //D.�q������
        public static string[] C_ItemTypeStr = {'1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D'};
        // �~���������
        public static string[] C_INVMBPriceNo = {"MB051", "MB047", "MB053", "MB054", "MB055", "MB056", "MB069", "MB070"};
        // �ӫ~�������ɡA�վ�᪺���
        public static string[] C_WSCMJPriceNo = {"MJ006", "MJ004", "MJ008", "MJ010", "MJ012", "MJ014", "MJ016", "MJ018"};
        // �~������
        public static string[] C_INVMBItemType = {"MB005", "MB006", "MB007", "MB008", "MB111", "MB112", "MB113", "MB114", "MB115", "MB116", "MB117", "MB118"};
        // �~������ - �A����
        public static string[] C_CMSNMItemType = {"NM007", "NM008", "NM009", "NM010", "NM011", "NM012", "NM013", "NM014", "NM015", "NM016", "NM017", "NM018"};
        public const string C_SearchINVMB_Name = "Proc_SearchINVMB";
        public const string C_QueryINVMB_Name = "Proc_QueryINVMB";
        public const string C_DSCPOSQ06_Name = "Proc_DSCPOSQ06";
        public const string C_DSCPOSQ28_Name = "Proc_DSCPOSQ28";
        // C_DataPoolNodeName : array[TDataPoolNode] of string = ('Ref', 'Pub', 'TX', 'Tmp');
        public static string[] C_DataPoolNodeName = {'1', '2', '3', '4', '5'};
        public const char NextArray = '��';
        public const char LineFeed = '��';
        public const string GetLine = "\r\n";
        public const int cmdBase = 0;
        // base
        public const int cmdWAITING = cmdBase + 0;
        // 0 ���`���
        public const int cmdTRANSACTION = cmdBase + 1;
        // 1 �����
        public const int cmdCANCEL_TRANSCATIION = cmdBase + 2;
        // 2 �������
        public const int cmdNULLIFY_INVOICE = cmdBase + 3;
        // 3 ��i�P�h
        public const int cmdORDER = cmdBase + 4;
        // 4 �w��
        public const int cmdCOUPON = cmdBase + 5;
        // 5 §��P��
        public const int cmdSUBTOTAL = cmdBase + 6;
        // 6 �p�p
        public const int cmdPAYMENT = cmdBase + 7;
        // 7 �I��
        public const int cmdTEMPORARY_SAVE = cmdBase + 8;
        // 8 �s���
        public const int cmdTEMPORARY_LEAVE = cmdBase + 9;
        // 9 �Ȯ�����
        public const int cmdQUERY = cmdBase + 10;
        // 10 ����d��
        public const int cmdREPRINT_INVOICE = cmdBase + 11;
        // 11 �o�����L
        public const int cmdINVOICE_CHANGED = cmdBase + 12;
        // 12 �ܧ�o��
        public const int cmdDEL_ITEM_ALLOCALED = cmdBase + 13;
        // 13 ���w�R���ӫ~
        public const int cmdDEL_ITEM_INSTANTLY = cmdBase + 14;
        // 14 �ߧY�R���ӫ~
        public const int cmdREFOUND = cmdBase + 15;
        // 15 �ӫ~�h�f
        public const int cmdPRICE_LOOK_UP = cmdBase + 16;
        // 16 �ӫ~����d��
        public const int cmdPRICE_CHANGED = cmdBase + 17;
        // 17 �����
        public const int cmdFREE_PLU = cmdBase + 18;
        // 18 �ث~
        public const int cmdARDeduct = cmdBase + 19;
        // 19 �R�P�����b��
        public const int cmdICPREPAID = cmdBase + 20;
        // 20 �x�ȥd�[��
        public const int cmdAddAwhileAmt = cmdBase + 21;
        // 21 �s�W�Ȧ���
        public const int cmdAbateAwhileAmt = cmdBase + 22;
        // 22 �P�h�Ȧ���
        public const int cmdDeductAwhileAmt = cmdBase + 23;
        // 23 ���ڨR�P
        public const int cmdCheckout = cmdBase + 24;
        // 24 ���b
        public const int cmdMBase = cmdBase + 1024;
        // �Ӯ׭ק�Cmd�N���ҩl*****************
        // ����������O
        public const int itmBase = 0;
        // base
        public const int itmNORMAL = itmBase + 1;
        // 1.�P�� default;
        public const int itmREFOUND = itmBase + 2;
        // 2.�h�f
        public const int itmFREE = itmBase + 3;
        // 3.�ث~
        public const int itmOrder = itmBase + 4;
        // 4.�q��
        public const int itmOrderDeduct = itmBase + 5;
        // 5.�R�q��
        public const int itmDEL_ITEM_ALLOCALED = itmBase + 6;
        // 6.���w��
        public const int itmDEL_ITEM_INSTANTLY = itmBase + 7;
        // 7.�ߧY��
        public const int itmCouponTrans = itmBase + 8;
        // 8.§��P��
        public const int itmCANCELED = itmBase + 9;
        // 9.�������
        public const int itmOrderBack = itmBase + 10;
        // A.�q���P�h
        public const int itmOrderDeductBack = itmBase + 11;
        // B.�R�q���P�h
        public const int itmICPrePaid = itmBase + 12;
        // C.�x�ȥd�[��
        public const int itmOrderDepositDisc = itmBase + 13;
        // D.�q������
        public const int itmMBase = itmBase + 1048;
        // �Ӯ�itm******************************
        // �|���I�Ʋ������O
        public const int vbtBase = 0;
        // base
        public const int vbtNONE = vbtBase + 0;
        // 0.�L
        public const int vbtNORMAL = vbtBase + 1;
        // 1.���O�s�W
        public const int vbtREFOUND = vbtBase + 2;
        // 2.�P�h���
        public const int vbtEXCHANGE = vbtBase + 3;
        // 3.�I�ƴ��ʰӫ~
        public const int vbtEXCHANGE_FREE = vbtBase + 4;
        // 4.�I�ƧI���ث~
        public const int vbtEXCHANGE_REFOUND = vbtBase + 5;
        // 5.�I�ƴ��ʰӫ~�P�h
        public const int vbtEXCHANGE_FREE_REFOUND = vbtBase + 6;
        // 6.�I�ƧI���ث~�P�h
        public const int vbtEXCHANGE_MONEY = vbtBase + 7;
        // 7.�I�Ƨ����B
        public const int vbtEXCHANGE_MONEY_REFOUND = vbtBase + 8;
        // 8.�I�Ƨ����B�P�h
        public const int vbtCount = vbtBase + 9;
        public const int vbtMBase = vbtBase + 1024;
        // �I�ƭӮ�****************************
        // 512�� �t�ά��� EX:�n�J�n�X�A��L��J��
        public const object CM_POS_Base = WM_User + 1024;
        // ��
        public const object CM_Login = CM_POS_Base + 1;
        // �n�J
        public const object CM_KeyBoardInput = CM_POS_Base + 2;
        // �����@��J
        public const object CM_KeyBoardMassInput = CM_POS_Base + 3;
        // �����ƿ�J
        public const object CM_CodeInput = CM_POS_Base + 4;
        // ENTER��J
        public const object CM_CodeClear = CM_POS_Base + 5;
        // �M���Ȧs
        public const object CM_DispClear = CM_POS_Base + 6;
        // �M���e��
        public const object CM_DispBack = CM_POS_Base + 7;
        // Backspace
        public const object CM_AddForm = CM_POS_Base + 8;
        // �W�[FORM
        public const object CM_ShowForm = CM_POS_Base + 9;
        // ���FORM
        public const object CM_HideForm = CM_POS_Base + 10;
        // ����FORM
        public const object CM_Wait = CM_POS_Base + 11;
        // wait
        public const object CM_ReWait = CM_POS_Base + 12;
        // �Ѱ�����
        public const object CM_ShowDisp = CM_POS_Base + 13;
        // ����
        public const object CM_CloseAPP = CM_POS_Base + 14;
        // �����t��
        // 512�� �\����� EX: �P�PĲ�o�A�|����J�A�~����J��
        public const object CM_POS_Std = WM_User + 1536;
        // �зǥ\��
        public const object CM_ItemInput = CM_POS_Std + 1;
        // �~����J  WParam: 1 ���\ 0 ���� LParam 0 �浧 1 ��Ū
        // CM_Checkout =               CM_POS_Std+ 2;    //���b
        public const object CM_MberInput = CM_POS_Std + 3;
        // �|����J      WParam: 1 ���|�� 0 �L�|��
        public const object CM_Cancel = CM_POS_Std + 4;
        // �������
        public const object CM_LoginAccess = CM_POS_Std + 5;
        // �n�J���\
        public const object CM_UnifiedNumber = CM_POS_Std + 6;
        // �Τ@�s����J  WParam: 1 ���\ 0 ����
        public const object CM_POSVI02_ChangePage = CM_POS_Std + 7;
        // POSVI02 ����
        public const object CM_POSVI03_ChangePage = CM_POS_Std + 8;
        // POSVI03 ����
        public const object CM_POSVI04_ChangePage = CM_POS_Std + 9;
        // POSVI04 ����
        public const object CM_POSVI05_ChangePage = CM_POS_Std + 10;
        // POSVI05 ����
        public const object CM_POSVI06_ChangePage = CM_POS_Std + 11;
        // POSVI06 ����
        public const object CM_POSVI07_ChangePage = CM_POS_Std + 12;
        // POSVI07 ����  WParam: 1 �W��  LParam: 1 �U��
        public const object CM_Change_Dpr = CM_POS_Std + 13;
        // �����ܰʮ�
        public const object CM_FunTrigger = CM_POS_Std + 14;
        // �\����Ĳ�o
        public const object CM_PayTrigger = CM_POS_Std + 15;
        // �I����Ĳ�o    WParam: �\��N�X �� ���B LParam 0.§���I�� 1.�{���I��  2.�H�Υd�I�� EX:80 posvi08��§���I�� 81 posvi08 �{���I�� 111 posvi11 ���
        public const object CM_PayItem = CM_POS_Std + 16;
        public const object CM_RefreshAll = CM_POS_Std + 17;
        // ���s��s�Ҧ��e��
        public const object CM_I05_Release = CM_POS_Std + 18;
        // POSCI05
        public const object CM_I05_ChangeMode = CM_POS_Std + 19;
        // POSCI05
        public const object CM_I05_ShowSaleItem = CM_POS_Std + 20;
        // POSCI05
        public const object CM_I05_ResetSaleItem = CM_POS_Std + 21;
        // POSCI05
        public const object CM_I05_ShowInfo = CM_POS_Std + 22;
        // POSCI05
        public const object CM_I05_ShowPayInfo = CM_POS_Std + 23;
        // POSCI05
        public const object CM_PayPanelSwitch = CM_POS_Std + 24;
        // �\���� �I�ڤ覡(VI06) �}��
        public const object CM_PluPanelSwitch = CM_POS_Std + 25;
        // �\���� �ӫ~(VI04)     �}��
        public const object CM_FuncPanelSwitch = CM_POS_Std + 26;
        // �\���� �\����(VI03)   �}�� ���T�Ӷ}���]���b�P�@PANEL�ҥH�n�վ�
        public const object CM_PayMealPayInfo = CM_POS_Std + 27;
        // �\���� �I�ڸ�T���
        public const object CM_DepPanelSwitch = CM_POS_Std + 28;
        // �\���� ������(VI05)   �}��
        public const object CM_sgSaleItemMouseUp = CM_POS_Std + 29;
        // �\���� STRINGGRID MOUSEUP(VI07)
        public const object CM_sgSaleItemSelectCell = CM_POS_Std + 30;
        // �\���� STRINGGRID SelectCell(VI07)
        public const object CM_BirthPicSWitch = CM_POS_Std + 31;
        // �ͤ�Ϯ� �}��
        // 1024�� �Ӯ׬���
        public const object CM_POS_Cust = WM_User + 2048;
    } // end LibDefine

}

// �Ӯץ\��
