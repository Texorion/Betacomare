namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ogg per creare la WebApp e caricare i servizi
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}