using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.Services;
using FrontEnd.Models;
using FrontEnd.Data;

namespace FrontEnd.Pages
{
    public class recomendedModel : PageModel
    {
        public RecomendationsContext dbcontext { get; set; }

        public RatingService _ratinggservice { get; set; }

        public IEnumerable<Book> bookslist;

        [BindProperty(SupportsGet = true)]
        public string User { get; set; }
        
        
        public recomendedModel (RecomendationsContext dbc,RatingService ratinggservice)
        {
            _ratinggservice = ratinggservice;
            dbcontext = dbc;
        }
        public void OnGet()
        {


        }
    }
}
