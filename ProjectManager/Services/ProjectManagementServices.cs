using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.Models;

namespace ProjectManager.Services
{
    /// <summary>
    /// Сервис управления проектами
    /// </summary>
    public class ProjectManagementServices : IProjectManagementServices
    {
        /// <summary>
        /// Хранилище данных проектов
        /// </summary>
        private readonly Dictionary<Guid, Project> _projects;

        /// <summary>
        /// Просто конструктор
        /// </summary>
        public ProjectManagementServices()
        {
            //Генерация начальных примеров проектов
            _projects = ProjectExamples.GetExamples();
        }

        /// <summary>
        /// Получение всех проектов из хранилища
        /// </summary>
        /// <returns></returns>
        public Dictionary<Guid, Project> GetAllProjects()
        {
            return _projects;
        }

        /// <summary>
        /// Добавление нового проекта в хранилище
        /// </summary>
        /// <param name="projName">Имя нового проекта</param>
        /// <returns></returns>
        public Project AddProject(string projName)
        {
            try
            {
                //Если имя проекта не пустая строка
                if(projName != string.Empty)
                {
                    //Создание экземпляра нового проекта
                    Project newProject = new Project();

                    newProject.Id = Guid.NewGuid();
                    newProject.ProjectName = projName;
                    newProject.ProjectState = ProjectStates.New;
                    newProject.Date_started = DateTime.Now;

                    //Добавление в коллекцию
                    _projects.Add(newProject.Id, newProject);

                    return newProject;                    
                }

                //Если имя проекта пустая строка
                throw new Exception("Имя проекта не может быть пустым.");
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Обновление содержащегося в коллекции проекта
        /// </summary>
        /// <param name="proj">Данные для обновления проекта</param>
        /// <returns></returns>
        public Project UpdateProject(Project proj)
        {
            try
            {
                //Обновить разрешено только существующий элемент коллекции
                if (_projects.ContainsKey(proj.Id))
                {
                    //Формирование нового экземпляра обновляемого проекта для атомарности операции обновления
                    Project modifiedProject = new Project();

                    //Копирующий конструктор не используется, т.к. возникнут проблемы с сериализацией\десериализацией
                    modifiedProject.Id = _projects[proj.Id].Id;
                    modifiedProject.ProjectName = _projects[proj.Id].ProjectName;
                    modifiedProject.ProjectState = _projects[proj.Id].ProjectState;
                    modifiedProject.Date_started = _projects[proj.Id].Date_started;
                    modifiedProject.Date_closed = _projects[proj.Id].Date_closed;

                    //Если статус обновляемого проекта позволяет выполнить обновление данных
                    if ((modifiedProject.ProjectState != ProjectStates.Closed) &&
                        (modifiedProject.ProjectState != ProjectStates.Removed))
                    {                        
                        //Для удаления есть отдельный функционал. На всякий проверка
                        if (proj.ProjectState != ProjectStates.Removed)
                        {
                            //Если статус проекта изменен
                            if (!modifiedProject.ProjectState.Equals(proj.ProjectState))
                            {
                                //Изменение статуса проекта
                                modifiedProject.ProjectState = proj.ProjectState;

                                //Если статус проекта обновляется на "Завершен", установка даты завершения
                                if (proj.ProjectState == ProjectStates.Closed)
                                {
                                    modifiedProject.Date_closed = DateTime.Now;
                                }
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }

                        //Если имя проекта изменено
                        if (!modifiedProject.ProjectName.Equals(proj.ProjectName))
                        {
                            //Если новое имя проекта не пустая строка
                            if (proj.ProjectName != string.Empty)
                            {
                                modifiedProject.ProjectName = proj.ProjectName;
                            }
                            else
                            {
                                throw new Exception("Имя проекта не может быть пустым.");
                            }
                        }

                        //Если указана новая дата начала
                        if (!modifiedProject.Date_started.Equals(proj.Date_started))
                        {
                            //Если указано не пустое значение
                            if (proj.Date_started != DateTime.MinValue) //Проверка на MinValue в качестве дефолтного значения, т.к. DateTime структура - тип значений и не может быть null
                            {
                                modifiedProject.Date_started = proj.Date_started;
                            }
                            else
                            {
                                throw new Exception("Дата начала проекта не может быть пустой.");
                            }
                        }

                        //Замена проекта в коллекции на обновленный
                        _projects[proj.Id] = modifiedProject;

                        return modifiedProject;
                    }

                    throw new Exception("Невозможно выполнить обновление проекта с текущим статусом.");
                }

                throw new Exception("Запрашиваемый проект не существует.");
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// Удаление проекта
        /// </summary>
        /// <param name="projId">Идентификатор проекта</param>
        /// <returns></returns>
        public bool RemoveProject(Guid projId)
        {
            try
            {
                //Если существует элемент коллекции с заданным идентификатором
                if (_projects.ContainsKey(projId))
                {
                    //В задание указан статус "Удалён", значит выполняется только изменение статуса, но элемент коллекции остается (в режиме readonly).
                    _projects[projId].ProjectState = ProjectStates.Removed;
                    return true;
                }

                throw new Exception("Запрашиваемый проект не существует.");
            }
            catch
            {
                return false;
            }
        }

    }
}
