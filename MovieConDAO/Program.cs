
using Dominio;
using Entidades.Model;
using Entidades.ViewModel;
using Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieConDAO
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FachadaMovies fachada = new FachadaMovies();

            var relat = await fachada.getRelatorioGeneros();

            Console.WriteLine("Relatorio - generos");

            foreach (GenreSummary gen in relat.OrderByDescending(g=>g.Faturamento))
            {
                Console.WriteLine("\nGenero: {0}", gen.Categoria);
                Console.WriteLine("\tFaturamento total: {0:C}\n\tAvaliação média: {1,6:N2}\n\tNumero de filmes: {2,2}",
                                gen.Faturamento, gen.Avaliacao, gen.Quantidade);
            }
        }

        

        static async Task Main0(string[] args)
        {
            GenresDAO_EF _dao = new GenresDAO_EF();
          //  FachadaNegocio _facahda = new FachadaNegocio();

            Genre g1 = new Genre()
            {
                Name = "Comedia DAO",
                Description = DateTime.Now.ToLongTimeString()
            };
            Genre g2 = new Genre()
            {
                Name = "Ficcao DAO",
                Description = DateTime.Now.ToLongTimeString()
            };

            Console.WriteLine("id de g1: " + g1.GenreId);

            //await _dao.Add(g1);
            //await _dao.Add(g2);

            //List<Genre> lista = new List<Genre>()
            //{
            //    g1, g2,
            //    new Genre() {Name="Acao", Description="Filmes de acao"},
            //    new Genre() {Name = "Supense", Description = "Filmes de suspense"}
            //};

            //foreach (Genre g in lista)
            //{
            //    await _dao.Inserir(g);

            //}

            //Movie m = new Movie()
            //{
            //    Title = "Pulp Fiction",
            //    Genre = g2
            //};

            ////_dao.Add(m);

            //Console.WriteLine("id de g1: " + g1.GenreId);

            //await _dao.Delete(g1);

            //g2.Description = "alterado em" + DateTime.Now.ToLongTimeString();

            //await _dao.Update(g2);
            
            List<Genre> genres = await _dao.List();
            foreach (Genre g in genres)
            {
                Console.WriteLine(String.Format("{0,2} {1,-10} {2}",
                     g.GenreId, g.Name, g.Description.Substring(0,40)));
            }
            Console.WriteLine("Done!");
        }
    }
}
