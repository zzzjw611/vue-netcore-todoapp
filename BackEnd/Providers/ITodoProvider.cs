using BackEnd.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;              


///CRUD interface
namespace BackEnd.Providers
{
    public interface ITodoProvider
    {
        Task<List<Todo>> GetTodosAsync(string? search = null);
        Task<Todo> AddTodoAsync(Todo todo);
        Task<Todo> UpdateTodoAsync(Todo todo);
        Task DeleteTodoAsync(int id);
    }
}
