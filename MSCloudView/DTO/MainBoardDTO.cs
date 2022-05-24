using MSCloudView.DAO;
using MSCloudView.Models;
using System.Data;

namespace MSCloudView.DTO
{
    public class MainBoardDTO
    {
        private List<MainBoard> _mainBoards;
        private MainBoardDAO boardDAO;
        private MainBoard mainBoard;
        private DataTable dataTable;
        public MainBoardDTO()
        {
            boardDAO = new();
        }

        public List<MainBoard> MainBoards()
        {
            _mainBoards = new List<MainBoard>();
            dataTable = boardDAO.GetMainBoard();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                mainBoard = new();
                mainBoard.CountryCode = dataTable.Rows[i]["CountryCode"].ToString();
                mainBoard.AthleteName = dataTable.Rows[i]["AthleteName"].ToString();
                mainBoard.ArranqueKg = int.Parse(dataTable.Rows[i]["ArranqueKG"].ToString());
                mainBoard.EnvionKg = int.Parse(dataTable.Rows[i]["EnvionKG"].ToString());
                mainBoard.TotalPesoKg = int.Parse(dataTable.Rows[i]["TotalPesoKG"].ToString());
                _mainBoards.Add(mainBoard);
            }
            return _mainBoards;
        }
    }
}
