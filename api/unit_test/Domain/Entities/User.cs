using NUnit.Framework;
using api.Domain.Entities;

namespace unit_test.Domain.Entities
{
    public class WelcomeTest
    {
        [Test]
        public void Instance()
        {
            var user = new User();
            Assert.AreEqual(user.Id, 0);
            Assert.AreEqual(user.Name, null);
            Assert.AreEqual(user.Email, null);
            Assert.AreEqual(user.Password, null);
            Assert.AreEqual(user.Role, null);
        }
    }
}
