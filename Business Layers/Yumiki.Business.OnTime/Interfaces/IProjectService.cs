namespace Yumiki.Business.OnTime.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Get all active/Inactive project
        /// </summary>
        void GetAllProjects(bool isActive);
    }
}
