//Default
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//SQL Server
using System.Data;
using System.Data.SqlClient;

//Geocoding
using System.Configuration;
using System.IO;
using System.Xml;
using System.Net;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string strcon = "Data source=10.68.54.16,1433;initial catalog=Spatial_Test;User ID=sa;Password=123456";
            SqlConnection conexao = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.tbrgeocod", conexao);

            try
            {
                int count = 0;
                List<string> list = new List<string>();
                conexao.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader[1].ToString() + "," + reader[2].ToString() + "," + reader[3].ToString() + "," + reader[4].ToString() + "," + reader[5].ToString() + "," + reader[6].ToString());
                    Console.WriteLine(list[count].ToString());
                    count += 1;
                }

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro " + ex.Message);
                Console.ReadLine();
                throw;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
