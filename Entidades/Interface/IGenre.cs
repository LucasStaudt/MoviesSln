using Entidades.Model;
using Entidades.ViewModel;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interface
{
    public interface IGenres
    {
        Task Inserir(Genre genre);
        Task<List<Genre>> List();
        Task Delete(Genre genre);
        Task<Genre> GetGenreById(int Id);
        Task Update(Genre genre);

        Task<ICollection<GenreSummary>> getGenreSummary();
    }
}
