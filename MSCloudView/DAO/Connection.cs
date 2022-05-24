using Microsoft.Data.SqlClient;
using System.Data;

namespace MSCloudView.DAO
{
    public class Connection
    {
        string configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), false)
            .Build().GetSection("ConnectionStrings:DbContext").Value;

        public SqlConnection AbrirConection()
        {
            SqlConnection conection = new(configurationBuilder);
            if (conection.State == ConnectionState.Closed)
                conection.Open();
            return conection;
        }
        public SqlConnection CerrarConection()
        {
            SqlConnection conection = new(configurationBuilder);
            if (conection.State == ConnectionState.Open)
                conection.Close();
            return conection;
        }
    }
}
