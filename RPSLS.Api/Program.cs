
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RPSLS.Api.Data;
using RPSLS.Api.Hubs;
using RPSLS.Api.Interfaces;
using RPSLS.Api.Repositories;
using RPSLS.Api.Services;
using System.Text.Json.Serialization;

namespace RPSLS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSignalR();

            builder.Services.AddTransient<IRoomService, RoomService>();
            builder.Services.AddTransient<IGameService, GameService>();

            builder.Services.AddTransient<IRoomRepository, RoomRepository>();
            builder.Services.AddTransient<IGameRepository, GameRepository>();

            builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder => {
                builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
            }));

            builder.Services.AddDbContext<RPSLSDbContext>(o => {
                o.UseInMemoryDatabase(databaseName: "RPSLS", b => b.EnableNullChecks(false));
            });

            var app = builder.Build();

            if(app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseAuthorization();

            app.MapControllers();

            app.UseCors("CorsPolicy");

            app.MapHub<GameHub>("/gameHub");

            app.Run();
        }
    }
}
