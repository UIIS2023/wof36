using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace ProdavnicaSportskeOpreme
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ucitanaTabela;
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj = false;

        #region Select upiti 

        private static string dostaveSelect = @"select dostavaID as ID, datumDostave as 'Datum dostave', proizvodID as 'ID proizvoda' from Dostava1";
        private static string kupciSelect = @"select kupacID as ID, ime as 'Ime kupca', prezime as 'Prezime kupca' , 
                                                            adresa as 'Adresa kupca',
                                                            username as 'Korisnicko ime', password as Lozinka from Kupac";
        private static string prodavciSelect = @"select prodavacID as ID, ime as ime, prezime as 'Prezime prodavca' from Prodavac";

        private static string placanjaSelect = @"select placanjeID as ID, kupacID as 'ID kupca', prodavacID as 'ID prodavca', tipPlacanjaID as 'ID tipa placanja', suma as 'Suma' from Placanje";
        private static string garancijeSelect = @"select garancijaID as ID, datumIsteka as 'Datum isteka' from Garancija1";
        private static string proizvodiSelect = @"select proizvodID as ID, tipProzivoda as 'Tip proizvoda', cena as 'Cena' from Proizvod";
        private static string tipPlacanjaSelect = @"select tipPlacanjaID as ID, suma as 'Suma', bankaID as 'ID banke', ime as 'Ime', prezime as 'Prezime' from TipPlacanja1";
        private static string transakcijeSelect = @"select transakcijaID as ID, kupacID as 'ID kupca', prodavacID as 'ID prodavca', proizvodID as 'ID proizvoda', tipPlacanjaID as 'ID tipa placanja', datumTransakcije as 'Datum transakcije' from Transakcija";
        #endregion

        #region Select sa uslovom
        private static string selectUsloviDostave = @"select * from Dostava1 where DostavaID=";
        private static string selectUsloviKupci = @"select * from Kupac where KupacID=";
        private static string selectUsloviProdavci = @"select * from Prodavac where ProdavacID=";
        private static string selectUsloviPlacanja = @"select * from Placanje where PlacanjeID=";
        private static string selectUsloviGarancije = @"select * from Garancija1 where GarancijaID=";
        private static string selectUsloviProizvodi = @"select * from Proizvod where ProizvodID=";
        private static string selectUsloviTipPlacanja = @"select * from TipPlacanja1 where TipPlacanjaID= ";
        private static string selectUsloviTransakcije = @"select * from Transakcija where TransakcijaID=";
        #endregion

        #region Delete naredbe
        private static string dostaveDelete = @"delete from Dostava1 where DostavaID=";
        private static string kupciDelete = @"delete  from Kupac where KupacID=";
        private static string prodavciDelete = @"delete from Prodavac where ProdavacID=";
        private static string placanjaDelete = @"delete  from Placanje where PlacanjeID=";
        private static string garancijeDelete = @"delete from Garancija1 where GarancijaID=";
        private static string proizvodiDelete = @"delete from Proizvod where ProizvodID=";
        private static string tipPlacanjaDelete = @"delete from TipPlacanja1 where TipPlacanjaID= ";
        private static string transakcijeDelete = @"delete from Transakcija where TransakcijaID=";

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.NapraviKonekciju();
            UcitajPodatke(kupciSelect);
        }

        private void UcitajPodatke(string selectUpit)
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                if (dataGridCentralni != null)
                {
                    dataGridCentralni.ItemsSource = dataTable.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dataAdapter.Dispose();
                dataTable.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Neuspesno ucitani podaci:{ex.Message}", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnKupci_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(kupciSelect);
        }

        private void btnProdavci_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(prodavciSelect);
        }

        private void btnProizvodi_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(proizvodiSelect);
        }

        private void btnDostave_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dostaveSelect);
        }
        private void btnGarancije_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(garancijeSelect);
        }

        private void btnPlacanja_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(placanjaSelect);
        }

        private void btnTipPlacanja_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(tipPlacanjaSelect);
        }
        private void btnTransakcije_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(transakcijeSelect);
        }
        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if (ucitanaTabela.Equals(kupciSelect))
            {
                prozor = new Kupac();
                prozor.ShowDialog();
                UcitajPodatke(kupciSelect);
            }
            else if (ucitanaTabela.Equals(prodavciSelect))
            {
                prozor = new Prodavac();
                prozor.ShowDialog();
                UcitajPodatke(prodavciSelect);
            }
            else if (ucitanaTabela.Equals(proizvodiSelect))
            {
                prozor = new Proizvod();
                prozor.ShowDialog();
                UcitajPodatke(proizvodiSelect);
            }
            else if (ucitanaTabela.Equals(dostaveSelect))
            {
                prozor = new Dostava1();
                prozor.ShowDialog();
                UcitajPodatke(dostaveSelect);
            }
            else if (ucitanaTabela.Equals(garancijeSelect))
            {
                prozor = new Garancija1();
                prozor.ShowDialog();
                UcitajPodatke(garancijeSelect);
            }
            else if (ucitanaTabela.Equals(placanjaSelect))
            {
                prozor = new Placanje();
                prozor.ShowDialog();
                UcitajPodatke(placanjaSelect);
            }
            else if (ucitanaTabela.Equals(tipPlacanjaSelect))
            {
                prozor = new TipPlacanja();
                prozor.ShowDialog();
                UcitajPodatke(tipPlacanjaSelect);
            }
            else if (ucitanaTabela.Equals(transakcijeSelect))
            {
                prozor = new Transakcija();
                prozor.ShowDialog();
                UcitajPodatke(transakcijeSelect);
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridCentralni.SelectedItems.Count == 1)
            {
                if (ucitanaTabela.Equals(kupciSelect))
                {
                    PopuniFormu(selectUsloviKupci);
                    UcitajPodatke(kupciSelect);
                }
                else if (ucitanaTabela.Equals(prodavciSelect))
                {
                    PopuniFormu(selectUsloviProdavci);
                    UcitajPodatke(prodavciSelect);
                }
                else if (ucitanaTabela.Equals(proizvodiSelect))
                {
                    PopuniFormu(selectUsloviProizvodi);
                    UcitajPodatke(proizvodiSelect);
                }
                else if (ucitanaTabela.Equals(dostaveSelect))
                {
                    PopuniFormu(selectUsloviDostave);
                    UcitajPodatke(dostaveSelect);
                }
                else if (ucitanaTabela.Equals(garancijeSelect))
                {
                    PopuniFormu(selectUsloviGarancije);
                    UcitajPodatke(garancijeSelect);
                }
                else if (ucitanaTabela.Equals(placanjaSelect))
                {
                    PopuniFormu(selectUsloviPlacanja);
                    UcitajPodatke(placanjaSelect);
                }
                else if (ucitanaTabela.Equals(tipPlacanjaSelect))
                {
                    PopuniFormu(selectUsloviTipPlacanja);
                    UcitajPodatke(tipPlacanjaSelect);
                }
                else if (ucitanaTabela.Equals(transakcijeSelect))
                {
                    PopuniFormu(selectUsloviTransakcije);
                    UcitajPodatke(transakcijeSelect);
                }
            }
            else
            {
                MessageBox.Show("Morate selektovati red koji želite da izmenite!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopuniFormu(string selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                var selectedRow = (DataRowView)dataGridCentralni.SelectedItem;
                object a = selectedRow.Row.ItemArray[0];
                int? id = (int?)a;
                SqlCommand cmd = new SqlCommand { Connection = konekcija };

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();

                if (citac.Read())
                {
                    if (ucitanaTabela.Equals(kupciSelect))
                    {
                        Kupac prozorKupac = new Kupac(azuriraj, id);
                        prozorKupac.txtImeKupac.Text = citac["Ime"].ToString();
                        prozorKupac.txtPrezimeKupac.Text = citac["Prezime"].ToString();
                        prozorKupac.txtAdresaKupca.Text = citac["AdresaKupca"].ToString();
                        prozorKupac.txtKorisnickoIme.Text = citac["KorisnickoIme"].ToString();
                        prozorKupac.txtLozinka.Text = citac["Lozinka"].ToString();
                        prozorKupac.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(prodavciSelect))
                    {
                        Prodavac prozorProdavac = new Prodavac(azuriraj, id);
                        prozorProdavac.txtImeProdavca.Text = citac["ImeProdavca"].ToString();
                        prozorProdavac.txtPrezimeProdavca.Text = citac["PrezimeProdavca"].ToString();
                        prozorProdavac.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(proizvodiSelect))
                    {
                        Proizvod prozorProizvod = new Proizvod(azuriraj, id);
                        prozorProizvod.txtTipProizvoda.Text = citac["TipProizvoda"].ToString();
                        prozorProizvod.txtCena.Text = citac["Cena"].ToString();
                        prozorProizvod.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(dostaveSelect))
                    {
                        Dostava1 prozorDostava = new Dostava1(azuriraj, id);
                        prozorDostava.dpDatumDostave.SelectedDate = (DateTime)citac["DatumDostave"];
                        prozorDostava.txtProizvodID.Text = citac["ProizvodID"].ToString();
                        prozorDostava.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(garancijeSelect))
                    {
                        Garancija1 prozorGarancija = new Garancija1(azuriraj, id);
                        prozorGarancija.dpDatumIsteka.Text = citac["DatumIsteka"].ToString();
                        prozorGarancija.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(placanjaSelect))
                    {
                        Placanje prozorPlacanje = new Placanje(azuriraj, id);
                        prozorPlacanje.txtKupacID.Text = citac["KupacID"].ToString();
                        prozorPlacanje.txtProdavacID.Text = citac["ProdavacID"].ToString();
                        prozorPlacanje.txtTipPlacanjaID.Text = citac["TipPlacanjaID"].ToString();
                        prozorPlacanje.txtSuma.Text = citac["Suma"].ToString();
                        prozorPlacanje.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(tipPlacanjaSelect))
                    {
                        TipPlacanja prozorTipPlacanja = new TipPlacanja(azuriraj, id);
                        prozorTipPlacanja.txtSuma.Text = citac["Suma"].ToString();
                        prozorTipPlacanja.txtBankaID.Text = citac["BankaID"].ToString();
                        prozorTipPlacanja.txtIme.Text = citac["Ime"].ToString();
                        prozorTipPlacanja.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorTipPlacanja.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(transakcijeSelect))
                    {
                        Transakcija prozorTransakcija = new Transakcija(azuriraj, id);
                        prozorTransakcija.txtKupacID.Text = citac["KupacID"].ToString();
                        prozorTransakcija.txtProdavacID.Text = citac["ProdavacID"].ToString();
                        prozorTransakcija.txtProizvodID.Text = citac["ProizvodID"].ToString();
                        prozorTransakcija.txtTipPlacanjaID.Text = citac["TipPlacanjaID"].ToString();
                        prozorTransakcija.txtDatumTransakcije.Text = citac["DatumTransakcije"].ToString();
                        prozorTransakcija.ShowDialog();
                    }
                    citac.Close();
                    cmd.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Greška prilikom popunjavanja forme.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void Obrisi(string uslov)
        {
            try
            {
                konekcija.Open();
                var selectedRow = (DataRowView)dataGridCentralni.SelectedItem;
                object a = selectedRow.Row.ItemArray[0];
                int? id = (int?)a;
                SqlCommand cmd = new SqlCommand { Connection = konekcija };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.CommandText = uslov + "@id";
                cmd.ExecuteNonQuery();
                cmd.Dispose();

            }
            catch (SqlException ex)
            {

                MessageBox.Show("Ne možete obrisati element koji se koristi u drugoj tabeli kao strani ključ! ", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)

                    konekcija.Close();
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {

            if (dataGridCentralni.SelectedItems.Count == 1)
            {
                MessageBoxResult pitanje = MessageBox.Show("Da li ste sigurni da želite da obrišete?", "Provera", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (pitanje == MessageBoxResult.Yes)
                {
                    if (ucitanaTabela.Equals(kupciSelect))
                    {
                        Obrisi(kupciDelete);
                        UcitajPodatke(kupciSelect);
                    }
                    else if (ucitanaTabela.Equals(prodavciSelect))
                    {
                        Obrisi(prodavciDelete);
                        UcitajPodatke(prodavciSelect);
                    }
                    else if (ucitanaTabela.Equals(proizvodiSelect))
                    {
                        Obrisi(proizvodiDelete);
                        UcitajPodatke(proizvodiSelect);
                    }
                    else if (ucitanaTabela.Equals(dostaveSelect))
                    {
                        Obrisi(dostaveDelete);
                        UcitajPodatke(dostaveSelect);
                    }
                    else if (ucitanaTabela.Equals(garancijeSelect))
                    {
                        Obrisi(garancijeDelete);
                        UcitajPodatke(garancijeSelect);
                    }
                    else if (ucitanaTabela.Equals(placanjaSelect))
                    {
                        Obrisi(placanjaDelete);
                        UcitajPodatke(placanjaSelect);
                    }
                    else if (ucitanaTabela.Equals(tipPlacanjaSelect))
                    {
                        Obrisi(tipPlacanjaDelete);
                        UcitajPodatke(tipPlacanjaSelect);
                    }
                    else if (ucitanaTabela.Equals(transakcijeSelect))
                    {
                        Obrisi(transakcijeDelete);
                        UcitajPodatke(transakcijeSelect);
                    }
                }
            }
            else
            {
                MessageBox.Show("Morate selektovati red koji želite da izmenite", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
