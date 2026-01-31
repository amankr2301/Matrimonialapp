using System ;
using System.ComponentModel.DataAnnotations;
namespace Api.Dtos ; 

public class RegisterDto
{

    // data annotation is required as adding required with property is for my code but has nothing 
    // to do with my web , it can still accept blank 
    [Required]

    // ="" this initialisation is for c# as it does not understand my annotations
    public string DisplayName {get ; set ;} = "" ;

    [Required]
    [EmailAddress]
    public string Email {get ; set ; } = "" ; 

    [Required]
    [MinLength(4)]
        public string password {get ; set ;} = "" ; 

}
