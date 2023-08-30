using System;
using System.Collections.Generic;

namespace ListeriaArch.Configurator.Layers.Filters {
  public interface IFilterRule {
    Type Type { get; }
    IEnumerable<IFilterRule> SubFilters { get; }
    void Add(IFilterRule filterRule);
  }
}
