using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentsApi.Services.IServices
{
    public interface IRandomGeneratorService
    {
        int RandomNumber(int min, int max);

        string RandomString(int size, bool lowerCase = false);

        string RandomPassword();
    }
}
