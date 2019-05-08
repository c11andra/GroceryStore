using System;
using System.Collections.Generic;
using PocServer.StoreManagement.Interfaces;
using PocServer.Data.Interfaces;
using System.Linq;

namespace PocServer.StoreManagement
{
    public class StoreManager : IStoreManager
    {
        private readonly IDataAccess _dataAccess;

        public StoreManager(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public IEnumerable<IProduct> GetProducts()
        {
            return _dataAccess.GetProducts();
        }

        public ISellResponse Sell(ISellRequest sellRequest)
        {
            var sellResonse = new SellResponse();

            foreach (var product in sellRequest.Products)
            {
                //get discount based on usertype and product
                var discounts = _dataAccess.GetDiscount(product, sellRequest.UserType);
                var soldProduct = new SoldProduct();
                bool isSold = false;
                if (discounts == null)
                {
                    var sellingPrice = product.Price;
                    isSold = Sell(product, sellingPrice);
                }
                else
                {
                    //if there are multiple discounts choose the max
                    var discount = ChooseMaxDiscount(product.Price, discounts);
                    var sellingPrice = product.Price - discount;
                    isSold = Sell(product, sellingPrice);
                }

                sellResonse.SoldProducts.Add(soldProduct);
            }
            return sellResonse;
        }

        private bool Sell(IProduct product, double sellingPrice)
        {
            //get available quantity
            int availableQuantity = _dataAccess.GetProductQuantity(product.Id);

            //sell only when enough quantity is available
            if (availableQuantity > product.Quantity)
            {
                _dataAccess.UpdateProductQuantity(product);
                return true;
            }
            return false;         
        }

        private double ChooseMaxDiscount(double price, IEnumerable<IDiscount> discounts)
        {
            var discount1 = discounts.ElementAt(0);
            var max = discount1.Amount;

            if (discount1.DiscountType == DiscountTypeEnum.Percentage)
            {
                max = price * discount1.Amount / 100.0;
            }

            for (int i = 1; i < discounts.Count(); i++)
            {
                var discount = discounts.ElementAt(i);
                var discountAmount = discount.Amount;
                if (discount.DiscountType == DiscountTypeEnum.Percentage)
                {
                    discountAmount = price * discount.Amount / 100.0;
                }

                if (discountAmount > max)
                {
                    max = discountAmount;
                }
            }
            return max;
        }

        public ISellReportResponse GetReportOfToday()
        {
            IEnumerable<ISellHistory> historyList = _dataAccess.GetSellHistoryOfToday();
            return this.CreateSellReport(historyList);
            
        }
        public ISellReportResponse GetSellReport(string fromdate, string todate)
        {
            var report = new SellReportResponse();

            IEnumerable<ISellHistory> historyList = _dataAccess.GetSellHistory(fromdate, todate);
            return this.CreateSellReport(historyList);
        }
        private ISellReportResponse CreateSellReport(IEnumerable<ISellHistory> historyList)
        {
            var response = new SellReportResponse();
            var soldProducts = new List<SoldProduct>();

            foreach (var historyEntry in historyList)
            {
                var soldProduct = new SoldProduct();
                soldProduct.Quantity = historyEntry.Quantity;
                soldProduct.Id = historyEntry.ProductId;
                soldProduct.Name = _dataAccess.GetProductById(historyEntry.ProductId).Name;
                soldProduct.SellingPrice = historyEntry.SellingPrice;

                soldProducts.Add(soldProduct);
            }

            response.Products = soldProducts;
            return response;
        }
    }
}
