using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloud.Domain.Model;

namespace TagsCloud.Interfaces
{
    public interface IMorphologyAnalyzer
    {
        Word Analyze(string word);
    }
}
