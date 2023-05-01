var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Pokemon> BDMemory = new List<Pokemon>();

// Crear un pokemon
Pokemon pokemon = new Pokemon();
pokemon.Id = 1;
pokemon.Nombre = "Pokemon 1";
pokemon.Tipo = 1;
pokemon.Habilidades[0] = 10;
pokemon.Habilidades[1] = 20;
pokemon.Habilidades[2] = 30;
pokemon.Habilidades[3] = 40;
pokemon.Defensa = 9.1m;

// Crear un pokemon
Pokemon pokemon2 = new Pokemon();
pokemon2.Id = 2;
pokemon2.Nombre = "Pokemon 2";
pokemon2.Tipo = 2;
pokemon2.Habilidades[0] = 11;
pokemon2.Habilidades[1] = 21;
pokemon2.Habilidades[2] = 31;
pokemon2.Habilidades[3] = 41;
pokemon2.Defensa = 9.2m;

// Agregar pokemones a la base de datos en memoria
BDMemory.Add(pokemon);
BDMemory.Add(pokemon2);


// Endpoint para obtener todos los pokemones
app.MapGet("/api/v1/pokemon", () => {
    return Results.Ok(BDMemory);
});


// Endpoint para obtener un pokemon por id
app.MapGet("/api/v1/pokemon/{id}", (int id) => {
    return Results.Ok(BDMemory.Single(pokemon => pokemon.Id == id));
});


// Endpoint para crear un pokemon
app.MapPost("/api/v1/pokemon", (Pokemon pokemon) => {
    BDMemory.Add(pokemon);
    return Results.Ok(pokemon);
});


// Endpoint para crear varios pokemones
app.MapPost("/api/v1/pokemon/bulk", (PokemonBulkRequest request) => {
    foreach (Pokemon pokemon in request.Pokemons) {
        BDMemory.Add(pokemon);
    }
    return Results.Ok(request.Pokemons);
});


// Endpoint para actualizar un pokemon
app.MapPut("/api/v1/pokemon/{id}", (int id, Pokemon pokemon_body) => {
    Pokemon pokemon_recuperado = BDMemory.Single(pokemon => pokemon.Id == id);
    pokemon_recuperado.Nombre = pokemon_body.Nombre;
    return Results.Ok(BDMemory);
});


// Endpoint para eliminar un pokemon
app.MapDelete("/api/v1/pokemon/{id}", (int id) => {
    Pokemon pokemon_recuperado = BDMemory.Single(pokemon => pokemon.Id == id);
    BDMemory.Remove(pokemon_recuperado);
    return Results.Ok(BDMemory);
});

// Endpoint para obtener pokemones por tipo
app.MapGet("/api/v1/pokemon/tipo/{tipo:int}", (int tipo) => {
    var pokemones = BDMemory.Where(pokemon => pokemon.Tipo == tipo).ToList();
    if (pokemones.Count == 0) {
        return Results.NotFound($"No se encontraron pokemones del tipo {tipo}");
    }
    return Results.Ok(pokemones);
});


// Endpoint para obtener pokemones ordenados por nombre de forma ascendente
app.MapGet("/api/v1/pokemon/orden_asc", () => {
    var pokemonesOrdenados = BDMemory.OrderBy(pokemon => pokemon.Nombre).ToList();
    if (pokemonesOrdenados.Count == 0) {
        return Results.NotFound("No hay pokemones en la base de datos.");
    }
    return Results.Ok(pokemonesOrdenados);
});


// Endpoint para obtener pokemones ordenados por nombre de forma descendente
app.MapGet("/api/v1/pokemon/orden_desc", () => {
    var pokemonesOrdenados = BDMemory.OrderByDescending(pokemon => pokemon.Nombre).ToList();
    if (pokemonesOrdenados.Count == 0) {
        return Results.NotFound("No hay pokemones en la base de datos.");
    }
    return Results.Ok(pokemonesOrdenados);
});

app.Run();


public class Pokemon {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Tipo { get; set; }
    public int[] Habilidades { get; set; } = new int[4];
    public decimal Defensa { get; set; }
}

public class PokemonBulkRequest {
    public List<Pokemon> Pokemons { get; set; }
}
