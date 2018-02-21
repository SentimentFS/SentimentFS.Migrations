namespace ImportDataToElasticSearch
open Microsoft.Extensions.Configuration
open System
open Nest

module Program =

    let elasticClient(uri: Uri) =
        let settings = ConnectionSettings(uri)
        ElasticClient(settings)

    [<EntryPoint>]
    let main argv =
        let builder = ConfigurationBuilder().AddJsonFile("appsettings.json", optional = false, reloadOnChange = true);
        let config = builder.Build()
        async {
            let! tweets = Postgresql.tweetsData (config.GetValue<string>("Data:Postgres:ConnectionString"))
            let elasticConnection = elasticClient(Uri(config.GetValue<string>("Data:ElasticSearch:Connection")))
            do! tweets |> Seq.map(fun tweet -> ElasticSearch.insertTweet elasticConnection tweet) |> Seq.toArray |> Async.Parallel |> Async.Ignore
            return ()
        } |> Async.RunSynchronously
        printfn "%A" argv
        0 // return an integer exit code
