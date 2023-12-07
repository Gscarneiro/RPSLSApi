using RPSLS.Api.Data.Enums;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Repositories;

namespace RPSLS.Api.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        List<Game> List(Guid roomId);
    }
}
