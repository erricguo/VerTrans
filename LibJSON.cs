using System;
namespace LibJSON.Units
{
    public class LibJSON
    {
        /*public static int IndexOfName(TJSONObject Owner, string Name)
        {
            int result;
            int i;
            result =  -1;
            //@ Unsupported property or method(C): 'Size'
            for (i = 0; i < Owner.Size; i ++ )
            {
                //@ Unsupported property or method(A): 'Get'
                //@ Unsupported property or method(D): 'JsonString'
                //@ Unsupported property or method(D): 'Value'
                if (Owner.Get(i).JsonString.Value.ToUpper() == Name.ToUpper())
                {
                    result = i;
                    break;
                }
            }
            return result;
        }*/

        // 自己實作之後可以改善效率
        public static bool HasName(TJSONObject Owner, string Name)
        {
            bool result;
            result = IndexOfName(Owner, Name) !=  -1;
            return result;
        }

        // 序列化JSON物件
        public static TJSONValue StrToJSONValue(string value)
        {
            TJSONValue result;
            //@ Undeclared identifier(3): 'BytesOf'
            //@ Undeclared identifier(3): 'TJSONObject'
            //@ Unsupported property or method(B): 'ParseJSONValue'
            result = TJSONObject.ParseJSONValue(BytesOf(value), 0);
            return result;
        }

        public static string JSONValueToStr(TJSONAncestor value)
        {
            string result;
            int mSize;
            sbyte[] mData;
            //@ Unsupported property or method(C): 'EstimatedByteSize'
            mData = new sbyte[value.EstimatedByteSize];
            //@ Unsupported property or method(A): 'ToBytes'
            mSize = value.ToBytes(mData, 0);
            mData = new sbyte[mSize];
            //@ Undeclared identifier(3): 'stringof'
            result = stringof(mData);
            mData = new sbyte[0];
            return result;
        }

        public static bool ValueToBoolean(TJSONValue Value)
        {
            bool result;
            //@ Undeclared identifier(3): 'TJSONFalse'
            if (Value is TJSONFalse)
            {
                result = false;
            }
            //@ Undeclared identifier(3): 'TJSONTrue'
            else if (Value is TJSONTrue)
            {
                result = true;
            }
            else
            {
                throw new Exception("不是布林");
            }
            return result;
        }

        public static DateTime ValueToDateTime(TJSONValue Value)
        {
            DateTime result;
            //@ Undeclared identifier(3): 'TJSONString'
            //@ Unsupported property or method(D): 'Value'
            //@ Unsupported function or procedure: 'TryStrToDateTime'
            if (!TryStrToDateTime(TJSONString(Value).Value, result))
            {
                throw new Exception("不是日期時間");
            }
            return result;
        }

        public static double ValueToDouble(TJSONValue Value)
        {
            double result;
            //@ Undeclared identifier(3): 'TJSONString'
            //@ Unsupported property or method(D): 'Value'
            //@ Unsupported function or procedure: 'TryStrToFloat'
            if (!TryStrToFloat(TJSONString(Value).Value, result))
            {
                throw new Exception("不是數值");
            }
            return result;
        }

        public static int ValueToInt(TJSONValue Value)
        {
            int result;
            //@ Undeclared identifier(3): 'TJSONString'
            //@ Unsupported property or method(D): 'Value'
            //@ Unsupported function or procedure: 'TryStrToInt'
            if (!TryStrToInt(TJSONString(Value).Value, result))
            {
                throw new Exception("不是數值");
            }
            return result;
        }

        // 值的轉換
        public static string ValueToString(TJSONValue Value)
        {
            string result;
            try {
                //@ Undeclared identifier(3): 'TJSONString'
                //@ Unsupported property or method(D): 'Value'
                result = TJSONString(Value).Value;
            }
            catch {
                throw new Exception("不是字串");
            }
            return result;
        }

        public static TJSONValue BooleanToValue(bool Value)
        {
            TJSONValue result;
            if (Value)
            {
                result = new TJSONTrue();
            }
            else
            {
                result = new TJSONFalse();
            }
            return result;
        }

        public static TJSONValue DoubleToValue(double Value)
        {
            TJSONValue result;
            result = new TJSONNumber(Value);
            return result;
        }

        public static TJSONValue IntToValue(int Value)
        {
            TJSONValue result;
            result = new TJSONNumber(Value);
            return result;
        }

        public static TJSONValue DateTimeToValue(DateTime Value)
        {
            TJSONValue result;
            result = new TJSONString((Value).ToString());
            return result;
        }

        public static TJSONValue VariantToValue(object Value)
        {
            TJSONValue result;
            //@ Unsupported function or procedure: 'VarType'
            switch(VarType(Value))
            {
                //@ Undeclared identifier(3): 'varOleStr'
                //@ Undeclared identifier(3): 'varString'
                //@ Undeclared identifier(3): 'varUString'
                case varOleStr:
                case varString:
                case varUString:
                    result = StringToValue((Value as string));
                    break;
                //@ Undeclared identifier(3): 'varSmallInt'
                //@ Undeclared identifier(3): 'varInteger'
                //@ Undeclared identifier(3): 'varShortInt'
                //@ Undeclared identifier(3): 'varByte'
                //@ Undeclared identifier(3): 'varWord'
                //@ Undeclared identifier(3): 'varLongWord'
                //@ Undeclared identifier(3): 'varInt64'
                //@ Undeclared identifier(3): 'varUInt64'
                case varSmallInt:
                case varInteger:
                case varShortInt:
                case varByte:
                case varWord:
                case varLongWord:
                case varInt64:
                case varUInt64:
                    result = IntToValue((int)Value);
                    break;
                //@ Undeclared identifier(3): 'varSingle'
                //@ Undeclared identifier(3): 'varDouble'
                //@ Undeclared identifier(3): 'varCurrency'
                case 271:
                case varSingle:
                case varDouble:
                case varCurrency:
                    result = DoubleToValue((double)Value);
                    break;
                // Extended(Value));
                //@ Undeclared identifier(3): 'varBoolean'
                case varBoolean:
                    result = BooleanToValue((bool)Value);
                    break;
                default:
                    throw new Exception("不是簡單型態");
                    break;
            }
            return result;
        }

        public static TJSONValue StringToValue(string Value)
        {
            TJSONValue result;
            result = new TJSONString(Value);
            return result;
        }

        // 以序列的方式操作JSON物件(支援多型)
        public static void SetStratumValue(TJSONObject Owner, string[] Stratum, TJSONValue Value)
        {
            int i;
            int index;
            int max;
            TJSONObject mObj;
            TJSONObject mObj1;
            TJSONPair mPar;
            if (Stratum.Length == 0)
            {
                return;
            }
            mObj = Owner;
            max = Stratum.GetUpperBound(0);
            for (i = 0; i < max; i ++ )
            {
                // 只作到倒數第2個
                index = IndexOfName(mObj, Stratum[i]);
                if (index ==  -1)
                {
                    mObj1 = new TJSONObject();
                    //@ Unsupported property or method(A): 'AddPair'
                    mObj.AddPair(Stratum[i], mObj1);
                    mObj = mObj1;
                // 現在的兒子是下一次的爸爸
                }
                else
                {
                    //@ Unsupported property or method(A): 'get'
                    mPar = mObj.get(index);
                    //@ Unsupported property or method(C): 'JsonValue'
                    //@ Undeclared identifier(3): 'TJSONObject'
                    if (mPar.JsonValue is TJSONObject)
                    {
                        //@ Unsupported property or method(C): 'JsonValue'
                        //@ Undeclared identifier(3): 'TJSONObject'
                        mObj = TJSONObject(mPar.JsonValue);
                    }
                    else
                    {
                        // 兒子不是TJSONObject就砍掉重建
                        //@ Unsupported property or method(C): 'JsonValue'
                        //@ Unsupported property or method(D): 'Free'
                        mPar.JsonValue.Free;
                        mObj1 = new TJSONObject();
                        //@ Unsupported property or method(C): 'JsonValue'
                        mPar.JsonValue = mObj1;
                        mObj = mObj1;
                    // 現在的兒子是下一次的爸爸
                    }
                }
            }
            // 做最後一個
            index = IndexOfName(mObj, Stratum[max]);
            if (index ==  -1)
            {
                //@ Unsupported property or method(A): 'AddPair'
                mObj.AddPair(Stratum[max], Value);
            }
            else
            {
                //@ Unsupported property or method(A): 'Get'
                //@ Unsupported property or method(D): 'JsonValue'
                //@ Unsupported property or method(D): 'Free'
                mObj.Get(index).JsonValue.Free;
                //@ Unsupported property or method(A): 'Get'
                //@ Unsupported property or method(D): 'JsonValue'
                mObj.Get(index).JsonValue = Value;
            }
        }

        // 以序列的方式操作JSON物件(支援多型)
        public static void SetStratumValue(TJSONObject Owner, string[] Stratum, string Value)
        {
            SetStratumValue(Owner, Stratum, StringToValue(Value));
        }

        // 以序列的方式操作JSON物件(支援多型)
        public static void SetStratumValue(TJSONObject Owner, string[] Stratum, int Value)
        {
            SetStratumValue(Owner, Stratum, IntToValue(Value));
        }

        // 以序列的方式操作JSON物件(支援多型)
        public static void SetStratumValue(TJSONObject Owner, string[] Stratum, double Value)
        {
            SetStratumValue(Owner, Stratum, DoubleToValue(Value));
        }

        // 以序列的方式操作JSON物件(支援多型)
        public static void SetStratumValue(TJSONObject Owner, string[] Stratum, bool Value)
        {
            SetStratumValue(Owner, Stratum, BooleanToValue(Value));
        }

        // 以序列的方式操作JSON物件(支援多型)
        public static void SetStratumValue(TJSONObject Owner, string[] Stratum, DateTime Value)
        {
            SetStratumValue(Owner, Stratum, DateTimeToValue(Value));
        }

        public static void SetStratumVariant(TJSONObject Owner, string[] Stratum, object Value)
        {
            SetStratumValue(Owner, Stratum, VariantToValue(Value));
        }

        public static TJSONValue GetStratumJSONValue(TJSONObject Owner, string[] Stratum)
        {
            TJSONValue result;
            int i;
            int index;
            result = null;
            if (Stratum.Length == 0)
            {
                return result;
            }
            result = Owner;
            for (i = Stratum.GetLowerBound(0); i <= Stratum.GetUpperBound(0); i ++ )
            {
                //@ Undeclared identifier(3): 'TJSONObject'
                if (result is TJSONObject)
                {
                    //@ Undeclared identifier(3): 'TJSONObject'
                    index = IndexOfName(TJSONObject(result), Stratum[i]);
                    if (index !=  -1)
                    {
                        //@ Undeclared identifier(3): 'TJSONObject'
                        //@ Unsupported property or method(B): 'Get'
                        //@ Unsupported property or method(D): 'JsonValue'
                        result = TJSONObject(result).Get(index).JsonValue;
                    }
                    else
                    {
                        result = null;
                    }
                }
                //@ Undeclared identifier(3): 'TJSONArray'
                //@ Unsupported function or procedure: 'TryStrToInt'
                else if (result is TJSONArray && TryStrToInt(Stratum[i], index))
                {
                    //@ Undeclared identifier(3): 'TJSONArray'
                    //@ Unsupported property or method(B): 'Get'
                    result = TJSONArray(result).Get(index);
                }
                else
                {
                    result = null;
                }
                if (!(result != null))
                {
                    break;
                }
            }
            return result;
        }

        // 將值存入JSONValue(支援多型)(會幫你釋放舊物件)
        public static void SetJSonValueToValue(TJSONValue xObject, TJSONValue xValue)
        {
            if ((xObject != null))
            {
                //@ Unsupported property or method(C): 'Free'
                xObject.Free;
            }
            xObject = xValue;
        }

        // 將值存入JSONValue(支援多型)(會幫你釋放舊物件)
        public static void SetJSonValueToValue(TJSONValue xObject, object xValue)
        {
            if ((xObject != null))
            {
                //@ Unsupported property or method(C): 'Free'
                xObject.Free;
            }
            xObject = VariantToValue(xValue);
            // SetJSonValueToValue(xObject,VariantToValue(xValue));

        }

        public static void ClearJSONObject(TJSONObject xObject)
        {
            int i;
            TJSONPair mPair;
            //@ Unsupported property or method(C): 'Size'
            for (i = 0; i < xObject.Size; i ++ )
            {
                //@ Unsupported property or method(A): 'Get'
                //@ Unsupported property or method(D): 'JsonString'
                //@ Unsupported property or method(D): 'Value'
                //@ Unsupported property or method(A): 'RemovePair'
                mPair = xObject.RemovePair(xObject.Get(i).JsonString.Value);
                //@ Unsupported property or method(C): 'Free'
                mPair.Free;
            }
        }

        // function GetStratumValueDef(Owner:TJSONObject; Stratum:array of string; DefValue:string=''):string; overload;
        // function GetStratumValueDef(Owner:TJSONObject; Stratum:array of string; DefValue:integer=0):integer; overload;
        // function GetStratumValueDef(Owner:TJSONObject; Stratum:array of string; DefValue:Extended=0.0):Extended; overload;
        // function GetStratumValueDef(Owner:TJSONObject; Stratum:array of string; DefValue:Boolean=false):Boolean; overload;
        // function GetStratumValueDef(Owner:TJSONObject; Stratum:array of string; DefValue:TDateTime=now):TDateTime; overload;
        // 
        // function GetStratumValueDef(Owner:TJSONObject; Stratum:array of string; DefValue:string):string; overload;
        // begin
        // try
        // Result := GetStratumJSONValue(Owner, Stratum).Value;
        // except
        // Result := DefValue;
        // end;
        // end;
        // 
        // function GetStratumValueDef(Owner:TJSONObject; Stratum:array of string; DefValue:integer):integer; overload;
        // begin
        // Result := StrToIntDef(GetStratumJSONValue(Owner, Stratum).Value,DefValue)
        // end;
        // 
        // function GetStratumValueDef(Owner:TJSONObject; Stratum:array of string; DefValue:Extended):Extended; overload;
        // begin
        // Result := StrToFloatDef(GetStratumJSONValue(Owner, Stratum).Value,DefValue);
        // end;
        // 
        // function GetStratumValueDef(Owner:TJSONObject; Stratum:array of string; DefValue:Boolean):Boolean; overload;
        // var
        // mJSONValue:TJSONValue;
        // begin
        // mJSONValue :=GetStratumJSONValue(Owner, Stratum);
        // if mJSONValue is TJSONFalse then
        // Result := false
        // else
        // if mJSONValue is TJSONTrue then
        // Result := True
        // else
        // Result := DefValue;
        // 
        // end;
        // 
        // function GetStratumValueDef(Owner:TJSONObject; Stratum:array of string; DefValue:TDateTime):TDateTime; overload;
        // begin
        // Result := StrToDateTimeDef(GetStratumJSONValue(Owner, Stratum).Value,DefValue)
        // end;
        public static string GetStratum_S(TJSONObject Owner, string[] Stratum)
        {
            string result;
            try {
                //@ Unsupported property or method(C): 'Value'
                result = GetStratumJSONValue(Owner, Stratum).Value;
            }
            catch {
                result = "";
            }
            return result;
        }

        public static int GetStratum_I(TJSONObject Owner, string[] Stratum)
        {
            int result;
            //@ Unsupported property or method(C): 'Value'
            result = Convert.ToInt32(GetStratumJSONValue(Owner, Stratum).Value);
            return result;
        }

        public static double GetStratum_E(TJSONObject Owner, string[] Stratum)
        {
            double result;
            //@ Unsupported property or method(C): 'Value'
            //@ Unsupported function or procedure: 'StrToFloatDef'
            result = StrToFloatDef(GetStratumJSONValue(Owner, Stratum).Value, 0.0);
            return result;
        }

        public static bool GetStratum_B(TJSONObject Owner, string[] Stratum)
        {
            bool result;
            TJSONValue mJSONValue;
            mJSONValue = GetStratumJSONValue(Owner, Stratum);
            //@ Undeclared identifier(3): 'TJSONFalse'
            if (mJSONValue is TJSONFalse)
            {
                result = false;
            }
            //@ Undeclared identifier(3): 'TJSONTrue'
            else if (mJSONValue is TJSONTrue)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static DateTime GetStratum_D(TJSONObject Owner, string[] Stratum)
        {
            DateTime result;
            //@ Unsupported property or method(C): 'Value'
            //@ Unsupported function or procedure: 'StrToDateTimeDef'
            result = StrToDateTimeDef(GetStratumJSONValue(Owner, Stratum).Value, DateTime.Now);
            return result;
        }

        public static void SetValueToJSONValue(TJSONValue xObject, TJSONValue xValue)
        {
            if ((xObject != null))
            {
                //@ Unsupported property or method(C): 'Free'
                xObject.Free;
            }
            xObject = xValue;
        }

        public static void SetValueToJSONValue(TJSONValue xObject, string Value)
        {
            SetValueToJSONValue(xObject, StringToValue(Value));
        }

        public static void SetValueToJSONValue(TJSONValue xObject, int Value)
        {
            SetValueToJSONValue(xObject, IntToValue(Value));
        }

        public static void SetValueToJSONValue(TJSONValue xObject, double Value)
        {
            SetValueToJSONValue(xObject, DoubleToValue(Value));
        }

        public static void SetValueToJSONValue(TJSONValue xObject, bool Value)
        {
            SetValueToJSONValue(xObject, BooleanToValue(Value));
        }

        public static void SetValueToJSONValue(TJSONValue xObject, DateTime Value)
        {
            SetValueToJSONValue(xObject, DateTimeToValue(Value));
        }

        public static void SetVariantToJSONValue(TJSONValue xObject, object Value)
        {
            SetValueToJSONValue(xObject, VariantToValue(Value));
        }

        public static TJSONValue VariantToValue2(object Value)
        {
            TJSONValue result;
            //@ Unsupported function or procedure: 'VarType'
            switch(VarType(Value))
            {
                //@ Undeclared identifier(3): 'varOleStr'
                //@ Undeclared identifier(3): 'varString'
                //@ Undeclared identifier(3): 'varUString'
                case varOleStr:
                case varString:
                case varUString:
                    result = StringToValue("S:" + (Value as string));
                    break;
                //@ Undeclared identifier(3): 'varSmallInt'
                //@ Undeclared identifier(3): 'varInteger'
                //@ Undeclared identifier(3): 'varShortInt'
                //@ Undeclared identifier(3): 'varByte'
                //@ Undeclared identifier(3): 'varWord'
                //@ Undeclared identifier(3): 'varLongWord'
                //@ Undeclared identifier(3): 'varInt64'
                //@ Undeclared identifier(3): 'varUInt64'
                case varSmallInt:
                case varInteger:
                case varShortInt:
                case varByte:
                case varWord:
                case varLongWord:
                case varInt64:
                case varUInt64:
                    result = StringToValue("I:" + (Value as string));
                    break;
                //@ Undeclared identifier(3): 'varSingle'
                //@ Undeclared identifier(3): 'varDouble'
                //@ Undeclared identifier(3): 'varCurrency'
                case 271:
                case 273:
                case varSingle:
                case varDouble:
                case varCurrency:
                    result = StringToValue("E:" + (Value as string));
                    break;
                // Extended(Value));
                //@ Undeclared identifier(3): 'varBoolean'
                case varBoolean:
                    result = StringToValue("B:" + (Value as string));
                    break;
                default:
                    throw new Exception("不是簡單型態");
                    break;
            }
            return result;
        }

        public static object Value2ToVariant(string Value)
        {
            object result;
            int i;
            string mStr;
            object mVar;
            mStr = Value.Substring(3 - 1 ,Value.Length);
            if (Value[1] == 'S')
            {
                mVar = (mStr as string);
            }
            else if (Value[1] == 'I')
            {
                mVar = Convert.ToInt32(mStr);
            }
            else if (Value[1] == 'E')
            {
                mVar = Convert.ToSingle(mStr);
            }
            else if (Value[1] == 'B')
            {
                if (mStr == "True")
                {
                    mVar = true;
                }
                else
                {
                    mVar = false;
                }
            }
            result = mVar;
            return result;
        }

    } // end LibJSON

}

