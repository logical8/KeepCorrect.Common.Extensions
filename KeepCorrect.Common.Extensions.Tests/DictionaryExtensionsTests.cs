using System.Collections.Generic;
using NUnit.Framework;

namespace KeepCorrect.Common.Extensions.Tests;

public class DictionaryExtensionsTests
{
    [TestCase(1, "first")]
    [TestCase(4, default(string))]
    public void GetValueOrDefault_IfKeyIsNotFound_ReturnsDefaultOfValue(int key, string expected)
    {
        //Arrange
        var dict = new Dictionary<int, string>
        {
            { 1, "first" },
            { 2, "second" },
            { 3, "third" }
        };
        //Act
        var result = dict.GetValueOrDefault(key);
        //Assert
        Assert.That(expected, Is.EqualTo(result));
    }
}