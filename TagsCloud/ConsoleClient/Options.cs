using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.ConsoleClient
{
    public sealed class Options
    {
        [Option('i', "input", Required = true)]
        public string Input { get; set; }

        [Option('s', "stopwords", Required = false, Default = "")]
        public string StopWords { get; set; }

        [Option('o', "output", Required = false, Default = "visualizations/cloud.png")]
        public string Output { get; set; }

        [Option("width", Required = false, Default = 0)]
        public int Width { get; set; }

        [Option("height", Required = false, Default = 0)]
        public int Height { get; set; }

        [Option("font", Required = false, Default = "GenericSansSerif")]
        public string Font { get; set; }

        [Option("min-font", Required = false, Default = 50f)]
        public float MinFont { get; set; }

        [Option("max-font", Required = false, Default = 240f)]
        public float MaxFont { get; set; }

        [Option("format", Required = false, Default = "png")]
        public string Format { get; set; }
    }
}
