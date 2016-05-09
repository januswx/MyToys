using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;
using System.Collections.Generic;

namespace NSubstituteLearn
{
    /// <summary>
    /// 09.检查接收到的调用
    /// </summary>
    [TestFixture]
    public class NUnitTest9
    {
        /// <summary>
        /// 在某些情况下（尤其是对void方法），检测替代实例是否能成功接收到一个特定的调用是非常有用的。
        /// 可以通过使用 Received() 扩展方法，并紧跟着被检测的方法。
        /// 如果没有收到对 Execute() 的调用，则 NSubstitute 会抛出一个 ReceivedCallsException 异常，
        /// 并且会显示具体是在期待什么方法被调用，参数是什么，以及列出实际的方法调用和参数。
        /// </summary>
        [Test]
        public void Test_CheckReceivedCalls_CallReceived()
        {
            var command = Substitute.For<ICommand>();

            var something = new SomethingThatNeedsACommand(command);

            something.DoSomething();

            command.Received().Execute();
        }

        /// <summary>
        /// 通过使用 DidNotReceive() 扩展方法，NSubstitute 可以确定一个调用未被接收到。
        /// </summary>
        [Test]
        public void Test_CheckReceivedCalls_CallDidNotReceived()
        {
            var command = Substitute.For<ICommand>();
            var something = new SomethingThatNeedsACommand(command);

            something.DontDoAnything();

            command.DidNotReceive().Execute();
        }

        /// <summary>
        /// NSubstitute 也提供允许断言某调用是否接收到了指定的次数的选择，通过传递一个整型值给 Received() 方法。
        /// 如果替代实例没有接收到给定的次数，则将会抛出异常。接收到的次数少于或多于给定次数，断言会失败。
        /// Received(0) 的行为与 DidNotReceive() 相同
        /// </summary>
        [Test]
        public void Test_CheckReceivedCalls_CallReceivedNumberOfSpecifiedTimes()
        {
            var command = Substitute.For<ICommand>();

            var repeater = new CommandRepeater(command, 3);

            repeater.Execute();

            command.Received(3).Execute();
        }

        /// <summary>
        /// 可以使用参数匹配器来检查是否收到了或者未收到包含特定参数的调用。
        /// </summary>
        [Test]
        public void Test_CheckReceivedCalls_CallReceivedWithSpecificArguments()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Add(1, 2);
            calculator.Add(-100, 100);

            // 检查接收到了第一个参数为任意值，第二个参数为2的调用
            calculator.Received().Add(Arg.Any<int>(), 2);

            // 检查接收到了第一个参数小于0，第二个参数为100的调用
            calculator.Received().Add(Arg.Is<int>(x => x < 0), 100);

            // 检查未接收到第一个参数为任意值，第二个参数大于等于500的调用
            calculator.DidNotReceive().Add(Arg.Any<int>(), Arg.Is<int>(x => x >= 500));
        }

        /// <summary>
        /// NSubstitute 可以检查收到或者未收到调用，同时忽略其中包含的参数。
        /// 此时我们需要使用 ReceivedWithAnyArgs() 和 DidNotReceiveWithAnyArgs()
        /// </summary>
        [Test]
        public void Test_CheckReceivedCalls_IgnoringArguments()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Add(1, 3);

            calculator.ReceivedWithAnyArgs().Add(1, 1);
            calculator.DidNotReceiveWithAnyArgs().Subtract(0, 0);
        }

        /// <summary>
        /// 检查对属性的调用
        /// </summary>
        [Test]
        public void Test_CheckReceivedCalls_CheckingCallsToPropeties()
        {
            var calculator = Substitute.For<ICalculator>();

            var mode = calculator.Mode;
            calculator.Mode = "TEST";

            // 检查接收到了对属性 getter 的调用
            // 这里需要使用临时变量以通过编译
            var temp = calculator.Received().Mode;

            // 检查接收到了对属性 setter 的调用，参数为"TEST"
            calculator.Received().Mode = "TEST";
        }

        /// <summary>
        /// 索引器只是另外一个属性，所以我们可以使用相同的语法来检查索引器调用
        /// </summary>
        [Test]
        public void Test_CheckReceivedCalls_CheckingCallsToIndexers()
        {
            var dictionary = Substitute.For<IDictionary<string, int>>();
            dictionary["test"] = 1;

            dictionary.Received()["test"] = 1;
            dictionary.Received()["test"] = Arg.Is<int>(x => x < 5);
        }

        /// <summary>
        /// 检查事件订阅
        /// 与属性一样，我们通常更赞成测试所需的行为，而非检查对特定事件的订阅。
        /// 可以通过使用在替代实例上引发一个事件的方式，并且断言我们的类在响应中执行的正确的行为
        /// </summary>
        [Test]
        public void Test_CheckReceivedCalls_CheckingEventSubscriptions()
        {
            var command = Substitute.For<ICommand>();
            var watcher = new CommandWatcher(command);

            command.Executed += Raise.Event();

            Assert.IsTrue(watcher.DidStuff);
        }

        /// <summary>
        /// Received() 会帮助我们断言订阅是否被收到
        /// </summary>
        [Test]
        public void Test_CheckReceivedCalls_MakeSureWatcherSubscribesToCommandExecuted()
        {
            var command = Substitute.For<ICommand>();
            var watcher = new CommandWatcher(command);

            // 不推荐这种方法。
            // 更好的办法是测试行为而不是具体实现。
            command.Received().Executed += watcher.OnExecuted;

            command.Received().Executed += Arg.Any<EventHandler>();
        }
    }
}
