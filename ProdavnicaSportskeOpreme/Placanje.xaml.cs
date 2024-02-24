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
    /// Interaction logic for Placanje.xaml
    /// </summary>
    public partial class Placanje : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private int? id;

        public Placanje(bool azuriraj, int? id)
        {
            InitializeComponent();
            txtKupacID.Focus();
            konekcija = kon.NapraviKonekciju();
            this.azuriraj = azuriraj;
            this.id = id;
        }
        public Placanje()
        {
            InitializeComponent();
            txtKupacID.Focus();
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
                cmd.Parameters.Add("@KupacID", SqlDbType.Int).Value = txtKupacID;
                cmd.Parameters.Add("@ProdavacID", SqlDbType.Int).Value = txtProdavacID;
                cmd.Parameters.Add("@TipPlacanjaID", SqlDbType.Int).Value = txtTipPlacanjaID;
                cmd.Parameters.Add("@Suma", SqlDbType.Int).Value = txtSuma.Text;

                if (azuriraj)
                {
                    cmd.Parameters.Add(@"id", SqlDbType.Int).Value = id;
                    cmd.CommandText = @"update Placanje
                                       set KupacID = @KupacID,
                                           ProdavacID = @ProdavacID,
                                           TipPlacanjaID = @TipPlacanjaID,
                                           Suma = @Suma,
                                       where PlacanjeID = @id";
                    id = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Placanje
                                            values(@KupacID, @ProdavacID, @TipPlacanjaID, @Suma)";
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
