using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloud;
namespace TagsCloud.Interfaces
{
    public interface IWordsPreprocessor
    {
        Result<List<string>> Preprocess(Result<List<string>> words);
    }
}
