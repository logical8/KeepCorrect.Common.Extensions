using System;
using System.Linq;

namespace KeepCorrect.Common.Extensions
{
    public static class EnumConverter
    {
        /// <summary>
        /// Convert uint value to Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumAsUInt"></param>
        /// <exception cref="NotSupportedException">T has not such int value</exception>
        /// <returns>Enum type of T</returns>
        public static T ToEnum<T>(this uint enumAsUInt) where T : struct, Enum
        {
            var enumType = typeof(T);

            var value = (Enum)Enum.ToObject(enumType, enumAsUInt);
            if (Enum.IsDefined(enumType, value) == false)
            {
                throw new NotSupportedException($"Unable to convert value {enumAsUInt} to the type: {enumType}");
            }

            return (T)value;
        }
        
        /// <summary>
        /// Convert string value to Enum. If failure returns defaultValue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct, Enum
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            return Enum.TryParse<T>(value, true, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Convert string value to Enum
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="NotSupportedException">T has not such string value</exception>
        /// <returns></returns>
        public static T ToEnum<T>(this string value) where T : struct, Enum
        {
            return Enum.TryParse<T>(value, true, out var result)
                ? result
                : throw new NotSupportedException($"Unable to convert value {value} to the type: {typeof(T)}");
        }
        
        /// <summary>
        /// Convert Enum to another Enum by string values
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="TSourceEnum"></typeparam>
        /// <typeparam name="TDestEnum"></typeparam>
        /// <returns></returns>
        public static TDestEnum ToEnumByString<TSourceEnum, TDestEnum>(this TSourceEnum value)
            where TSourceEnum : struct, Enum
            where TDestEnum : struct, Enum
        {
            return value.ToString().ToEnum<TDestEnum>();
        }
        
        /// <summary>
        /// Convert Enum to another Enum by int values
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="TSourceEnum"></typeparam>
        /// <typeparam name="TDestEnum"></typeparam>
        /// <returns></returns>
        public static TDestEnum ToEnumByUInt<TSourceEnum, TDestEnum>(this TSourceEnum value)
            where TSourceEnum : struct, Enum
            where TDestEnum : struct, Enum
        {
            var sourceType = Enum.GetUnderlyingType(typeof(TSourceEnum));
            var destype = Enum.GetUnderlyingType(typeof(TDestEnum));

            if (new[] { sourceType, destype }.All(x => x == typeof(uint)))
                return ToEnum<TDestEnum>(Convert.ToUInt32(value));
            throw new ArgumentException("Argument has different underlying type (not uint)");
        }
    }
}