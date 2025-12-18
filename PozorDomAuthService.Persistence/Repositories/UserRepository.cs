using Microsoft.EntityFrameworkCore;
using Npgsql;
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
            catch (PostgresException ex) when (ex.IsUniqueKeyViolation("IX_Users_PhoneNumber"))
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

        public async Task<int> UpdatePhoneNumberAsync(Guid id, string phoneNumber)
        {
            try
            {
                return await _context.Users
                    .Where(u => u.Id == id)
                    .ExecuteUpdateAsync(u => u
                        .SetProperty(user => user.PhoneNumber, phoneNumber));
            }
            catch (PostgresException ex) when (ex.IsUniqueKeyViolation("IX_Users_PhoneNumber"))
            {
                throw new ConflictException("User with this phone number already exists.");
            }
        }

        public async Task<int> UpdateInfoAsync(Guid id, string fullName)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(user => user.FullName, fullName));
        }

        public async Task<int> UpdateEmailAsync(Guid id, string email)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(user => user.Email, email));
        }

        public async Task<int> UpdateImageUrlAsync(Guid id, string imageUrl)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(u => u
                    .SetProperty(user => user.ImageUrl, imageUrl));
        }
    }
}
