using System.Collections.Generic;
using Lakeside.Models;
namespace Lakeside.Models.ViewModels
{
    public class ViewFilmVM
    {
        public Film selectedfilm { get; set; }
        public List<FilmReview> reviewlist { get; set; }
    }
}