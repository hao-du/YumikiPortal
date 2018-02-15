namespace Yumiki.Data.OnTime.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Get all active/Inactive project
        /// </summary>
        void GetAllProjects(bool isActive);
    }
}
