using System ;
using System.ComponentModel.DataAnnotations;
namespace Api.Dtos ; 

public class LoginDto
{

    // Added email address data annotation so if invalid email address is given it does not 
    // have to go to my db just return invalid instantly 
    [Required]
    [EmailAddress]
    public string Email{get  ; set ; } = "" ; 

    [Required]
    public string Password{get ; set ;} = "" ; 
    
}