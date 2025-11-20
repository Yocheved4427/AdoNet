using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet
{
    public class DataAccess
    {
        public static bool InCategories(string connectionString, string categoryID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select * from Category", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader[0].ToString() == categoryID)
                            return true;

                    }
                }
                else
                {
                    Console.WriteLine("no rows found");
                }
                reader.Close();
            }
            return false;
        }
        public int InsertCategory(string connectionString)
        {
            string categotyName,answer="y";
            int rowAffected = 0;
            while (answer == "y")
            {
                Console.WriteLine("Insert category name");
                categotyName = Console.ReadLine();
                string query = "INSER INTO Category([CategoryName])" + "VALEUS (@CategoryName)";
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@CategoryName", SqlDbType.NVarChar, 50).Value = categotyName;
                    cn.Open();
                    rowAffected += cmd.ExecuteNonQuery();
                    cn.Close();
                    
                }
                Console.WriteLine("Are you want to continue y/n");
                answer = Console.ReadLine();
            }
            return rowAffected;
        }
        public int InsertProduct(string connectionString)
        {
            string categoryId="",productName,description,price,image, answer = "y";
            int rowAffected = 0;
            while (answer == "y")
            {
                bool flag = false;
                while (!flag)
                {
                    Console.WriteLine("Enter category ID");
                    categoryId = Console.ReadLine();
                    flag = InCategories(connectionString, categoryId);
                    if (!flag)
                        Console.WriteLine("Category does not exist");
                }
                Console.WriteLine("Insert category id");
                categoryId = Console.ReadLine();
                Console.WriteLine("Insert product name");
                productName = Console.ReadLine();
                Console.WriteLine("Insert description");
                description = Console.ReadLine();
                Console.WriteLine("Insert price");
                price = Console.ReadLine();
                Console.WriteLine("Insert image url");
                image = Console.ReadLine();
                string query = "INSER INTO Category([CategoryID],[ProductName],[ProdDescription],[Price],[ImagePath])" +
                                "VALEUS (@CategoryID,@ProductName,@ProdDescription,@Price,@ImagePath)";
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@CategoryID", SqlDbType.SmallInt).Value = categoryId;
                    cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar, 50).Value = productName;
                    cmd.Parameters.Add("@ProdDescription", SqlDbType.NVarChar, 50).Value = description;
                    cmd.Parameters.Add("@Price", SqlDbType.Money).Value = price;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 255).Value = image;
                    cn.Open();
                    rowAffected += cmd.ExecuteNonQuery();
                    cn.Close();
                   
                }
                Console.WriteLine("Are you want to continue y/n");
                answer = Console.ReadLine();
            }
            return rowAffected;
        }
        public void Printcategory(string connectionString)
        {
            string query = "select * form Category";
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                 SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}\t{1}",
                      reader[0], reader[1]);
                    }
                    reader.Close();
                     
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
               
            }
        }
        public void PrintProduct(string connectionString)
        {
            string query = "select * form Product";
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                try
                {
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t",
                        reader[0], reader[1], reader[2], reader[2], reader[4], reader[5]);
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();

            }
        }
        }
}
