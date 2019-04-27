using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Services
{
    /// <summary>
    /// Интерфейс сервиса управления проектами
    /// </summary>
    public interface IProjectManagementServices
    {
        /// <summary>
        /// Получение всех проектов из хранилища
        /// </summary>
        /// <returns></returns>
        Dictionary<Guid, Project> GetAllProjects();

        /// <summary>
        /// Добавление нового проекта в хранилище
        /// </summary>
        /// <param name="projName">Имя нового проекта</param>
        /// <returns></returns>
        Project AddProject(string projName);

        /// <summary>
        /// Обновление содержащегося в коллекции проекта
        /// </summary>
        /// <param name="proj">Данные для обновления проекта</param>
        /// <returns></returns>
        Project UpdateProject(Project proj);

        /// <summary>
        /// Удаление проекта
        /// </summary>
        /// <param name="projId">Идентификатор проекта</param>
        /// <returns></returns>
        bool RemoveProject(Guid projId);
    }
}
