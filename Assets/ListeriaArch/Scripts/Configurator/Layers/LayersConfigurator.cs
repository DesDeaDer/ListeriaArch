using System;
using System.Collections.Generic;
using ListeriaArch.Configurator.Layers.Filters;

namespace ListeriaArch.Configurator.Layers {
  public class LayersConfigurator : ILayersConfigurator {
    Dictionary<Type, IFilterRule> rules = new();

    public IFilterRule this[Type id] => rules[id];

    public IReadOnlyDictionary<Type, IFilterRule> Map => rules;

    public void AddLayer(IFilterRule rule) => rules.Add(rule.Type, rule);
  }
}
