﻿using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;
using System.Collections.Generic;

namespace NSubstituteLearn
{
    /// <summary>
    /// 参数匹配器
    /// </summary>
    [TestFixture]
    public class NUnitTest11
    {
        /// <summary>
        /// 忽略参数
        /// 通过使用 Arg.Any<T>() 可以忽略一个T类型的参数。
        /// </summary>
        [Test]
        public void Test_ArgumentMatchers_IgnoringArguments()
        {
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(Arg.Any<int>(), 5).Returns(7);

            Assert.AreEqual(7, calculator.Add(42, 5));
            Assert.AreEqual(7, calculator.Add(123, 5));
            Assert.AreNotEqual(7, calculator.Add(1, 7));
        }

        /// <summary>
        /// 通过Arg.Any<T>()方法来匹配任意的子类型
        /// </summary>
        [Test]
        public void Test_ArgumentMatchers_MatchSubTypes()
        {
            IFormatter formatter = Substitute.For<IFormatter>();

            formatter.Format(new object());
            formatter.Format("some string");

            formatter.Received().Format(Arg.Any<object>());
            formatter.Received().Format(Arg.Any<string>());
            formatter.DidNotReceive().Format(Arg.Any<int>());
        }

        /// <summary>
        /// 参数条件匹配
        /// 通过使用 Arg.Is<T>(Predicate<T> condition) 来对一个T类型的参数进行条件匹配。
        /// </summary>
        [Test]
        public void Test_ArgumentMatchers_ConditionallyMatching()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Add(1, -10);

            // 检查接收到第一个参数为1，第二个参数小于0的调用
            calculator.Received().Add(1, Arg.Is<int>(x => x < 0));
            // 检查接收到第一个参数为1，第二个参数为 -2、-5和-10中的某个数的调用
            calculator.Received().Add(1, Arg.Is<int>(x => (new List<int>() { -2, -5, -10 }).Contains(x)));

            // 检查未接收到第一个参数大于10，第二个参数为-10的调用
            calculator.DidNotReceive().Add(Arg.Is<int>(x => x > 10), -10);
        }

        /// <summary>
        /// 如果某参数的条件表达式抛出异常，则将假设该参数未被匹配，异常本身会被隐藏。
        /// </summary>
        [Test]
        public void Test_ArgumentMatchers_ConditionallyMatchingThrowException()
        {
            IFormatter formatter = Substitute.For<IFormatter>();

            formatter.Format(Arg.Is<string>(x => x.Length <= 10)).Returns("matched");

            Assert.AreEqual("matched", formatter.Format("short"));
            Assert.AreNotEqual("matched", formatter.Format("not matched, too long"));

            // 此处将不会匹配，因为在尝试访问 null 的 Length 属性时会抛出异常，
            // 而 NSubstitute 会假设其为不匹配并隐藏掉异常。
            Assert.AreNotEqual("matched", formatter.Format(null));
        }

        /// <summary>
        /// 匹配指定的参数 
        /// 使用 Arg.Is<T>(T value) 可以匹配指定的T类型参数。
        /// </summary>
        [Test]
        public void Test_ArgumentMatchers_MatchingSpecificArgument()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Add(0, 42);

            // 这里可能不工作，NSubstitute 在这种情况下无法确定在哪个参数上应用匹配器
            //calculator.Received().Add(0, Arg.Any<int>());
            calculator.Received().Add(Arg.Is(0), Arg.Any<int>());
        }
    }
}
