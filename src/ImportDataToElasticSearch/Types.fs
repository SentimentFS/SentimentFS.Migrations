namespace ImportDataToElasticSearch.Types
open System 

type Emotion =
    | VeryNegative = -2
    | Negative = -1
    | Neutral = 0
    | Positive = 1
    | VeryPositive = 2

[<CLIMutable>]
type TweetDto = { IdStr: string
                  Text: string
                  CreationDate: DateTime
                  Lang: string
                  Longitude: double
                  Latitude: double
                  TwitterUser: string
                  Sentiment: Emotion }

