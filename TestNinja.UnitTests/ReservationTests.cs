using NUnit.Framework;
using System;
using TestNinja.Fundamentals;



// Refer to TestNinja -> Fundamentals -> Reservation.cs
namespace TestNinja.UnitTests
{
    // Naming convention for classes :- ClassNameTests
    [TestFixture]
    public class ReservationTests
    {
        // Naming convention for methods :- function name_scenario_expectedResult
        [Test]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            var user = new User();
            var reservation = new Reservation {  MadeBy = user};

            var result = reservation.CanBeCancelledBy(user);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            var reservation = new Reservation { MadeBy = new User() };

            var result = reservation.CanBeCancelledBy(new User());

            Assert.That(result, Is.False);
        }
    }
}
