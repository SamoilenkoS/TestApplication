using System;

namespace DataAccessLayer
{
    public class WeatherForecastDTO
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int Temperature { get; set; }

        public string Summary { get; set; }
    }
}
