// Подключаем библиотеку для работы с JSON
using Newtonsoft.Json;
//Класс для ответа с API
public class CurrencyResponse
{
    public Quotes quotes { get; set; }
}
//Под-класс для ответа с API
public class Quotes
{
    public double RUBUSD { get; set; }
    public double RUBEUR { get; set; }
    public double RUBGBP { get; set; }
}
// Веселуха
class Currency
{
    static HttpClient httpClient = new HttpClient();
    // Выполняем асихронный запросик к API
    static async Task Main()
    {
        using var Request = new HttpRequestMessage(
            HttpMethod.Post,"http://apilayer.net/api/live?access_key=YOURKEYHERE&currencies=USD,EUR,GBP&source=RUB&format=1");
        using var Response = await httpClient.SendAsync(Request);
        string RespTest = await Response.Content.ReadAsStringAsync();
        
        // Десериализация JSON-ответа в объект CurrencyResponse
        CurrencyResponse response = JsonConvert.DeserializeObject<CurrencyResponse>(RespTest);

        // Извлечение значений "RUBUSD", "RUBEUR" и "RUBGBP"
        double rub2usd = response.quotes.RUBUSD;
        double rub2eur = response.quotes.RUBEUR;
        double rub2gbp = response.quotes.RUBGBP;

        // Считаем курс 1 иностранной валюты к рублю
        double usd = 1 / rub2usd;
        double eur = 1 / rub2eur;
        double gbp = 1 / rub2gbp;
        // Вывод курса 1 к рублю
        Console.WriteLine($"USD to RUB: {Math.Round(usd, 2)}");
        Console.WriteLine($"EUR to RUB: {Math.Round(eur, 2)}");
        Console.WriteLine($"GBP to RUB: {Math.Round(gbp, 2)}");
    }
}
