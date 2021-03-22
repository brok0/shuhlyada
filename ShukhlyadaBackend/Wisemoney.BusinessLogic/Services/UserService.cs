using Shukhlyada.BusinessLogic.Abstractions;
using Shukhlyada.BusinessLogic.Exstensions;
using Shukhlyada.Domain.Exceptions;
using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shukhlyada.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMailService _mailService;

        public UserService(IAccountRepository accountRepository, IMailService mailService)
        {
            _accountRepository = accountRepository;
            _mailService = mailService;
        }

        public async Task<Account> AuthenticateAsync(string email, string password)
        {
            var user = await _accountRepository.GetByEmailAsync(email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var hashedPassword = password.SHA2Hash(user.Salt);
            if (user.Password == hashedPassword)
            {
                return user;
            }

            throw new UserNotFoundException();

        }

        public async Task<Account> CreateAccountAsync(Account acc)
        {
            if ((await _accountRepository.GetByEmailAsync(acc.Email)) != null)
            {
                throw new UserAlreadyExistException();
            }

            var salt = hashExtension.GenerateSalt();

            acc.Password = acc.Password.SHA2Hash(salt);
            acc.Salt = salt;

            
            Account insertedAccount = _accountRepository.Insert(acc);

            await _accountRepository.UnitOfWork.SaveChangesAsync();

            return insertedAccount;
        }

        public async Task SendChangePasswordMailAsync(string email)
        {
            if ((await _accountRepository.GetByEmailAsync(email)) != null)
            {
                throw new UserNotFoundException();
            }
            string subject = "Recover Password";
            string body = "Recover Password";
            await _mailService.SendMailAsync(email, subject, body, false);
        }

    }
}
