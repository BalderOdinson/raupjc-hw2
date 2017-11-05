using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaupjcThird;
using SecondAssignment;
using SecondAssignment.Helpers;

namespace ThirdAssignment
{
    [TestClass]
    public class SecondAssignmentUnitTest
    {
        private ITodoRepository _emptyTodoRepository;
        private ITodoRepository _todoRepository;
        private TodoItem _todoItem;

        [TestInitialize]
        public void InitializeMethod()
        {
            _emptyTodoRepository = new TodoRepository();
            _todoItem = new TodoItem("testItem");
            _todoRepository = new TodoRepository(new GenericList<TodoItem>()
            {
                new TodoItem("first")
                {
                    DateCreated = DateTime.UtcNow.Subtract(new TimeSpan(10,10,10,10))
                },
                new TodoItem("second")
                {
                    DateCreated = DateTime.UtcNow.Subtract(new TimeSpan(1,1,1,1))
                },
                new TodoItem("third")
                {
                    DateCreated = DateTime.UtcNow.Subtract(new TimeSpan(20,20,20,20)) 
                },
                _todoItem,
                new TodoItem("fourth")
                {
                    DateCreated = DateTime.UtcNow.Subtract(new TimeSpan(5,5,5,5)),
                    DateCompleted = DateTime.UtcNow
                }
            });
        }

        [TestMethod]
        public void TestAdd()
        {
            Assert.AreEqual(_emptyTodoRepository.Add(_todoItem), _todoItem);
            Assert.ThrowsException<DuplicateTodoItemException>(() => _todoRepository.Add(_todoItem));
        }

        [TestMethod]
        public void TestRemove()
        {
            Assert.AreEqual(_todoRepository.Get(_todoItem.Id),_todoItem);
            _todoRepository.Remove(_todoItem.Id);
            Assert.IsNull(_todoRepository.Get(_todoItem.Id));
        }

        [TestMethod]
        public void TestUpdate()
        {
            var newTodoItem = new TodoItem("");
            newTodoItem.PropertyMapper(_todoItem);
            _todoItem.Text = "newTestItem";
            Assert.AreNotEqual(_todoRepository.Update(_todoItem).Text, newTodoItem.Text);
        }

        [TestMethod]
        public void TestMarkAsCompleted()
        {
            Assert.IsFalse(_emptyTodoRepository.MarkAsCompleted(_todoItem.Id));
            Assert.IsTrue(_todoRepository.MarkAsCompleted(_todoItem.Id));
            Assert.IsTrue(_todoItem.IsCompleted);
        }

        [TestMethod]
        public void TestGetAll()
        {
            Assert.AreEqual(_todoRepository.GetAll().Count, 5);
            Assert.AreEqual(_emptyTodoRepository.GetAll().Count, 0);
            Assert.AreEqual(_todoRepository.GetAll()[0], _todoItem);
            Assert.AreEqual(_todoRepository.GetAll()[4].Text, "third");
        }

        [TestMethod]
        public void TestGetActive()
        {
            _todoRepository.MarkAsCompleted(_todoItem.Id);
            Assert.AreEqual(_todoRepository.GetActive().Count, 3);
            Assert.IsFalse(_todoRepository.GetActive().Contains(_todoItem));
        }

        [TestMethod]
        public void TestGetCompleted()
        {
            Assert.AreEqual(_todoRepository.GetCompleted().Count,1);
            _todoRepository.MarkAsCompleted(_todoItem.Id);
            Assert.IsTrue(_todoRepository.GetCompleted().Contains(_todoItem));
        }

        [TestMethod]
        public void TestGetFiltered()
        {
            Assert.AreEqual(_todoRepository.GetFiltered(s => DateTime.UtcNow.Subtract(s.DateCreated).Days > 6).Count, 2);
            Assert.AreEqual(_emptyTodoRepository.GetFiltered(s => true).Count, 0);
        }


    }
}
