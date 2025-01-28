namespace AKKTN_Pr00.Models
{
    public class ViewTasks
    {
        public IEnumerable<Tasks> Tasks { get; set; } // List of all tasks
        public Dictionary<int, List<CompanyTeam>> TaskMembers { get; set; } // Map TaskID to its members
    }
}
