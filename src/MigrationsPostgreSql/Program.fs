namespace MigrationsPostgreSql
open System.Reflection
open Argu
open Npgsql
open SimpleMigrations.DatabaseProvider
open SimpleMigrations
open SimpleMigrations.Console

type ServiceConfig =
    | PostgresConnectionString of conn: string
with 
    interface IArgParserTemplate with
        member x.Usage =
            match x with
            | PostgresConnectionString _ -> "ConnectionString do bazy danych"

module Program =

    [<EntryPoint>]
    let main argv =
        let parser = ArgumentParser.Create<ServiceConfig>(programName = "MigrationsPostgreSql.exe")
        let result = parser.Parse()
        let assembly = Assembly.GetExecutingAssembly()
        use db = new NpgsqlConnection(result.GetResult PostgresConnectionString)
        let provider = PostgresqlDatabaseProvider(db)
        let migrator = SimpleMigrator(assembly, provider)
        let consoleRunner = ConsoleRunner(migrator)
        consoleRunner.Run(argv)
        printfn "%A" argv
        0 // return an integer exit code
