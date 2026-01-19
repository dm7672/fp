using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.Interfaces
{
    public interface ITagCloudGenerator
    {
        Result<None> Generate(string outputPath, int imageWidth, int imageHeight, string fontName);
    }
}
