using RPSLS.Api.Data.DTO;
using RPSLS.Api.Data.Enums;

namespace RPSLS.Api.Interfaces
{
    public interface IPlayerService
    {
        Player CreatePlayer(string name);
    }
}
