using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using Persistencia.Entidades;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Repositorios
{
    public class GenresDAO_EF : IDisposable

    {
        private MovieContext _context;

        public GenresDAO_EF()
        {
            _context = new MovieContext();

        }
        public async Task Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

        }
        public async Task<List<Genre>> List()
        {
            return await _context.Genres.ToListAsync();
        }

    
            public async Task Delete(Genre genre)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
                
            }

            public async Task<Genre> GetGenreById(int Id)
            {
                    return await _context.Genres.FindAsync(Id);
               
            }

            public async Task Update(Genre genre)
            {
                 _context.Genres.Update(genre);
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
