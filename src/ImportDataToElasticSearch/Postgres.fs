namespace ImportDataToElasticSearch

module Postgresql =
    open ImportDataToElasticSearch.Types
    open Dapper
    open Npgsql

    let tweetsData connectionString =
        async {
            use connection = new NpgsqlConnection(connectionString)
            return! connection.QueryAsync<TweetDto>(""" SELECT
                                                          idstr,
                                                          text,
                                                          creationdate,
                                                          lang,
                                                          longitude,
                                                          latitude,
                                                          twitteruser,
                                                          sentiment
                                                        FROM sentimentfs.tweets; """) |> Async.AwaitTask
        }

    