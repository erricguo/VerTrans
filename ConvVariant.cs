using System;
using System.IO;
using System.Collections;
 // *******************************************************
 // Memory Structor:
 // Signature     : 16 bytes
 // Data Size     : 4 bytes (Integer)
 // VarType       : 4 bytes (Integer)
 // see any type ???
 // *******************************************************
namespace ConvVariant
{
    public class TConvVariant
    {
        public MemoryStream DataStream
        {
          get {
            return c_oStream;
          }
          set {
            Set_DataStream(value);
          }
        }
        private MemoryStream c_oStream = null;
        private string c_sSignature = String.Empty;
        private int c_nSignatureBytes = 0;
        private int c_nVarSizeBytes = 0;
        private int c_nReadPos = 0;
        private int c_nWritePos = 0;
        //Constructor  Create()
        public TConvVariant() : base()
        {
            c_oStream = new MemoryStream();
            c_sSignature = "DS VARIANT  0100";
            c_nSignatureBytes = 16;
            c_nVarSizeBytes = sizeof(int);
            this.ResetWrite();
        }
        //@ Destructor  Destroy()
        ~TConvVariant()
        {
            //@ Unsupported property or method(C): 'Free'
            c_oStream.Free;
            // base.Destroy();
        }
        // Protected Method
        protected void ResetWrite()
        {
            c_oStream.Position = 0;
            c_oStream.WriteByte(c_sSignature[1], c_nSignatureBytes);
            c_nReadPos = c_nSignatureBytes + c_nVarSizeBytes;
            c_nWritePos = c_nReadPos;
            c_oStream.Length = c_nReadPos;
        }

        protected void ResetRead()
        {
            c_oStream.Position = 0;
            c_nReadPos = c_nSignatureBytes + c_nVarSizeBytes;
            c_nWritePos = c_nReadPos;
        }

        protected bool CheckSignatrue()
        {
            bool result;
            string l_sStr;
            //@ Unsupported function or procedure: 'SetLength'
            l_sStr.Length = c_nSignatureBytes;
            c_oStream.Position = 0;
            c_oStream.ReadByte(l_sStr[1], c_nSignatureBytes);
            result = (l_sStr == c_sSignature);
            return result;
        }

        protected int Write(object Buffer, int Count)
        {
            int result;
            c_oStream.Position = c_nWritePos;
            result = c_oStream.WriteByte(Buffer, Count);
            c_nWritePos = c_oStream.Position;
            return result;
        }

        protected int Read(ref object Buffer, int Count)
        {
            int result;
            c_oStream.Position = c_nReadPos;
            result = c_oStream.ReadByte(Buffer, Count);
            c_nReadPos = c_oStream.Position;
            return result;
        }

        protected void WriteVariant(object Value)
        {
            int i;
            int l_nVType;
            string l_WideStr;
            //@ Unsupported function or procedure: 'VarType'
            l_nVType = VarType(Value);
            //@ Unsupported function or procedure: 'VarIsArray'
            if (VarIsArray(Value))
            {
                this.WriteArray(Value);
            }
            else
            {
                //@ Undeclared identifier(3): 'varTypeMask'
                switch((l_nVType && varTypeMask))
                {
                    //@ Undeclared identifier(3): 'varEmpty'
                    //@ Undeclared identifier(3): 'varNull'
                    case varEmpty:
                    case varNull:
                        // Memory = VType
                        this.Write(l_nVType, sizeof(l_nVType));
                        break;
                    //@ Undeclared identifier(3): 'varOleStr'
                    case varOleStr:
                        // Memory = VType, Length, String Data
                        l_WideStr = (Value as string);
                        i = l_WideStr.Length;
                        this.Write(l_nVType, sizeof(l_nVType));
                        this.Write(i, sizeof(i));
                        if (i > 0)
                        {
                            this.Write(l_WideStr[1], i * 2);
                        }
                        break;
                    //@ Undeclared identifier(3): 'varVariant'
                    case varVariant:
                        //@ Undeclared identifier(3): 'varByRef'
                        //@ Undeclared identifier(3): 'varByRef'
                        if (l_nVType && varByRef != varByRef)
                        {
                            //@ Unsupported property or method(A): 'CreateFmt'
                            throw Exception.CreateFmt(Units.ConvVariant.MSG_BadVariantType, new string[] {(l_nVType.ToString("x")});
                        }
                        //@ Undeclared identifier(3): 'varByRef'
                        i = varByRef;
                        this.Write(i, sizeof(int));
                        //@ Undeclared identifier(3): 'TVarData'
                        //@ Unsupported property or method(D): 'VPointer'
                        this.WriteVariant((TVarData(Value).VPointer as object));
                        break;
                    //@ Undeclared identifier(3): 'varDispatch'
                    //@ Undeclared identifier(3): 'varUnknown'
                    case varDispatch:
                    case varUnknown:
                        //@ Unsupported property or method(A): 'CreateFmt'
                        throw Exception.CreateFmt(Units.ConvVariant.MSG_BadVariantType, new string[] {(l_nVType.ToString("x")});
                        break;
                    default:
                        this.Write(l_nVType, sizeof(l_nVType));
                        //@ Undeclared identifier(3): 'varByRef'
                        //@ Undeclared identifier(3): 'varByRef'
                        if (l_nVType && varByRef == varByRef)
                        {
                            //@ Undeclared identifier(3): 'TVarData'
                            //@ Unsupported property or method(D): 'VPointer'
                            //@ Undeclared identifier(3): 'varTypeMask'
                            this.Write(TVarData(Value).VPointer, Units.ConvVariant.VariantSize[l_nVType && varTypeMask]);
                        }
                        else
                        {
                            //@ Undeclared identifier(3): 'TVarData'
                            //@ Unsupported property or method(D): 'VPointer'
                            //@ Undeclared identifier(3): 'varTypeMask'
                            this.Write(TVarData(Value).VPointer, Units.ConvVariant.VariantSize[l_nVType && varTypeMask]);
                        }
                        break;
                }
            // case
            }
        }

        protected void WriteArray(object Value)
        {
            int l_nVType;
            int l_nVSize;
            int l_nDimCount;
            int l_nElemSize;
            PSafeArray l_pVarArrayPtr;
            int[] l_aIndices;
            int[] l_aDim;
            object l_vVar;
            object l_pPtr;
            int i;
            // 1.簡單 Memory : VType,DimCount,Dim1Low,Dim1High,DimNLow,DimNHigh,DataSize,Datas
            // 2.複雜 Memory : VType,DimCount,Dim1Low,Dim1High,DimNLow,DimNHigh,
            // Data00,Data01,Data02.Data10,Data11....
            //@ Unsupported function or procedure: 'VarType'
            l_nVType = VarType(Value);
            this.Write(l_nVType, sizeof(l_nVType));
            //@ Unsupported function or procedure: 'VarArrayDimCount'
            l_nDimCount = VarArrayDimCount(Value);
            this.Write(l_nDimCount, sizeof(l_nDimCount));
            //@ Undeclared identifier(3): 'TVarData'
            //@ Unsupported property or method(D): 'VArray'
            //@ Undeclared identifier(3): 'PSafeArray'
            l_pVarArrayPtr = PSafeArray(TVarData(Value).VArray);
            l_nVSize = sizeof(int) * l_nDimCount * 2;
            //@ Unsupported function or procedure: 'GetMem'
            GetMem(l_aDim, l_nVSize);
            try {
                for (i = 1; i <= l_nDimCount; i ++ )
                {
                    //@ Unsupported function or procedure: 'VarArrayLowBound'
                    l_aDim[(i - 1) * 2] = VarArrayLowBound(Value, i);
                    //@ Unsupported function or procedure: 'VarArrayHighBound'
                    l_aDim[(i - 1) * 2 + 1] = VarArrayHighBound(Value, i);
                }
                this.Write(l_aDim, l_nVSize);
                // varSmallInt,varInteger,varSingle,varDouble,varCurrency,varDate,varBoolean,varByte
                //@ Undeclared identifier(3): 'varTypeMask'
                if (new ArrayList(Units.ConvVariant.EasyArrayTypes).Contains(l_nVType && varTypeMask))
                {
                    // 取出每個元素長度
                    //@ Undeclared identifier(3): 'SafeArrayGetElemSize'
                    l_nElemSize = SafeArrayGetElemSize(l_pVarArrayPtr);
                    l_nVSize = 1;
                    for (i = 0; i < l_nDimCount; i ++ )
                    {
                        l_nVSize = (l_aDim[i * 2 + 1] - l_aDim[i * 2] + 1) * l_nVSize;
                    }
                    l_nVSize = l_nVSize * l_nElemSize;
                    //@ Unsupported function or procedure: 'VarArrayLock'
                    l_pPtr = VarArrayLock(Value);
                    try {
                        this.Write(l_nVSize, sizeof(l_nVSize));
                        this.Write(l_pPtr, l_nVSize);
                    } finally {
                        //@ Unsupported function or procedure: 'VarArrayUnlock'
                        VarArrayUnlock(Value);
                    }
                }
                else
                {
                    // varOleStr,varDispatch,varError,varVariant,varUnknown,varString,varAny
                    //@ Unsupported function or procedure: 'GetMem'
                    GetMem(l_aIndices, l_nVSize / 2);
                    try {
                        for (i = 0; i < l_nDimCount; i ++ )
                        {
                            l_aIndices[i] = l_aDim[i * 2];
                        }
                        // Low bound
                        while (true)
                        {
                            //@ Undeclared identifier(3): 'varTypeMask'
                            //@ Undeclared identifier(3): 'varVariant'
                            if (l_nVType && varTypeMask != varVariant)
                            {
                                //@ Undeclared identifier(3): 'TVarData'
                                //@ Unsupported property or method(D): 'VPointer'
                                //@ Undeclared identifier(3): 'SafeArrayGetElement'
                                //@ Undeclared identifier(3): 'OleCheck'
                                OleCheck(SafeArrayGetElement(l_pVarArrayPtr, l_aIndices, TVarData(l_vVar).VPointer));
                                //@ Undeclared identifier(3): 'TVarData'
                                //@ Undeclared identifier(3): 'varTypeMask'
                                //@ Unsupported property or method(D): 'VType'
                                TVarData(l_vVar).VType = l_nVType && varTypeMask;
                            }
                            else
                            {
                                //@ Undeclared identifier(3): 'SafeArrayGetElement'
                                //@ Undeclared identifier(3): 'OleCheck'
                                OleCheck(SafeArrayGetElement(l_pVarArrayPtr, l_aIndices, l_vVar));
                            }
                            WriteVariant(l_vVar);
                            l_aIndices[l_nDimCount - 1] ++;
                            if (l_aIndices[l_nDimCount - 1] > l_aDim[(l_nDimCount - 1) * 2 + 1])
                            {
                                for (i = l_nDimCount - 1; i >= 0; i-- )
                                {
                                    if (l_aIndices[i] > l_aDim[i * 2 + 1])
                                    {
                                        if (i == 0)
                                        {
                                            return;
                                        }
                                        l_aIndices[i - 1] ++;
                                        l_aIndices[i] = l_aDim[i * 2];
                                    }
                                }
                            }
                        }
                    } finally {
                        //@ Unsupported function or procedure: 'FreeMem'
                        FreeMem(l_aIndices);
                    }
                }
            } finally {
                //@ Unsupported function or procedure: 'FreeMem'
                FreeMem(l_aDim);
            }
        }

        protected object ReadVariant(out TVarFlag[] Flags)
        {
            object result;
            int i;
            int l_nVType;
            string l_WideStr;
            TVarFlag[] l_TmpFlags;
            //@ Unsupported function or procedure: 'VarClear'
            VarClear(result);
            if (c_oStream.Length <= this.c_nSignatureBytes + this.c_nVarSizeBytes)
            {
                return result;
            }
            Flags = new object[] {};
            this.Read(ref l_nVType, sizeof(l_nVType));
            //@ Undeclared identifier(3): 'varByRef'
            //@ Undeclared identifier(3): 'varByRef'
            if (l_nVType && varByRef == varByRef)
            {
                Flags += TVarFlag.vfByRef;
            }
            //@ Undeclared identifier(3): 'varByRef'
            if (l_nVType == varByRef)
            {
                Flags += TVarFlag.vfVariant;
                result = this.ReadVariant(out l_TmpFlags);
                return result;
            }
            if (new ArrayList(Flags).Contains(TVarFlag.vfByRef))
            {
                //@ Undeclared identifier(3): 'varByRef'
                l_nVType = l_nVType ^ varByRef;
            }
            //@ Undeclared identifier(3): 'varArray'
            //@ Undeclared identifier(3): 'varArray'
            if ((l_nVType && varArray) == varArray)
            {
                result = this.ReadArray(l_nVType);
            }
            else
            {
                //@ Undeclared identifier(3): 'varTypeMask'
                switch(l_nVType && varTypeMask)
                {
                    //@ Undeclared identifier(3): 'varEmpty'
                    case varEmpty:
                        //@ Unsupported function or procedure: 'VarClear'
                        VarClear(result);
                        break;
                    //@ Undeclared identifier(3): 'varNull'
                    case varNull:
                        //@ Unsupported function or procedure: 'NULL'
                        result = NULL;
                        break;
                    //@ Undeclared identifier(3): 'varOleStr'
                    case varOleStr:
                        this.Read(ref i, sizeof(int));
                        //@ Unsupported function or procedure: 'SetLength'
                        l_WideStr.Length = i;
                        if (i > 0)
                        {
                            this.Read(ref l_WideStr[1], i * 2);
                        }
                        result = l_WideStr;
                        break;
                    //@ Undeclared identifier(3): 'varDispatch'
                    //@ Undeclared identifier(3): 'varUnknown'
                    case varDispatch:
                    case varUnknown:
                        //@ Unsupported property or method(A): 'CreateFmt'
                        throw Exception.CreateFmt(Units.ConvVariant.MSG_BadVariantType, new string[] {(l_nVType.ToString("x")});
                        break;
                    default:
                        //@ Undeclared identifier(3): 'TVarData'
                        //@ Unsupported property or method(D): 'VType'
                        TVarData(result).VType = l_nVType;
                        //@ Undeclared identifier(3): 'TVarData'
                        //@ Unsupported property or method(D): 'VPointer'
                        //@ Undeclared identifier(3): 'varTypeMask'
                        this.Read(ref TVarData(result).VPointer, Units.ConvVariant.VariantSize[l_nVType && varTypeMask]);
                        break;
                }
            }
            return result;
        }

        protected object ReadArray(int VType)
        {
            object result;
            TVarFlag[] l_Flags;
            int i;
            int l_nDimCount;
            int l_nVSize;
            int[] l_aDim;
            int[] l_aIndices;
            object l_pPtr;
            object l_vVar;
            PSafeArray l_pVarArrayPtr;
            //@ Unsupported function or procedure: 'VarClear'
            VarClear(result);
            this.Read(ref l_nDimCount, sizeof(l_nDimCount));
            l_nVSize = l_nDimCount * sizeof(int) * 2;
            //@ Unsupported function or procedure: 'GetMem'
            GetMem(l_aDim, l_nVSize);
            try {
                this.Read(ref l_aDim, l_nVSize);
                //@ Unsupported function or procedure: 'Slice'
                //@ Undeclared identifier(3): 'varTypeMask'
                //@ Unsupported function or procedure: 'VarArrayCreate'
                result = VarArrayCreate(Slice(l_aDim, l_nDimCount * 2), VType && varTypeMask);
                //@ Undeclared identifier(3): 'TVarData'
                //@ Unsupported property or method(D): 'VArray'
                //@ Undeclared identifier(3): 'PSafeArray'
                l_pVarArrayPtr = PSafeArray(TVarData(result).VArray);
                //@ Undeclared identifier(3): 'varTypeMask'
                if (new ArrayList(Units.ConvVariant.EasyArrayTypes).Contains(VType && varTypeMask))
                {
                    this.Read(ref l_nVSize, sizeof(l_nVSize));
                    //@ Unsupported function or procedure: 'VarArrayLock'
                    l_pPtr = VarArrayLock(result);
                    try {
                        this.Read(ref l_pPtr, l_nVSize);
                    } finally {
                        //@ Unsupported function or procedure: 'VarArrayUnlock'
                        VarArrayUnlock(result);
                    }
                }
                else
                {
                    l_nVSize = l_nVSize / 2;
                    //@ Unsupported function or procedure: 'GetMem'
                    GetMem(l_aIndices, l_nVSize);
                    try {
                        //@ Unsupported function or procedure: 'FillChar'
                        FillChar(l_aIndices, l_nVSize, 0);
                        for (i = 0; i < l_nDimCount; i ++ )
                        {
                            l_aIndices[i] = l_aDim[i * 2];
                        }
                        while (true)
                        {
                            l_vVar = ReadVariant(out l_Flags);
                            //@ Undeclared identifier(3): 'varTypeMask'
                            //@ Undeclared identifier(3): 'varVariant'
                            if (VType && varTypeMask == varVariant)
                            {
                                //@ Undeclared identifier(3): 'SafeArrayPutElement'
                                //@ Undeclared identifier(3): 'OleCheck'
                                OleCheck(SafeArrayPutElement(l_pVarArrayPtr, l_aIndices, l_vVar));
                            }
                            else
                            {
                                //@ Undeclared identifier(3): 'TVarData'
                                //@ Unsupported property or method(D): 'VPointer'
                                //@ Undeclared identifier(3): 'SafeArrayPutElement'
                                //@ Undeclared identifier(3): 'OleCheck'
                                OleCheck(SafeArrayPutElement(l_pVarArrayPtr, l_aIndices, TVarData(l_vVar).VPointer));
                            }
                            l_aIndices[l_nDimCount - 1] ++;
                            if (l_aIndices[l_nDimCount - 1] > l_aDim[(l_nDimCount - 1) * 2 + 1])
                            {
                                for (i = l_nDimCount - 1; i >= 0; i-- )
                                {
                                    if (l_aIndices[i] > l_aDim[i * 2 + 1])
                                    {
                                        if (i == 0)
                                        {
                                            return result;
                                        }
                                        l_aIndices[i - 1] ++;
                                        l_aIndices[i] = l_aDim[i * 2];
                                    }
                                }
                            }
                        }
                    } finally {
                        //@ Unsupported function or procedure: 'FreeMem'
                        FreeMem(l_aIndices);
                    }
                }
            } finally {
                //@ Unsupported function or procedure: 'FreeMem'
                FreeMem(l_aDim);
            }
            return result;
        }

        // property method
        protected void Set_DataStream(MemoryStream value)
        {
        }

        // public Method
        public void SaveVariant(object Value)
        {
            int l_nSize;
            // 先清除Memory Stream
            this.ResetWrite();
            this.WriteVariant(Value);
            l_nSize = c_oStream.Length - (c_nSignatureBytes + c_nVarSizeBytes);
            c_oStream.Position = c_nSignatureBytes;
            c_oStream.WriteByte(l_nSize, c_nVarSizeBytes);
        }

        public object RestoreVariant()
        {
            object result;
            TVarFlag[] l_VarFlags;
            if (!this.CheckSignatrue())
            {
                throw new Exception(Units.ConvVariant.MSG_UnKnownDataStream);
            }
            this.ResetRead();
            result = this.ReadVariant(out l_VarFlags);
            return result;
        }

    } // end TConvVariant

    public enum TVarFlag
    {
        vfByRef,
        vfVariant
    } // end TVarFlag

}

namespace ConvVariant.Units
{
    public class ConvVariant
    {
    //@ Undeclared identifier(3): 'varByte'
    //@ Undeclared identifier(3): 'varSmallInt'
    //@ Undeclared identifier(3): 'varInteger'
    //@ Undeclared identifier(3): 'varSingle'
    //@ Undeclared identifier(3): 'varDouble'
    //@ Undeclared identifier(3): 'varCurrency'
    //@ Undeclared identifier(3): 'varDate'
    //@ Undeclared identifier(3): 'varBoolean'
    //@ Undeclared identifier(3): 'varByte'
        public const string MSG_BadVariantType = "Unsupported variant type: %s";
        public const string MSG_UnKnownDataStream = "Unknown Data Stream";
        public static ushort[] VariantSize = {0, 0, sizeof(short), sizeof(int), sizeof(float), sizeof(double), sizeof(decimal), sizeof(DateTime), 0, 0, sizeof(int), sizeof(bool), 0, 0, 0, 0, 0, sizeof(byte)};
        public const object EasyArrayTypes = {varSmallInt, varInteger, varSingle, varDouble, varCurrency, varDate, varBoolean, varByte};
    } // end ConvVariant

}

