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
    /// Interaction logic for Transakcija.xaml
    /// </summary>
    public partial class Transakcija : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private int? id;

        public Transakcija(bool azuriraj, int? id)
        {
            InitializeComponent();
            txtKupacID.Focus();
            konekcija = kon.NapraviKonekciju();
            this.azuriraj = azuriraj;
            this.id = id;
        }
        public Transakcija()
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
                cmd.Parameters.Add("@KupacID", SqlDbType.Int).Value = Convert.ToInt32(txtKupacID);
                cmd.Parameters.Add("@ProdavacID", SqlDbType.Int).Value = Convert.ToInt32(txtProdavacID);
                cmd.Parameters.Add("@ProizvodID", SqlDbType.Int).Value = Convert.ToInt32(txtProizvodID);
                cmd.Parameters.Add("@TipPlacanjaID", SqlDbType.Int).Value = Convert.ToInt32(txtTipPlacanjaID);
                cmd.Parameters.Add("@DatumTransakcije", SqlDbType.DateTime).Value = DateTime.Parse(txtDatumTransakcije.Text);

                if (azuriraj)
                {
                    cmd.Parameters.Add(@"id", SqlDbType.Int).Value = id;
                    cmd.CommandText = @"update Transakcija
                                       set KupacID = @KupacID,
                                           ProdavacID = @ProdavacID,
                                           ProizvodID = @ProizvodID,
                                           TipPlacanjaID = @TipPlacanjaID,
                                          DatumTransakcije = @DatumTransakcije 
                                       where TransakcijaID = @id";
                    id = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Transakcija
                                            values(@KupacID, @ProdavacID, @ProizvodID, @TipPlacanjaID, @DatumTransakcije)";
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
