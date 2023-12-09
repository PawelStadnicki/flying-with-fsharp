module Places 

open Domain

let casino =
    {
        Name = "Casino de Monte Carlo"
        Location = (7.4281709814824035, 43.739119524191096)
        TransitionDuration = 3000
        Zoom = 16.
        Bearing = -90
    }

let stadium =
    {
        Name = "Stadium"
        Location = 7.415421407692317, 43.72755838705166
        TransitionDuration = 2000
        Zoom = 16.
        Bearing = 120
    }

let museum = 
    {
        Name = "Oceanographic Museum"
        Location = 7.425776955772957, 43.73087456086412
        Zoom = 15.5
        Bearing = 45
        TransitionDuration = 3000
    }