namespace ImportDataToElasticSearch


module ElasticSearch =

    open Nest
    open ImportDataToElasticSearch.Types

    let indexName (name: string) =
        IndexName.op_Implicit name

    let typeName (name: string) =
        TypeName.op_Implicit name

    let tweetsIndexName = indexName "tweets"

    let tweetTypeName = typeName "tweet"

    let indexer index (ides:IndexDescriptor<'T>)  =
        ides.Index(index) :> IIndexRequest<'T>

    let insertTweet (tweet: TweetDto)(client: ElasticClient) =
        async {
            return! client.IndexAsync(tweet, (fun idx -> indexer tweetsIndexName idx)) |> Async.AwaitTask
        }


    let insertTweets (client: ElasticClient)(tweets: TweetDto seq) =
        async {
            return! client.IndexManyAsync(tweets, tweetsIndexName, tweetTypeName) |> Async.AwaitTask
        }
