using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant{ ID = 1, Name ="Pizza", Location = "Manchester", Cuisine = CuisineType.Italian},
                new Restaurant{ ID = 2, Name ="Burger", Location = "Salford", Cuisine = CuisineType.None},
                new Restaurant{ ID = 3, Name ="Curry", Location = "Manchester", Cuisine = CuisineType.Indian }
            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.FirstOrDefault(a => a.ID == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return restaurants
                .Where(a => string.IsNullOrEmpty(name) || a.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(b => b.Name)
                .ToList();
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.ID == updatedRestaurant.ID);

            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }

            return restaurant;
        }
        public int Commit()
        {
            return 0;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.ID = restaurants.Max(r => r.ID) + 1;

            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.ID == id);
            if (restaurant != null)
            {
                restaurants.Remove(restaurant);
            }

            return restaurant;

        }
    }
}
