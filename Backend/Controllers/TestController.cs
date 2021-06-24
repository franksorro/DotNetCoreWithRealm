using System.Threading.Tasks;
using DotNetCoreWithRealm.Models;
using Microsoft.AspNetCore.Mvc;
using FS.Interfaces;

namespace DotNetCoreWithRealm.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TestController : BaseController
    {
        private readonly IRealmService service;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public TestController(IRealmService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await service.Select<TestModel>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add()
        {
            var model = new TestModel
            {
                Id = 1,
                Label = "Test"
            };

            return Ok(await service.InsertOrUpdate(model, false));
        }
    }
}
