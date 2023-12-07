using Microsoft.EntityFrameworkCore;
using RPSLS.Api.Data;

namespace RPSLS.Api.Services
{
    public class BaseService
    {

        protected RPSLSDbContext DbContext { get; }

        public BaseService(RPSLSDbContext context)
        {
            DbContext = context;
        }
    }
}
