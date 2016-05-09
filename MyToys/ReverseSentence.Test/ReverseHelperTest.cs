using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReverseSentence.Test
{
    [TestClass]
    public class ReverseHelperTest
    {
        [TestMethod]
        public void ReverseWordsTest()
        {
            string input = "how, are, you!";
            string output = ReverseHelper.ReverseWords(input);

            Assert.AreEqual("!you ,are ,how", output);
        }
    }
}
