namespace RoleBasedManagementSystem.App.Dtos
{
    public record RoleGetDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string PermissionId { get; set; }
        public string Permission { get; set; }
    }
}
