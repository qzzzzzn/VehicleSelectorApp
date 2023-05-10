using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using VehicleSelectorApp;

namespace VehicleSelectorApp
{
    public class DataService
    {
        private readonly AppDBContext _dbContext;

        public DataService()
        {
            _dbContext = new AppDBContext();
        }

        public List<Car> SearchCars(string brand, string model, int year, decimal minPrice, decimal maxPrice, string color, int mileage, string condition, string type)
        {
            IQueryable<Car> query = _dbContext.Car.Include("Image").Include("CarDescription");

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(c => c.brand.Contains(brand));
            }

            if (!string.IsNullOrEmpty(model))
            {
                query = query.Where(c => c.model.Contains(model));
            }

            if (year > 0)
            {
                query = query.Where(c => c.year == year);
            }

            if (minPrice > 0)
            {
                query = query.Where(c => c.price >= minPrice);
            }

            if (maxPrice < decimal.MaxValue)
            {
                query = query.Where(c => c.price <= maxPrice);
            }

            if (!string.IsNullOrEmpty(color))
            {
                query = query.Where(c => c.color.Contains(color));
            }

            if (mileage > 0)
            {
                query = query.Where(c => c.mileage == mileage);
            }

            if (!string.IsNullOrEmpty(condition))
            {
                query = query.Where(c => c.condition.Contains(condition));
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(c => c.type.Contains(type));
            }

            List<Car> cars = query.ToList();
            foreach (var car in cars)
            {
                if (car.Image != null)
                {
                    car.Image.image1 = _dbContext.Image.Find(car.Image.id)?.image1;
                }
            }
            return cars;
        }

        public string GetCarDescription(int carId)
        {
            var carDescription = _dbContext.CarDescription.FirstOrDefault(cd => cd.Car_id == carId);
            return carDescription?.description;
        }

        public List<Part> SearchParts(string name, string description, decimal minPrice, decimal maxPrice)
        {
            IQueryable<Part> query = _dbContext.Part.Include("PartImage");

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.name.Contains(name));
            }

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(p => p.description.Contains(description));
            }

            if (minPrice > 0)
            {
                query = query.Where(p => p.price >= minPrice);
            }

            if (maxPrice < decimal.MaxValue)
            {
                query = query.Where(p => p.price <= maxPrice);
            }


            List<Part> parts = query.ToList();
            foreach (var part in parts)
            {
                if (part.PartImage != null)
                {
                    part.PartImage.image = _dbContext.PartImage.Find(part.PartImage.id)?.image;
                }
            }
            return parts;
        }
    }
}
