using System;
using System.Linq;
using static System.Runtime.CompilerServices.Unsafe;

namespace KeepCorrect.Common.Extensions;

public static class EnumFlagsExtensions
{
    public static T CombineFlags<T>(this T current, T other) where T : Enum
    {
        var areFlags = typeof(T).GetCustomAttributes(typeof(FlagsAttribute), true).Any();
        if (!areFlags) throw new InvalidOperationException("You can only combine Enums with attribute \"Flags\".");
        var type = Enum.GetUnderlyingType(typeof(T));
        
        if (type == typeof(byte))
        {
            var result = (byte)(As<T, byte>(ref current) | As<T, byte>(ref other));
            return As<byte, T>(ref result);
        }

        if (type == typeof(ushort))
        {
            var result = (ushort)(As<T, ushort>(ref current) | As<T, ushort>(ref other));
            return As<ushort, T>(ref result);
        }

        if (type == typeof(uint))
        {
            var result = As<T, uint>(ref current) | As<T, uint>(ref other);
            return As<uint, T>(ref result);
        }
        else
        {
            var result = As<T, ulong>(ref current) | As<T, ulong>(ref other);
            return As<ulong, T>(ref result);
        }
    }
}