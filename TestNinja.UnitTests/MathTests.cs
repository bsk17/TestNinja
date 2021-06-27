using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        // The function with SetUp attribute gets called before each test function gets executed
        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        // [Ignore("Any Message can be written here to justify why we are ignoring this test")]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            // The Arrange part is being taken care by the [SetUp]

            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));

        }

        [Test]
        [TestCase(2, 1, 2)] // when first argument is greater
        [TestCase(1, 2, 2)] // when second argument is greater
        [TestCase(1, 1, 1)] // when both are equal
        public void Max_WhenCalled_ReturnGreaterArgument(int a, int b, int expectedResult)
        {
            // The Arrange part is being taken care by the [SetUp]

            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUptoLimit()
        {
            // as we have 5 as limit therefore we know that only 3 numbers should be in the array
            var result = _math.GetOddNumbers(5);

            // DEPENDING UPON REQUIREMENT WE CAN USE ANY OF THE FOLLOWING OR COMBINED

            // most general check 
                //Assert.That(result, Is.Not.Empty);

            // a bit general check
                //Assert.That(result.Count(), Is.EqualTo(3));

            //specific check
                //Assert.That(result, Does.Contain(1));
                //Assert.That(result, Does.Contain(3));
                //Assert.That(result, Does.Contain(5));

            // shorter way of specific check 
            Assert.That(result, Is.EquivalentTo(new [] { 1,3,5}));
        }
    }
}
