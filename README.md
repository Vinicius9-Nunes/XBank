# XBank
##### Resumo
XBank é uma webapi que têm o objetivo de gerenciar/Criar uma conta de banco e realizar transações (Debito, Credito, Deposito).
Esse serviço também possui uma outra web api para realizar consulta de extratos das contas e suas faturas, esse projeto fica no caminho *https://github.com/Vinicius9-Nunes/XBankQuery*
##### Requisitos
* Possuir uma instância do SQL SERVER na maquina para criação do banco de dados.
* Possuir SDK do .Net Core 5.0.13.
* Validar string de conexão do sql server local, caso necessario atualizar a string atual no arquivo XBank\XBank\appsettings.json.
* Acessar pasta do projeto e rodar os comandos abaixo para criação das migrations:
    cd XBank.Repository
    dotnet ef --startup-project ..\XBank\ migrations add initXBank
    dotnet ef --startup-project ..\XBank\ database update

##### Executar o projeto
* Depois da criação do banco basta acessar a pasta do projeto a seguir XBank\XBank\XBank.csproj e rodar o comando **dotnet run**.
**Obs:** Atualmente no appsettings.json desse projeto temos um endpoint chamado BaseEndPointAccount, que contêm a url base da controller Account para chamadas http pelo Visual Studio 2019 na *"sslPort": 44393*, caso execute pelo VSCode observar a porta do endpoint.