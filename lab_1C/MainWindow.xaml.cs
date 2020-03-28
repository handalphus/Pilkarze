using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab_1C
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string plikArchiwizacji = "archiwum.txt";


        public MainWindow()
        {
            
            TextBoxWithErrorProvider.BrushForAll = Brushes.Red;
            InitializeComponent();
            textBoxWEPImie.SetFocus();
            
           
        }
        //Test sprawdzający czy pole nie jest puste
        //jeśli tak to przy okazji zgłasza błąd w obrębie kontrolki
        private bool IsNotEmpty(TextBoxWithErrorProvider tb)
        {
            if (tb.Text.Trim() == "")
            {
                tb.SetError("Pole nie może być puste!");
                return false;
            }
            tb.SetError("");
            return true;
        }

        private void Clear()
        {
            //czyszczenie formularza i ustawienie stanu początkowego
            textBoxWEPImie.Text = "";
            textBoxWEPNazwisko.Text = "";
            sliderWaga.Value = 75;
            sliderWiek.Value = 25;
            buttonEdytuj.IsEnabled = false;
            buttonUsun.IsEnabled = false;
            //odznaczenie listy
            listBoxPilkarze.SelectedIndex = -1;
            textBoxWEPImie.SetFocus();
        }

        private void LoadPlayer(Pilkarz pilkarz)
        {
            textBoxWEPImie.Text = pilkarz.Imie;
            textBoxWEPNazwisko.Text = pilkarz.Nazwisko;
            sliderWaga.Value = pilkarz.Waga;
            sliderWiek.Value = pilkarz.Wiek;
            buttonEdytuj.IsEnabled = true;
            buttonUsun.IsEnabled = true;
            textBoxWEPImie.SetFocus();
        }

        private void buttonEdytuj_Click(object sender, RoutedEventArgs e)
        {
            // operator logiczny & a nie podwójny operator warunkowy &&
            //sprawdź dlaczego
            //musi zweryfikować oba operandy, gdyby było &&, to w przypadku, gdy dwa pola byłyby puste
            //to tylko pierwsze zostałoby podświetlone przez ISNotEmpty i cały warunek dostałby wartość false
            if (IsNotEmpty(textBoxWEPImie) & IsNotEmpty(textBoxWEPNazwisko))
            {
                var biezacyPilkarz = new Pilkarz(textBoxWEPImie.Text.Trim(), textBoxWEPNazwisko.Text.Trim(), (uint)sliderWiek.Value, (uint)sliderWaga.Value);
                var czyJuzJestNaLiscie = false;
                foreach (var p in listBoxPilkarze.Items)
                {
                    var pilkarz = p as Pilkarz;
                    if (pilkarz.isTheSame(biezacyPilkarz))
                    {
                        czyJuzJestNaLiscie = true;
                        break;
                    }
                }
                if (!czyJuzJestNaLiscie)
                {
                    var dialogResult=MessageBox.Show($"Czy na pewno chcesz zmienić dane  {Environment.NewLine} {listBoxPilkarze.SelectedItem}?", "Edycja", MessageBoxButton.YesNo);

                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        //zamiana refernecji do obiektu piłkarza edytowanego
                        //zmień implementację tak aby zmieniać stan obiektu a nie podmieniać referencję
                        ((Pilkarz)listBoxPilkarze.Items[listBoxPilkarze.SelectedIndex]).Imie = biezacyPilkarz.Imie;
                        ((Pilkarz)listBoxPilkarze.Items[listBoxPilkarze.SelectedIndex]).Nazwisko = biezacyPilkarz.Nazwisko;
                        ((Pilkarz)listBoxPilkarze.Items[listBoxPilkarze.SelectedIndex]).Wiek = biezacyPilkarz.Wiek;
                        ((Pilkarz)listBoxPilkarze.Items[listBoxPilkarze.SelectedIndex]).Waga = biezacyPilkarz.Waga;
                        listBoxPilkarze.Items.Refresh();
                        
                        
                    }
                    Clear();
                    listBoxPilkarze.SelectedIndex = -1;
                    
                }
                else
                    MessageBox.Show($"{biezacyPilkarz.ToString()} już jest na liście.", "Uwaga");
                    
                    
                
            }
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (IsNotEmpty(textBoxWEPImie) & IsNotEmpty(textBoxWEPNazwisko))
            {
                var biezacyPilkarz = new Pilkarz(textBoxWEPImie.Text.Trim(), textBoxWEPNazwisko.Text.Trim(), (uint)sliderWiek.Value, (uint)sliderWaga.Value);
                var czyJuzJestNaLiscie = false;
                foreach (var p in listBoxPilkarze.Items)
                {
                    var pilkarz = p as Pilkarz;
                    if (pilkarz.isTheSame(biezacyPilkarz))
                    {
                        czyJuzJestNaLiscie = true;
                        break;
                    }
                }
                if (!czyJuzJestNaLiscie)
                {
                    listBoxPilkarze.Items.Add(biezacyPilkarz);
                    Clear();
                }
                else
                {
                    var dialog = MessageBox.Show($"{biezacyPilkarz.ToString()} już jest na liście {Environment.NewLine} Czy wyczyścić formularz?", "Uwaga", MessageBoxButton.OKCancel);
                    if (dialog == MessageBoxResult.OK)
                    {
                        Clear();
                    }

                }
            }
        }

        private void buttonUsun_Click(object sender, RoutedEventArgs e)
        {
            //zaimplementuj mechanizm usuwania zaznaczonej pozycji na liście
            //zapytaj czy napewno usunąć
            if (IsNotEmpty(textBoxWEPImie) & IsNotEmpty(textBoxWEPNazwisko))
            {
                //tworzę nowy obiekt Pilkarz i sprawdzam, czy jest na liście, bo można było mu zmienić jakiś parametr
                var biezacyPilkarz = new Pilkarz(textBoxWEPImie.Text.Trim(), textBoxWEPNazwisko.Text.Trim(), (uint)sliderWiek.Value, (uint)sliderWaga.Value);
                
                int index = -1;
                foreach (var p in listBoxPilkarze.Items)
                {
                    var pilkarz = p as Pilkarz;
                    if (pilkarz.isTheSame(biezacyPilkarz))
                    {
                        
                        index = listBoxPilkarze.Items.IndexOf(pilkarz);
                        break;
                    }
                }

                if (index>-1)
                {
                    var dialog = MessageBox.Show($"Czy na pewno chcesz usunąć {biezacyPilkarz.ToString()}?", "Uwaga", MessageBoxButton.OKCancel);
                    if (dialog == MessageBoxResult.OK)
                    {
                        listBoxPilkarze.Items.RemoveAt(index);
                        Clear();
                    }

                }
                else
                {
                    var dialog = MessageBox.Show($"Nie ma takiego piłkarza na liście", "Uwaga", MessageBoxButton.OK);

                    
                }
                listBoxPilkarze.SelectedIndex = -1;
            }
        }

        // zmiana zaznaczenia na liscie piłkarzy
        //uwaga brak zaznaczenia również wywołuje to zdarzenie i wówczas indeks zaznaczonwego
        //wynosi -1
        
        private void listBoxPilkarze_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //wtedy, gdy faktycznie coś jest zaznaczone
            if (listBoxPilkarze.SelectedIndex > -1)
            {
                LoadPlayer((Pilkarz)listBoxPilkarze.SelectedItem);
            }

        }
        //nadpisujemy plik archiwum przy zamknięciu okna
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int n = listBoxPilkarze.Items.Count;
            Pilkarz[] pilkarze = null;
            if (n > 0)
            {
                pilkarze = new Pilkarz[n];
                int index = 0;
                foreach (var o in listBoxPilkarze.Items)
                {
                    pilkarze[index++] = o as Pilkarz;
                }
                Archiwizacja.ZapisPilkarzyDoPliku(plikArchiwizacji,pilkarze);
            }
            
                
        }
        //metoda wykonana po załadowaniu okna
        //ładujemy zawartość pliku z zapisanymi piłkarzami jeśli tylko istnieje
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var pilkarze=Archiwizacja.CzytajPilkarzyZPliku(plikArchiwizacji);
            if(pilkarze!=null)
            foreach (var p in pilkarze)
            {
                listBoxPilkarze.Items.Add(p);
            }

        }
    }
}
