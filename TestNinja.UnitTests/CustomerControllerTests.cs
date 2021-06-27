using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IDIsZero_ReturnNotFound()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(0);

            // check for type equals to NotFound Exactly
            Assert.That(result, Is.TypeOf<NotFound>());

            // check for type equals to NotFound OR one of its DERIVATES
                //Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IDIsNotZero_ReturnOk()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(1);

            // check for type equals to Ok Exactly
            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
