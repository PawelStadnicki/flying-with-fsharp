# flying-with-fsharp
PoC of using F#/Fable with Google 3D Photorealistic Tiles



** About Google 3d tiles **
You need the API key from the Google Maps Platform: https://developers.google.com/maps/documentation/tile/get-api-key
Put it in src/API_keys.fs while doing experiments and remember to restrict/hide when going to the public.
Mind the costs of that service (~6$ for 1000 root requests).
Timed session tokens allow for up to three hours of renderer tile requests from a single root tileset request.
As Google gives 200$ monthly, you can play with this service for free in most cases if this is the only service you use.

Read the Google docs on your own, I provide the aforementioned information based on my short experiments, I might understood it wrongly, or/and they can become quickly outdated.

