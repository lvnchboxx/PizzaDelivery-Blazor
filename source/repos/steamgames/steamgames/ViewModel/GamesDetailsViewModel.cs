namespace steamgames.ViewModel


[QueryProperty("Games", "Games")]
public partial class GamesDetailsViewModel : BaseViewModel
{
    public GamesDetailsViewModel()
    {

    }

    [ObservableProperty]
    Games games;

    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
