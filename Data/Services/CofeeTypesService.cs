using BisleriumCafe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumCafe.Data.Services
{
    public class CofeeTypesService
    {
        private List<CoffeeTypes> coffeeTypes;

        public CofeeTypesService()
        {
            coffeeTypes = new List<CoffeeTypes>()
            {
                new CoffeeTypes {CoffeeID = Guid.NewGuid(), CoffeeName = "Espresso", CoffeePrice = 140, ImageLocation = "/CoffeeImages/Espresso.jpg" },
                new CoffeeTypes {CoffeeID = Guid.NewGuid(), CoffeeName = "Cappuccino", CoffeePrice = 160, ImageLocation = "/CoffeeImages/Cappuccino.png" },
                new CoffeeTypes {CoffeeID = Guid.NewGuid(), CoffeeName = "Mocha", CoffeePrice = 180, ImageLocation = "/CoffeeImages/Mocha.png" },
                new CoffeeTypes {CoffeeID = Guid.NewGuid(), CoffeeName = "Americano", CoffeePrice = 150, ImageLocation = "/CoffeeImages/Americano.jpg" },
                new CoffeeTypes {CoffeeID = Guid.NewGuid(), CoffeeName = "Macchiato", CoffeePrice = 170, ImageLocation = "/CoffeeImages/Macchiato.jpg" },
                new CoffeeTypes {CoffeeID = Guid.NewGuid(), CoffeeName = "Affogato", CoffeePrice = 270, ImageLocation = "/CoffeeImages/Affogato.jpg" },
                new CoffeeTypes {CoffeeID = Guid.NewGuid(), CoffeeName = "IcedCoffee", CoffeePrice = 220, ImageLocation = "/CoffeeImages/IcedCoffee.jpg" },
                new CoffeeTypes {CoffeeID = Guid.NewGuid(), CoffeeName = "Dalgona", CoffeePrice = 250, ImageLocation = "/CoffeeImages/Dalgona.jpg" },
            };
        }


        public List<CoffeeTypes> GetCoffeeTypes()
        {
            return coffeeTypes;
        }

        public CoffeeTypes? GetCoffeeByID(Guid id)
        {
            return coffeeTypes.FirstOrDefault(c => c.CoffeeID == id);
        }
    }
}
