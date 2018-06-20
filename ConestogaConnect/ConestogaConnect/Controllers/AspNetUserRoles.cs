namespace ConestogaConnect.Controllers
{
    internal class AspNetUserRoles : System.Data.Entity.DbContext
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}