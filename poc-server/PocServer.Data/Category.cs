using System;
using PocServer.Data.Interfaces;

namespace PocServer.Data
{
    public class Category: ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
