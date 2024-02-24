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
    /// Interaction logic for Proizvod.xaml
    /// </summary>
    public partial class Proizvod : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private int? id;

        public Proizvod(bool azuriraj, int? id)
        {
            InitializeComponent();
            txtTipProizvoda.Focus();
            konekcija = kon.NapraviKonekciju();
            this.azuriraj = azuriraj;
            this.id = id;
        }
        public Proizvod()
        {
            InitializeComponent();
            txtTipProizvoda.Focus();
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
                cmd.Parameters.Add("@TipProizvoda", SqlDbType.VarChar).Value = txtTipProizvoda.Text;
                cmd.Parameters.Add("@Cena", SqlDbType.Int).Value = txtCena;
                if (azuriraj)
                {
                    cmd.Parameters.Add(@"id", SqlDbType.Int).Value = id;
                    cmd.CommandText = @"update Proizvod
                                       set TipProizvoda = @TipProizvoda,
                                       Cena = @Cena,
                                       where ProizvodID = @id";
                    id = null;
                }
                else
                {
                    cmd.CommandText = @"insert into Proizvod
                                            values(@TipProizvoda, @Cena)";
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
