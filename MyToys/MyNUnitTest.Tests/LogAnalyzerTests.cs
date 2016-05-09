using System;
using NUnit.Framework;
using NSubstitute;

namespace MyNUnitTest.Tests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidFileName_BadExtension_ReturnsFalse()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("filewithbadextension.foo");
            Assert.AreEqual(false, result);
        }

        [Test]
        public void IsValidFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("filewithgoodextension.slf");
            Assert.AreEqual(true, result);
        }

        [Test]
        public void IsValidFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName("filewithgoodextension.SLF");
            Assert.AreEqual(true, result);
        }

        [TestCase("filewithgoodextension.slf")]
        [TestCase("filewithgoodextension.SLF")]
        public void IsValidFileName_ValidExtensions_ReturnsTrue(string fileName)
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer.IsValidLogFileName(fileName);
            Assert.AreEqual(true, result);
        }

        [Test]
        [Category("Exception")]
        public void IsValidFileName_EmptyName_Throws()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            // 使用Assert.Catch
            var ex = Assert.Catch<Exception>(() => analyzer.IsValidLogFileName(string.Empty));
            // 使用Assert.Catch返回的Exception对象
            StringAssert.Contains("filename has to be provided", ex.Message);
        }

        [TestCase("badfile.foo", false)]
        [TestCase("goodfile.slf", true)]
        public void IsValidFileName_WhenCalled_ChangesWasLastFileNameValid(string fileName, bool expected)
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            analyzer.IsValidLogFileName(fileName);
            Assert.AreEqual(expected, analyzer.WasLastFileNameValid);
        }

        [Test]
        public void Analyze_TooShortFileName_CallLogger()
        {
            // 创建模拟对象，用于测试结尾的断言
            ILogger logger = Substitute.For<ILogger>();
            LogAnalyzer analyzer = new LogAnalyzer(logger);
            analyzer.MinNameLength = 6;
            analyzer.Analyze("a.txt");

            // 使用NSub API设置预期字符串
            logger.Received().LogError("Filename too short : a.txt");
        }
    }
}