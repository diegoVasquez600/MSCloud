using Microsoft.Data.SqlClient;
using System.Data;

namespace MSCloudView.DAO
{
    public class MainBoardDAO
    {
        private Connection connection = new();
        SqlDataReader Reader;
        SqlDataReader ReaderListar;
        DataTable tableListar = new();
        DataTable table = new();
        SqlCommand command = new();

        public DataTable GetMainBoard()
        {
            command.Connection = connection.AbrirConection();
            command.CommandText = "GetMainBoardData";
            command.CommandType = CommandType.StoredProcedure;
            Reader = command.ExecuteReader();
            table.Load(Reader);
            Reader.Close();
            connection.CerrarConection();
            return table;
        }
    }
}
