module Prelude

module Int =
    let tryParse (s: string) =
        match System.Int32.TryParse s with
        | true, value -> Some value
        | false, _ -> None

module List =
    let truncateLast count =
        List.rev >> List.truncate count >> List.rev

type MaybeBuider () =
    member __.Bind(m, f) =
        Option.bind f m
    member __.Return(x) =
        Some x

    // F# 5 and!
    member __.MergeSources(a, b) =
        (a, b) ||> Option.map2 (fun a b -> a, b)           // Not sure if this is the correct implementation
    member __.MergeSource3(a, b, c) =
        (a, b, c) |||> Option.map3 (fun a b c -> a, b, c)  // Not sure if this is the correct implementation

let maybe = MaybeBuider()
