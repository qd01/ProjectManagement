using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Models;
using ProjectManager.Services;

namespace ProjectManager.Controllers
{
    [Route("pm/")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectManagementServices _services;

        public ProjectController(IProjectManagementServices services)
        {
            _services = services;
        }


        /// <summary>
        /// Получение информации обо всех проектах в коллекции
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllProjects")]
        public ActionResult<Dictionary<Guid, Project>> GetAllProjects()
        {
            var projects = _services.GetAllProjects();

            if (projects.Count == 0)
            {
                return NotFound();
            }

            return projects;
        }

        /// <summary>
        /// Добавление нового проекта
        /// </summary>
        /// <param name="proj">Экземпляр добавляемого проекта</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddProject")]
        public ActionResult<Project> AddProject(Project proj)
        {
            var addedProject = _services.AddProject(proj.ProjectName);

            if (addedProject == null)
            {
                return NotFound();
            }

            return addedProject;
        }

        /// <summary>
        /// Обновление существующего проекта
        /// </summary>
        /// <param name="proj">Экземпляр обновляемого проекта</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateProject")]
        public ActionResult<Project> UpdateProject(Project proj)
        {
            var updatedProject = _services.UpdateProject(proj);

            if (updatedProject == null)
            {
                return NotFound();
            }

            return updatedProject;
        }

        /// <summary>
        /// Удаление проекта из коллекции
        /// </summary>
        /// <param name="proj">Экземпляр удаляемого проекта</param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveProject")]
        public ActionResult<bool> RemoveProject(Project proj)
        {
            var removed = _services.RemoveProject(proj.Id);

            if (removed == false)
            {
                return NotFound();
            }

            return removed;
        }
    }
}