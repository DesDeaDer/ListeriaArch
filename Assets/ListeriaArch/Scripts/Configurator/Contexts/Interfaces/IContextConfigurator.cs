using ListeriaArch.Configurator.Layers;
using ListeriaArch.Configurator.Links;
using ListeriaArch.Configurator.Resolvers;
using ListeriaArch.Configurator.ResolversRuntime;

namespace ListeriaArch {
  public interface IContextConfigurator {
    ILayersConfigurator Layers { get; }
    ILinksConfigurator Links { get; }
    IResolversConfigurator Resolvers { get; }
    IResolversRuntimeConfigurator ResolversRuntime { get; }
  }
}
