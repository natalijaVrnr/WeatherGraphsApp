namespace WeatherGraphsApp_MVC.Server.Data
{
    public class WeatherData : IWeatherData
    {
        private readonly AppDbContext _dbContext;

        public WeatherData(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WeatherModel>> GetAllWeatherRecords()
        {
            return await _dbContext.Weather.ToListAsync();
        }

        public async Task<List<WeatherModel>> GetWeatherRecordsByDate(DateTime date)
        {
            var output = await GetAllWeatherRecords();

            return output
                .Where(w => w.CreatedDateTime.Date == date)
                .OrderBy(w => w.City)
                .ToList();
        }

        public async Task CreateWeatherRecord(WeatherModel w)
        {
            _dbContext.Weather.Add(w);
            await _dbContext.SaveChangesAsync();
        }
    }
}
