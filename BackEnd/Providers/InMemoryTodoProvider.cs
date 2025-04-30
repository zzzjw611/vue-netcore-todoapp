// InMemoryTodoProvider.cs // for the guest mode 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BackEnd.Model;

namespace BackEnd.Providers
{
    /// guest mode:
    /// Stores up to 5 items in a List<Todo>; older items are dropped when capacity is exceeded.
    public class InMemoryTodoProvider : ITodoProvider
    {
      
        private readonly List<Todo> _todos = new List<Todo>();

        private int _nextId = 1;

  
        /// Returns all todos, optionally filtered by a search term.
        public async Task<List<Todo>> GetTodosAsync(string? search = null)
        {
            var result = _todos;

            if (!string.IsNullOrEmpty(search))
            {
                // Filter by title, ignoring case
                result = result
                    .Where(t => t.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Wrap synchronous data in a Task for interface compatibility
            return await Task.FromResult(result);
        }


        /// Adds a new todo to the in-memory list.
        /// If capacity (5 items) is reached, removes the oldest entry first.
        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            // Enforce maximum of 5 items: drop first (oldest) if needed
            if (_todos.Count >= 5)
            {
                _todos.RemoveAt(0);
            }

            // Assign a new ID and add to list
            todo.Id = _nextId++;
            _todos.Add(todo);

            return await Task.FromResult(todo);
        }

   
        /// Updates an existing todo’s Title and Completed flag.
        public async Task<Todo> UpdateTodoAsync(Todo todo)
        {
            // Find existing item by ID
            var existing = _todos.FirstOrDefault(t => t.Id == todo.Id);
            if (existing != null)
            {
                existing.Title = todo.Title;
                existing.Completed = todo.Completed;
            }

            return await Task.FromResult(existing);
        }

      
        /// Deletes a todo by ID. If not found, does nothing.
        public async Task DeleteTodoAsync(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo != null)
            {
                _todos.Remove(todo);
            }

            await Task.CompletedTask;
        }
    }
}
