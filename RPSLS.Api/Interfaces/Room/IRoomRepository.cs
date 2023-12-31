﻿using RPSLS.Api.Data.Enums;
using RPSLS.Api.Data.DTO;
using RPSLS.Api.Repositories;

namespace RPSLS.Api.Interfaces
{
    public interface IRoomRepository : IBaseRepository<Room>
    {
        List<Room> List(bool? publicRoom = null, bool showFull = false);
    }
}
