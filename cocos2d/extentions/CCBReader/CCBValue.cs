using System.Diagnostics;

namespace cocos2d
{
    internal class ccColor3BWapper : CCObject
    {
        private ccColor3B color;

        public static ccColor3BWapper Create(ccColor3B color)
        {
            var ret = new ccColor3BWapper();
            ret.color.r = color.r;
            ret.color.g = color.g;
            ret.color.b = color.b;
            return ret;
        }

        public ccColor3B getColor()
        {
            return color;
        }
    };

    internal enum ValueType
    {
        kIntValue,
        kFloatValue,
        kPointerValue,
        kBoolValue,
        kUnsignedCharValue,
    }

    internal class CCBValue : CCObject
    {
        private float fValue;
        private ValueType mType;
        private int nValue;
        private byte[] pointer;

        public static CCBValue Create(int nValue)
        {
            var ret = new CCBValue();
            ret.nValue = nValue;
            ret.mType = ValueType.kIntValue;
            return ret;
        }

        public static CCBValue Create(bool bValue)
        {
            var ret = new CCBValue();
            ret.nValue = bValue ? 1 : 0;
            ret.mType = ValueType.kBoolValue;
            return ret;
        }

        public static CCBValue Create(float fValue)
        {
            var ret = new CCBValue();
            ret.fValue = fValue;
            ret.mType = ValueType.kFloatValue;
            return ret;
        }

        public static CCBValue Create(byte bValue)
        {
            var ret = new CCBValue();
            ret.nValue = bValue;
            ret.mType = ValueType.kUnsignedCharValue;
            return ret;
        }

        public static CCBValue Create(byte[] pointer)
        {
            var ret = new CCBValue();
            ret.pointer = pointer;
            ret.mType = ValueType.kPointerValue;
            return ret;
        }

        public int getIntValue()
        {
            Debug.Assert(mType == ValueType.kIntValue);
            return nValue;
        }

        public float getFloatValue()
        {
            Debug.Assert(mType == ValueType.kFloatValue);
            return fValue;
        }

        public bool getBoolValue()
        {
            Debug.Assert(mType == ValueType.kBoolValue);
            return nValue == 1;
        }

        public byte getByteValue()
        {
            Debug.Assert(mType == ValueType.kUnsignedCharValue);
            return (byte) nValue;
        }

        public byte[] getPointer()
        {
            Debug.Assert(mType == ValueType.kPointerValue);
            return pointer;
        }
    }
}