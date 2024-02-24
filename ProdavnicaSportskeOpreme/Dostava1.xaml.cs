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
    /// Interaction logic for Dostava1.xaml
    /// </summary>
    public partial class Dostava1 : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private int? id;

        public Dostava1(bool azuriraj, int? id)
        {
            InitializeComponent();
            txtProizvodID.Focus();
            konekcija = kon.NapraviKonekciju();
            this.azuriraj = azuriraj;
            this.id = id;
        }
        public Dostava1()
        {
            InitializeComponent();
            txtProizvodID.Focus();
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
                cmd.Parameters.Add("@ProizvodID", SqlDbType.Int).Value = txtProizvodID;
                cmd.Parameters.Add("@DatumDostave", SqlDbType.DateTime).Value = DateTime.Parse(dpDatumDostave.Text);
                if (azuriraj)
                {
                    cmd.Parameters.Add(@"id", SqlDbType.Int).Value = id;
                    cmd.CommandText = @"update Proizvod
                                       set ProizvodID = @ProizvodID,
                                       DatumDostave = @DatumDostave,
                                       where DostavaID = @id";
                    id = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Dostava1
                                            values(@ProizvodID, @DatumDostave)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Neke od unetih vrednosti nisu validne:{ex.Message} ", "Greška!", MessageBoxButton.OK, MessageBoxImage.Error);
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
