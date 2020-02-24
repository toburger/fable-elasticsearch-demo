module Elasticsearch

open Prelude

type Days = Days of int

type Hostname = Hostname of string

type Port = Port of int

type Ttl = Ttl of Days

type Today = Today of System.DateTime

type Index = Index of string

type DatedIndex = {
    Name: Index
    Date: System.DateTime
}

type ElasicsearchClient = {
    Indices: Async<Index list>
    DeleteIndex: Index -> Async<unit>
}

let findExpired (Today today) (Ttl (Days days)) =
    List.filter (fun indexDate -> (today - indexDate.Date).TotalDays > float days)

let parseDate year month day = maybe {
    let! year = Int.tryParse year
    let! month = Int.tryParse month  // in F# 5 use and!
    let! day = Int.tryParse day      // in F# 5 use and!
    return System.DateTime(year, month, day)
}

let determineDate (Index name) = maybe {
    let chunks =
        name.Split('_')   // in F# 5: .[^2..]
        |> List.ofArray
        |> List.truncateLast 3
    let! date =
        match chunks with
        | [year; month; day] -> parseDate year month day
        | _ -> None
    return { Name = Index name; Date = date }
}

let deleteExpriredIndices client today ttl = async {
    let! indices = client.Indices
    let datedIndices = indices |> List.choose determineDate
    let expireds = findExpired today ttl datedIndices
    for expired in expireds do
        do! client.DeleteIndex expired.Name
}