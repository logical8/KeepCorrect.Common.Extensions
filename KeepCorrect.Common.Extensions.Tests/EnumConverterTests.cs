using System;
using NUnit.Framework;

namespace KeepCorrect.Common.Extensions.Tests
{
    public class EnumConverterTests
    {
        [TestCase(Enum1.One, Enum2.One)]
        [TestCase(Enum1.TWO, Enum2.Two)]
        [TestCase(Enum1.Three, Enum2.THREE)]
        [TestCase(Enum1.Four, Enum2.Four)]
        public void ToEnumByString_ByDefault_ReturnsExpectedResults(Enum1 enum1, Enum2 enum2)
        {
            var result  = enum1.ToEnumByString<Enum1, Enum2>();
            Assert.That(result, Is.EqualTo(enum2));
        }
        
        [TestCase(Enum1.Five, Enum2.Fix)]
        public void ToEnumByString_WhenStringValuesAreNotTheSame_ReturnsNotSupportedException(Enum1 enum1, Enum2 enum2)
        {
            Assert.Throws<NotSupportedException>(() => enum1.ToEnumByString<Enum1, Enum2>());
        }
        
        [TestCase(Enum1.One, Enum2.One)]
        [TestCase(Enum1.TWO, Enum2.Two)]
        [TestCase(Enum1.Three, Enum2.THREE)]
        [TestCase(Enum1.Five, Enum2.Fix)]
        public void ToEnumByInt_ByDefault_ReturnsExpectedResults(Enum1 enum1, Enum2 enum2)
        {
            var result  = enum1.ToEnumByInt<Enum1, Enum2>();
            Assert.That(result, Is.EqualTo(enum2));
        }
        
        [TestCase(Enum1.Four, Enum2.Four)]
        public void ToEnumByInt_WhenStringValuesAreNotTheSame_ReturnsNotSupportedException(Enum1 enum1, Enum2 enum2)
        {
            Assert.Throws<NotSupportedException>(() => enum1.ToEnumByInt<Enum1, Enum2>());
        }
        
        [TestCase("One", Enum2.One)]
        [TestCase("TWO", Enum2.Two)]
        [TestCase("Three", Enum2.THREE)]
        [TestCase("Four", Enum2.Four)]
        [TestCase("Five", Enum2.Default)]
        [TestCase("", Enum2.Default)]
        [TestCase(null, Enum2.Default)]
        public void ToEnum_WithDefaultValue_ReturnsExpectedResults(string stringValue, Enum2 enum2)
        {
            var result  = stringValue.ToEnum(Enum2.Default);
            Assert.That(result, Is.EqualTo(enum2));
        }
    }

    public enum Enum1
    {
        Default,
        One,
        TWO,
        Three,
        Four = 4,
        Five = 6
    }

    public enum Enum2
    {
        Default,
        One,
        Two,
        THREE,
        Four = 5,
        Fix = 6
    }
}