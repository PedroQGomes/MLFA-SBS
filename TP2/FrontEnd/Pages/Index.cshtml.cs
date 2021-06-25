using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Data;
namespace FrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        

        public IndexModel(ILogger<IndexModel> logger,RecomendationsContext dbc)
        {
            _logger = logger;
            dbcontext = dbc;
        }

        public RecomendationsContext dbcontext { get; set; }

        
        public void OnGet()
        {

        }
    }
}
