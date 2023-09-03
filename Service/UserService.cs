using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Contract;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        private User? _user;
        public async Task<IEnumerable<UserIdentitiesDto>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var usersDto = _mapper.Map<IEnumerable<UserIdentitiesDto>>(users);

            return usersDto;
        }
    }
}
