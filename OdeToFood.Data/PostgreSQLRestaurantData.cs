using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class PostgreSQLRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _db;

        public PostgreSQLRestaurantData(OdeToFoodDbContext db)
        {
            _db = db;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
            _db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            //Returns how many rows been affected
            return _db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if(restaurant != null)
            {
                _db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
           return  _db.Restaurants.Find(id);

        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return _db.Restaurants
                .Where(a => string.IsNullOrEmpty(name) || a.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(b => b.Name)
                .ToList();
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = _db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
