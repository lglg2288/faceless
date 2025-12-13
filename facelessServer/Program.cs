using System.Data.SQLite;
using System.Text;
using MessServiceApp;

namespace facelessServer
{
    public class Program
    {
        private readonly SQLiteConnection _DbConnection;
        private static Program _Instance = new Program();
        private Program()
        {
            
            _DbConnection = new SQLiteConnection(@"Data Source=database\users.db");
            _DbConnection.Open();
        }
        static void Main()
        {
            var _idSender = new SenderIDService();
            _idSender.OnUsernameReceived += _Instance.ReceivedIdHandler;
        }

        private ulong ReceivedIdHandler(string username)
        {
            using var DbCommand = _DbConnection.CreateCommand();
            List<string> l = new List<string>();

            DbCommand.CommandText = $"SELECT Name FROM Users WHERE Name = @name";
            DbCommand.Parameters.AddWithValue("@name", username);

            using var reader = DbCommand.ExecuteReader();
                
            while (reader.Read())
            {
                string? CurrentName = reader.GetValue(0).ToString();
                if (CurrentName != null)
                    l.Add(CurrentName);
            }

            return 0;
        }
    }
}
