# PetProducts
A CRUD sample


Aplicação desenvolvida usando Visual Studio Community 2019. 
Banco de dados utilizado SQL Server Express 2019.
SQL Server Management Studo (Recomendado)

-Para Executar este projeto-

Precisa ter instalado o SQL Server.
Restaurar o back up utilizando o arquivo PetProducts.bak, que esta localizado na pasta Database desse repositorio.
Caso necessario seguir o guia
https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/restore-a-database-backup-using-ssms?view=sql-server-ver15

Ao abrir o projeto, é necessario atlerar a string de conexão para o a sua instancia do SQL Server

Examplo:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=MySqlServerInstance\\SQLEXPRESS;Database=PetProducts;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}



