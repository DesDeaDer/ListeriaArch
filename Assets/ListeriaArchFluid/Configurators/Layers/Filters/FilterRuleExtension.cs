using System;
using ListeriaArch.Configurator.Layers.Filters;

namespace ListeriaArch.FluidAPI {
  public static class FilterRuleExtension {
    public static IFilterRule Maybe<T>(this IFilterRule rule) {
      var maybe = Listeria.ConfigurateFilterRuleMaybe<T>();

      rule.Add(maybe);

      return rule;
    }

    public static IFilterRule Maybe<T>(this IFilterRule rule, Action<IFilterRule> proc) {
      var maybe = Listeria.ConfigurateFilterRuleMaybe<T>();

      proc(maybe);

      rule.Add(maybe);

      return rule;
    }

    public static IFilterRule Without<T>(this IFilterRule rule) {
      var without = Listeria.ConfigurateFilterRuleExcept<T>();

      rule.Add(without);

      return rule;
    }

    public static IFilterRule Need<T>(this IFilterRule rule) {
      var need = Listeria.ConfigurateFilterRuleRequire<T>();

      rule.Add(need);

      return rule;
    }
  }
}
