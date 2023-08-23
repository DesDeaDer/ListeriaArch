using System;
using System.Collections.Generic;

namespace ListeriaArch {
  public interface ILayersConfigurator : IMap<Type, IFilterRule> {
    void AddInjectType(IFilterRule rule);
  }

  public interface IFilterRule {
    Type Type { get; }
    IEnumerable<IFilterRule> SubFilters { get; }
    void Add(IFilterRule filterRule);
  }

  public interface IMap<K, V> {
    IReadOnlyDictionary <K, V> Map { get; }
    V this[K k] { get; }
  }
}
