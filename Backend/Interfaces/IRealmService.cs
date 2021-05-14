using System.Collections.Generic;
using System.Threading.Tasks;
using Realms;

namespace DotNetCoreWithRealm.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRealmService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IEnumerable<T>> Select<T>() where T : RealmObject;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> InsertOrUpdate<T>(T entity, bool update) where T : RealmObject;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entities"></param>
        /// <returns></returns>
        Task<bool> InsertOrUpdate<T>(T entity) where T : RealmObject;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<bool> InsertOrUpdate<T>(IEnumerable<T> entities, bool update) where T : RealmObject;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> InsertOrUpdate<T>(IEnumerable<T> entities) where T : RealmObject;
    }
}
