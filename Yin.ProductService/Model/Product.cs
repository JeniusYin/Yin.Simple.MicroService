using System;

namespace Yin.ProductService
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
