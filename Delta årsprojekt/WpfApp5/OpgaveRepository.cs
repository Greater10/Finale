using System;
using System.Data.SqlClient;
using WpfApp5.Models;

namespace WpfApp5.Data
{
    public class OpgaveRepository
    {                                       // kopier connectring hertil
        private string connectionString = "Data Source=LAPTOP-D68JR2MG\\SQLEXPRESS; Initial Catalog=Delta_projekt; Integrated Security=True";

        public void AddOpgave(OpgaveModel opgave)
            //opgave oprettes i den rigtige tabel

        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Opgaver (CPR, Stue, Isolation, prøver, [særlige forhold], inaktiv, prioritet, dato, kommentar, patientnavn, afdelingID) " +
                               "VALUES (@CPR, @Stue, @Isolation, @prover, @saerligeForhold, @inaktiv, @prioritet, @dato, @kommentar, @patientnavn, @afdelingID)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CPR", opgave.CPR);
                command.Parameters.AddWithValue("@Stue", opgave.Stue);
                command.Parameters.AddWithValue("@Isolation", opgave.Isolation);
                command.Parameters.AddWithValue("@prover", opgave.Prøver ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@saerligeForhold", opgave.SærligeForhold ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@inaktiv", opgave.Inaktiv);
                command.Parameters.AddWithValue("@prioritet", opgave.Prioritet ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@dato", opgave.Dato);
                command.Parameters.AddWithValue("@kommentar", opgave.Kommentar ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@patientnavn", opgave.Patientnavn ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@afdelingID", opgave.AfdelingID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        
    }
}




