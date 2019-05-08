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
        public IEnumerable<IDiscount> GetDiscount(IProduct product, UserTypeEnum userType)
        {
            string sql = "select * from Discount where fk_product = @productid and fk_ValidForUserType = @userType";
            using (var connection = SimpleDbConnection())
            {
                var res = connection.Query<Discount>(sql, new { productid = product.Id, userType = (int)userType }).AsList();
                return res;
            }

        }

        public IProduct GetProductById(int productId)
        {
            string sql = "select * from Product where id=@productid";

            using (var connection = SimpleDbConnection())
            {
                return connection.QuerySingleOrDefault<Product>(sql);
            }
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
                    [Quantity] INTEGER NOT NULL,
                    [Price] Decimal NOT NULL,
                    [InsertUtc] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [fk_CategoryId] INTEGER NOT NULL,
                    FOREIGN KEY (fk_CategoryId) REFERENCES Category(fk_CategoryId)
                )");

            //create sell history table
            _dbConnection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [SellHistory] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Quantity] INTEGER NOT NULL,
                    [SellingPrice] Decimal NOT NULL,
                    [InsertUtc] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [Date] DATE DEFAULT CURRENTDATE,
                    [fk_ProductId] INTEGER NOT NULL,

                    FOREIGN KEY (fk_ProductId) REFERENCES Product(fk_ProductId)
                )");


            //Create usertype table
            _dbConnection.ExecuteNonQuery(@"
                  CREATE TABLE IF NOT EXISTS [UserType] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] VARCHAR(50) NOT NULL
                )");

            //Create discounttype table
            _dbConnection.ExecuteNonQuery(@"
                  CREATE TABLE IF NOT EXISTS [DiscountType] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Name] VARCHAR(50) NOT NULL
                )");

            //Create discount table
            _dbConnection.ExecuteNonQuery(@"
                  CREATE TABLE IF NOT EXISTS [Discount] (
                    [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [Amount] Decimal NOT NULL,
                    [InsertUtc] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [ValidTill] TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    [fk_ValidForUserType] INTEGER NOT NULL,
                    [fk_ProductId] INTEGER NOT NULL,
                    [fk_discounttype] INTEGER NOT NULL,
                    [Max] INTEGER NOT NULL,
                    FOREIGN KEY (fk_ProductId) REFERENCES Product(fk_ProductId),
                    FOREIGN KEY (fk_ValidForUserType) REFERENCES UserType(fk_ValidForUserType),
                    FOREIGN KEY (fk_discounttype) REFERENCES DiscountType(discount_type)
                )");


            // Insert Category
            InsertCategory("Beverages");
            InsertCategory("Cleaners");

            // Insert Product
            InsertProduct("Goga Gola", 5, 1, 10.5);
            InsertProduct("Misleri water bottle", 8, 1, 15.0);
            InsertProduct("Neem Soap", 7, 2, 5.5);
            InsertProduct("Karpic Bathroom Cleaner", 10, 2, 15.5);

            //Insert Discount type
            _dbConnection.ExecuteNonQuery("INSERT INTO DiscountType (Name) VALUES ('Percentage')");
            _dbConnection.ExecuteNonQuery("INSERT INTO DiscountType (Name) VALUES ('FlatNumber')");

            //Insert User type
            _dbConnection.ExecuteNonQuery("INSERT INTO UserType (Name) VALUES ('Senior Citizen')");
            _dbConnection.ExecuteNonQuery("INSERT INTO UserType (Name) VALUES ('Employee')");

            InsertDiscount(1, 50, 1, DateTime.UtcNow.AddDays(30), 1, 50);
            InsertDiscount(2, 50, 2, DateTime.UtcNow.AddDays(30), 1, 50);
        }

        private static void InsertDiscount(int discounttype,
        int amount, int producttype,
         DateTime validTill, int validforUserType, int max)
        {
            _dbConnection.ExecuteNonQuery(
                $"INSERT INTO Discount (fk_discounttype, Amount, fk_ProductId, validTill, fk_ValidForUserType, Max) "
            + $" VALUES ('{discounttype}','{amount}','{producttype}','{validTill}','{validforUserType}','{max}')");
        }

        private static void InsertCategory(string name)
        {
            _dbConnection.ExecuteNonQuery($"INSERT INTO Category (Name) VALUES ('{name}')");
        }

        private static void InsertProduct(string name, int quantity, int category, double price)
        {
            _dbConnection.ExecuteNonQuery(
                $"INSERT INTO Product"
                 + $"(Name, Quantity, fk_CategoryId, Price)"
                 + $" VALUES ('{name}', {quantity}, {category}, {price}) ");
        }

        public int GetProductQuantity(int id)
        {
            string sql = "select Quantity from Product where id = @productid";
            using (var connection = SimpleDbConnection())
            {
                return connection.QuerySingleOrDefault<int>(sql, new { productid = id });
            }

        }

        public void UpdateProductQuantity(IProduct product)
        {
            string sql = "Update Product set Quantity = @quantity where id=@id";

            using (var connection = SimpleDbConnection())
            {
                connection.Execute(sql, new { quantity = product.Quantity, id = product.Id, });
            }
        }

        public bool InsertSellHistory(ISellHistory sellHistory)
        {
            try
            {
                string sql = "Insert into SellHistory (Quantity, SellingPrice)"
                            + $" values ({sellHistory.Quantity}, {sellHistory.SellingPrice})";

                using (var connection = SimpleDbConnection())
                {
                    connection.Execute(sql);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IEnumerable<ISellHistory> GetSellHistoryOfToday()
        {
            try
            {
                string sql = "select * from SellHistory where date=current_date";

                using (var connection = SimpleDbConnection())
                {
                    var res = connection.Query<SellHistory>(
                        sql).AsList();
                    return res;

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<ISellHistory> GetSellHistory(string fromdate, string todate)
        {
            try
            {
                string sql = "select * from SellHistory where date between fromdate and todate";

                using (var connection = SimpleDbConnection())
                {
                    var res = connection.Query<SellHistory>(
                        sql).AsList();
                    return res;

                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
