using Microsoft.EntityFrameworkCore;
using RPSLS.Api.Data;

namespace RPSLS.Api.Services
{
    public class BaseService(RPSLSDbContext context)
    {
        protected RPSLSDbContext DbContext { get; } = context;
    }
}
