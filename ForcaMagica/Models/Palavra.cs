namespace ForcaMagica.Models;

public class Palavra
{
    public string TextoOriginal { get; private set; }
    public char[] TextoExibido { get; private set; }
    public List<char> LetrasErradas { get; private set; }
    public int Tentativas { get; private set; }
    public bool PalavraCompleta => !TextoExibido.Contains('_');

    public Palavra(string palavra)
    {
        if (string.IsNullOrWhiteSpace(palavra))
            throw new ArgumentException("A palavra não pode estar vazia.");

        TextoOriginal = palavra.ToUpper();
        TextoExibido = new string('_', palavra.Length).ToCharArray();
        LetrasErradas = new List<char>();
        Tentativas = 0;
    }

    public bool TentarLetra(char letra)
    {
        letra = char.ToUpper(letra);
        Tentativas++;

        if (LetrasErradas.Contains(letra) || TextoExibido.Contains(letra))
            return false;

        bool letraEncontrada = false;
        for (int i = 0; i < TextoOriginal.Length; i++)
        {
            if (TextoOriginal[i] == letra)
            {
                TextoExibido[i] = letra;
                letraEncontrada = true;
            }
        }

        if (!letraEncontrada)
            LetrasErradas.Add(letra);

        return letraEncontrada;
    }

    public override string ToString()
    {
        return string.Join(" ", TextoExibido);
    }
}