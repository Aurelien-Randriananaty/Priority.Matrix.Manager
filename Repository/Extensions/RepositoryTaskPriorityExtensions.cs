using Entities.Models;

namespace Repository.Extensions
{
    public static class RepositoryTaskPriorityExtensions
    {
        public static IQueryable<TaskPriority> FilterTaskPriorities(this IQueryable<TaskPriority> taskPriority, int minHour, int maxHour) =>
                taskPriority.Where(e => (e.Hour >= minHour && e.Hour <= maxHour));

        public static IQueryable<TaskPriority> Search(this IQueryable<TaskPriority> taskPriority, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return taskPriority;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return taskPriority.Where(e => e.TaskTitle.ToLower().Contains(lowerCaseTerm));
        }

    }
}
