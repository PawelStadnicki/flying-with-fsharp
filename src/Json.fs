module Json

open Thoth.Json
open Thoth.Json.Net

let inline encode<'T>(state: 'T) = Encode.Auto.toString(0, state)

let inline decode<'T>(raw: string) = Decode.Auto.fromString<'T> raw