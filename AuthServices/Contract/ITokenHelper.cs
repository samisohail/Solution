using DataTransferObjects;

namespace AuthServices.Contracts
{
    public interface ITokenHelper
    {
        string GenerateToken(UserDto user);
        int? ValidateToken(string token);
    }
}
