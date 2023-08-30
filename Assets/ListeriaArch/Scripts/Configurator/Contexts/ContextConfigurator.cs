using System;
using ListeriaArch.Configurator.Layers;
using ListeriaArch.Configurator.Links;
using ListeriaArch.Configurator.Resolvers;
using ListeriaArch.Configurator.ResolversRuntime;

namespace ListeriaArch {
  public class ContextConfigurator : IContextConfigurator {
    public ILayersConfigurator Layers { get; init; }
    public ILinksConfigurator Links { get; init; }
    public IResolversConfigurator Resolvers { get; init; }
    public IResolversRuntimeConfigurator ResolversRuntime { get; init; }
  }

  public class LinksCreator : ILinksCreator {
    private IContextConfigurator configurator;

    public LinksCreator(IContextConfigurator configurator) => this.configurator = configurator;

    public object Create(Type type) {
      throw new System.NotImplementedException();
    }
  }
}
