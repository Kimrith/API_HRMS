using HRMS.API.Data;

namespace HRMS.API.Commands.Delete
{
    public class DeleteUserCommand
    {
        private readonly AppDbContext _context;
        public DeleteUserCommand(AppDbContext context) => _context = context;

        public async Task<bool> ExecuteAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null) return false;

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}