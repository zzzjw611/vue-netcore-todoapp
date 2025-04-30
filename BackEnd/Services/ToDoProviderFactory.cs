using System;
using BackEnd.Providers;

namespace BackEnd.Services
{
    /// Factory that returns the appropriate ITodoProvider implementation
    /// based on the X-Provider-Mode request header ("guest" or "user").
    public class TodoProviderFactory
    {
        private readonly InMemoryTodoProvider _inMemory;
        private readonly SqlTodoProvider _sql;

        /// Constructor injects both concrete providers via DI.
        public TodoProviderFactory(
            InMemoryTodoProvider inMemory,
            SqlTodoProvider sql)
        {
            _inMemory = inMemory;
            _sql = sql;
        }

        /// Returns the guest (in-memory) provider if mode equals "guest",
        /// otherwise returns the SQL-backed provider.
        public ITodoProvider GetProvider(string mode)
        {
            return string.Equals(mode, "guest", StringComparison.OrdinalIgnoreCase)
                ? (ITodoProvider)_inMemory
                : _sql;
        }
    }
}
