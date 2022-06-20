using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentsApi.Models.Excetpions
{
    public class ExceptionModel
    {
        public string ErrorMessage { get; set; }
        public string? InnerMessage { get; set; }
    }
}
