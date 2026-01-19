using System.Drawing;
using TagsCloud.Interfaces;
using TagsCloud.CircularCloudLayouter;
using Autofac;
using TagsCloud.Morphology;
using TagsCloud.Domain;
using TagsCloud.ConsoleClient;
public static class AppModule
{
    public static IContainer Build(Options options)
    {
        var builder = new ContainerBuilder();

        RegisterApplication(builder, options);
        RegisterInfrastructure(builder, options);

        return builder.Build();
    }   

    private static void RegisterApplication(ContainerBuilder builder, Options options)
    {
        builder.RegisterType<TagCloudGenerator>()
               .As<ITagCloudGenerator>();

        builder.RegisterType<FrequencyAnalyzer>()
        .As<IFrequencyAnalyzer>();

        builder.RegisterInstance(new LinearTagSizeCalculator(options.MinFont, options.MaxFont))
        .As<ITagSizeCalculator>()
           .SingleInstance();
    }

    private static void RegisterInfrastructure(ContainerBuilder builder, Options options)
    {
        builder.RegisterInstance(new FileWordsSource(options.Input))
               .As<IWordsSource>();
        builder.RegisterInstance(new FileStopWordsProvider(options.StopWords))
               .As<IStopWordsProvider>()
               .SingleInstance();

        builder.RegisterType<MystemSharpMorphology>()
               .As<IMorphologyAnalyzer>()
               .SingleInstance();

        builder.RegisterType<MorphologicalPreprocessor>()
               .As<IWordsPreprocessor>();

        builder.Register(ctx =>
                new CircularCloudLayouter(
                    new Point(0, 0),
                    new Spiral(new Point(0, 0))))
               .As<ITagLayouter>();

        builder.Register<Func<ITagLayouter>>(ctx =>
        {
            var c = ctx.Resolve<IComponentContext>();
            return () => c.Resolve<ITagLayouter>();
        });
        builder.RegisterType<DefaultPartOfSpeechFilter>().As<IPartOfSpeechFilter>().SingleInstance();

        


        var imageFormat = ImageFormatHelper.Parse(options.Format);
        builder.RegisterInstance(new GenericImageSaver(imageFormat))
               .As<IImageSaver>()
               .SingleInstance();

        builder.Register(ctx => new ImageRenderer(
             Color.White,
             new[] { Color.Black, Color.DarkBlue, Color.DarkRed },
             ctx.Resolve<IImageSaver>()))
         .As<IImageRenderer>()
         .SingleInstance();
    }
}
