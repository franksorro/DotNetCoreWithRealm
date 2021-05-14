using Newtonsoft.Json;
using Realms;

namespace DotNetCoreWithRealm.Models
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class TestModel : RealmObject
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string Label { get; set; }
    }
}
