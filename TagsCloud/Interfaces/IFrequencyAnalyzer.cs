using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.Interfaces
{
    public interface IFrequencyAnalyzer
    {
        Result<IDictionary<string, int>> CountFrequencies(Result<List<string>> words);
    }
}
