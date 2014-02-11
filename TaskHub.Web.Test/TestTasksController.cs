using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskHub.Web.Controllers;
using TaskHub.Web.Models;
using System.Web.Mvc;
using System.Collections.Generic;

namespace TaskHub.Web.Test
{
    [TestClass]
    public class TestTasksController
    {
        [TestMethod]
        public void GetIndexTasks()
        {
            var context = new TaskHubTestContext
            {
                Tasks =
        {
            new Tasks { Title = "ABC",Status ="Open"},
            new Tasks { Title = "XYZ", Status="Open"},
            
        }
            };

            var controller = new TasksController(context);
            var result = controller.GetAllTasks();
           
            var i = 0;
            foreach (var t in result)
            {
                if (i == 0)
                    Assert.AreEqual("ABC", t.Title);
                if(i == 1)
                    Assert.AreEqual("XYZ", t.Title);
                i++;
            }

           
            
        }
    }
}
