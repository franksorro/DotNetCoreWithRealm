using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreWithRealm.Interfaces;
using Newtonsoft.Json;
using Realms;

namespace DotNetCoreWithRealm.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class RealmService : IRealmService
    {
        private readonly RealmConfiguration configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public RealmService(RealmConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private async Task<Realm> RealmAsync()
        {
            Realm realm = await Realm.GetInstanceAsync(configuration);

            if (realm == null)
                throw new Exception("Realm datasource error");

            return realm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> Select<T>() where T : RealmObject
        {
            string json = JsonConvert.SerializeObject((await RealmAsync()).All<T>());
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdate<T>(T entity, bool update) where T : RealmObject
        {
            try
            {
                using (var transaction = (await RealmAsync()).BeginWrite())
                {
                    (await RealmAsync()).Add(entity, update);
                    transaction.Commit();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdate<T>(T entity) where T : RealmObject
        {
            return await InsertOrUpdate(entity, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entities"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdate<T>(IEnumerable<T> entities, bool update) where T : RealmObject
        {
            try
            {
                using (var transaction = (await RealmAsync()).BeginWrite())
                {
                    (await RealmAsync()).Add(entities, update);
                    transaction.Commit();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdate<T>(IEnumerable<T> entities) where T : RealmObject
        {
            return await InsertOrUpdate(entities, false);
        }
    }
}
