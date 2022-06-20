using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Triggers;

namespace TalentsApi.Entities.Events
{
    public interface IOnUpdate 
    {
        public void BeforeUpdate(Object entity);
        public void AfterUpdate(Object entity);
    }
}
