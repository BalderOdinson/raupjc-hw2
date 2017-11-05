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
            Assert.AreEqual(_todoItem, _emptyTodoRepository.Add(_todoItem));
            Assert.ThrowsException<DuplicateTodoItemException>(() => _todoRepository.Add(_todoItem));
        }

        [TestMethod]
        public void TestRemove()
        {
            Assert.AreEqual(_todoItem, _todoRepository.Get(_todoItem.Id));
            _todoRepository.Remove(_todoItem.Id);
            Assert.IsNull(_todoRepository.Get(_todoItem.Id));
        }

        [TestMethod]
        public void TestUpdate()
        {
            var newTodoItem = new TodoItem("");
            newTodoItem.PropertyMapper(_todoItem);
            _todoItem.Text = "newTestItem";
            Assert.AreNotEqual(newTodoItem.Text, _todoRepository.Update(_todoItem).Text);
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
            Assert.AreEqual(5, _todoRepository.GetAll().Count);
            Assert.AreEqual(0, _emptyTodoRepository.GetAll().Count);
            Assert.AreEqual(_todoItem, _todoRepository.GetAll()[0]);
            Assert.AreEqual("third", _todoRepository.GetAll()[4].Text);
        }

        [TestMethod]
        public void TestGetActive()
        {
            _todoRepository.MarkAsCompleted(_todoItem.Id);
            Assert.AreEqual(3, _todoRepository.GetActive().Count);
            Assert.IsFalse(_todoRepository.GetActive().Contains(_todoItem));
        }

        [TestMethod]
        public void TestGetCompleted()
        {
            Assert.AreEqual(1, _todoRepository.GetCompleted().Count);
            _todoRepository.MarkAsCompleted(_todoItem.Id);
            Assert.IsTrue(_todoRepository.GetCompleted().Contains(_todoItem));
        }

        [TestMethod]
        public void TestGetFiltered()
        {
            Assert.AreEqual(2, _todoRepository.GetFiltered(s => DateTime.UtcNow.Subtract(s.DateCreated).Days > 6).Count);
            Assert.AreEqual(0, _emptyTodoRepository.GetFiltered(s => true).Count);
        }


    }
}
