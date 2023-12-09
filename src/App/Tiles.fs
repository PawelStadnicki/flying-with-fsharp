module Tiles

let layer: obj = 
    {|
        id = "tile-3d-layer"
        pointSize = 2
        data = "https://tile.googleapis.com/v1/3dtiles/root.json"
        onTilesetLoad = ignore //TODO: add credits addition as per this example:https://github.com/visgl/deck.gl/tree/8.9-release/examples/website/google-3d-tiles
        opacity = 1.0
        loadOptions = 
            {|
                fetch = 
                    {| 
                        headers = 
                            {|
                                ``X-GOOG-API-KEY`` = API_KEYS.Google_Photorealistic_3d
                            |}
                    |}
            |}
        operation = "terrain+draw"
    |}