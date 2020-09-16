using Entidades.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Repositorios
{
    public class MoviesDAO_EF : IDisposable
    //public class GenresDAO_EF : IDisposable

    {
        private MovieContext _context;

        public MoviesDAO_EF()
        {
            _context = new MovieContext();

        }
        public async Task Inserir(Movie m)
        {
            await _context.Movies.AddAsync(m);
            await _context.SaveChangesAsync();

        }
        public async Task<List<Movie>> List()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task Delete(Movie m)
        {
            _context.Movies.Remove(m);
            await _context.SaveChangesAsync();

        }

        public async Task<Movie> GetGenreById(int Id)
        {
            return await _context.Movies.FindAsync(Id);

        }

        public async Task Update(Movie m)
        {
            _context.Movies.Update(m);
            await _context.SaveChangesAsync();

        }



        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);



        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion

    }
}