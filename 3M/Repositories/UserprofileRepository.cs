using _3M.DataModels.Account;
using _3M.DbContexts;
using _3M.Dtos.Account;
using _3M.Repositories.Base;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.Repositories
{
    public class UserprofileRepository : IRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserprofileRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task RegisterUserProfileAsync(UserProfileDto userProfile)
        {
            if (userProfile == null)
                return;

            var current = await _dbContext.UserProfiles
                .FirstOrDefaultAsync(i => i.UserId == userProfile.UserId);

            if (current == null)
                await AddUserProfileAsync(userProfile);
            else
            {
                //current.FirstName = userProfile.FirstName;
                //current.LastName = userProfile.LastName;
                current.PostalCode = userProfile.PostalCode;
                current.Address = userProfile.Address;
                current.City = userProfile.City;
                current.Province = userProfile.Province;
                current.Tel = userProfile.Number;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddUserProfileAsync(UserProfileDto userProfile)
        {
            var profile = new UserProfile()
            {
                UserId = userProfile.UserId,
                LastName = userProfile.LastName,
                FirstName = userProfile.FirstName,
                Address = userProfile.Address,
                City = userProfile.City,
                PostalCode = userProfile.PostalCode,
                Province = userProfile.Province,
                Tel = userProfile.Number,
            };
            _dbContext.UserProfiles.Add(profile);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserProfileDto> GetUserProfileAsync(Guid userId)
        {
            var current = await _dbContext.UserProfiles
                .FirstOrDefaultAsync(i => i.UserId == userId);

            if (current == null)
                return null;

            return _mapper.Map<UserProfileDto>(current);
        }
    }
}
