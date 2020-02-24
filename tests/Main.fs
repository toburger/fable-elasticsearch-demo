module Main

open Fable.Mocha

[<EntryPoint>]
let main _ =
    Mocha.runTests Tests.Elasticsearch.elasticsearchTests
