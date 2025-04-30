using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Model;
using Microsoft.EntityFrameworkCore;
/// for the User mode; stores items in database
/// and uses Entity Framework Core for data access.
namespace BackEnd.Providers
{
    public class SqlTodoProvider : ITodoProvider
    {
        private readonly AppDbContext _dbContext;

        public SqlTodoProvider(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Todo>> GetTodosAsync(string? search = null)
        {
            var query = _dbContext.Todos.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.Title.Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            _dbContext.Todos.Add(todo);
            await _dbContext.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> UpdateTodoAsync(Todo todo)
        {
            var existing = await _dbContext.Todos.FindAsync(todo.Id);
            if (existing != null)
            {
                existing.Title = todo.Title;
                existing.Completed = todo.Completed;
                await _dbContext.SaveChangesAsync();
            }
            return existing;
        }

        public async Task DeleteTodoAsync(int id)
        {
            var todo = await _dbContext.Todos.FindAsync(id);
            if (todo != null)
            {
                _dbContext.Todos.Remove(todo);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
