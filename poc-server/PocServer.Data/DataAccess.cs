using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using PocServer.Data.Interfaces;
using Dapper;
namespace PocServer.Data
{
    public class DataAccess : IDataAccess
    {
        private static SQLiteConnection _dbConnection;
        private static string _dbFilePath = string.Empty;

        public static SQLiteConnection SimpleDbConnection()
        {
            return new SQLiteConnection(string.Format("Data Source={0};Version=3;", _dbFilePath));
        }

        public IEnumerable<IProduct> GetProducts()
        {
            string sql = "select * from Product p inner join Category c on p.fk_CategoryId = c.Id";

            using (var connection = SimpleDbConnection())
            {
                var res = connection.Query<Product, Category, Product>(
                    sql,
                    (product, category) =>
                    {
                        product.Category = category;
                        return product;
                    },
                    splitOn: "Id");
                return res;
            }
        }

        public static void CreateAndOpenDb(string path)
        {
            _dbFilePath = path;
            if (!File.Exists(_dbFilePath))
            {
                SQLiteConnection.CreateFile(_dbFilePath);
            }
            _dbConnection = SimpleDbConnection();
            _dbConnection.Open();
        }

        public static void SeedData()
        {
            //Create Category table
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Category] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] NVARCHAR(128) NOT NULL,
                    [InsertUtc] TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                )");


            // Create a Product table
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Product] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] NVARCHAR(128) NOT NULL,
                    [Price] Decimal NOT NULL,
                    [InsertUtc] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [fk_CategoryId] INTEGER NOT NULL,
                    FOREIGN KEY (fk_CategoryId) REFERENCES Category(fk_CategoryId)
                )");

            // Insert Category

            _dbConnection.ExecuteNonQuery(@"
                INSERT INTO Category (Name)
                VALUES ('Beverages')");

            _dbConnection.ExecuteNonQuery(@"
            INSERT INTO Category (Name)
            VALUES ('Cleaners')");


            // Insert Product
            _dbConnection.ExecuteNonQuery(@"
                INSERT INTO Product
                    (Name, fk_CategoryId, Price)
                VALUES
                    ('Goga Cola','1', 10.5)
                    ");

            _dbConnection.ExecuteNonQuery(@"
                INSERT INTO Product
                    (Name, fk_CategoryId, Price)
                VALUES
                    ('Misleri water bottle', '1', 15.0)
                    ");

            _dbConnection.ExecuteNonQuery(@"
                INSERT INTO Product
                    (Name, fk_CategoryId, Price)
                VALUES
                    ('Neem Soap', '2', 5.0)
                    ");

            _dbConnection.ExecuteNonQuery(@"
            INSERT INTO Product
                (Name, fk_CategoryId, Price)
            VALUES
                ('Karpic Bathroom Cleaner', '2', 15.5)
                ");
        }
    }
}
