# OpenGL (C#.OpenTK)

Para o desenvolvimento do nosso código gráfico iremos usar a biblioteca OpenGL por intermédio do OpenTK, com a linguagem C# e a IDE VSCode.  

## SDK .Net

O primeiro passo é instalar o SDK do .NET Core para poder programar na linguagem C#.  

<https://www.microsoft.com/net/download>  

### Testar o SDK .NET

Para os passos a seguir é possível utilizar o prompt do Windows (cmd), terminal do MacOS (terminal), ou o próprio terminal do [VSCode](#ide-vscode).  
Crie uma nova pasta que será o diretório do projeto OpenTK no VSCode e navegue até ela. Nesse exemplo o nome da pasta será 'OlaMundo':  

  $ mkdir OlaMundo  
  $ cd OlaMundo  

Em seguida crie um ```Console Application``` nessa pasta:  

  $ dotnet new console  

Nesse ponto um novo arquivo Program.cs contendo um método main é criado. Para executar o projeto digite:  

  $ dotnet run  

Se o projeto foi criado corretamente, após a sua execução deve aparecer a mensagem 'Hello, World!' no terminal.  
Observe que no projeto foi criado dois arquivos:  

- OlaMundo.csproj  
- Program.cs  

E as pastas:

- bin  
- obj  

## IDE VSCode

O segundo passo é instalar a IDE VSCode (ou outra da sua escolha)  

<https://code.visualstudio.com/>  

Não esqueça de também instalar a extensão para CSharp <https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp> ou <https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp>  

Se necessário, se pode obter mais informações sobre extensões do VSCode em: [https://code.visualstudio.com/docs/editor/extension-gallery](https://code.visualstudio.com/docs/editor/extension-gallery "https://code.visualstudio.com/docs/editor/extension-gallery")  

As extensões que eu uso podem ser vistas em: <https://github.com/dalton-reis/dalton-reis#vscode>  

### Testar o VSCode

Para testar a IDE VSCode tente abrir o projeto criado. Para quem quiser aprender um pouco mais sobre a IDE VSCode segue alguns links:

- [Lista Geral](https://code.visualstudio.com/docs/getstarted/introvideos)
- [Começando](https://code.visualstudio.com/docs/introvideos/basics)
- [Edição de Código](https://code.visualstudio.com/docs/introvideos/codeediting)
- [Personalizar](https://code.visualstudio.com/docs/introvideos/configure)
- [Extensões](https://code.visualstudio.com/docs/introvideos/extend)
- [Depurando](https://code.visualstudio.com/docs/introvideos/debugging)
- [Controle de Versão](https://code.visualstudio.com/docs/introvideos/versioncontrol)
- [Customizar](https://code.visualstudio.com/docs/introvideos/customize)

- Para mais informações sobre como usar o C#:
  [<https://code.visualstudio.com/docs/languages/csharp>](https://code.visualstudio.com/docs/languages/csharp "Uso do CSharp no VSCode")  

## Toolkit OpenTK

O terceiro passo é fazer com que o projeto use as dependências do Toolkit OpenTK no projeto criado:  

  $ dotnet add package OpenTK --version 4.7.4

### Testar o OpenTK

Nesse ponto, para testar se o OpenTK está funcionando a linha 'using OpenTK;' no cabeçalho da classe:  

  using OpenTK;  

Se nenhum erro ocorrer é porque o OpenTK já está disponível para ser usado.  
Caso ocorra algum erro de 'undefined command' tente executar o comando no terminal para recarregar o projeto:  

  $ dotnet restore

## [Voltar](./README.md#opentk)  
