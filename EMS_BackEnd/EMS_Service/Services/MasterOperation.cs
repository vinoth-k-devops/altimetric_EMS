using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using EMS_Domain.EMS;
using EMS_Service.Interfaces;
using EMS_Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace EMS_Service.Services
{
	public class MasterOperation<TEntity> : IMasterOperation<TEntity> where TEntity : class
    {
        private readonly EMSContext _context;
       
        public MasterOperation(EMSContext context)
        {
            this._context = context;
        }
        private DbSet<TEntity> _dbSet;
        private DbSet<TEntity> Dbset
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = this._context.Set<TEntity>();
                }

                return _dbSet;
            }
        }
        public async Task<string> Add(TEntity entity)
        {
            await Dbset.AddAsync(entity);
            await this._context.SaveChangesAsync();

            return Common.INSERT;
        }
        public async Task<string> Update(TEntity entity)
        {
            await Task.FromResult(Dbset.Update(entity));
            await this._context.SaveChangesAsync();
            await Task.FromResult(this._context.Entry(entity).State = EntityState.Modified);

            return Common.UPDATE;
        }

        public async Task<string> Delete(TEntity entity)
        {
            if (await Task.FromResult(this._context.Entry(entity).State == EntityState.Detached))
                await Task.FromResult(Dbset.Attach(entity));

            await Task.FromResult(Dbset.Remove(entity));

            return Common.DELETE;
        }
        public async Task<TEntity> GetById(int id)
        {
            var result = this.Dbset.Find(id);

            if (result == null)
                throw new NullReferenceException("record not found");

            return await Task.FromResult(result);
        }

        public async Task<List<TEntity>> GetAll()
        {        
            return await this.Dbset.ToListAsync();
        }

        //public async Task<List<TEntity>> GetActiveRecords(string propertyName)
        //{ 

        //    return await Dbset.Where(x => x.Equals(propertyName) == true);
        //}
    }
}

