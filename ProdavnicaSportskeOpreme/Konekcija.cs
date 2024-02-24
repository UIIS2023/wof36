using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProdavnicaSportskeOpreme
{
    internal class Konekcija
    {
        public SqlConnection NapraviKonekciju()
        {
            SqlConnectionStringBuilder ccnSb = new SqlConnectionStringBuilder
            {
                DataSource = @"DESKTOP-53TLF8B\SQLEXPRESS",
                InitialCatalog = "Prodavnica Sportske Opreme",
                IntegratedSecurity = true
            };
            string con = ccnSb.ToString();
            SqlConnection konekcija = new SqlConnection(con);
            return konekcija;
        }
    }
}
