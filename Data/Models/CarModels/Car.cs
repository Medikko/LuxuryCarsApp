using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuxuryCarsApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Engine { get; set; }
        public string Brand { get; set; }
        public string ImageUrl { get; set; }
        public BodyType BodyType { get; set; }
        public FuelType FuelType { get; set; }
        public int HorsePower { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public double Mileage { get; set; }
        public int ManufactureId { get; set; }
        public virtual Manufacture Manufacture { get; set; }
    }
}
