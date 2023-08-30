using System;
using ListeriaArch.Configurator.Layers;
using ListeriaArch.Configurator.Layers.Filters;

namespace ListeriaArch.FluidAPI {
  public static class LayersConfiguratorExtension {
    public static ILayersConfigurator Layer<Layer>(this ILayersConfigurator configurator) {
      var layer = Listeria.ConfigurateFilterRuleBasic<Layer>();

      configurator.AddLayer(layer);

      return configurator;
    }

    public static ILayersConfigurator Layer<Layer>(this ILayersConfigurator configurator, Action<IFilterRule> proc) {
      var layer = Listeria.ConfigurateFilterRuleBasic<Layer>();

      proc(layer);

      configurator.AddLayer(layer);

      return configurator;
    }
  }
}
