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
            try
            {
                return await Realm.GetInstanceAsync(configuration);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> Select<T>() where T : RealmObject
        {
            try
            {
                string json = JsonConvert.SerializeObject((await RealmAsync()).All<T>());
                return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdate<T>(T entity, bool update) where T : RealmObject
        {
            try
            {
                Realm realm = await RealmAsync();
                using Transaction transaction = realm.BeginWrite();
                realm.Add(entity, update);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<bool> InsertOrUpdate<T>(IEnumerable<T> entities, bool update) where T : RealmObject
        {
            try
            {
                Realm realm = await RealmAsync();
                using Transaction transaction = realm.BeginWrite();
                realm.Add(entities, update);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}