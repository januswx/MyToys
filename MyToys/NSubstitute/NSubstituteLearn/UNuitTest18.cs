using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;
using System.Collections.Generic;

namespace NSubstituteLearn
{
    [TestFixture]
    public class UNuitTest18
    {
        /// <summary>
        /// 有时调用需要满足特定的顺序。就像已知的 "Temporal Coupling"，其取决于调用收到的时间。
        /// 理想情况下，我们可能会修改设计来移除这些耦合。但当不能移除时，凭借 NSubstitute 我们可以断言调用的顺序。
        /// </summary>
        [Test]
        public void Test_CheckingCallOrder_CommandRunWhileConnectionIsOpen()
        {
            var connection = Substitute.For<IConnection>();
            var command = Substitute.For<ICommand>();
            var subject = new Controller(connection, command);

            subject.DoStuff();

            Received.InOrder(() =>
            {
                connection.Open();
                command.Run(connection);
                connection.Close();
            });
        }

        /// <summary>
        /// 如果接收到调用的顺序不同，Received.InOrder 会抛出异常，并显示期待的结果和实际的调用结果。
        /// 我们也可以使用标准的参数匹配器来匹配调用，就像当我们需要检查单个调用时一样。
        /// </summary>
        [Test]
        public void Test_CheckingCallOrder_SubscribeToEventBeforeOpeningConnection()
        {
            var connection = Substitute.For<IConnection>();
            connection.SomethingHappened += () => { /* some event handler */ };
            connection.Open();

            Received.InOrder(() =>
            {
                connection.SomethingHappened += Arg.Any<Action>();
                connection.Open();
            });
        }
    }
}
