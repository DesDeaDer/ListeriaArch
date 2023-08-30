using System;
using ListeriaArch.Configurator.Layers;
using ListeriaArch.Configurator.Links;
using ListeriaArch.Configurator.Resolvers;
using ListeriaArch.Configurator.ResolversRuntime;

namespace ListeriaArch.FluidAPI {
  public static class ContextConfiguratorExtension {
    public static IContextConfigurator Layers(this IContextConfigurator c, Action<ILayersConfigurator> proc) {
      proc(c.Layers);

      return c;
    }

    public static IContextConfigurator Links(this IContextConfigurator c, Action<ILinksConfigurator> proc) {
      proc(c.Links);

      return c;
    }

    public static IContextConfigurator Resolvers(this IContextConfigurator c, Action<IResolversConfigurator> proc) {
      proc(c.Resolvers);

      return c;
    }

    public static IContextConfigurator ResolversRuntime(this IContextConfigurator c, Action<IResolversRuntimeConfigurator> proc) {
      proc(c.ResolversRuntime);

      return c;
    }

    public static IContext Build(this IContextConfigurator c) {
      var context = Listeria.Context();
      var linksCreator = Listeria.LinksCreator(c);

      //TODO lyaers rulles
      //TODO link creators
      //TODO links
      //TODO setup context

      return context;
    }
  }
}
