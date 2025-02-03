using System.Text.Json;

namespace ForcaMagica.Services;

public class BancoDePalavrasService
{
    private List<string> _palavras;

    public BancoDePalavrasService()
    {
        CarregarPalavras();
    }

    private void CarregarPalavras()
    {
        string jsonString = File.ReadAllText("Data/palavras.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var data = JsonSerializer.Deserialize<PalavrasData>(jsonString, options);
        _palavras = data.Palavras;
    }

    public string ObterPalavraAleatoria()
    {
        Random random = new Random();
        return _palavras[random.Next(_palavras.Count)];
    }
}

public class PalavrasData
{
    public List<string> Palavras { get; set; }
}