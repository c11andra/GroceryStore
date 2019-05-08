using System;
using System.Collections.Generic;
using PocServer.StoreManagement.Interfaces;
using PocServer.Data.Interfaces;
using System.Linq;

namespace PocServer.StoreManagement
{
    public class StoreManager:IStoreManager
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
                var discounts = _dataAccess.GetDiscount(product);
                var soldProduct = new SoldProduct();
                bool isSold = false;
                if(discounts == null)
                {
                    var sellingPrice = product.Price;
                    isSold = Sell(product, sellingPrice);
                }
                else
                {
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
            int availableQuantity = _dataAccess.GetProductQuantity(product.Id);

            if (availableQuantity > product.Quantity)
            {
                _dataAccess.UpdateProductQuantity(product);
            }
            var sellHistory = new SellHistoryDto();

            return _dataAccess.InsertSellHistory(sellHistory);
        }

        private double ChooseMaxDiscount(double price , IEnumerable<IDiscount> discounts)
        {
            var discount1 = discounts.ElementAt(0);
            var max = discount1.Amount;

            if(discount1.DiscountType == DiscountTypeEnum.Percentage )
            {
                max = price * discount1.Amount/100.0;
            }

            for (int i = 1; i < discounts.Count(); i++)
            {
                var discount = discounts.ElementAt(i);
                var discountAmount = discount.Amount;
                if(discount.DiscountType == DiscountTypeEnum.Percentage )
                {
                    discountAmount = price * discount.Amount/100.0;
                }
                
                if( discountAmount > max)
                {
                    max = discountAmount;
                }
            }
            return max;
        }
    }
}
