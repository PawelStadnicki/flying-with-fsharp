namespace App

open Feliz
open Feliz.UseElmish
open Fable.Core.JsInterop
open Elmish

open DeckGL
open Domain
open Geojson

module Deck = 

    let geodata: Feature<LineString, obj> = GeoData.get GeoData.monteCarloF1 // |> TurfJS.rewind>
        // you can create your own route easily with geojson.io. 
        // just copy/paste the geojson from there, note that:
        //  - it must be a single feature with either LineString or Polygon (Polgyn will still be converted to LineString)

    let initState =
        {
            Animation = None
            RouteTime = 50000.<milisecond>
            Route = geodata
            DisplayCenterMarker = false
        
            View = {
                  longitude = fst GeoData.monaco
                  latitude = snd GeoData.monaco
                  altitude = 1.5
                  pitch = 60.
                  bearing = 0
                  zoom = 16
                  transitionDuration = 3000
                  transitionInterpolator = DeckGL.FlyToInterpolator() 
                }
            Places = [Places.casino; Places.stadium; Places.museum]
        }

    let init() = 
        initState, 
            Cmd.batch
                [
                    //Cmd.OfPromise.perform Geolocation.getCurrentPosition (GeoData.monaco) Msg.TrySetCurrentPosition
                    Cmd.ofMsg (StartFrom GeoData.monaco)
                ]

    let update (msg: Msg) (state: AppState) =
        match msg with
        | StartAnimation animation -> 
            match animation with
            | Rotate ->  { state with Animation = Some animation; DisplayCenterMarker = false }, Animations.rotate state
            | FollowRoute -> { state with Animation = Some animation; DisplayCenterMarker = true }, Animations.route state
        | StopAnimation -> { state with Animation = None }, Cmd.none
        | SetView view -> { state with View = view }, Cmd.none
        | TrySetCurrentPosition pos -> 
            match pos with 
            | None -> state, Cmd.none
            | Some (lon,lat) -> { state with View.longitude = lon; View.latitude = lat }, Cmd.none
        | StartFrom (lon,lat) -> { state with View.longitude = lon; View.latitude = lat }, Cmd.none
        | EnableCenterMarker -> { state with DisplayCenterMarker = not state.DisplayCenterMarker }, Cmd.none

    let dashboard = React.functionComponent(fun () ->

        let app, dispatch = React.useElmish(init, update, [| |])
   
        Html.div [
            prop.onKeyPress (Animations.registerKeyboard app dispatch)
            prop.className "flex w-full"
            prop.children [
                
                Html.div [  
                    prop.className "w-3/5 flex h-screen justify-center items-center "
                    prop.style [style.height (length.vh 100) ]
                    prop.children [
                        
                        match app.DisplayCenterMarker with 
                        | true -> Html.i [prop.disabled app.DisplayCenterMarker; prop.classes [ FA.fa_solid;FA.fa_car;FA.fa_2x; "text-orange-500"; tw.``z-10``; "m-auto absolute"]]
                        | false -> Html.none

                        DeckGL.element [
                            deckGL.controller()
                            deckGL.initialViewState app.View
                            deckGL.layers [| DeckGL.Tile3DLayer Tiles.layer |]
                            deckGL.onViewStateChange (
                                fun o -> o?viewState |> SetView |> dispatch)
                            prop.style [style.position.relative]
                        ]
                    ]
                ]
                Html.div [
                    prop.className "w-2/5"
                    prop.children [
                        View.Sidebar.element (app, dispatch)
                    ]
                ]
            ]
        ]  
    )
