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
    /// Interaction logic for Garancija1.xaml
    /// </summary>
    public partial class Garancija1 : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private int? id;

        public Garancija1(bool azuriraj, int? id)
        {
            InitializeComponent();
            dpDatumIsteka.Focus();
            konekcija = kon.NapraviKonekciju();
            this.azuriraj = azuriraj;
            this.id = id;
        }
        public Garancija1()
        {
            InitializeComponent();
            dpDatumIsteka.Focus();
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
                cmd.Parameters.Add("@DatumIsteka", SqlDbType.DateTime).Value = DateTime.Parse(dpDatumIsteka.Text);

                if (azuriraj)
                {
                    cmd.Parameters.Add(@"id", SqlDbType.Int).Value = id;
                    cmd.CommandText = @"update Garancija1
                                       set DatumIsteka = @DatumIsteka,
                                       where GarancijaID = @id";
                    id = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Garancija1
                                            values(@DatumIsteka)";
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
