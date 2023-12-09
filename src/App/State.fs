module Domain

open Geojson

//TODO: think about more clear definition of the speed 
[<Measure>] type milisecond

type ViewState =
    {
        longitude: float
        latitude: float
        pitch: float
        altitude: float //Distance of the camera relative to viewport height. Default 1.5.
        bearing: int
        //maxPitch: float
        //minZoom = 2
        //maxZoom = 30
        zoom: float
        transitionDuration: int
        transitionInterpolator: obj
    }

type Animation =
    | Rotate 
    | FollowRoute 

type PlaceView = 
   {
       Name: string
       Location: float*float
       TransitionDuration : int
       Zoom: float
       Bearing: float
   }

type AppState = 
    {
        Animation: Animation option
        View: ViewState
        Places: PlaceView list
        Route: Feature<LineString, obj> 
        RouteTime: float<milisecond>
        DisplayCenterMarker: bool
    }

type Msg =
    | StartAnimation of Animation
    | StopAnimation
    | SetView of ViewState
    | TrySetCurrentPosition of (float*float) option
    | StartFrom of float*float
    | EnableCenterMarker