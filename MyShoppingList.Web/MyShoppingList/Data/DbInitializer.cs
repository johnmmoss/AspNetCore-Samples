using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShoppingList.Data.Entities;

namespace MyShoppingList.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ShoppingListContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var fruit = new Category()
            {
                Name = "Fruit"
            };

            var vegetables = new Category()
            {
                Name = "Vegetable"
            };

            var frozen = new Category()
            {
                Name = "Frozen"
            };

            var storeCupboard = new Category()
            {
                Name = "Store Cupboard"
            };

            context.Categories.Add(fruit);
            context.Categories.Add(vegetables);
            context.Categories.Add(frozen);
            context.Categories.Add(storeCupboard);

            context.SaveChanges();
        }
    }
}
