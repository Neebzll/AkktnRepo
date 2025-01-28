namespace AKKTN_Pr00.Models
{
    public class CreateTask
    {
        public Tasks Tasks { get; set; } // List of all tasks
        public List<CompanyTeam> TeamMembers { get; set; } // Map TaskID to its members
    }
}
