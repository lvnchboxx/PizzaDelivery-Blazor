using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
namespace Games.Services;

public class GamesService
{
    HttpClient httpClient;
    public GamessService()
    {
        httpClient = new HttpClient();
    }
    List<Games> gamesList = new();
    public async Task<List<Games>> GetGames()
    {
        if (gamesList?.Count > 0) return gamesList;

        var url = "https://github.com/lvnchboxx/.NET-MAUI-with-json-dataset/blob/master/source/repos/steamgames/steamgames/Resources/Raw/steam.games.json";

        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            ganesList = await response.Content.ReadFromJsonAsync<List<Games>>();
        }

        /*using var stream = await FileSystem.OpenAppPackageFileAsync("Movies.json");
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();
        moviesList = JsonSerializer.Deserialize<List<Movie>>(content); */

        return gamesList;
    }


}