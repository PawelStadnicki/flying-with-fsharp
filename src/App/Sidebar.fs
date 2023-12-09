module View.Sidebar

open App
open Domain
open Feliz
open Feliz.DaisyUI

let element(app: AppState, dispatch: Msg -> unit) =

  let rec move (timestamp: float) = 
    Browser.Dom.window.requestAnimationFrame move |> ignore

  let goto (place: PlaceView) = 
           
    SetView { 
        app.View with 
            longitude = fst place.Location; 
            latitude = snd place.Location; 
            bearing = int place.Bearing
            zoom = place.Zoom
            transitionDuration = place.TransitionDuration; 
            transitionInterpolator = DeckGL.LinearInterpolator() 
    } |> dispatch
    move 0.
  
  let round (x: float) = System.Math.Round(x, 1)

  Html.div[
    prop.classes [
        tw.``p-5``
        tw.flex
        tw.``flex-col``
        tw.``h-screen``
        tw.``justify-between``
    ]
    prop.children [

        Html.div [
            prop.className "grid w-full p-3"
            prop.children [
                Daisy.labelText $"Zoom: {round app.View.zoom}"
                Daisy.range [prop.min 13.; prop.max 18.; prop.value app.View.zoom; prop.step 0.1; prop.onChange (fun x -> SetView { app.View with zoom = x } |> dispatch)]
            ]
        ]
        Html.div [
            prop.className " grid w-full p-3"
            prop.children [
                Daisy.labelText $"""Bearing: {if app.View.bearing > 0 then "+" else ""}{round app.View.bearing}"""
                Daisy.range [prop.min -180; prop.max 180; prop.value app.View.bearing; prop.step 1; prop.onChange (fun x -> SetView { app.View with bearing = x } |> dispatch)]
            ]
        ]
        Html.div [
            prop.className "grid w-full p-3"
            prop.children [
                Daisy.labelText $"Pitch: {round app.View.pitch}"
                Daisy.range [prop.min 10.; prop.max 80.; prop.value app.View.pitch; prop.step 1.; prop.onChange (fun x -> SetView { app.View with pitch = x } |> dispatch)]
            ]
        ] 
        Html.div [
            prop.className "grid w-full p-3"
            prop.children [
                Daisy.labelText $"Altitute: {round app.View.altitude}"
                Daisy.range [prop.min 0.; prop.max 4.; prop.value app.View.altitude; prop.step 0.01; prop.onChange (fun x -> SetView { app.View with altitude = x } |> dispatch)]
           ]
        ]

        let animationButton(animation: Animation) =
            Daisy.button.button [
                prop.className "m-2"
                //prop.disabled app.Animation.IsSome
                if app.Animation = Some animation then button.secondary else button.neutral
                prop.text $"{animation}"
                prop.onClick (fun _ -> dispatch (Msg.StartAnimation animation))
            ]         

        Html.div [
            prop.className "flex"
            prop.children [
                animationButton FollowRoute
                animationButton Rotate
            ]
        ]
        Html.div [
            prop.className "flex p-3"
                 
            app.Places 
            |> List.map (
                fun place -> 
                    Daisy.button.button [
                        prop.className "m-2"
                        button.neutral
                        prop.text place.Name
                        prop.onClick (fun _ -> goto place)
                    ]
            )
            |> prop.children
                
        ]
        Daisy.formControl [
            Daisy.label [Daisy.labelText "Mark center"; Daisy.toggle [prop.name "colors";prop.isChecked app.DisplayCenterMarker; prop.onChange (fun (x: bool) -> dispatch EnableCenterMarker)]]
        ]
        Daisy.footer [
            prop.className "p-10 bg-neutral text-neutral-content p-3"
            prop.children [
                Html.div [
                    Daisy.footerTitle "State"
                    Daisy.link [link.hover; prop.text $"Rotate {app.Animation = Some Rotate}"]
                    Daisy.link [link.hover; prop.text $"Follow route {app.Animation = Some FollowRoute}"]
                ]
                Html.div [
                    Daisy.footerTitle "Position"
                    Daisy.link [link.hover; prop.text $"Lon: {System.Math.Round(float app.View.longitude, 6)}"]
                    Daisy.link [link.hover; prop.text $"Lat: {System.Math.Round(float app.View.latitude, 6)}"]
                ]
                Html.div [
                    Daisy.footerTitle "Other"
                ]
            ]
        ]    
    ]
  ]
