using ForcaMagica.Models;
using Spectre.Console;
using System;
using System.Threading;

namespace ForcaMagica.Services;

public class JogoForcaService
{
    private readonly int _maxTentativas = 6;
    private Palavra _palavraAtual;
    private readonly BancoDePalavrasService _bancoDePalavras;
    private string _dificuldadeAtual;

    public JogoForcaService(BancoDePalavrasService bancoDePalavras)
    {
        _bancoDePalavras = bancoDePalavras;
        _dificuldadeAtual = _bancoDePalavras.ObterNiveisDificuldade().First();
        _palavraAtual = new Palavra(_bancoDePalavras.ObterPalavraAleatoria(_dificuldadeAtual));
    }

    public void IniciarNovoJogo()
    {
        EscolherDificuldade();
        _palavraAtual = new Palavra(_bancoDePalavras.ObterPalavraAleatoria(_dificuldadeAtual));
        ExecutarJogo();
    }

    private void EscolherDificuldade()
    {
        var niveis = _bancoDePalavras.ObterNiveisDificuldade();
        var escolha = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Escolha o [green]nível de dificuldade[/]:")
                .PageSize(10)
                .AddChoices(niveis));

        _dificuldadeAtual = escolha;
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

        var table = new Table();
        table.AddColumn(new TableColumn("Status do Jogo").Centered());
        table.AddRow(new Markup($"[bold]Palavra:[/] {_palavraAtual}"));
        table.AddRow(new Markup($"[bold]Letras erradas:[/] {string.Join(", ", _palavraAtual.LetrasErradas)}"));
        table.AddRow(new Markup($"[bold]Tentativas restantes:[/] {_maxTentativas - _palavraAtual.LetrasErradas.Count}"));
        table.AddRow(new Markup($"[bold]Dificuldade:[/] {_dificuldadeAtual}"));
        table.Border(TableBorder.Rounded);

        AnsiConsole.Write(table);
    }

    private void ProcessarTentativa()
    {
        var letra = AnsiConsole.Prompt(
            new TextPrompt<char>("Digite uma letra: ")
                .Validate(input =>
                {
                    return char.IsLetter(input)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Por favor, digite apenas letras![/]");
                }));

        bool acertou = _palavraAtual.TentarLetra(letra);
        ExibirMensagemTentativa(acertou, letra);
    }

    private void ExibirResultadoFinal()
    {
        ExibirStatusJogo();
        if (_palavraAtual.PalavraCompleta)
        {
            AnsiConsole.Markup("[bold green]Parabéns! Você venceu![/]\n");
        }
        else
        {
            AnsiConsole.Markup($"[bold red]Game Over![/] A palavra era: [bold]{_palavraAtual.TextoOriginal}[/]\n");
        }
    }

    private void DesenharForca()
    {
        int erros = _palavraAtual.LetrasErradas.Count;
        var canvas = new Canvas(20, 20);

        // Desenhar a base da forca
        for (int i = 0; i < 20; i++)
        {
            canvas.SetPixel(i, 19, new Color(139, 69, 19)); // SaddleBrown
        }

        // Desenhar o poste vertical
        for (int i = 0; i < 18; i++)
        {
            canvas.SetPixel(5, i, new Color(139, 69, 19)); // SaddleBrown
        }

        // Desenhar o topo horizontal
        for (int i = 5; i < 15; i++)
        {
            canvas.SetPixel(i, 0, new Color(139, 69, 19)); // SaddleBrown
        }

        // Desenhar a corda
        canvas.SetPixel(14, 1, new Color(139, 69, 19)); // SaddleBrown
        canvas.SetPixel(14, 2, new Color(139, 69, 19)); // SaddleBrown

        // Desenhar o boneco
        if (erros >= 1) // Cabeça
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                        canvas.SetPixel(14 + i, 3 + j, Color.White);
                }
            }
        }
        if (erros >= 2) // Corpo
        {
            for (int i = 4; i <= 7; i++)
            {
                canvas.SetPixel(14, i, Color.White);
            }
        }
        if (erros >= 3) // Braço esquerdo
        {
            for (int i = 0; i <= 2; i++)
            {
                canvas.SetPixel(13 - i, 5 + i, Color.White);
            }
        }
        if (erros >= 4) // Braço direito
        {
            for (int i = 0; i <= 2; i++)
            {
                canvas.SetPixel(15 + i, 5 + i, Color.White);
            }
        }
        if (erros >= 5) // Perna esquerda
        {
            for (int i = 0; i <= 2; i++)
            {
                canvas.SetPixel(13 - i, 8 + i, Color.White);
            }
        }
        if (erros >= 6) // Perna direita
        {
            for (int i = 0; i <= 2; i++)
            {
                canvas.SetPixel(15 + i, 8 + i, Color.White);
            }
        }

        AnsiConsole.Write(canvas);
    }

    private void ExibirMensagemTentativa(bool acertou, char letra)
    {
        if (acertou)
        {
            AnsiConsole.Markup($"[green]Muito bem! A letra '{letra}' existe na palavra![/]\n");
        }
        else
        {
            AnsiConsole.Markup($"[red]Ops! A letra '{letra}' não existe na palavra.[/]\n");
        }
        Thread.Sleep(1500);
    }
}