using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VerTrans
{
    public class GenericList
    {
        public string XIndex;
        public string XID;
        public string XName;
        public string XType;
        public float XLength;
        public string XInfo;
        public bool XPK;
    }

    public class RootList
    {
        public List<GenericList> GetRecord() { return XGenericList; }
        List<GenericList> XGenericList;
        public RootList()
        {
            XGenericList = new List<GenericList>();
        }	
        public void Add(string mIndex,string mID,string mName,string mType,float mLength,string mInfo,bool mPK)
        {
            GenericList g = new GenericList();
            g.XIndex = mIndex;
            g.XID = mID;
            g.XName = mName;
            g.XType = mType;
            g.XLength = mLength;
            g.XInfo = mInfo;
            g.XPK = mPK;
            XGenericList.Add(g);
        }
        public void Clear()
        {
            XGenericList.Clear(); 
        }
        ~RootList()
        {
            XGenericList.Clear();          
        }
        /*public void SetHead(int mindex,string mCode,string mCodeName,string mCodeType,bool mIsTramsed,string mPrimary,
                            string mIndex01, string mIndex02, string mIndex03)
        {
            XCode = mCode;
            XCodeName=mCodeName;
            XCodeType=mCodeType;
            XIsTransed=mIsTramsed;
            XPrimary=mPrimary;
            XIndex01=mIndex01;
            XIndex02=mIndex02;
            XIndex03=mIndex03;
            intdex = mindex;
        }*/
        public string XCode;
        public string XCodeName;
        public string XCodeType;
        public bool XIsTransed;
        public string XPrimary;
        public string XIndex01;
        public string XIndex02;
        public string XIndex03;
        public int intdex;
    }
    public class XXX2003
    {
        public List<RootList> GetRoot() { return XRoot; }
        List<RootList> XRoot;
        public XXX2003()
        {
            XRoot = new List<RootList>();
        }
        public void Add(int mindex, string mCode, string mCodeName, string mCodeType, bool mIsTramsed, string mPrimary,
                            string mIndex01, string mIndex02, string mIndex03)
        {
            RootList r = new RootList();
            r.XCode = mCode;
            r.XCodeName=mCodeName;
            r.XCodeType=mCodeType;
            r.XIsTransed=mIsTramsed;
            r.XPrimary=mPrimary;
            r.XIndex01=mIndex01;
            r.XIndex02=mIndex02;
            r.XIndex03=mIndex03;
            r.intdex = mindex;
            XRoot.Add(r);
        }
        public void Clear()
        {
            XRoot.Clear();
        }
        ~XXX2003()
        {
            XRoot.Clear();            
        }

    }

}
