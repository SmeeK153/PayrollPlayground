using System.Collections.Generic;

namespace Infrastructure.Configurations
{
    public class CorsConfiguration
    {
        public List<string> AllowedOrigins { get; set; } = new List<string>();
    }
}