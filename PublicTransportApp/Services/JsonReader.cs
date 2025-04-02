using PublicTransportApp.Models;
using PublicTransportApp.Models.Stops;
using System.Text.Json;

namespace PublicTransportApp.Services
{
   
    public class JsonReader
    {
        private readonly  string _filePath = "wwwroot/veriler.json";
        public List<Stop> ReadStops()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException("JSON dosyası bulunamadı.");

            string jsonString = File.ReadAllText(_filePath);
            var jsonDocument = JsonDocument.Parse(jsonString);
            var root = jsonDocument.RootElement;

            // JSON içindeki 'duraklar' kısmını al
            var stopsJson = root.GetProperty("duraklar").GetRawText();

            // JSON'u Stop nesnelerine dönüştür
            return JsonSerializer.Deserialize<List<Stop>>(stopsJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                AllowTrailingCommas = true });
        }
        //public PublicTransportData ReadData()
        //{
        //    if (!File.Exists(_filePath))
        //        throw new FileNotFoundException("JSON dosyası bulunamadı.");

        //    string jsonString = File.ReadAllText(_filePath);
        //    return JsonSerializer.Deserialize<PublicTransportData>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //}
    }

}
