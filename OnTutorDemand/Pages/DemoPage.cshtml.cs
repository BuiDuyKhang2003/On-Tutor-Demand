using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnTutorDemand.Pages
{
    public class DemoPageModel : PageModel
    {
        public string Message { get; set; } = "Hello from code behind";
        public void OnGet() //Action method
        {
            ViewData["Message"] = "tailor";
           
        }
    }
}
