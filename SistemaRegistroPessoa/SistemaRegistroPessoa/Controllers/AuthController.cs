using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SistemaRegistroPessoa.Extensions;
using SistemaRegistroPessoa.Interfaces;
using SistemaRegistroPessoa.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaRegistroPessoa.Controllers
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public AuthController(INotifier notifier,
                              SignInManager<IdentityUser> signManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings) : base(notifier)
        {
            _signManager = signManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("account")]
        public async Task<ActionResult> RegisterUser(RegisterUser registerUser) 
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (result.Succeeded)
            {
                await _signManager.SignInAsync(user, false);
                return CustomResponse(GenerateToken());
            }
            foreach(var error in result.Errors)
            {
                NotifyError(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn(LoginUser loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);
            if (result.Succeeded)
            {
                return CustomResponse(GenerateToken());
            }
            if(result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }

            NotifyError("Usuario ou senha incorretos");
            return CustomResponse();
        }
        private string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidOn,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpireHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }
    }

}
