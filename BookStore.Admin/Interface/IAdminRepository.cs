using BookStore.Admin.Entity;

namespace BookStore.Admin.Interface
{
    public interface IAdminRepository
    {
        public AdminEntity AddAdmin(AdminEntity admin);
        public string AdminLogin(AdminEntity adminEntity);
    }
}
