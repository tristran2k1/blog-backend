using backend.Models;

namespace backend.Interfaces
{
    public interface IToken
    {
        string CreateToken(Users user);
    }
}
