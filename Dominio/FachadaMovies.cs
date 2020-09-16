using Entidades.Interface;
using Entidades.ViewModel;
using Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class FachadaMovies
    {
        IGenres genreDB = new GenresDAO_EF();

        public async Task<ICollection<GenreSummary>> getRelatorioGeneros()
        {
            return await genreDB.getGenreSummary();
        }
    }
}
