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

        Dati = new Contatti[nRighe];
        for (int i = 0; i < nRighe; i++)
        {
            string temp = Fi.ReadLine();
            Dati[i] = new Contatti(temp);
        }
    }

    private async void BTN_Visualizza_Clicked(object sender, EventArgs e)
    {
        if (Scelto)//se la variabile di stato risulta true, allora svolgiamo l'operazione di ricerca e visualizzazione
        {
            string SceltaCombobox = Convert.ToString(CBX_Scelta.SelectedItem); //legge cosa ho scelto nella combobox 
            if (SceltaCombobox == "")
            {
                dsContatti.Clear();
                await DisplayAlert("ERRORE", "Non hai scelto niente.", "Ok"); //errore in caso non abbiamo scelto niente nella combobox
                return; //blocco la funzione
            }

            string Parola = TXT_Cognome.Text; //Legge cosa ho scritto sulla seconda textbox
            if (Parola == "" && SceltaCombobox != "Stampa Tutto")
            {
                dsContatti.Clear();
                await DisplayAlert("ERRORE", "Non hai inserito nessuna parola.", "Ok"); //errore in caso non abbiamo scritto niente nella textbox
                return; //blocco la funzione
            }

            if (SceltaCombobox == "")
            {
                dsContatti.Clear();
                await DisplayAlert("ERRORE", "Non hai scelto niente.", "Ok"); //errore in caso non abbiamo scelto niente nella combobox
                return; //blocco la funzione
            }

            if (SceltaCombobox == "Inizia")
            {
                dsContatti.Clear(); //pulisco la LISTBOX prima di scrivere
                for (int i = 0; i < nRighe; i++)
                {
                    if (Dati[i].Cognome.ToUpper().StartsWith(Parola.ToUpper())) //mando tutto in uppercase per evitare problemi inutili. Uso lo .StartsWith() per cercare i risultati combacianti.
                    {
                        dsContatti.Add(new Item() { ItemName = $"{Dati[i].Cognome} {Dati[i].Nome} {Dati[i].Citta}" }); //stampo i risultati.

                    }
                }
                LST_Elenco.ItemsSource = dsContatti;
            }
            else if (SceltaCombobox == "Contiene")
            {
                dsContatti.Clear(); //pulisco la LISTBOX prima di scrivere
                for (int i = 0; i < nRighe; i++)
                {
                    if (Dati[i].Cognome.ToUpper().Contains(Parola.ToUpper())) //mando tutto in uppercase per evitare problemi inutili. Uso lo .Contains() per cercare i risultati combacianti.
                    {
                        dsContatti.Add(new Item() { ItemName = $"{Dati[i].Cognome} {Dati[i].Nome} {Dati[i].Citta}" }); //stampo i risultati.
                    }

                }
                LST_Elenco.ItemsSource = dsContatti;
            }
            else if (SceltaCombobox == "Finisce")
            {
                dsContatti.Clear(); //pulisco la LISTBOX prima di scrivere
                for (int i = 0; i < nRighe; i++)
                {
                    if (Dati[i].Cognome.ToUpper().EndsWith(Parola.ToUpper())) //mando tutto in uppercase per evitare problemi inutili. Uso lo .EndsWith() per cercare i risultati combacianti.
                    {
                        dsContatti.Add(new Item() { ItemName = $"{Dati[i].Cognome} {Dati[i].Nome} {Dati[i].Citta}" }); //stampo i risultati.
                    }
                }
                LST_Elenco.ItemsSource = dsContatti;
            }
            else if (SceltaCombobox == "Stampa tutto")
            {
                dsContatti.Clear(); //pulisco la LISTBOX prima di scrivere 
                for (int i = 0; i < nRighe; i++)
                {
                    dsContatti.Add(new Item() { ItemName = $"{Dati[i].Cognome} {Dati[i].Nome} {Dati[i].Citta}" }); //stampo tutti i nomi cognomi e città contenute all'interno del file. 
                }
                LST_Elenco.ItemsSource = dsContatti;
            }


        }
        else
        {
            await DisplayAlert("ERRORE", "Non hai inserito un file.", "Ok"); //in caso Scelto sia falso, mando il messaggio di errore e blocco la funzione.
            return;
        }
    }

}

