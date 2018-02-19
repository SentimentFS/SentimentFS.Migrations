namespace MigrationsPostgreSql
open System.Reflection

module Program =

    [<EntryPoint>]
    let main argv =
        let assembly = Assembly.GetExecutingAssembly()
        printfn "%A" argv
        0 // return an integer exit code
