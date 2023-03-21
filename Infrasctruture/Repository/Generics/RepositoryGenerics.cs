﻿using Domain.Interfaces.Generics;
using Infrasctruture.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrasctruture.Repository.Generics
{
    public class RepositoryGenerics<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        public RepositoryGenerics()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }
        public async Task Add(T Objeto)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                await data.Set<T>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T Objeto)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {

                data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityById(int Id)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {

              return await data.Set<T>().FindAsync(Id);
                
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task Update(T Objeto)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {

               data.Set<T>().Update(Objeto);
               await data.SaveChangesAsync();
            }
        }

        // To detect redundant calls
        bool disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _safeHandle.Dispose();
                }

                disposed = true;
            }


        }
    }
}