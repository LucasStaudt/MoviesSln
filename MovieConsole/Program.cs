using Persistencia.Entidades;
using Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieContext _context = new MovieContext();

            Genre g1 = new Genre()
            {
                Name = "Comedia",
                Description = DateTime.Now.ToLongTimeString()
            };
            Genre g2 = new Genre()
            {
                Name = "Ficcao",
                Description = DateTime.Now.ToLongTimeString()
            };

            _context.Genres.Add(g1); 
            _context.Genres.Add(g2);

            Console.WriteLine("id de g1: " + g1.GenreId);

            _context.SaveChanges();

            Console.WriteLine("id de g1: " + g1.GenreId);

            g1.Description = "Alterado";

            _context.SaveChanges();

            List<Genre> genres = _context.Genres.ToList();
            foreach (Genre g in genres)
            {
                Console.WriteLine(String.Format("{0,2} {1,-10} {2}",
                     g.GenreId, g.Name, g.Description));
            }


        }
       
    }
}
