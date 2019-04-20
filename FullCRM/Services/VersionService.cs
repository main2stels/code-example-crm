using FullCRM.Database.MongoDB.Model;
using FullCRM.Database.MongoDB.Model.Enums;
using FullCRM.Database.MongoDB.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Services
{
    public class VersionService
    {
        private VersionsLogRepository _versionLogRepository;
        private readonly IHttpContextAccessor _httpContext;


        public VersionService(VersionsLogRepository versionsLogRepository,
            IHttpContextAccessor httpContext)
        {
            _versionLogRepository = versionsLogRepository;
            _httpContext = httpContext;
        }

        public void Insert<T>(string id, T model) where T : class
        {
            var version = new VersionsLog
            {
                ModelType = typeof(T)?.Name,
                ModelId = id,
                OperationType = OperationType.Create,
                UserId = GetCurrentUser(),
                Date = DateTime.Now,
                Model = model
            };

            _versionLogRepository.Insert(version);
        }

        /// <summary>
        /// Зарегистрировать обнавление модели
        /// </summary>
        /// <typeparam name="T">Тип регестрируемой модели</typeparam>
        /// <param name="id">Идентификатор модели</param>
        /// <param name="model">Обновлённая модель</param>
        public void Update<T>(string id, T model) where T : class
        {
            var version = new VersionsLog
            {
                ModelType = typeof(T)?.Name,
                ModelId = id,
                OperationType = OperationType.Update,
                UserId = GetCurrentUser(),
                Date = DateTime.Now,
                Model = model
            };

            _versionLogRepository.Insert(version);
        }

        public void Delete<T>(string id, T model)
        {
            var version = new VersionsLog
            {
                ModelType = typeof(T)?.Name,
                ModelId = id,
                OperationType = OperationType.Delete,
                UserId = GetCurrentUser(),
                Date = DateTime.Now,
                Model = model
            };

            _versionLogRepository.Insert(version);
        }

        long? GetCurrentUser()
        {
            var userId = _httpContext.HttpContext.User.Identity.Name;

            return userId == null ? null : (long?)long.Parse(userId);
        }
    }
}
