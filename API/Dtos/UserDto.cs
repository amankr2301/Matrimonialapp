namespace API.Dtos ; 

public class UserDto
{
    public required string Email {get ; set ; }

    public required string Id {get ; set ; }

    public required string DisplayName{get ; set; }

    public string? imageUrl{get ; set ;}

    public required string token{get ; set ; }


}