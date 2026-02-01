using System ;
using System.Security.Cryptography;
using System.Text;
using Api.Dtos;
using API.Data ;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AccountController(AppDbContext context , ITokenService tokenService) : BaseApiController
    {                 

        
        [HttpPost("register")]    // /api/account/register 
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerdto)
        {

            if (await Emailexist(registerdto.Email))
            {
                return BadRequest("Email already exist") ; 
            }

            
            using var hmac = new HMACSHA512() ; 

            var user = new AppUser
            {
                DisplayName = registerdto.DisplayName, 
                Email = registerdto.Email, 
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerdto.password)) ,
                passwordSalt = hmac.Key

            } ;

            context.Add(user) ; 
            await context.SaveChangesAsync() ; 

            return user.ToDto(tokenService) ; 


        }

        private async Task<bool> Emailexist(string email)
        {
            return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower()) ; 
        }
        

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> login(LoginDto logindto)
        {
            var user = await context.Users.SingleOrDefaultAsync(x=> x.Email == logindto.Email) ; 

            if (user == null)
            {
                return Unauthorized("Invalid email address") ; 
            }

            using var hmac = new HMACSHA512(user.passwordSalt) ;

            var computed_hash  = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password)) ; 

            for (int i = 0 ; i < computed_hash.Length ; ++i)
            {
                if(computed_hash[i] != user.passwordHash[i])
                {
                    return Unauthorized("Wrong password enterd") ; 
                }
            }

            return user.ToDto(tokenService) ; 


        }





        
    }
}
