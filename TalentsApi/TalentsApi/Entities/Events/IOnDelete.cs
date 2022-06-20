using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentsApi.Entities.Events
{
    public interface IOnDelete
    {
        public void BeforeDelete(Object entity);
        public void AfterDelete(Object entity);
    }
}
