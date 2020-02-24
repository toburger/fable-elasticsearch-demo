module Tests.Elasticsearch

open Elasticsearch
open Fable.Mocha

let client result = {
    Indices = async.Return [
        Index "foo_2020_01_11"
        Index "foo_2020_01_10"
        Index "foo_2020_01_09"
        Index "foo_2020_01_08"
        Index "foo_2020_01_07"
        Index "foo_2020_01_06"
        Index "foo_2019_11_15"
        Index "index_without_date" 
    ]
    DeleteIndex = fun index -> async {
        result := index::!result
    }
}

let elasticsearchTests =
    testList "deleteExpiredIndices" [
        testCaseAsync "should delete expired indices" <| async {
            let result = ref []
            let expected = [
                Index "foo_2019_11_15"
                Index "foo_2020_01_06"
            ]
            do! deleteExpriredIndices
                    (client result)
                    (Today (System.DateTime(2020, 1, 10)))
                    (Ttl (Days 3))
            let actual = !result
            Expect.equal actual expected "not equal"
        }
    ]
