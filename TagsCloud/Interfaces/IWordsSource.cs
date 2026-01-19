using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.Interfaces
{
    public interface IWordsSource
    {
        Result<List<string>> ReadWords();
    }
}
