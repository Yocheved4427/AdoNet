namespace AdoNet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=DESKTOP-KTT59QQ;Initial Catalog=Production;Integrated Security=True";
            DataAccess dataAccess = new DataAccess();
            int numRoW1 = dataAccess.InsertCategory(connectionString);
            int numRoW2 = dataAccess.InsertProduct(connectionString);
            Console.WriteLine($"rows effected {numRoW1}");
            Console.WriteLine($"rows effected {numRoW2}");
            dataAccess.Printcategory(connectionString);
            dataAccess.PrintProduct(connectionString );

        }
    }
}
