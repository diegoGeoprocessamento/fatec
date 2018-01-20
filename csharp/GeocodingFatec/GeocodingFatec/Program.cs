//Default
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Geocoding
using GeoLib;
// SQL Server
using System.Data;
using System.Data.SqlClient;

namespace GeocodingFatec
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Data source=fe80::414e:6ed4:547:5e66%9;initial catalog=projeto;User ID=sa;Password=123");
            string sqlselect = ("SELECT * FROM dbo.tbrgeocod");
            SqlCommand select = new SqlCommand(sqlselect, connection);

            try
            {
                int countselect = 0;
                List<string> listaddress = new List<string>();
                List<string> listcity = new List<string>();
                List<string> liststate = new List<string>();
                List<string> listzip = new List<string>();
                List<string> listlat = new List<string>();
                List<string> listlng = new List<string>();
                connection.Open();
                SqlDataReader reader = select.ExecuteReader();

                while (reader.Read())
                {
                    listaddress.Add(reader[1].ToString() + "," + reader[2].ToString() + "," + reader[3].ToString());
                    listcity.Add(reader[4].ToString());
                    liststate.Add(reader[5].ToString());
                    listzip.Add(reader[6].ToString());

                    Console.WriteLine("Linha " + countselect + " verificada");

                    var g = new FatecGeo();
                    g.Key = "AIzaSyDwkRVrWvNjSqi - OnhvcvPGDu6 - rl1a4u4";
                    var r = g.GoogleGeoCodeInfo(new Address { Address1 = listaddress[countselect], City = listcity[countselect], State = liststate[countselect], Zip = listzip[countselect] });
                    if (g.GeoResult == FatecGeo.Result.OK)
                    {
                        string lat = r.Result[0].Geometry.Location.Lat;
                        listlat.Add(lat);
                        string lng = r.Result[0].Geometry.Location.Long;
                        listlng.Add(lng);
                    }
                    countselect += 1;
                }
                connection.Close();
                connection.Open();
                int countinsert = 0;

                while (countselect > 0)
                {
                    string sqlinsert = "INSERT INTO dbo.tbwgeocod(lat, lon) VALUES (" + listlat[countinsert] + "," + listlng[countinsert] + ");";
                    SqlCommand insert = new SqlCommand(sqlinsert, connection);
                    insert.ExecuteNonQuery();

                    Console.WriteLine("Linha " + countinsert + " inserida");

                    countinsert += 1;
                    countselect -= 1;
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
                connection.Close();
            }
        }
    }
}
