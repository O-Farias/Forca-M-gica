using ForcaMagica.Models;

namespace ForcaMagica.Services;

public class JogoForcaService
{
    private readonly string[] _palavras = {
        "PROGRAMACAO",
        "DESENVOLVIMENTO",
        "COMPUTADOR",
        "JAVASCRIPT",
        "PYTHON",
        "CSHARP"
    };
    private readonly int _maxTentativas = 6;
    private Palavra _palavraAtual;
    private Random _random;

    public JogoForcaService()
    {
        _random = new Random();
    }

    public void IniciarNovoJogo()
    {
        _palavraAtual = new Palavra(_palavras[_random.Next(_palavras.Length)]);
        ExecutarJogo();
    }

    private void ExecutarJogo()
    {
        while (!_palavraAtual.PalavraCompleta && _palavraAtual.LetrasErradas.Count < _maxTentativas)
        {
            ExibirStatusJogo();
            ProcessarTentativa();
        }

        ExibirResultadoFinal();
    }

    private void ExibirStatusJogo()
    {
        Console.Clear();
        DesenharForca();
        Console.WriteLine($"\nPalavra: {_palavraAtual}");
        Console.WriteLine($"Letras erradas: {string.Join(", ", _palavraAtual.LetrasErradas)}");
        Console.WriteLine($"Tentativas restantes: {_maxTentativas - _palavraAtual.LetrasErradas.Count}");
    }

    private void ProcessarTentativa()
    {
        Console.Write("\nDigite uma letra: ");
        if (char.TryParse(Console.ReadLine(), out char letra))
        {
            if (!char.IsLetter(letra))
            {
                Console.WriteLine("Por favor, digite apenas letras!");
                Thread.Sleep(1000);
                return;
            }

            bool acertou = _palavraAtual.TentarLetra(letra);
            ExibirMensagemTentativa(acertou, letra);
        }
    }

    private void ExibirResultadoFinal()
    {
        ExibirStatusJogo();
        if (_palavraAtual.PalavraCompleta)
        {
            Console.WriteLine("\nParabéns! Você venceu!");
        }
        else
        {
            Console.WriteLine($"\nGame Over! A palavra era: {_palavraAtual.TextoOriginal}");
        }
    }

    private void DesenharForca()
    {
        int erros = _palavraAtual.LetrasErradas.Count;
        Console.WriteLine("  _______");
        Console.WriteLine("  |     |");
        Console.WriteLine($"  {(erros >= 1 ? "O" : " ")}     |");
        Console.WriteLine($" {(erros >= 3 ? "/" : " ")}{(erros >= 2 ? "|" : " ")}{(erros >= 4 ? "\\" : " ")}    |");
        Console.WriteLine($" {(erros >= 5 ? "/" : " ")} {(erros >= 6 ? "\\" : " ")}    |");
        Console.WriteLine("        |");
        Console.WriteLine("     __|__");
    }

    private void ExibirMensagemTentativa(bool acertou, char letra)
    {
        Console.WriteLine(acertou 
            ? $"\nMuito bem! A letra '{letra}' existe na palavra!"
            : $"\nOps! A letra '{letra}' não existe na palavra.");
        Thread.Sleep(1500);
    }
}