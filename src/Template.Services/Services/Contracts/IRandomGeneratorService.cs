using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template.Services.Services.Contracts
{
    public interface IRandomGeneratorService
    {
        Task<int> Range(int minInclusive, int maxExclusive);
        Task<double> Range(double minInclusive, double maxExclusive);

        Task<List<int>> RandomIntegers(int length, int maxValue, int minValue = 0);

        Task<string> RandomString(int length, bool upperCase = true, bool lowerCase = true, bool numberCase = true);
    }
}
