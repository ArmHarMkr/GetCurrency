using System.Xml.Linq;

Console.WriteLine("Choose currency Valute: USD..");
string currencyCode = Console.ReadLine();

string url = $"https://www.cbr.ru/scripts/XML_daily.asp";

using HttpClient client = new HttpClient();

HttpResponseMessage response = await client.GetAsync(url);

if (response.IsSuccessStatusCode)
{
    string xmlString = await response.Content.ReadAsStringAsync();

    XDocument doc = XDocument.Parse(xmlString);

    var valutes = doc.Root.Elements("Valute");

    foreach (var valute in valutes)
    {
        if(valute.Element("ChatCode").Value == currencyCode)
        {
            Console.WriteLine($"Курс {valute.Element("Name").Value} ({currencyCode}) за {DateTime.Today.ToShortDateString()}: {valute.Element("Value").Value} руб.");
            break;
        }
    }
}
else
{
    Console.WriteLine($"Connection error {response.StatusCode}");
}