using Microsoft.EntityFrameworkCore;
using PozorDomAuthService.Domain.Entities;
using PozorDomAuthService.Domain.Interfaces.Repositories;
using PozorDomAuthService.Infrastructure.Exceptions;
using PozorDomAuthService.Persistence.Extensions;

namespace PozorDomAuthService.Persistence.Repositories
{
    public class UserRepository(PozorDomAuthServiceDbContext context) : IUserRepository
    {
        private readonly PozorDomAuthServiceDbContext _context = context;

        public async Task CreateAsync(string phoneNumber)
        {
            await _context.Users.AddAsync(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = phoneNumber,
                });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueKeyViolation("IX_Users_PhoneNumber"))
            {
                throw new ConflictException("User with this phone number already exists.");
            }
        }

        public async Task<UserEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<int> UpdateAsync(Guid id, string fullName, string imageUrl)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(user => user.FullName, fullName)
                    .SetProperty(user => user.ImageUrl, imageUrl));
        }
    }
}
