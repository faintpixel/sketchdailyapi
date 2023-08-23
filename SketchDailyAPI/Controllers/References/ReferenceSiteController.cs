using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SketchDailyAPI.DAO.References;
using SketchDailyAPI.Models;
using SketchDailyAPI.Models.References;

namespace SketchDailyAPI.Controllers.References
{
    [ApiController]
    [Route("[controller]")]
    [Tags("ReferenceSite")]
    public class ReferenceSiteController
    {
        private readonly AppSettings _appSettings;
        private readonly NewsDAO _newsDAO;

        public ReferenceSiteController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _newsDAO = new NewsDAO(appSettings.Value);
        }

        /// <summary>
        /// Get news
        /// </summary>
        /// <param name="offsetLimit"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("News")]
        public List<News> GetNews([FromQuery(Name = "")] OffsetLimit offsetLimit)
        {
            return _newsDAO.Get(offsetLimit);
        }

        /// <summary>
        /// Save news
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [Authorize("admin")]
        [Route("News")]
        public bool SaveNews([FromBody] News news)
        {
            return _newsDAO.Save(news);
        }

        /// <summary>
        /// Get announcement
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("Announcement")]
        public Announcement GetAnnouncement()
        {
            return _newsDAO.GetAnnouncement();
        }

        /// <summary>
        /// Save announcement
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [Authorize("admin")]
        [Route("Announcement")]
        public bool SaveAnnouncement([FromBody] Announcement announcement)
        {
            return _newsDAO.SaveAnnouncement(announcement);
        }
    }
}
