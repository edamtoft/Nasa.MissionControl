using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace Nasa.MissionControl
{
  internal static class ConfigOps
  {
    public static IContainer Build(this IModule module)
    {
      var builder = new ContainerBuilder();
      builder.RegisterModule(module);
      return builder.Build();
    }

    public static void Execute<TEntryPoint>(this IModule module, Action<TEntryPoint> runnable)
    {
      using (var container = module.Build())
      {
        var entryPoint = container.Resolve<TEntryPoint>();
        runnable.Invoke(entryPoint);
      }
    }
  }
}
