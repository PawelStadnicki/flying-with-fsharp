module DeckGL

open Fable.Core
open Fable.Core.JsInterop
open Feliz

module Interop =
    [<Emit("Object.assign({}, $0, $1)")>]
    let objectAssign (x: obj) (y: obj) = jsNative

[<Import("TripsLayer","@deck.gl/geo-layers")>]
[<Emit("new $0($1)")>]
let TripsLayer(options) = jsNative

[<Import("PathLayer", "@deck.gl/layers")>]
[<Emit("new $0($1)")>]
let PathLayer(options) = jsNative

[<Import("FlyToInterpolator", "@deck.gl/core")>]
[<Emit("new $0($1)")>]
let FlyToInterpolator(options) = jsNative

[<Import("LinearInterpolator", "@deck.gl/core")>]
[<Emit("new $0($1)")>]
let LinearInterpolator(options) = jsNative

[<Import("Tile3DLayer", "@deck.gl/geo-layers")>]
[<Emit("new $0($1)")>]
let Tile3DLayer(options) = jsNative

[<Erase>]
type DeckGL()  =
    static member inline element (properties: IReactProperty list) =
        Interop.reactApi.createElement(importDefault "@deck.gl/react", createObj !!properties)

type deckGL =
    static member inline layers(value: obj[]) = 
        Interop.mkAttr "layers" value

    static member inline initialViewState value = 
        Interop.mkAttr "initialViewState" value

    static member inline controller() = 
        Interop.mkAttr "controller" true

    static member inline onViewStateChange value = 
        Interop.mkAttr "onViewStateChange" value