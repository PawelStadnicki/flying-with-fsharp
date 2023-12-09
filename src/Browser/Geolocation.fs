namespace App

open Browser.Types
open Browser
open Fable.Core

exception GeolocationException of PositionError

module Geolocation =
    let getCurrentPosition(default': float*float): JS.Promise<(float*float) option> = 
        JS.Constructors.Promise.Create (fun resolve reject ->
            match navigator.geolocation with
            | None -> resolve None
            | Some geolocation -> 
                geolocation.getCurrentPosition(fun pos ->
                    (pos.coords.longitude, pos.coords.latitude)
                    |> Some
                    |> resolve  
                , fun error -> 
                    JS.console.warn $"User declined to provide geolocation, default {default'} is taken. Exact message: {error}"
                    default' |> Some |> resolve ) 
        )

// or use:  GeolocationException >> reject
