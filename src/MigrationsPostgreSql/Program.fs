namespace MigrationsPostgreSql
open System.Reflection
open Npgsql
open SimpleMigrations.DatabaseProvider
open SimpleMigrations
open SimpleMigrations.Console
open Microsoft.Extensions.Configuration

module Program =

    [<EntryPoint>]
    let main argv =
        let builder = ConfigurationBuilder().AddJsonFile("appsettings.json", optional = false, reloadOnChange = true);
        let config = builder.Build()
        let assembly = Assembly.GetExecutingAssembly()
        use db = new NpgsqlConnection(config.GetValue<string>("Data:Postgres:ConnectionString"))
        let provider = PostgresqlDatabaseProvider(db)
        let migrator = SimpleMigrator(assembly, provider)
        let consoleRunner = ConsoleRunner(migrator)
        consoleRunner.Run(argv)
        printfn "%A" argv
        0 // return an integer exit code
