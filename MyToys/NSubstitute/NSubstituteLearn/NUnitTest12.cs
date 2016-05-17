using System;
using NUnit.Framework;
using NUnit.Common;
using NSubstitute;
using NSubstitute.Exceptions;
using System.Collections.Generic;

namespace NSubstituteLearn
{
    /// <summary>
    /// 使用回调函数和WhenDo语法
    /// </summary>
    [TestFixture]
    public class NUnitTest12
    {

        [Test]
        public void Test_CallbacksWhenDo_PassFunctionsToReturns()
        {
            var calculator = Substitute.For<ICalculator>();

            var counter = 0;

            calculator.Add(0, 0).ReturnsForAnyArgs(x => 0).AndDoes(x => counter++);

            calculator.Add(7, 3);
            calculator.Add(2, 2);
            calculator.Add(11, -3);

            Assert.AreEqual(counter, 3);
        }

        /// <summary>
        /// When..Do 使用两个调用来配置回调。
        /// 首先，调用替代实例的 When() 方法来传递一个函数。该函数的参数是替代实例自身，
        /// 然后此处我们可以调用我们需要的成员，即使该成员返回 void。
        /// 然后再调用 Do() 方法来传递一个回调，当替代实例的成员被调用时，执行这个回调。
        /// </summary>
        [Test]
        public void Test_CallbacksWhenDo_UseWhenDo()
        {
            var counter = 0;
            var foo = Substitute.For<IFoo>();

            foo.When(x => x.SayHello("World")).Do(x => counter++);

            foo.SayHello("World");
            foo.SayHello("World");

            Assert.AreEqual(2, counter);
        }

        /// <summary>
        /// 传递给 Do() 方法的参数中包含的调用信息与传递给 Returns() 回调的参数中的相同，
        /// 这些调用信息可以用于对参数进行访问。
        /// 注意，我们也可以对非 void 成员使用 When..Do 语法，
        /// 但是，通常来说更加推荐 Returns() 语法，因为其更加简洁明确。
        /// 你可能会发现，对于非 void 函数，当你想执行一个函数而不改变之前的返回值时，这个功能是非常有用的。
        /// 如果在某些地方，我们仅需要对一个特殊的参数创建回调，
        /// 则我们可能会使用为每个参数创建回调的方法，例如 Arg.Do() 和 Arg.Invoke()，而不是使用 When..Do。
        /// </summary>
        [Test]
        public void Test_CallbacksWhenDo_UseWhenDoOnNonVoid()
        {
            var calculator = Substitute.For<ICalculator>();

            var counter = 0;
            calculator.Add(1, 2).Returns(3);
            calculator.When(x => x.Add(Arg.Any<int>(), Arg.Any<int>())).Do(x => counter++);

            var result = calculator.Add(1, 2);
            Assert.AreEqual(3, result);
            Assert.AreEqual(1, counter);
        }
    }
}
