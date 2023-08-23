using System;
using ListeriaArch.Configurator.Layers.Filters;

namespace ListeriaArch.Configurator.Layers {
  public interface ILayersConfigurator : IMap<Type, IFilterRule> {
    void AddLayer(IFilterRule rule);
  }
}
