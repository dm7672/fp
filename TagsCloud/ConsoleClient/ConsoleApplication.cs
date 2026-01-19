using TagsCloud.Interfaces;
using System;
using CommandLine;
using Autofac;
namespace TagsCloud.ConsoleClient;

public sealed class ConsoleApplication
{
    public int Run(string[] args)
    {
        return Parser.Default.ParseArguments<Options>(args)
            .MapResult(
                RunWithOptions,
                _ => 1);
    }

    private int RunWithOptions(Options options)
    {
        var container = AppModule.Build(options);

        using (container)
        {
            var generator = container.Resolve<ITagCloudGenerator>();
            generator.Generate(
                options.Output,
                options.Width,
                options.Height,
                options.Font);
        }

        return 0;
    }
}
