using Microsoft.AspNetCore.Mvc;
using BackEnd.Services;       // For TodoProviderFactory
using BackEnd.Model;          // For the Todo model
using BackEnd.Providers;      // For ITodoProvider implementations

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoProviderFactory _factory;

        // Constructor injection of the provider factory
        public TodoController(TodoProviderFactory factory)
        {
            _factory = factory;
        }

        /// summary：
        /// Read the "X-Provider-Mode" header to decide which provider to use.
        /// Defaults to "user" if the header is not present.
        private string GetMode()
        {
            if (Request.Headers.ContainsKey("X-Provider-Mode"))
            {
                return Request.Headers["X-Provider-Mode"].ToString();
            }
            return "user"; // default to database-backed provider
        }

        /// GET /api/todo?search=...
        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos([FromQuery] string? search)
        {
            // Get appropriate provider (Guest or User)
            var provider = _factory.GetProvider(GetMode());
            // Fetch todos, optionally filtering by 'search'
            var todos = await provider.GetTodosAsync(search);
            return Ok(todos);
        }

        /// POST /api/todo
        /// Adds a new todo item.
        [HttpPost]
        public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo)
        {
            var provider = _factory.GetProvider(GetMode());
            var newTodo = await provider.AddTodoAsync(todo);
            return Ok(newTodo);
        }

        /// PUT /api/todo/{id}
        /// Updates an existing todo.

        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> UpdateTodo(int id, [FromBody] Todo todo)
        {
            var provider = _factory.GetProvider(GetMode());
            todo.Id = id;
            var updatedTodo = await provider.UpdateTodoAsync(todo);
            if (updatedTodo == null)
                return NotFound();
            return Ok(updatedTodo);
        }

        /// DELETE /api/todo/{id}
        /// Deletes the todo with the given ID.

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var provider = _factory.GetProvider(GetMode());
            await provider.DeleteTodoAsync(id);
            return NoContent();
        }
    }
}
