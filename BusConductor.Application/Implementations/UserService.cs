using System;
using BusConductor.Application.Contracts;
using BusConductor.Application.Requests;
using BusConductor.Data.Common;
using BusConductor.Domain.Common;
using BusConductor.Domain.Entities;
using BusConductor.Domain.RepositoryContracts;

namespace BusConductor.Application.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public void EnsureExists(string userName)
        {
            var actioningUser = _userRepository.GetByUsername("Application");
            var role = _roleRepository.GetById(new Guid("80fc2a10-d07e-4e06-9b91-4ba936e335ba"));
            var user = _userRepository.GetByUsername(userName);

            if (user == null)
            {
                var newUser = User.Create(userName, role, actioningUser);
                _userRepository.Save(newUser);
            }
        }
    }
}
