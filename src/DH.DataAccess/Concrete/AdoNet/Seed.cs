using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DH.DataAccess.Concrete.AdoNet
{
    public static class Seed
    {
        public static void ConnectAndSeed()
        {
            SqlConnection myConn = new SqlConnection("Server=ms-sql-server,1433;Initial Catalog=master;User ID=SA;Password=Pa708WoRD");

            String str = "IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'DHDb') " +
                            "BEGIN " +
                                "CREATE DATABASE DHDb " +
                             "END";
            SqlCommand myCommand = new SqlCommand(str, myConn);
            myConn.Open();
            myCommand.ExecuteNonQuery();

            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
            //if table exists yapılacak
            SqlConnection DHDbConn = new SqlConnection("Server=ms-sql-server,1433;Initial Catalog=DHDb;User ID=SA;Password=Pa708WoRD");
            String str2 = "USE DHDb; " +
                            "IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Posts')" +
                            "BEGIN " +
                                "CREATE TABLE Posts(" +
                                    "Id int IDENTITY(1,1) PRIMARY KEY," +
                                    "RootId int," +
                                    "FirstName nvarchar(50)," +
                                    "LastName nvarchar(50)," +
                                    "Email nvarchar(50)," +
                                    "PostTitle nvarchar(50)," +
                                    "Content nvarchar(max)," +
                                    "DatePublished datetime" +
                                ") " +
                             "END";

            SqlCommand myCommand2 = new SqlCommand(str2, DHDbConn);
            myCommand2.Parameters.AddWithValue("@date1", DateTime.Now);
            DHDbConn.Open();
            myCommand2.ExecuteNonQuery();

            if (DHDbConn.State == ConnectionState.Open)
            {
                DHDbConn.Close();
            }

        }
    }
}

