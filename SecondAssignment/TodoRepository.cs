using System;
using System.Collections.Generic;
using System.Linq;
using RaupjcThird;
using SecondAssignment.Helpers;

namespace SecondAssignment
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </ summary >
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;
        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            _inMemoryTodoDatabase = initialDbState ?? new GenericList<TodoItem>();
        }

        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.FirstOrDefault(s => s.Id == todoId);
        }

        public TodoItem Add(TodoItem todoItem)
        {
            if(Get(todoItem.Id) != null)
                throw new DuplicateTodoItemException($"duplicate id: {todoItem.Id}");
            _inMemoryTodoDatabase.Add(todoItem);
            return todoItem;
        }

        public bool Remove(Guid todoId)
        {
            return _inMemoryTodoDatabase.Remove(Get(todoId));
        }

        public TodoItem Update(TodoItem todoItem)
        {
            var item = Get(todoItem.Id);
            if (item == null)
                return Add(todoItem);
            item.PropertyMapper(todoItem);
            return item;
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            var item = Get(todoId);
            if (item == null) return false;
            item.DateCompleted = DateTime.UtcNow;
            return true;
        }

        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.OrderByDescending(s => s.DateCreated).ToList();
        }

        public List<TodoItem> GetActive()
        {
            return _inMemoryTodoDatabase.Where(s => !s.IsCompleted).ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return _inMemoryTodoDatabase.Where(s => s.IsCompleted).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _inMemoryTodoDatabase.Where(filterFunction).ToList();
        }
    }
}
