namespace API.Extensions ; 
using API.Entities ; 
using API.Interfaces ; 
using API.Dtos ; 


public static class AppUserExtensions
{
    public static UserDto ToDto(this AppUser user , ITokenService tokenService){
         return new UserDto
            {
                Id = user.Id ,
                Email = user.Email, 
                DisplayName = user.DisplayName , 
                token = tokenService.CreateToken(user)

            } ; 

        
    }
    
}

