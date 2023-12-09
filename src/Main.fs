module Main

open Feliz
open App
open Browser.Dom

let root = ReactDOM.createRoot(document.getElementById "feliz-app")

Components.Tiles3d()
|> root.render