using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;

namespace NSubstituteLearn
{
    /// <summary>
    /// 02.创建替代实例
    /// 类的替代可能会有一些不太好的副作用。
    /// NSubstitute 只能作用于类中的虚拟成员，
    /// 所以类中的任何非虚成员代码将都会被真实的执行。
    /// 如果你尝试替代一个类，
    /// 而该类会在其构造函数或某个非虚属性中格式化硬盘，
    /// 那么你就是自讨苦吃了。
    /// 如果可能的话，请坚持只替代接口类型
    /// var someClass = Substitute.For<SomeClassWithCtorArgs>(5, "hello world");
    /// </summary>
    [TestFixture]
    public class NUnitTest2
    {
        /// <summary>
        /// 有些时候，你可能需要为多个类型创建替代实例。
        /// 一个最好的例子就是，当你有代码使用了某类型后，
        /// 需要检查是否其实现了 IDisposable 接口，
        /// 并且确认是否调用了 Dispose 进行类型销毁
        /// </summary>
        [Test]
        public void Test_CreatingSubstitute_MultipleInterfaces()
        {
            var command = Substitute.For<ICommand, IDisposable>();

            var runner = new CommandRunner(command);

            runner.RunCommand();

            command.Received().Execute();

            ((IDisposable)command).Received().Dispose();
        }

        /// <summary>
        /// 一个类最多只能实现一个类。
        /// 如果你愿意的话，你可以指定多个接口，但是其中只能有一个是类类型。
        /// 为多个类型创建替代实例的最灵活的方式是使用重载。
        /// </summary>
        [Test]
        public void Test_CreatingSubstitute_SpecifiedOneClassType()
        {
            var substitute = Substitute.For(new[] { typeof(ICommand), typeof(IDisposable), typeof(SomeClassWithCtorArgs) }, new object[] { 5, "hello world" });

            Assert.IsInstanceOf<ICommand>(substitute);
            Assert.IsInstanceOf<IDisposable>(substitute);
            Assert.IsInstanceOf<SomeClassWithCtorArgs>(substitute);
        }

        /// <summary>
        /// 通过使用 Substiute.For<T>() 语法，NSubstitute 可以为委托类型创建替代。
        /// 当为委托类型创建替代时，将无法使该替代实例实现额外的接口或类。
        /// </summary>
        [Test]
        public void Test_CreatingSubstitute_ForDelegate()
        {
            var func = Substitute.For<Func<string>>();
            func().Returns("hello");

            Assert.AreEqual("hello", func());
        }
    }
}
