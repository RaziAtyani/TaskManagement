using TASK_2.models;

namespace TASK_2.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Registration> Registrations { get; set; }
    }

}
