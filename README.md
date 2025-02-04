# Forca Mágica 🎮

Um jogo da forca moderno implementado em C# com interface em console aprimorada usando Spectre.Console.

## 📋 Sobre o Projeto

Forca Mágica é uma versão digital do clássico jogo da forca, focado em palavras relacionadas à programação e tecnologia. O jogo apresenta interface colorida, animações e diferentes níveis de dificuldade.

## ✨ Características

- 🎯 3 níveis de dificuldade (Fácil, Médio, Difícil)
- 🎨 Interface colorida e interativa usando Spectre.Console
- 🎭 Animação da forca desenhada em ASCII art
- 📚 Banco de palavras relacionadas à tecnologia
- ⌨️ Sistema de tentativas com feedback visual
- 🔄 Opção de jogar novamente

## 🛠️ Tecnologias Utilizadas

- .NET 8.0
- C#
- Spectre.Console (v0.49.1)
- JSON para armazenamento de dados

## 📥 Instalação

```bash
# Clone o repositório
git clone https://github.com/O-Farias/Forca-Magica.git

# Entre no diretório
cd Forca-Magica

# Restaure as dependências
dotnet restore

# Execute o projeto
dotnet run --project ForcaMagica
```

## 🎮 Como Jogar

1. Inicie o jogo
2. Escolha um nível de dificuldade
3. Digite uma letra por vez para adivinhar a palavra
4. Você tem 6 tentativas antes de perder
5. Complete a palavra para vencer!

## 🏗️ Estrutura do Projeto

```
ForcaMagica/
├── Models/
│   └── Palavra.cs           # Modelo da palavra do jogo
├── Services/
│   ├── BancoDePalavrasService.cs    # Gerencia o banco de palavras
│   └── JogoForcaService.cs          # Lógica principal do jogo
├── Data/
│   └── palavras.json       # Banco de palavras por dificuldade
└── Program.cs              # Ponto de entrada da aplicação
```

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## ✨ Contribuindo

Contribuições são bem-vindas! Por favor, sinta-se à vontade para enviar pull requests.

1. Faça o fork do projeto
2. Crie sua branch de feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona alguma feature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

## 🎯 Status do Projeto

O projeto está em desenvolvimento ativo e aberto para contribuições.