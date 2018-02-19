namespace MigrationsPostgreSql
open SimpleMigrations

[<Migration(201801282311L, "Create Tweets")>]
type Tweet() =
    inherit Migration()

    override x.Up() =
        base.Execute("""
                        CREATE TABLE sentimentfs.tweets
                        (
                          idstr        VARCHAR(50) NOT NULL
                            CONSTRAINT tweets_pkey
                            PRIMARY KEY,
                          text         VARCHAR(260),
                          creationdate TIMESTAMP,
                          lang         VARCHAR(50),
                          longitude    DOUBLE PRECISION,
                          latitude     DOUBLE PRECISION,
                          twitteruser  VARCHAR(50),
                          sentiment    INTEGER
                        );

                        CREATE UNIQUE INDEX tweets_idstr_uindex
                        ON sentimentfs.tweets (idstr);                    
                    """)

        override x.Down() =
            base.Execute("""
                            DROP TABLE sentimentfs.tweets
                        """)
       