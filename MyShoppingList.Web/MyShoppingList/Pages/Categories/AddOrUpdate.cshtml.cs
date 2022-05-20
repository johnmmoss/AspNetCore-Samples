using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyShoppingList.Data;
using MyShoppingList.Data.Entities;
using MyShoppingList.Models;

namespace MyShoppingList.Pages.Categories
{
    public class AddOrUpdate : PageModel
    {
        private readonly ShoppingListContext _context;

        public AddOrUpdate(ShoppingListContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CategoryViewModel CategoryViewModel { get; set; }

        [BindProperty]
        public int CategoryID { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            CategoryID = 0;

            if (id.HasValue && id.Value != 0)
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                CategoryID = id.Value;
                CategoryViewModel = new CategoryViewModel()
                {
                    Name = category.Name
                };
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await TryUpdateModelAsync(CategoryViewModel,
                    "categories",
                    x => x.Name))
            {
                //
                // Create New Category
                //
                if (id == 0)
                {
                    var newCategory = new Category()
                    {
                        Name = CategoryViewModel.Name
                    };
                    _context.Categories.Add(newCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                //
                // Edit Existing Category
                //
                var current = await _context.Categories.FindAsync(id);
                if (current == null)
                {
                    return NotFound();
                }
                current.Name = CategoryViewModel.Name;
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
