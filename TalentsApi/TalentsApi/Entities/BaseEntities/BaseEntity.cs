using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentsApi.Entities.BaseEntities
{
    public class BaseEntity
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
