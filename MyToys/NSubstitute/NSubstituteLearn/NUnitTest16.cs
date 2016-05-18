using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;
using System.Collections.Generic;

namespace NSubstituteLearn
{
    /// <summary>
    /// 设置out和ref参数
    /// </summary>
    [TestFixture]
    public class NUnitTest16
    {
        /// <summary>
        /// 我们可以配置其返回值，并设置第二个参数的输出
        /// </summary>
        [Test]
        public void Test_SetOutRefArgs_SetOutArg()
        {
            // Arrange
            var value = "";
            var lookup = Substitute.For<ILookup>();
            lookup
              .TryLookup("hello", out value)
              .Returns(x =>
              {
                  x[1] = "world!";
                  return true;
              });

            // Act
            var result = lookup.TryLookup("hello", out value);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(value, "world!");
        }
    }
}
