using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Enum;
using FourmBuilder.Common.Helper;
using FourmBuilder.Common.Mongo;

namespace FourmBuilder.Api.Core.Application.System
{
    public class SeedData
    {
        private readonly IMongoRepository<User> _userRepository;
        private readonly IMongoRepository<Role> _roleRepository;


        public SeedData(IMongoRepository<User> userRepository, IMongoRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task SeedAllAsync(CancellationToken cancellation)
        {
            #region SeedData

            var adminRole = new Role
            {
                Name = "Admin",
                Description = "System Admin Role",
                IsVital = true,
            };
            var user = new User
            {
                FirstName = "نیما",
                LastName = "نصرتی",
                Email = "nimanosrati93@gmail.com",
                Password = PasswordManagement.HashPass("nima1234!"),
                ActiveCode = Guid.NewGuid().ToString("N"),
                Mobile = "09107602786",
                IsEmailConfirm = true,
                IsMobileConfirm = true,
                RegisterDate = DateTime.Now,
                ModifiedDate = DateTime.Now.AddDays(2),
                ExpiredCode = DateTime.Now.AddDays(2),
                StudentNumber = "900477065",
                UserType = UserType.Student,
                Role = adminRole
            };

            if (!_roleRepository.GetAll().Any())
            {
                await _roleRepository.AddAsync(adminRole);
                

                await _roleRepository.AddAsync(new Role
                {
                    Name = "User",
                    Description = "System Admin Role",
                    IsVital = true,
                });
            }

            if (!_userRepository.GetAll().Any())
            {
                await _userRepository.AddAsync(user);
            }

            #endregion
        }
    }
}