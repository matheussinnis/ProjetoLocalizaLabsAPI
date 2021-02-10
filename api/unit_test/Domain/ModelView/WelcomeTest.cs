using NUnit.Framework;
using api.Domain.ViewModel;

namespace unit_test.Domain.ViewModel
{
    public class WelcomeTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void Instance()
        {
            var welcome = new Welcome();
            Assert.AreEqual(welcome.Message, "Bem vindo a API");
            Assert.Pass(welcome.Documentation, "https://localhost:5001/swagger");
        }
    }
}
