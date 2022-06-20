using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentsApi.Entities.Events
{
    public interface IOnInsert
    {
        public void BeforeInsert(Object entity);
        public void AfterInsert(Object entity);
    }
}
