using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProdavnicaSportskeOpreme
{
    /// <summary>
    /// Interaction logic for Kupac.xaml
    /// </summary>
    public partial class Kupac : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private int? id;

        public Kupac(bool azuriraj, int? id)
        {
            InitializeComponent();
            txtImeKupac.Focus();
            konekcija = kon.NapraviKonekciju();
            this.azuriraj = azuriraj;
            this.id = id;
        }
        public Kupac()
        {
            InitializeComponent();
            txtImeKupac.Focus();
            konekcija = kon.NapraviKonekciju();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@Ime", SqlDbType.VarChar).Value = txtImeKupac.Text;
                cmd.Parameters.Add("@Prezime", SqlDbType.VarChar).Value = txtPrezimeKupac.Text;
                cmd.Parameters.Add("@AdresaKupca", SqlDbType.VarChar).Value = txtAdresaKupca.Text;
                cmd.Parameters.Add("@KorisnickoIme", SqlDbType.VarChar).Value = txtKorisnickoIme.Text;
                cmd.Parameters.Add("@Lozinka", SqlDbType.VarChar).Value = txtLozinka.Text;

                if (azuriraj)
                {
                    cmd.Parameters.Add(@"id", SqlDbType.Int).Value = id;
                    cmd.CommandText = @"update Kupac
                                       set Ime = @Ime,
                                           Prezime = @Prezime,
                                           AdresaKupca = @AdresaKupca,
                                           KorisnickoIme = @KorisnickoIme,
                                          Lozinka = @Lozinka 
                                       where KupacID = @id";
                    id = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Kupac
                                            values(@Ime, @Prezime, @AdresaKupca, @KorisnickoIme, @Lozinka)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Unos određenih vrednosti nije validan: {ex.Message} ", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }

            }
        }
        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
