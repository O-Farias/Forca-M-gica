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
        if (erros >= 1) canvas.SetPixel(14, 3, Color.White); // Cabeça
        if (erros >= 2) canvas.SetPixel(14, 4, Color.White); // Corpo
        if (erros >= 3) canvas.SetPixel(13, 4, Color.White); // Braço esquerdo
        if (erros >= 4) canvas.SetPixel(15, 4, Color.White); // Braço direito
        if (erros >= 5) canvas.SetPixel(13, 5, Color.White); // Perna esquerda
        if (erros >= 6) canvas.SetPixel(15, 5, Color.White); // Perna direita

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