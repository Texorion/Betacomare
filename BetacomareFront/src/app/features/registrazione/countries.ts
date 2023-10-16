// https://github.com/stefangabos/world_countries/blob/fd90a81bff27369f0d22efe31c0c74cc32baa6ac/data/countries/en/world.json
// https://github.com/stefangabos/world_countries/blob/fd90a81bff27369f0d22efe31c0c74cc32baa6ac/data/subdivisions/subdivisions.json

export interface Countries{
    [x: string]: any;
    alpha2: string,
    name: string
}