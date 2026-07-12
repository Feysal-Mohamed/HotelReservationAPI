using Microsoft.Extensions.Configuration;

namespace HotelReservationAPI.Helper
{
    public static class AppConfig
    {
        public static IConfiguration? Configuration { get; set; }

        public static string ConnectionString
        {
            get
            {
                if (Configuration == null)
                    throw new Exception("Configuration has not been initialized.");

                return Configuration.GetConnectionString("DefaultConnection")!;
            }
        }
    }
}