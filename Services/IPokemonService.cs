public interface IPokemonService
{
    List<Pokemon> GetPokemonList();
    Pokemon GetPokemonById(int id);
    Pokemon AddPokemon(Pokemon pokemon);
    List<Pokemon> AddPokemonList(List<Pokemon> pokemons);
    Pokemon UpdatePokemon(int id, Pokemon pokemon);
    void DeletePokemon(int id);
}
    