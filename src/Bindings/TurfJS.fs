module TurfJS

open Geojson

open Fable.Core
open Fable.Core.JsInterop

[<ImportDefault("@turf/bbox")>]
let bbox(geojson: FeatureCollection<_,_>) = jsNative

[<ImportDefault("@turf/polygon-to-line")>]
let polygonToLine(f: Feature<Polygon, _>): Feature<LineString, _> = jsNative

[<ImportDefault("@turf/rewind")>]
let rewind(f: Feature<LineString,_>): Feature<LineString,_> = jsNative

[<ImportDefault("@turf/centroid")>]
let centroid(geojson: FeatureCollection<_,_>): Feature<Point,_> = jsNative

[<ImportDefault("@turf/bearing")>]
let bearing(p1: Position, p2: Position): int = jsNative

[<Import("point","@turf/helpers")>]
let pointWithProps(location: ResizeArray<float>, props: obj): Feature<Point,obj> = jsNative

let point(location: ResizeArray<float>) = pointWithProps(location, obj())

let position(lon: float, lat: float) = point(ResizeArray[|lon;lat|]).geometry

[<Import("featureCollection","@turf/helpers")>]
let featureCollection(features: Feature<Point,_>[]): FeatureCollection<Point,_> = jsNative

[<ImportDefault("@turf/boolean-point-in-polygon")>]
let inPolygon(point, polygon) = jsNative

[<ImportDefault("@turf/circle")>]
let circle(point: Feature<Point,_>, radius: float, opts: obj) = jsNative

[<ImportDefault("@turf/along")>]
let alongOpts(line: Feature<LineString, obj>, distance: float, opts: obj): Feature<Point, obj> = jsNative

let along(line: Feature<LineString, obj>, distance: float): Feature<Point, obj> = alongOpts(line, distance, obj())


[<ImportDefault("@turf/destination")>]
let destination(point: Point, distance: float, int): Feature<Point, obj> = jsNative

let coordinates (f: Feature<Point,_>) = f.geometry.coordinates

[<ImportDefault("@turf/distance")>]
let distance(point1: Feature<Point,obj>, point2: Feature<Point,obj>): float = jsNative

[<ImportDefault("@turf/length")>]
let length(line: Feature<LineString,_>): float = jsNative

let lineDistance(line: Feature<LineString,_>) = 
    line.geometry.coordinates.ToArray()
    |> Array.pairwise
    |> Array.map (fun (a,b) -> point a, point b)
    |> Array.sumBy distance

let first (fc: FeatureCollection<_,_>) = fc.features.[0] 
