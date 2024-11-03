using ScrappingWebsite.Linxea.Tools;

namespace ScrappingWebsite.Linxea;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {

        CounterBtn.IsEnabled = false;

        try
        {
            var sous = new SubscripitonTranversePath(eAdresse.Text ?? string.Empty);
            await sous.Run();
        }
        catch (ArgumentException ex)
        {
            await this.DisplayAlert("Erreur", ex.Message, "OK");
        }
        finally
        {
            CounterBtn.IsEnabled = true;
        }
    }
}
