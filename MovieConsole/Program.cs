using Entidades.Model;
using Microsoft.EntityFrameworkCore;

using Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieConsole
{
    class Program
    {

        static void Main(String[] args)
        {
            MovieContext db = new MovieContext();

            #region # LINQ - consultas 16 de setembro


            var q1 = (from m in db.Movies
                      select m.Rating).Average();

            Console.WriteLine("Avaliacao media dos filmes: " + q1);

            var q2 = db.Movies
                       .Average(f => f.Rating);

            Console.WriteLine("\nAvaliacao media dos filmes (method syntax): " + q2);

            var q3 = db.Movies
                       .Where(f => f.Genre.Name == "Action")
                       .Average(f => f.Rating);

            Console.WriteLine("\nAvaliacao media dos filmes de acao:" + q3);

            var q4 = db.Movies
                       .Where(f => f.Genre.Name == "Action")
                       .Max(f => f.Rating);

            Console.WriteLine("\nMelhor avaliacao de um filme de acao: " + q4);


            // explict loading
            var q5 = (from f in db.Movies
                      let melhor = db.Movies.Max(f => f.Rating)
                      where f.Rating == melhor
                      select f).ToList();

            Console.WriteLine("\nFilme melhor avaliado (explict loading): ");

            foreach (Movie m in q5)
            {
                db.Entry(m)
                  .Reference(m => m.Genre)
                  .Load();

                Console.WriteLine("\t{0} {1} {2} ", m.Rating, m.Genre.Name, m.Title);
            }

            // eager loading
            var q6 = from f in db.Movies.Include(movie => movie.Genre)
                     let melhor = db.Movies.Max(f => f.Rating)
                     where f.Rating == melhor
                     select f;

            Console.WriteLine("\nFilme melhor avaliado (eager loading): ");
            foreach (Movie f in q6)
            {
                Console.WriteLine("\t{0} {1} {2} ", f.Rating, f.Genre.Name, f.Title);
            }


            //
            // para utilizar 'lazy loading'
            // https://docs.microsoft.com/en-us/ef/core/querying/related-data/lazy
            //
            var q7 = (from f in db.Movies
                      let melhor = db.Movies
                                     .Where(f => f.Genre.Name == "Action")
                                     .Max(f => f.Rating)
                      where f.Genre.Name == "Action" && f.Rating == melhor
                      select f).FirstOrDefault();

            Console.WriteLine("\nFilme melhor avaliado de Action: ");
           // foreach (Movie f in q7)
           // {
                Console.WriteLine("\t{0} {1}", q7.Rating, q7.Title);
            //   }



            #endregion

            #region # LINQ - consultas
            //Console.WriteLine();
            //Console.WriteLine("Todos os gêneros da base de dados:");
            //foreach (Genre genero in db.Genres)
            //{
            //    Console.WriteLine("{0} \t {1}", genero.GenreId, genero.Name);
            //}

            ////listar todos os filmes de acao
            //Console.WriteLine();
            //Console.WriteLine("Todos os filmes de acao:");

            //List<Movie> filmes = new List<Movie>();
            //foreach (Movie f in db.Movies)
            //{
            //    if (f.GenreId == 1)
            //        filmes.Add(f);
            //}

            //var filmes = from f in db.Movies
            //             where f.GenreId == 1
            //             select f;


            //Console.WriteLine("Todos os filmes do genero 'Action':");
            //var filmesAction = db.Movies.Where(m => m.GenreId == 1);
            //foreach (Movie filme in filmesAction)
            //{
            //    Console.WriteLine("\t{0}", filme.Title);
            //}

            //Console.WriteLine();
            //Console.WriteLine("Todos os filmes do genero 'Action':");
            //var filmesAction2 = from m in db.Movies
            //                    where m.GenreId == 1
            //                    select m;
            //foreach (Movie filme in filmesAction2)
            //{
            //    Console.WriteLine("\t{0}", filme.Title);
            //}

            //Console.WriteLine();
            //Console.WriteLine("Todos os filmes de cada genero:");
            //var generosFilmes = from g in db.Genres.Include(gen => gen.Movies)
            //                    select g;
            ////var generosFilmes2 = db.Genres.Include(gen => gen.Movies).ToList();

            //foreach (var gf in generosFilmes)
            //{
            //    Console.WriteLine("Filmes do genero: " + gf.Name);
            //    foreach (var f in gf.Movies)
            //    {
            //        Console.WriteLine("\t{0}", f.Title);
            //    }
            //}



            //Console.WriteLine();
            //Console.WriteLine("Nomes dos filmes do diretor Quentin Tarantino:");
            //var q1 = from f in db.Movies
            //         where f.Director == "Quentin Tarantino"
            //         select new
            //         {
            //             Ano = f.ReleaseDate.Year,
            //             Titulo = f.Title
            //         };

            // var q2 = db.Movies.Where(f => f.Director == "Quentin Tarantino").Select(f => f.Title);

            //foreach (var item in q1)
            //{
            //    Console.WriteLine("{0} - {1}", item.Ano, item.Titulo);
            //}


            //Console.WriteLine();
            //Console.WriteLine("Nomes e data dos filmes do diretor Quentin Tarantino:");
            //var q3 = from f in db.Movies
            //         where f.Director == "Quentin Tarantino"
            //         select new { f.Title, f.ReleaseDate };
            //foreach (var f in q3)
            //{
            //    Console.WriteLine("{0}\t {1}", f.ReleaseDate.ToShortDateString(), f.Title);
            //}

            //Console.WriteLine();
            //Console.WriteLine("Todos os gêneros ordenados pelo nome:");
            ////var q4 = db.Genres.OrderByDescending(g => g.Name);
            //foreach (var genero in q4)
            //{
            //    Console.WriteLine("{0, 20}\t {1}", genero.Name, genero.Description.Substring(0,30));
            //}
            //Console.WriteLine();
            //Console.WriteLine("Numero de filmes agrupados pelo anos de lançamento:");
            //var q5 = from f in db.Movies
            //         group f by f.ReleaseDate.Year into grupo
            //         select new
            //         {
            //             Chave = grupo.Key,
            //             NroFilmes = grupo.Count()
            //         };

            //foreach (var ano in q5.OrderByDescending(g => g.NroFilmes))
            //{
            //    Console.WriteLine("Ano: {0}  Numero de filmes: {1}", ano.Chave, ano.NroFilmes);

            //}
            //Console.WriteLine("tecle algo para continuar");
            //Console.ReadKey();

            //Console.WriteLine();
            //Console.WriteLine("Projeção do faturamento total, quantidade de filmes e avaliação média agrupadas por gênero:");
            //var q6 = from f in db.Movies
            //         group f by f.Genre.Name into grpGen
            //         select new
            //         {
            //             Categoria = grpGen.Key,
            //             Faturamento = grpGen.Sum(e => e.Gross),
            //             Avaliacao = grpGen.Average(e => e.Rating),
            //             Quantidade = grpGen.Count()
            //         };

            //foreach (var genero in q6)
            //{
            //    Console.WriteLine("Genero: {0}", genero.Categoria);
            //    Console.WriteLine("\tFaturamento total: {0}\n\t Avaliação média: {1}\n\tNumero de filmes: {2}",
            //                    genero.Faturamento, genero.Avaliacao, genero.Quantidade);
            //}
            #endregion

            #region - consultas com casting
            MovieContext cntx2 = new MovieContext();

            Console.WriteLine("\nElenco de Star Wars");
            var query9 = from p in cntx2.Characters.Include("Movie").Include("Actor")
                         where p.Movie.Title == "Star Wars"
                         select p;

            foreach (var res in query9)
            {
                Console.WriteLine("\t{0}\t {1}", res.Character, res.Actor.Name);
            }

            Console.WriteLine("\nAtores que desempenharam James Bond");
            var query10 = from p in cntx2.Characters.Include("Movie").Include("Actor")
                          where p.Character == "James Bond"
                          orderby p.Movie.ReleaseDate.Year
                          select p;

            foreach (var res in query10)
            {
                Console.WriteLine("\t{0}\t {1}\t {2}", res.Movie.ReleaseDate.Year, res.Actor.Name, res.Movie.Title);
            }
            #endregion

            Console.ReadKey();



        }


        static void Main0(string[] args)
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
