using System;
using System.Collections.Generic;

namespace ListeriaArch.Configurator.Layers.Filters {
  public class LayerRuleBasic : IFilterRule {
    public Type Type { get; init; }

    public IEnumerable<IFilterRule> SubFilters => rules;

    List<IFilterRule> rules = new();

    public void Add(IFilterRule rule) => rules.Add(rule);
  }
}
