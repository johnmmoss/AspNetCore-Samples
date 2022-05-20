using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyShoppingList.Data;
using MyShoppingList.Data.Entities;

namespace MyShoppingList.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ShoppingListContext _context;

        public IndexModel(ShoppingListContext context)
        {
            _context = context;
        }

        public IList<Category> Categories { get; set; } = default!;

        public async Task OnGet()
        {
            Categories = await _context.Categories.ToListAsync();
        }
    }
}
