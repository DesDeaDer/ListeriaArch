using ListeriaArch.Configurator.Links;

namespace ListeriaArch {
  public interface IContextConfigurator {
    ILayersConfigurator Layers { get; }
    ILinksConfigurator Links { get; }
    IResolversConfigurator Resolvers { get; }
    IResolversRuntimeConfigurator ResolversRuntime { get; }
  }
}
