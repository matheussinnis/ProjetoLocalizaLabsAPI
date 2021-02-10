using NUnit.Framework;
using api.Domain.Entities;
using api.Domain.UseCase.UserServices;
using System.Threading.Tasks;
using System;

namespace unit_test.Domain.UseCase.UserServices
{
    public class UserServiceTest
    {
        [Test]
        public async Task SaveUserEmailNotSaved()
        {
            var service = new UserService(new UserRepositoryNotExistedMail());
            var user = new User();
            user.Name = "Danilo";
            user.Email = "danilo@teste.com";
            user.Password = "123456";
            user.Role = UserRole.Administrador;

            Exception exception = null;
            try
            {
              await service.Save(user);        
            }
            catch (NotImplementedException ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task SaveUserEmailExisted()
        {
            var service = new UserService(new UserRepositoryExistedMail());
            var user = new User();
            user.Name = "Danilo";
            user.Email = "danilo@teste.com";
            user.Password = "123456";
            user.Role = UserRole.Administrador;

            Exception exception = null;
            try
            {
              await service.Save(user);        
            }
            catch (UserUniqMail ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task SaveUserExistedAndDuplacateEmail()
        {
            var service = new UserService(new UserRepositoryExistedMail());
            var user = new User();
            user.Id = 1;
            user.Name = "Danilo";
            user.Email = "danilo@teste.com";
            user.Password = "123456";
            user.Role = UserRole.Administrador;

            Exception exception = null;
            try
            {
              await service.Save(user);        
            }
            catch (UserUniqMail ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task DeleteEmptyId()
        {
            var service = new UserService(new UserRepositoryExistedMail());

            Exception exception = null;
            try
            {
              await service.Delete(0);
            }
            catch (UserEmptyId ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }

        [Test]
        public async Task DeleteUserNotFound()
        {
            var service = new UserService(new UserRepositoryUserNotFound());

            Exception exception = null;
            try
            {
              await service.Delete(1);
            }
            catch (UserNotFound ex)
            {
                exception = ex;
            }

            Assert.AreNotEqual(exception, null);
        }
    }
}
