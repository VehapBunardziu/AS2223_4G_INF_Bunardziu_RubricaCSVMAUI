using System.Collections.ObjectModel;

namespace AS2223_4G_INF_Bunardziu_RubricaCSVMAUI;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
    public class Item
    {
        public string ItemName { get; set; }
    }
    ObservableCollection<Item> dsContatti = new ObservableCollection<Item>();
    static bool Scelto = false;
    static int nRighe;
    static int nColonne = 3;
    static Contatti[] Dati;

    private async void BTN_File_Clicked(object sender, EventArgs e)
	{
        var FinestraFile = await Microsoft.Maui.Storage.FilePicker.PickAsync(); //inizializza una nuova variabile che contiene il File selezionato tramite il metodo FilePicker
        if (FinestraFile != null)
        {
            TXT_File.Text = FinestraFile.FullPath; //.FileName ottiene l'indirizzo del file selezionato sulla finestra
            Scelto = true; //aggiorno lo stato della variabile di stato
        }
        else
        {
            await DisplayAlert("ERRORE", "Non hai selezionato un File. Selezionarne uno per continuare.", "Ok"); //errore in caso non abbiamo scelto un file. 
            return; //blocco la funzione
        }
        StreamReader Fi = new StreamReader(TXT_File.Text); //leggo il file .CSV
        nRighe = File.ReadLines(TXT_File.Text).Count();
        string temp = Fi.ReadLine();
        Dati = new Contatti[nRighe];
        for(int i = 0; i < nRighe; i++)
        {
            Dati[i] = new Contatti(temp);
        }
    }

	private void BTN_Visualizza_Clicked(object sender, EventArgs e)
	{

	}
}

