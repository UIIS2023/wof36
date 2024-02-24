using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ProdavnicaSportskeOpreme
{
    class Popuni1
    {
        public static void Popuni(ComboBox cb, string selectUpit)
        {
            Konekcija kon = new Konekcija();
            SqlConnection konekcija = new SqlConnection();
            try
            {
                konekcija = kon.NapraviKonekciju();
                konekcija.Open();

                SqlDataAdapter da = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (cb != null)
                {
                    cb.ItemsSource = dt.DefaultView;
                }
                da.Dispose();
                dt.Dispose();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();

            }
        }
    }
}
