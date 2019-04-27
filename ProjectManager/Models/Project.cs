using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    /// <summary>
    /// Статусы проекта
    /// </summary>
    public enum ProjectStates
    {
        /// <summary>
        /// Новый
        /// </summary>
        New = 0,

        /// <summary>
        /// В работе
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Завершен
        /// </summary>
        Closed = 2,

        /// <summary>
        /// Удален
        /// </summary>
        Removed = 3
    }

    /// <summary>
    /// Проект
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя проекта
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Статус проекта
        /// </summary>
        public ProjectStates ProjectState { get; set; }

        /// <summary>
        /// Дата начала проекта
        /// </summary>
        public DateTime Date_started { get; set; }

        /// <summary>
        /// Дата завершения проекта
        /// </summary>
        public DateTime Date_closed { get; set; }
    }
}
