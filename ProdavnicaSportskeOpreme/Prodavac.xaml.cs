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
    /// Interaction logic for Prodavac.xaml
    /// </summary>
    public partial class Prodavac : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private int? id;

        public Prodavac(bool azuriraj, int? id)
        {
            InitializeComponent();
            txtImeProdavca.Focus();
            konekcija = kon.NapraviKonekciju();
            this.azuriraj = azuriraj;
            this.id = id;
        }
        public Prodavac()
        {
            InitializeComponent();
            txtImeProdavca.Focus();
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
                cmd.Parameters.Add("@ImeProdavca", SqlDbType.VarChar).Value = txtImeProdavca.Text;
                cmd.Parameters.Add("@PrezimeProdavca", SqlDbType.VarChar).Value = txtPrezimeProdavca.Text;
                if (azuriraj)
                {
                    cmd.Parameters.Add(@"id", SqlDbType.Int).Value = id;
                    cmd.CommandText = @"update Prodavac
                                       set ImeProdavca = @ImeProdavca,
                                           PrezimeProdavca = @PrezimeProdavca
                                       where ProdavacID = @id";
                    id = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Prodavac
                                            values(@ImeProdavca, @PrezimeProdavca)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unos određenih vrednosti nije validan:{ex.Message} ", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
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
