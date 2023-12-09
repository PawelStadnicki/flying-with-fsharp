namespace App

open Feliz
open Feliz.DaisyUI

type Components =
   
    [<ReactComponent>]
    static member Tiles3d() =
        Html.div [
            theme.business
            prop.className "text-center flex  w-full text-neutral-content"
            prop.children [
                Deck.dashboard()
            ]
        ]
