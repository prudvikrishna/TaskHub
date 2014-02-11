using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskHub.Web.Models;

namespace TaskHub.Web.Test
{
  public class FakeTaskSet: FakeDbSet<Tasks>
    {
        
    public override Tasks Find(params object[] keyValues)
    {
        return this.SingleOrDefault(t => t.TaskId == (Guid)keyValues.Single());
    }
}
    }

