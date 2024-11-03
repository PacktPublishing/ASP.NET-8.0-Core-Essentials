using System.Data.SqlClient;

SqlConnection sql = new SqlConnection("Server=localhost;Database=DbStore;user id=sa;password=Password123");

try
{
    sql.Open();
    Console.WriteLine("Connection opened");

    SqlCommand cmd = new SqlCommand("SELECT * FROM Product", sql);
    SqlDataReader reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        Console.WriteLine($"{reader[0]} - {reader[1]} - {reader[2]:C2}");
    }
}
catch (SqlException ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    sql.Close();
    Console.WriteLine("Connection closed");
}