using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;
using System.Collections.Generic;
using System.ComponentModel;

namespace NSubstituteLearn
{
    /// <summary>
    /// 引发事件
    /// </summary>
    [TestFixture]
    public class UNuitTest14
    {
        /// <summary>
        /// 在 .NET 中，事件是非常有意思的功能，因为你不能像操作其他成员似地进行传递。
        /// 相反地，你只能添加或移除事件处理器，NSubstitute 正是使用了这个添加事件处理器的语法来引发事件。
        /// </summary>
        [Test]
        public void Test_RaisingEvents_RaiseEvent()
        {
            var engine = Substitute.For<IEngine>();

            var wasCalled = false;
            engine.Idling += (sender, args) => wasCalled = true;

            // 告诉替代实例引发异常，并携带指定的sender和事件参数
            engine.Idling += Raise.EventWith(new object(), new EventArgs());

            Assert.IsTrue(wasCalled);
        }

        /// <summary>
        /// 在上面的例子中，我们不能真实地了解到我们所引发事件的发送者和参数，仅是知道它被调用了。
        /// 在这种条件下，NSubstitute 通过为我们的事件处理器创建所需的参数，来使该操作更便捷些。
        /// </summary>
        [Test]
        public void Test_RaisingEvents_RaiseEventButNoMindSenderAndArgs()
        {
            var engine = Substitute.For<IEngine>();

            var wasCalled = false;
            engine.Idling += (sender, args) => wasCalled = true;

            engine.Idling += Raise.Event();

            Assert.IsTrue(wasCalled);
        }

        /// <summary>
        /// 当参数没有默认构造函数时引发事件
        /// NSubstitute 不总是能够创建事件参数。
        /// 如果事件的参数没有默认的构造函数，你可能不得不使用 Raise.EventWith<TEventArgs>(...) 来创建事件参数，
        /// 例如下面例子中的 LowFuelWarning 事件。如果没有提供事件的发送者，则 NSubstitute 会创建。
        /// </summary>
        [Test]
        public void Test_RaisingEvents_ArgsDoNotHaveDefaultCtor()
        {
            var engine = Substitute.For<IEngine>();

            int numberOfEvents = 0;
            engine.LowFuelWarning += (sender, args) => numberOfEvents++;

            // 发送事件，并携带指定的事件参数，未指定发送者
            engine.LowFuelWarning += Raise.EventWith(new LowFuelWarningEventArgs(10));

            // 发送事件，并携带指定的事件参数，并指定发送者
            engine.LowFuelWarning += Raise.EventWith(new object(), new LowFuelWarningEventArgs(10));

            Assert.AreEqual(2, numberOfEvents);
        }

        /// <summary>
        /// 引发Delegate事件
        /// 有时事件会通过委托来声明，而没有继承自 EventHandler<> 或 EventHandler。
        /// 这种事件可以通过使用 Raise.Event<TypeOfEventHandlerDelegate>(arguments)来引发。
        /// NSubstitute 会尝试和猜测该委托所需的参数，但如果没成功，NSubstitute 会告诉你具体需要提供哪个参数。
        /// 下面这个示例演示了引发 INotifyPropertyChanged 事件，该事件使用 PropertyChangedEventHandler 委托并需要2个参数。
        /// </summary>
        [Test]
        public void Test_RaisingEvents_RaisingDelegateEvents()
        {
            var sub = Substitute.For<INotifyPropertyChanged>();
            bool wasCalled = false;

            sub.PropertyChanged += (sender, args) => wasCalled = true;
            sub.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(this, new PropertyChangedEventArgs("test"));

            Assert.IsTrue(wasCalled);
        }

        /// <summary>
        /// 引发Action事件
        /// 在 IEngine 示例中，RevvedAt 事件被声明为 Action<int>。
        /// 这是委托事件的另外一种形式，我们可以使用 Raise.Event<Action<int>>() 来引发该事件。
        /// </summary>
        [Test]
        public void Test_RaisingEvents_RaisingActionEvents()
        {
            var engine = Substitute.For<IEngine>();

            int revvedAt = 0;
            engine.RevvedAt += rpm => revvedAt = rpm;

            engine.RevvedAt += Raise.Event<Action<int>>(123);

            Assert.AreEqual(123, revvedAt);
        }
    }
}
