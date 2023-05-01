public class PokemonService : IPokemonService
{
    private List<Pokemon> BDMemory;

    public PokemonService()
    {
        BDMemory = new List<Pokemon>();
        Pokemon pokemon = new Pokemon();
        pokemon.Id = 1;
        pokemon.Nombre = "Pokemon 1";
        pokemon.Tipo = 1;
        pokemon.Habilidades[0] = 10;
        pokemon.Habilidades[1] = 20;
        pokemon.Habilidades[2] = 30;
        pokemon.Habilidades[3] = 40;
        pokemon.Defensa = 9.1m;

        BDMemory.Add(pokemon);
    }

    public List<Pokemon> GetPokemonList()
    {
        return BDMemory;
    }

    public Pokemon GetPokemonById(int id)
    {
        return BDMemory.Single(pokemon => pokemon.Id == id);
    }

    public Pokemon AddPokemon(Pokemon pokemon)
    {
        BDMemory.Add(pokemon);
        return pokemon;
    }

    public List<Pokemon> AddPokemonList(List<Pokemon> pokemons)
    {
        foreach (Pokemon pokemon in pokemons)
        {
            BDMemory.Add(pokemon);
        }
        return pokemons;
    }

    public Pokemon UpdatePokemon(int id, Pokemon pokemon_body)
    {
        Pokemon pokemon_recuperado = BDMemory.Single(pokemon => pokemon.Id == id);
        pokemon_recuperado.Nombre = pokemon_body.Nombre;
        return pokemon_recuperado;
    }

    public void DeletePokemon(int id)
    {
        Pokemon pokemon_recuperado = BDMemory.Single(pokemon => pokemon.Id == id);
        BDMemory.Remove(pokemon_recuperado);
    }
}
