module Animations

open Fable.Core
open Domain
open Elmish
open Fable.Core.JsInterop
open Browser.Types

//TODO: is there anything already in place in Fable that handles requestAnimationFrame in a functional way ?

let requestAnimationFrame (x: float<milisecond> -> unit): float<milisecond> = !!(Browser.Dom.window.requestAnimationFrame !!x)

let rec private subscribe (state: AppState) (animation: (ViewState*float<milisecond>) -> ViewState) =

    let sub dispatch =

        let rec loop (state: ViewState) (frame: float<milisecond>) =
            let next = animation(state, frame) 
            next |> SetView |> dispatch

            requestAnimationFrame (loop next) |> ignore

        loop state.View 0.<milisecond>

    Cmd.ofEffect sub

let rotate (app: AppState) = 

    let exe =
        fun (state, timestamp) ->
            { state with 
                bearing = int ((timestamp/ 120.<milisecond>) % 360.)
                transitionDuration = 1000
                transitionInterpolator = DeckGL.LinearInterpolator() 
            }

    subscribe app exe

let route (app: AppState) =
    let routeDistance = TurfJS.length app.Route
    let mutable start = 0.<milisecond>

    let exe =

        fun (state, timestamp) ->

            let phase = (timestamp / app.RouteTime) % 1.

            let alongRoute = 
                (app.Route,routeDistance * phase)
                |> TurfJS.along 
                |> TurfJS.coordinates

            let next = 
                (app.Route,routeDistance * (phase + 0.01))
                |> TurfJS.along
                |> TurfJS.coordinates

            let bearing = TurfJS.bearing(alongRoute, next)
            { state with 
                pitch = 0
                zoom = 17
                longitude = alongRoute.Item 0
                latitude = alongRoute.Item 1
                bearing = bearing
                transitionDuration = 10
                transitionInterpolator = DeckGL.LinearInterpolator() 
            }

    subscribe app exe

let registerKeyboard (app: AppState) dispatch (evt: KeyboardEvent) = 

    JS.console.log $"{evt.key} pressed"

    if evt.key = "w" then
        let origin = TurfJS.position(app.View.longitude, app.View.latitude) 
        let target = TurfJS.destination(origin, 0.01, app.View.bearing)
        { app.View with longitude = target.geometry.coordinates.Item 0;latitude = target.geometry.coordinates.Item 1;transitionDuration = 100 } |> SetView |> dispatch
    if evt.key = "s" then
        let origin = TurfJS.position(app.View.longitude, app.View.latitude) 
        let target = TurfJS.destination(origin, -0.01, (app.View.bearing))
        { app.View with longitude = target.geometry.coordinates.Item 0;latitude = target.geometry.coordinates.Item 1;transitionDuration = 100 } |> SetView |> dispatch
    if evt.key = "a" then
        { app.View with bearing = app.View.bearing + 1;transitionDuration = 10  } |> SetView |> dispatch
    if evt.key = "d" then
        { app.View with bearing = app.View.bearing - 1;transitionDuration = 10  } |> SetView |> dispatch
    if evt.key = "n" then
        { app.View with zoom = app.View.zoom + 0.02;transitionDuration = 500  } |> SetView |> dispatch
    if evt.key = "m" then
        { app.View with zoom = app.View.zoom - 0.02;transitionDuration = 500  } |> SetView |> dispatch            
    if evt.key = "i" then
        { app.View with zoom = app.View.zoom + 0.002; pitch = app.View.pitch + 0.5;  transitionDuration = 1 } |> SetView |> dispatch
    