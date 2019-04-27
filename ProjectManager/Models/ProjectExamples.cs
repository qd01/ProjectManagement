using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    /// <summary>
    /// Статический класс для формирования первоначального наполнения хранилища сервиса
    /// </summary>
    public static class ProjectExamples
    {
        private static Dictionary<Guid, Project> _projectExamples;

        /// <summary>
        /// Получение примеров проектов
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Guid, Project> GetExamples()
        {
            if(_projectExamples == null)
            {
                _projectExamples = new Dictionary<Guid, Project>();

                //Генерация начальных примеров
                for (int i = 0; i < 10; i++)
                {
                    Project pEx = new Project();
                                        
                    pEx.Id = Guid.NewGuid();
                    pEx.ProjectName = String.Format("ProjectExample_{0}", i);

                    Random rnd = new Random();
                    pEx.ProjectState = (ProjectStates)rnd.Next(0, 3);

                    pEx.Date_started = DateTime.Now.AddDays(-rnd.Next(4, 10));


                    //Если сгенерилось состояние "Завершен", выполняется генерация даты завершения проекта
                    if (pEx.ProjectState == ProjectStates.Closed)
                    {                        
                        pEx.Date_closed = DateTime.Now.AddDays(-rnd.Next(0, 3));
                    }

                    _projectExamples.Add(pEx.Id, pEx);
                }

            }

            return _projectExamples;
        }
    }
}
