using System.ComponentModel.DataAnnotations.Schema;

namespace RoleBasedManagementSystem.App.Models
{
    public class Permission
    {
        public string Id { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Role))]
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
