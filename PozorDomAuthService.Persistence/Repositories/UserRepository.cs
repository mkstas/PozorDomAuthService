using Microsoft.EntityFrameworkCore;
using Npgsql;
using PozorDomAuthService.Domain.Interfaces.Repositories;
using PozorDomAuthService.Domain.Models;
using PozorDomAuthService.Domain.Shared.Exceptions;
using PozorDomAuthService.Domain.ValueObjects;
using PozorDomAuthService.Persistence.Extensions;

namespace PozorDomAuthService.Persistence.Repositories
{
    public class UserRepository(PozorDomAuthServiceDbContext context) : IUserRepository
    {
        private readonly PozorDomAuthServiceDbContext _context = context;

        public async Task CreateUserAync(string email, string passwordHash)
        {
            await _context.Users.AddAsync(
                User.Create(
                    EmailAddress.Create(email),
                    PasswordHash.Create(passwordHash)
                ));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueCreateConstraintViolation("IX_users_email"))
            {
                throw new ConflictException("User with this email address already exists.");
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Address == email);
        }

        public async Task<User?> GetUserByIdAsync(UserId id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateEmailAsync(UserId id, string email)
        {
            var user = await _context.Users.FindAsync(id) 
                ?? throw new NotFoundException("User with this email address does not exist.");

            try
            {
                user.ChangeEmailAddress(EmailAddress.Create(email));
                await _context.SaveChangesAsync();
            }
            catch (PostgresException ex) when (ex.IsUniqueUpdateKeyViolation("IX_users_email"))
            {
                throw new ConflictException("User with this email address already exists.");
            }
        }

        public async Task UpdatePasswordAsync(UserId id, string passwordHash)
        {
            var user = await _context.Users.FindAsync(id)
                ?? throw new NotFoundException("User with this email address does not exist.");

            user.ChangePasswordHash(PasswordHash.Create(passwordHash));
            await _context.SaveChangesAsync();
        }
    }
}
