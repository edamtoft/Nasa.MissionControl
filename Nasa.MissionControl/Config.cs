using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Autofac;
using Autofac.Core;
using Nasa.MissionControl.IO;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControl
{
  /// <summary>
  /// Configuration properties for the application.
  /// </summary>
  class Config : Module
  {
    public string InputFile { get; set; }
    public string OutputFile { get; set; }
    public bool GraphicOutput { get; set; }
    public int AnimationSpeed { get; set; } = 500;
    public bool NoPrompts { get; set; }

    protected override void Load(ContainerBuilder builder)
    {
      if (string.IsNullOrEmpty(InputFile))
      {
        builder
          .RegisterType<CLI>()
          .As<IReadable>()
          .As<IWritable>()
          .SingleInstance();

        if (NoPrompts)
        {
          builder
           .RegisterType<ProgrammableTransciever>()
           .As<IMissionTransciever>()
           .SingleInstance();
        }
        else
        {
          builder
           .RegisterType<MissionControlTransciever>()
           .As<IMissionTransciever>()
           .SingleInstance();
        }
      }
      else
      {
        builder
          .Register(c => new FileInterface(InputFile))
          .As<IReadable>()
          .SingleInstance();

        builder
          .RegisterType<ProgrammableTransciever>()
          .As<IMissionTransciever>()
          .SingleInstance();
      }

      if (string.IsNullOrEmpty(OutputFile))
      {
        if (GraphicOutput)
        {
          builder
            .Register(c => new RoverPositionView(AnimationSpeed))
            .As<IView<IList<Rover>>>()
            .SingleInstance();
        }
        else
        {
          builder
            .RegisterType<ConsoleRoverView>()
            .As<IView<IList<Rover>>>()
            .SingleInstance();
        }

      }
      else
      {
        builder
          .Register(c => new FileRoverView(OutputFile))
          .As<IView<IList<Rover>>>()
          .SingleInstance();
      }
      
      builder
        .RegisterType<MarsMission>()
        .As<IMission>()
        .InstancePerLifetimeScope();

      builder
        .RegisterType<CommandParser>()
        .As<IParser<Command[]>>()
        .SingleInstance();

      builder
        .RegisterType<RoverParser>()
        .As<IParser<Rover>>()
        .SingleInstance();

      builder
        .RegisterType<YesNoParser>()
        .As<IParser<bool>>()
        .SingleInstance();

      builder
        .RegisterType<CoordinateParser>()
        .As<IParser<Coordinate>>()
        .SingleInstance();

      builder
        .RegisterType<PlateauParser>()
        .As<IParser<IMap>>()
        .SingleInstance();
    }

    public static IModule Default => new Config();
  }
}
