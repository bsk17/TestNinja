using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private HouseKeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private DateTime _statementDate = new DateTime(2017, 1, 1);
        private Housekeeper _houseKeeper;
        private string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _houseKeeper
            }.AsQueryable());

            _statementFileName = "fileName";
            
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
               .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
               .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HouseKeeperService(
                unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => 
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate)); 
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatements()
        {
            _houseKeeper.Email = null;

            _service.SendStatementEmails(_statementDate);

            // Times.Never helps us to check that the method is never called
            // we can also use this method to check how many times this method was called if required
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsWhiteSpace_ShouldNotGenerateStatements()
        {
            _houseKeeper.Email = " ";

            _service.SendStatementEmails(_statementDate);

            // Times.Never helps us to check that the method is never called
            // we can also use this method to check how many times this method was called if required
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsEmpty_ShouldNotGenerateStatements()
        {
            _houseKeeper.Email = "";

            _service.SendStatementEmails(_statementDate);

            // Times.Never helps us to check that the method is never called
            // we can also use this method to check how many times this method was called if required
            _statementGenerator.Verify(sg =>
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }


        [Test]
        public void SendStatementEmails_StatementFileNameIsNull_ShouldNotEmailTheStatement()
        {
            _statementFileName = null;

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsEmptyString_ShouldNotEmailTheStatement()
        {
            _statementFileName = "";

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileNameIsWhitespace_ShouldNotEmailTheStatement()
        {
            _statementFileName = " ";

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
                )).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            _messageBox.Verify(mb => mb.Show(
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                MessageBoxButtons.OK));
        }


        // HELPER METHODS

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<string>()), Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                            _houseKeeper.Email,
                            _houseKeeper.StatementEmailBody,
                            _statementFileName,
                            It.IsAny<string>()));
        }
    }
}
