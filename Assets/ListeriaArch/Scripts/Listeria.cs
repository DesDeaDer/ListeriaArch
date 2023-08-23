using ListeriaArch.Configurator.Layers;
using ListeriaArch.Configurator.Layers.Filters;
using ListeriaArch.Configurator.Links;
using ListeriaArch.Configurator.Resolvers;
using ListeriaArch.Configurator.ResolversRuntime;

namespace ListeriaArch {
  public static class Listeria {
    public static IContextConfigurator Create() => new ContextConfigurator() {
      Layers = ConfigurateLayers(),
      Links = ConfigurateLinks(),
      Resolvers = ConfigurateResolvers(),
      ResolversRuntime = ConfigurateResolversRuntime(),
    };

    public static IContext Context() => new Context();

    public static ILinksConfigurator ConfigurateLinks() => new LinksConfigurator();

    public static IResolversConfigurator ConfigurateResolvers() => new ResolversConfigurator();

    public static IResolversRuntimeConfigurator ConfigurateResolversRuntime() => new ResolversRuntimeConfigurator();

    public static ILayersConfigurator ConfigurateLayers() => new LayersConfigurator();

    public static IFilterRule ConfigurateFilterRuleBasic<T>() => new LayerRuleBasic() {
      Type = typeof(T),
    };

    public static IFilterRule ConfigurateFilterRuleRequire<T>() => new LayerRuleRequire() {
      Type = typeof(T),
    };

    public static IFilterRule ConfigurateFilterRuleExcept<T>() => new LayerRuleExcept() {
      Type = typeof(T),
    };

    public static IFilterRule ConfigurateFilterRuleMaybe<T>() => new LayerRuleMaybe() {
      Type = typeof(T),
    };

    public static ILinkConfigurator ConfigurateLink() => new Link();
  }
}
