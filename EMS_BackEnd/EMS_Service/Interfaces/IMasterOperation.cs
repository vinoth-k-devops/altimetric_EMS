using System;
namespace EMS_Service.Interfaces
{
	public interface IMasterOperation<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task<string> Add(TEntity entity);

        Task<string> Update(TEntity entity);

        Task<string> Delete(TEntity entity);

        //Task<List<TEntity>> GetActiveRecords(string property);
    }
}

