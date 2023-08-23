using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SketchDailyAPI.DAO.References;
using SketchDailyAPI.Models;
using SketchDailyAPI.Models.References;

namespace SketchDailyAPI.Controllers.References
{
    /// <summary>
    /// API for working with logs
    /// </summary>
    [ApiController]
    [Route("ReferenceSite/[controller]")]
    [Tags("ReferenceSite")]
    public class LogsController : BaseController
    {
        private Logger _dao;

        /// <summary>
        /// Constructor
        /// </summary>
        public LogsController(IOptions<AppSettings> appSettings)
        {
            _dao = new Logger("LogsController", appSettings.Value);
        }

        /// <summary>
        /// Gets all the logs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("admin")]
        [Route("")]
        public async Task<List<Log>> Get()
        {
            return await _dao.GetAllLogs();
        }

        /// <summary>
        /// Delete a log
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize("admin")]
        [Route("{id}")]
        public bool Delete(string id)
        {
            return _dao.DeleteLog(id);
        }

        /// <summary>
        /// Counts all the logs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("admin")]
        [Route("Count")]
        public async Task<int> GetCount()
        {
            return await _dao.CountLogs();
        }
    }
}
