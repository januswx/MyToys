using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;
using System.Collections.Generic;

namespace NSubstituteLearn
{
    /// <summary>
    /// 参数匹配器上的操作
    /// </summary>
    [TestFixture]
    public class NUnitTest17
    {
        /// <summary>
        /// 调用回调函数
        /// 除了指定调用，当替代实例接收到了匹配的调用时，参数匹配器还可以用于执行特定的操作，并指定操作参数。
        /// 这是一个相当罕见的需求，但在某些情况下可以使测试程序变得简单一些。
        /// 警告：一旦我们为替代实例添加有意义的行为，我们的测试和代码将面临过度指定和紧耦合的风险。
        /// 对于类似的测试，通过抽象来更好地封装行为可能是更好的选择，甚至可以使用真实的协作对象并切换至 coarser-grained 测试。
        /// 假设被测试类需要调用一个依赖对象的方法，并为其提供了一个回调函数，当依赖对象调用结束时通过回调来通知该类。
        /// 当替代实例被调用时，我们可以使用 Arg.Invoke() 来立即调用这个回调。
        /// 比如说我们要测试 OrderPlacedCommand，其需要使用 IOrderProcessor 来处理订单，
        /// 当处理成功地完成时使用 IEvents 来引发事件通知。
        /// 这里我们构造了 processor，用于处理 ID 为 3 的订单，并调用回调函数。
        /// 我们使用 Arg.Invoke(true) 来传递 true 给回调函数。
        /// Arg.Invoke 有几个重载方法，可以用于调用参数数量和类型不同的回调函数。
        /// 我们也可以使用 Arg.InvokeDelegate 来调用定制的委托类型（那些不只是简单的 Action 类型的委托）。
        /// </summary>
        [Test]
        public void Test_ActionsWithArgumentMatchers_InvokingCallbacks()
        {
            // Arrange
            var cart = Substitute.For<ICart>();
            var events = Substitute.For<IEvents>();
            var processor = Substitute.For<IOrderProcessor>();
            cart.OrderId = 3;
            // 设置 processor 当处理订单ID为3时，调用回调函数，参数为true
            processor.ProcessOrder(3, Arg.Invoke(true));

            // Act
            var command = new OrderPlacedCommand(processor, events);
            command.Execute(cart);

            // Assert
            events.Received().RaiseOrderProcessed(3);
        }

        /// <summary>
        /// 执行带参数的操作
        /// Multiply 方法的第一个参数可为任意值，第二个参数将被传递值一个 argumentUsed 变量。
        /// </summary>
        [Test]
        public void Test_ActionsWithArgumentMatchers_PerformingActionsWithArgs()
        {
            var calculator = Substitute.For<ICalculator>();

            var argumentUsed = 0;
            calculator.Multiply(Arg.Any<int>(), Arg.Do<int>(x => argumentUsed = x));

            calculator.Multiply(123, 42);

            Assert.AreEqual(42, argumentUsed);
        }

        /// <summary>
        /// 当我们想断言某参数的多个属性时，这个功能是非常有用的
        /// 当调用 Multiply 方法第二个参数是 10 时，不管第一个 int 类型的参数的值是多少，Arg.Do 都会将其添加到一个列表当中。
        /// </summary>
        [Test]
        public void Test_ActionsWithArgumentMatchers_PerformingActionsWithAnyArgs()
        {
            var calculator = Substitute.For<ICalculator>();

            var firstArgsBeingMultiplied = new List<int>();
            calculator.Multiply(Arg.Do<int>(x => firstArgsBeingMultiplied.Add(x)), 10);

            calculator.Multiply(2, 10);
            calculator.Multiply(5, 10);

            // 由于第二个参数不为10，所以不会被 Arg.Do 匹配
            calculator.Multiply(7, 4567);

            CollectionAssert.AreEqual(firstArgsBeingMultiplied, new[] { 2, 5 });
        }

        /// <summary>
        /// 参数操作和调用指定参数
        /// 就像 Arg.Any<T>() 参数匹配器一样，当参数的类型为 T 时，
        /// 参数操作会调用一个指定操作（所以也可以用于设置返回值和检查接收到的调用）。
        /// 它只是多了个能与匹配指定规格的调用的参数交互的功能。
        /// </summary>
        [Test]
        public void Test_ActionsWithArgumentMatchers_ArgActionsCallSpec()
        {
            var calculator = Substitute.For<ICalculator>();

            var numberOfCallsWhereFirstArgIsLessThan0 = 0;

            // 指定调用参数：
            // 第一个参数小于0
            // 第二个参数可以为任意的int类型值
            // 当此满足此规格时，为计数器加1。
            calculator
              .Multiply(
                Arg.Is<int>(x => x < 0),
                Arg.Do<int>(x => numberOfCallsWhereFirstArgIsLessThan0++)
              ).Returns(123);

            var results = new[] {
                             calculator.Multiply(-4, 3),
                             calculator.Multiply(-27, 88),
                             calculator.Multiply(-7, 8),
                             calculator.Multiply(123, 2) // 第一个参数大于0，所以不会被匹配
                           };

            Assert.AreEqual(3, numberOfCallsWhereFirstArgIsLessThan0); // 4个调用中有3个匹配上
            CollectionAssert.AreEqual(results, new[] { 123, 123, 123, 0 }); // 最后一个未匹配
        }
    }
}
