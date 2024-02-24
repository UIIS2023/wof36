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
    /// Interaction logic for TipPlacanja.xaml
    /// </summary>
    public partial class TipPlacanja : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private int? id;

        public TipPlacanja(bool azuriraj, int? id)
        {
            InitializeComponent();
            txtSuma.Focus();
            konekcija = kon.NapraviKonekciju();
            this.azuriraj = azuriraj;
            this.id = id;
        }
        public TipPlacanja()
        {
            InitializeComponent();
            txtSuma.Focus();
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
                cmd.Parameters.Add("@Suma", SqlDbType.Int).Value = txtSuma;
                cmd.Parameters.Add("@BankaID", SqlDbType.VarChar).Value = txtBankaID;
                cmd.Parameters.Add("@Ime", SqlDbType.VarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@Prezime", SqlDbType.VarChar).Value = txtPrezime.Text;

                if (azuriraj)
                {
                    cmd.Parameters.Add(@"id", SqlDbType.Int).Value = id;
                    cmd.CommandText = @"update TipPlacanja1
                                       set Suma = @Suma,
                                           BankaID = @BankaID,
                                           Ime = @Ime,
                                           Prezime = @Prezime
                                       where TipPlacanjaID = @id";
                    id = null;
                }
                else
                {
                    cmd.CommandText = @"insert into TipPlacanja1
                                            values(@Suma, @BankaID, @Ime, @Prezime)";
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
