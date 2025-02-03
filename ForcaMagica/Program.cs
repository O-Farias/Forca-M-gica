using ForcaMagica.Models;
using ForcaMagica.Services;

namespace ForcaMagica;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Bem-vindo à Forca Mágica! ===\n");
        
        var jogoService = new JogoForcaService();
        bool jogarNovamente = true;

        while (jogarNovamente)
        {
            try
            {
                jogoService.IniciarNovoJogo();
                
                Console.Write("\nDeseja jogar novamente? (S/N): ");
                jogarNovamente = Console.ReadLine()?.ToUpper() == "S";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                jogarNovamente = false;
            }
        }

        Console.WriteLine("\nObrigado por jogar Forca Mágica!");
    }
}