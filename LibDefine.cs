using System;
namespace LibDefine
{
    // NET02使用
    public struct PARAMINFO
    {
        public string BAS_CompNo;
        public string BAS_StoreNo;
        public string BAS_POSNo;
    } // end PARAMINFO

    public struct DBINFO
    {
        public TDBType DBType;
        // 資料庫類別
        // 資料庫IP
        // 資料庫名稱
        // 登入帳號
        // 登入密碼
        public string ServerIP;
        public string DBName;
        public string UserID;
        public string UserPass;
        public string DSCSYS;
    } // end DBINFO

    public struct USERINFO
    {
        // 帳號
        // 姓名
        // 登入密碼
        // 班別
        public string ID;
        public string Name;
        public string Password;
        public string Note;
        public string LastLogin;
        // 前次登入日期
        public int Level;
        // 等級
        public TLoginMode LoginMode;
        // 登入模式
        public string USR_GROUP;
        // 群組代碼
        // 超級使用者       //MU015
        // 個資權限         //MU016
        // 停用             //MU017
        public string Supervisor;
        public string PIP;
        public string NonUse;
        public string LimitStore;
    } // end USERINFO

    public struct VIPINFO
    {
        // 會員編號
        // 會員等級
        // 會員卡號
        // IC晶片卡卡號
        // 姓名
        // 電話
        // 行動電話
        // 身份證號
        // 生日
        // 地址
        // 類別
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
        // 註記
        // 折扣率
        // 累計銷費金額
        // 可用積點數
        public double DiscRate;
        public double TotAmt;
        public double TotBonusLeft;
        public double TotBonusUsed;
        // 已兌換點數
        public string Gender;
        // 性別
        public double MultiplePnt;
        // 年度生日禮有效區間-點數加倍
        public string BirthPmtActNo;
        // 生日活動代號
        public double NewBonusPoint;
        // 本次交易新增點數
        public double Freegive;
        // 贈送點數
        // 20130620 add by 01622 for SC01-20130430001 GP20虛實會員整合 ↓
        // 累計網路消費金額
        public double TotNetAmt;
        public double TotNetPOINT;
        // 總可用網路點數
        public string netVIPno;
        // 網路會員編號,             不同網路會員以『;』分隔
        public string TotBonusLeftNet;
        // 可用網路點數,             不同網路會員以『;』分隔
        public string ChangePointNet;
        // 本次交易兌換網路點數,     不同網路會員以『;』分隔
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

    // 機台          備用             借用              傳輸主機     未設定
    public enum TProgramType
    {
        pt_POS,
        pt_Set,
        pt_Sync,
        pt_UnKnow
    } // end TProgramType

    // 機台程式  設定程式 傳輸程式    未設定
    public enum TBeClose
    {
        bc_None,
        bc_Will,
        bc_Close
    } // end TBeClose

    // 平常狀態, 即將關閉, 可以關閉
    public enum TDBAdaptertype
    {
        dbaLOCAL,
        dbaERP,
        dbaUPLOAD
    } // end TDBAdaptertype

    // 本機，ERP，傳輸主機
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

    // POS前台虛擬按鍵產生
    public enum TPrintInvType
    {
        pitPOSDouble,
        pitPOSTriple,
        pitPOSEInv,
        pitManualDouble,
        pitManualTriple,
        pitNoPrint
    } // end TPrintInvType

    // 5.不印發票
    // 信用卡別
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

    // 非信用卡類
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

    // DataPool結點型態
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
    // 點數個案****************************
    // 512個 系統相關 EX:登入登出，鍵盤輸入等
    //@ Undeclared identifier(3): 'WM_User'
    // 結束系統
    // 512個 功能相關 EX: 促銷觸發，會員輸入，品號輸入等
    //@ Undeclared identifier(3): 'WM_User'
    // 生日圖案 開關
    // 1024個 個案相關
    //@ Undeclared identifier(3): 'WM_User'
        // 系統參數, 公用變數, 交易類, 個案
        public const string C_ProductName = "COSMOS_POS";
        // 產品名稱
        public const string C_ProgramName = "POS";
        // 程式名稱
        public const string C_COSMOSPOS_REG = "\\SOFTWARE\\DSC\\" + C_ProductName;
        // COSMOS POS資訊在Registry的路徑
        public const string C_COSMOSPOS_INSTALLPATH = "ProgPath";
        // 程式安裝路徑
        public const string C_COSMOSPOS_MODULE_ID = "MODULE_ID";
        // 版本別代號
        public const string C_COSMOSPOS_MODULE_NAME = "MODULE_NAME";
        // 版本別名稱
        public const string C_COSMOSPOS_VERSION_NO = "VERSION_NO";
        // 版號
        public const string C_COSMOSPOS_VERSION_DATE = "VERSION_DATE";
        // 版本日期
        public const string C_COSMOSPOS_USETERMINAL = "USETERMINAL";
        // 是否設定為使用Terminal
        public const string C_DefInstallPath = "C:\\" + C_ProductName;
        // 預設安裝路徑
        public const string C_DefPOSDBName = C_ProductName;
        // 預設的資料庫名稱
        public const string C_DataPath = "DATA";
        public const string C_LOGPath = "LOG";
        public const string C_PKGPath = "PKG";
        public const string c_MODIPath = "MODI";
        public const string C_PICPath = "PIC";
        // 20090618 add by 2919 for 個案編號1014C4_001 增加圖片副檔名名稱
        public const string C_Token = "^`";
        public const string C_DataSyncName = "DSC_DataSync";
        // 傳輸程式的APPName
        public const string C_POSAppName = "DSC_POSMainAppName";
        // POS主程式的APPName;
        public const string C_POSTransFrmName = "DSC_POSTransFrmName";
        // POS立即傳輸的FormName;
        public const string C_WM_DataSync = "WM_DATASYNC";
        // 傳輸程式Message_id
        public const string C_WM_PosKey = "WM_POSKEYBOARD";
        // POS鍵盤的Message_id
        public const string C_POSBackupTxFileName = "BackupTx.dat";
        // POS暫存交易資料檔
        public const double C_SECOND = 0.00001157407;
        public const double C_MINUTE = 0.00069444443;
        public const double C_HOUR = 0.04166666666;
        public const int C_RetryTimes = 3;
        public const int C_DataSyncInt = 30;
        public const string C_IniFileName = "DSCPOSSetup.ini";
        public const string C_SampleStr = "Sample";
        public const string C_PrivateKey = "12375447";
        // 此Key勿任意更改
        public const int C_DeptDefColor = 0x00A7D7FF;
        public const int C_PluDefColor = 0x00AFDFC7;
        public const int C_FuncDefColor = 0x00F7E7E7;
        public const System.Drawing.Color C_HighLightColor = System.Drawing.Color.Yellow;
        public const int C_HighLightRange = 150;
        public const System.Drawing.Color C_FontDefColor = System.Drawing.Color.Black;
        public const int C_FontDefSize = 10;
        public const string C_FontDefName = "細明體";
        public const int C_MsgClrError = 0x0099A3E6;
        public const int C_MsgClrNormal = 0x00A3CDB4;
        public const int C_MsgClrHint = 0x0093BEE1;
        public const string C_DIGIT = "零壹貳參肆伍陸柒捌玖";
        // 繁體中文CHT $404
        // 簡體中文CHS $804
        public static int[] C_LocaleID = {0, 1028, 2052, 1033};
        // 英語(美國)ENU $409
        public static string[] C_InvTypeID = {'3', '4', '5', '1', '2', ""};
        // 1.二聯 2.三聯 3.二聯式收銀機發票 4.三聯式收銀機發票 5.電子計算機發票
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
        // 20090618 add by 2919 for 個案編號1014C4_001 增加圖片副檔名名稱
        public static string[] C_CurrModeType = {'0', '1', '2', '3', '4', '5', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '6', '0', '0', '0', '0'};
        public static string[] C_VipBonusStr = {'0', '1', '2', '3', '4', '5', '6', '7', '8'};
        // 1.銷售     //2.退貨     //3.贈品     //4.訂金  //5.沖訂金 //6.指定更正 //7.立即更正
        // 8.禮券銷售 //9.交易取消 //A.訂金銷退 //B.沖訂金銷退       //C.儲值卡加值   //D.訂金折讓
        public static string[] C_ItemTypeStr = {'1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D'};
        // 品號價格欄位
        public static string[] C_INVMBPriceNo = {"MB051", "MB047", "MB053", "MB054", "MB055", "MB056", "MB069", "MB070"};
        // 商品價格資料檔，調整後的單價
        public static string[] C_WSCMJPriceNo = {"MJ006", "MJ004", "MJ008", "MJ010", "MJ012", "MJ014", "MJ016", "MJ018"};
        // 品號分類
        public static string[] C_INVMBItemType = {"MB005", "MB006", "MB007", "MB008", "MB111", "MB112", "MB113", "MB114", "MB115", "MB116", "MB117", "MB118"};
        // 品號分類 - 服飾版
        public static string[] C_CMSNMItemType = {"NM007", "NM008", "NM009", "NM010", "NM011", "NM012", "NM013", "NM014", "NM015", "NM016", "NM017", "NM018"};
        public const string C_SearchINVMB_Name = "Proc_SearchINVMB";
        public const string C_QueryINVMB_Name = "Proc_QueryINVMB";
        public const string C_DSCPOSQ06_Name = "Proc_DSCPOSQ06";
        public const string C_DSCPOSQ28_Name = "Proc_DSCPOSQ28";
        // C_DataPoolNodeName : array[TDataPoolNode] of string = ('Ref', 'Pub', 'TX', 'Tmp');
        public static string[] C_DataPoolNodeName = {'1', '2', '3', '4', '5'};
        public const char NextArray = '€';
        public const char LineFeed = '↓';
        public const string GetLine = "\r\n";
        public const int cmdBase = 0;
        // base
        public const int cmdWAITING = cmdBase + 0;
        // 0 正常交易
        public const int cmdTRANSACTION = cmdBase + 1;
        // 1 交易中
        public const int cmdCANCEL_TRANSCATIION = cmdBase + 2;
        // 2 交易取消
        public const int cmdNULLIFY_INVOICE = cmdBase + 3;
        // 3 整張銷退
        public const int cmdORDER = cmdBase + 4;
        // 4 預購
        public const int cmdCOUPON = cmdBase + 5;
        // 5 禮券銷售
        public const int cmdSUBTOTAL = cmdBase + 6;
        // 6 小計
        public const int cmdPAYMENT = cmdBase + 7;
        // 7 付款
        public const int cmdTEMPORARY_SAVE = cmdBase + 8;
        // 8 存交易
        public const int cmdTEMPORARY_LEAVE = cmdBase + 9;
        // 9 暫時離機
        public const int cmdQUERY = cmdBase + 10;
        // 10 交易查詢
        public const int cmdREPRINT_INVOICE = cmdBase + 11;
        // 11 發票重印
        public const int cmdINVOICE_CHANGED = cmdBase + 12;
        // 12 變更發票
        public const int cmdDEL_ITEM_ALLOCALED = cmdBase + 13;
        // 13 指定刪除商品
        public const int cmdDEL_ITEM_INSTANTLY = cmdBase + 14;
        // 14 立即刪除商品
        public const int cmdREFOUND = cmdBase + 15;
        // 15 商品退貨
        public const int cmdPRICE_LOOK_UP = cmdBase + 16;
        // 16 商品價格查詢
        public const int cmdPRICE_CHANGED = cmdBase + 17;
        // 17 更改售價
        public const int cmdFREE_PLU = cmdBase + 18;
        // 18 贈品
        public const int cmdARDeduct = cmdBase + 19;
        // 19 沖銷應收帳款
        public const int cmdICPREPAID = cmdBase + 20;
        // 20 儲值卡加值
        public const int cmdAddAwhileAmt = cmdBase + 21;
        // 21 新增暫收款
        public const int cmdAbateAwhileAmt = cmdBase + 22;
        // 22 銷退暫收款
        public const int cmdDeductAwhileAmt = cmdBase + 23;
        // 23 尾款沖銷
        public const int cmdCheckout = cmdBase + 24;
        // 24 結帳
        public const int cmdMBase = cmdBase + 1024;
        // 個案修改Cmd代號啟始*****************
        // 交易明細類別
        public const int itmBase = 0;
        // base
        public const int itmNORMAL = itmBase + 1;
        // 1.銷售 default;
        public const int itmREFOUND = itmBase + 2;
        // 2.退貨
        public const int itmFREE = itmBase + 3;
        // 3.贈品
        public const int itmOrder = itmBase + 4;
        // 4.訂金
        public const int itmOrderDeduct = itmBase + 5;
        // 5.沖訂金
        public const int itmDEL_ITEM_ALLOCALED = itmBase + 6;
        // 6.指定更正
        public const int itmDEL_ITEM_INSTANTLY = itmBase + 7;
        // 7.立即更正
        public const int itmCouponTrans = itmBase + 8;
        // 8.禮券銷售
        public const int itmCANCELED = itmBase + 9;
        // 9.交易取消
        public const int itmOrderBack = itmBase + 10;
        // A.訂金銷退
        public const int itmOrderDeductBack = itmBase + 11;
        // B.沖訂金銷退
        public const int itmICPrePaid = itmBase + 12;
        // C.儲值卡加值
        public const int itmOrderDepositDisc = itmBase + 13;
        // D.訂金折讓
        public const int itmMBase = itmBase + 1048;
        // 個案itm******************************
        // 會員點數異動類別
        public const int vbtBase = 0;
        // base
        public const int vbtNONE = vbtBase + 0;
        // 0.無
        public const int vbtNORMAL = vbtBase + 1;
        // 1.消費新增
        public const int vbtREFOUND = vbtBase + 2;
        // 2.銷退減少
        public const int vbtEXCHANGE = vbtBase + 3;
        // 3.點數換購商品
        public const int vbtEXCHANGE_FREE = vbtBase + 4;
        // 4.點數兌換贈品
        public const int vbtEXCHANGE_REFOUND = vbtBase + 5;
        // 5.點數換購商品銷退
        public const int vbtEXCHANGE_FREE_REFOUND = vbtBase + 6;
        // 6.點數兌換贈品銷退
        public const int vbtEXCHANGE_MONEY = vbtBase + 7;
        // 7.點數折抵金額
        public const int vbtEXCHANGE_MONEY_REFOUND = vbtBase + 8;
        // 8.點數折抵金額銷退
        public const int vbtCount = vbtBase + 9;
        public const int vbtMBase = vbtBase + 1024;
        // 點數個案****************************
        // 512個 系統相關 EX:登入登出，鍵盤輸入等
        public const object CM_POS_Base = WM_User + 1024;
        // 基本
        public const object CM_Login = CM_POS_Base + 1;
        // 登入
        public const object CM_KeyBoardInput = CM_POS_Base + 2;
        // 按鍵單一輸入
        public const object CM_KeyBoardMassInput = CM_POS_Base + 3;
        // 按鍵文數輸入
        public const object CM_CodeInput = CM_POS_Base + 4;
        // ENTER輸入
        public const object CM_CodeClear = CM_POS_Base + 5;
        // 清除暫存
        public const object CM_DispClear = CM_POS_Base + 6;
        // 清除畫面
        public const object CM_DispBack = CM_POS_Base + 7;
        // Backspace
        public const object CM_AddForm = CM_POS_Base + 8;
        // 增加FORM
        public const object CM_ShowForm = CM_POS_Base + 9;
        // 顯示FORM
        public const object CM_HideForm = CM_POS_Base + 10;
        // 隱藏FORM
        public const object CM_Wait = CM_POS_Base + 11;
        // wait
        public const object CM_ReWait = CM_POS_Base + 12;
        // 解除等待
        public const object CM_ShowDisp = CM_POS_Base + 13;
        // 客顯
        public const object CM_CloseAPP = CM_POS_Base + 14;
        // 結束系統
        // 512個 功能相關 EX: 促銷觸發，會員輸入，品號輸入等
        public const object CM_POS_Std = WM_User + 1536;
        // 標準功能
        public const object CM_ItemInput = CM_POS_Std + 1;
        // 品號輸入  WParam: 1 成功 0 失敗 LParam 0 單筆 1 重讀
        // CM_Checkout =               CM_POS_Std+ 2;    //結帳
        public const object CM_MberInput = CM_POS_Std + 3;
        // 會員輸入      WParam: 1 有會員 0 無會員
        public const object CM_Cancel = CM_POS_Std + 4;
        // 取消交易
        public const object CM_LoginAccess = CM_POS_Std + 5;
        // 登入成功
        public const object CM_UnifiedNumber = CM_POS_Std + 6;
        // 統一編號輸入  WParam: 1 成功 0 失敗
        public const object CM_POSVI02_ChangePage = CM_POS_Std + 7;
        // POSVI02 換頁
        public const object CM_POSVI03_ChangePage = CM_POS_Std + 8;
        // POSVI03 換頁
        public const object CM_POSVI04_ChangePage = CM_POS_Std + 9;
        // POSVI04 換頁
        public const object CM_POSVI05_ChangePage = CM_POS_Std + 10;
        // POSVI05 換頁
        public const object CM_POSVI06_ChangePage = CM_POS_Std + 11;
        // POSVI06 換頁
        public const object CM_POSVI07_ChangePage = CM_POS_Std + 12;
        // POSVI07 換頁  WParam: 1 上捲  LParam: 1 下捲
        public const object CM_Change_Dpr = CM_POS_Std + 13;
        // 部門變動時
        public const object CM_FunTrigger = CM_POS_Std + 14;
        // 功能鍵觸發
        public const object CM_PayTrigger = CM_POS_Std + 15;
        // 付款鍵觸發    WParam: 功能代碼 或 金額 LParam 0.禮卷付款 1.現金付款  2.信用卡付款 EX:80 posvi08的禮卷付款 81 posvi08 現金付款 111 posvi11 顯示
        public const object CM_PayItem = CM_POS_Std + 16;
        public const object CM_RefreshAll = CM_POS_Std + 17;
        // 重新更新所有畫面
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
        // 餐飲版 付款方式(VI06) 開關
        public const object CM_PluPanelSwitch = CM_POS_Std + 25;
        // 餐飲版 商品(VI04)     開關
        public const object CM_FuncPanelSwitch = CM_POS_Std + 26;
        // 餐飲版 功能鍵(VI03)   開關 此三個開關因為在同一PANEL所以要調整
        public const object CM_PayMealPayInfo = CM_POS_Std + 27;
        // 餐飲版 付款資訊顯示
        public const object CM_DepPanelSwitch = CM_POS_Std + 28;
        // 餐飲版 部門鍵(VI05)   開關
        public const object CM_sgSaleItemMouseUp = CM_POS_Std + 29;
        // 餐飲版 STRINGGRID MOUSEUP(VI07)
        public const object CM_sgSaleItemSelectCell = CM_POS_Std + 30;
        // 餐飲版 STRINGGRID SelectCell(VI07)
        public const object CM_BirthPicSWitch = CM_POS_Std + 31;
        // 生日圖案 開關
        // 1024個 個案相關
        public const object CM_POS_Cust = WM_User + 2048;
    } // end LibDefine

}

// 個案功能
