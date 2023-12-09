# flying-with-fsharp
PoC of using F#/Fable with Google 3D Photorealistic Tiles

Created for F# Adent 2023, the blog post is here: ""
It is not finished but can serve value and be a base for the experiments of others.
I use Feliz/Elmish/DaisyUI, DeckGL/TurfJS.

F# 8 :)

**Instructions**
Update src/API_keys.fs with your own API key to the Google Map Tile service. Read their docs/ pricing and make your keys appropriately restricted/secured when publishing your own work

- dotnet tool restore (for Fable/Femto)
- npm i 
- npm start
- visit url displayed in the console
- adjust/extend/play/enjoy
- 
**TODO**:
- add a possibility to cancel animations
- add a possibility to mix animations

**About Google 3d tiles**
You need the API key from the Google Maps Platform: https://developers.google.com/maps/documentation/tile/get-api-key
Put it in src/API_keys.fs while doing experiments and remember to restrict/hide when going to the public.
Mind the costs of that service (~6$ for 1000 root requests).
Timed session tokens allow for up to three hours of renderer tile requests from a single root tileset request.
As Google gives 200$ monthly, you can play with this service for free in most cases if this is the only service you use.

Read the Google docs on your own, I provide the aforementioned information based on my short experiments, I might understood it wrongly, or/and they can become quickly outdated.

