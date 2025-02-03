using System.Text.Json;

namespace ForcaMagica.Services;

public class BancoDePalavrasService
{
    private Dictionary<string, List<string>> _palavras = new();

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
        var palavrasCarregadas = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonString, options);

        if (palavrasCarregadas != null)
        {
            _palavras = palavrasCarregadas;
        }
        else
        {
            throw new InvalidOperationException("Não foi possível carregar as palavras do arquivo JSON.");
        }
    }

    public string ObterPalavraAleatoria(string dificuldade)
    {
        if (!_palavras.ContainsKey(dificuldade))
        {
            throw new ArgumentException("Nível de dificuldade inválido");
        }

        var random = new Random();
        int index = random.Next(_palavras[dificuldade].Count);
        return _palavras[dificuldade][index];
    }

    public List<string> ObterNiveisDificuldade()
    {
        return _palavras.Keys.ToList();
    }
}