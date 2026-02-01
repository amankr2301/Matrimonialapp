namespace API.Services ;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Entities ; 
using API.Interfaces ; 
using Microsoft.IdentityModel.Tokens ; 


// configuration is telling to get TokenKey from my configuration file (appsetting.json)
public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("Token key not provided") ;  

        // this condition is for me (the developer purpose)
        if (tokenKey.Length < 64)
        {
            throw new Exception("Your token needs to be atleast 64 characters") ; 
        }
        // this key is what we are going to use to sign our token 
        // Converts our 'TokenKey' string into a mathematical "Stamp" used to seal the token.
        // Unlike password salts, this one key is shared by the whole server to keep things fast.
        // JWT logic is for "Verification" (Proof of Identity), not "Encryption" (Hiding secrets).
        // We use a signature to prove the server created this, rather than a database lookup.
        // Symmetric means the one we use to both encrypt and decrypt 

        var security_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)) ; 

        // claims is something that the user claims to be 
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email , user.Email), 
            new(ClaimTypes.NameIdentifier , user.Id) 

        }; 

        var creds = new SigningCredentials(security_key , SecurityAlgorithms.HmacSha512Signature) ;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), 
            Expires = DateTime.UtcNow.AddDays(7) , 
            SigningCredentials = creds 
        };  

        var tokenHandler = new JwtSecurityTokenHandler() ; 

        var token = tokenHandler.CreateToken(tokenDescriptor) ; 

        return tokenHandler.WriteToken(token) ; 


    }

    
}