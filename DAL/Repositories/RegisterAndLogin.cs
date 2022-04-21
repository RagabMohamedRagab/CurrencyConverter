using BLL.Dtos;
using BOL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RegisterAndLogin : IRegisterAndLoginServices
    {
        private readonly UserManager<IdentityUser> _userManger;
        private readonly SignInManager<IdentityUser> _signIn;

        public RegisterAndLogin(UserManager<IdentityUser> userManger, SignInManager<IdentityUser> signIn)
        {
            _userManger = userManger;
            _signIn = signIn;
        }
        // Register
        #region
        public async Task<int> CreateAsync(RegisterDTO register)
        {
            if (register != null)
            {
                var admin = new IdentityUser
                {

                    UserName = register.Name,
                    Email = register.Email,
                    PasswordHash = register.Password,
                    PhoneNumber = register.Phone,
                };
                var result = await _userManger.CreateAsync(admin, register.Password);
                if (result.Succeeded)
                {
                    await _signIn.SignInAsync(admin, isPersistent: false);
                    return 1;
                }
            }
            return 0;
        }
        #endregion
        // Login
        #region
        public async Task<int> LoginAsync(LoginDto login)
        {
            try
            {
                var IsSigned = await _signIn.PasswordSignInAsync(login.UserName, login.Password, isPersistent: false, lockoutOnFailure: false);
                if (IsSigned.Succeeded)
                {
                    return 1;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return 0;
        }
        #endregion
    }
}




