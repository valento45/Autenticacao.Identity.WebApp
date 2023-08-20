using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Identity.WebApp.Authorization
{
    public class UserManagerBase<TUser> : UserManager<TUser> where TUser : class
    {
        public UserManagerBase(IUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators, IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }


        public override Task<bool> CheckPasswordAsync(TUser user, string password)
        {
            if (user is MyUser usr)
                return Task.FromResult(usr.PasswordHash.Equals(password));
            else
                return base.CheckPasswordAsync(user, password);
        }
    }
}
