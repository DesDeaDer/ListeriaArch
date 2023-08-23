using System;
using System.Collections.Generic;
using ListeriaArch.Configurator.Links;

namespace ListeriaArch {

  public class InjectAttribute : System.Attribute { }

  //-------------------------

  public class Links {

  }

  public class Injector {
    public void Inject() { }
  }

  public interface IResolver<T> {

  }

  public class LayersConfigurator : ILayersConfigurator {
    Dictionary<Type, IFilterRule> rules = new();

    public IFilterRule this[Type k] => throw new NotImplementedException();

    public IReadOnlyDictionary<Type, IFilterRule> Map => throw new NotImplementedException();

    public void AddInjectType(IFilterRule rule) {
      if (rules.ContainsKey(rule.Type)) {
        throw new Exception();
      }

      rules[rule.Type] = rule;
    }
  }

  public class LayerRuleBasic : IFilterRule {
    public Type Type { get; init; }

    public IEnumerable<IFilterRule> SubFilters => rules;

    List<IFilterRule> rules = new();

    public void Add(IFilterRule rule) => rules.Add(rule);
  }

  public class LayerRuleMaybe : LayerRuleBasic { }
  public class LayerRuleRequire : LayerRuleBasic { }
  public class LayerRuleExcept : LayerRuleBasic { }

  public class LinksConfigurator : ILinksConfigurator {
    public void Register<T>() {
      throw new NotImplementedException();
    }

    public void Register<D, T>() {
      throw new NotImplementedException();
    }
  }

  public class ResolversConfigurator : IResolversConfigurator {
    public void Register<T>() {
      throw new NotImplementedException();
    }
  }

  public class ResolversRuntimeConfigurator : IResolversRuntimeConfigurator {
    public void Register<T>() {
      throw new NotImplementedException();
    }
  }

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

  public static class LayersConfiguratorExtension {
    public static ILayersConfigurator Layer<Layer>(this ILayersConfigurator configurator) {
      var layer = Listeria.ConfigurateFilterRuleBasic<Layer>();

      configurator.AddInjectType(layer);

      return configurator;
    }

    public static ILayersConfigurator Layer<Layer>(this ILayersConfigurator configurator, Action<IFilterRule> proc) {
      var layer = Listeria.ConfigurateFilterRuleBasic<Layer>();

      proc(layer);

      configurator.AddInjectType(layer);

      return configurator;
    }
  }

  public static class ResolversConfiguratorExtension {
    public static IResolversConfigurator Add<T>(this IResolversConfigurator c) {
      c.Register<T>();

      return c;
    }
  }

  public static class ResolversRuntimeConfiguratorExtension {
    public static IResolversRuntimeConfigurator Add<T>(this IResolversRuntimeConfigurator c) {
      c.Register<T>();

      return c;
    }
  }


  public interface IRelease {
    void Release();
  }

  public interface IContext : IRelease {
    void ResolveAll(); //TODO temp
    void Processing<T>(Func<T, Action> getProcessing); //TODO temp
  }

  public class ContextConfigurator : IContextConfigurator {
    public ILayersConfigurator Layers { get; init; }
    public ILinksConfigurator Links { get; init; }
    public IResolversConfigurator Resolvers { get; init; }
    public IResolversRuntimeConfigurator ResolversRuntime { get; init; }
  }

  public class Context : IContext {
    public ILayersConfigurator CreateLayerRule<T>() {
      throw new NotImplementedException();
    }

    public void Processing<T>(Func<T, Action> getProcessing) { //TODO: use code Tree for parce static version of method
      var objs = default(IEnumerable<T>); //TODO

      foreach (var obj in objs) {
        getProcessing(obj)();
      }
    }

    public void Release() {
      throw new NotImplementedException();
    }

    public void ResolveAll() {
      throw new NotImplementedException();
    }
  }

  public static class ContextExtension {

    public static IContext Run<T>(this IContext c, Func<T, Action> getProcessing) {
      c.Run(getProcessing);

      return c;
    }
  }

  public static class ContextConfiguratorExtension {
    public static IContextConfigurator Layers(this IContextConfigurator c, Action<ILayersConfigurator> proc) {
      proc(c.Layers);

      return c;
    }
    
    public static IContextConfigurator Links(this IContextConfigurator c, Action<ILinksConfigurator> proc) {
      proc(c.Links);

      return c;
    }

    public static IContextConfigurator Resolvers(this IContextConfigurator c, Action<IResolversConfigurator> proc) {
      proc(c.Resolvers);

      return c;
    }

    public static IContextConfigurator ResolversRuntime(this IContextConfigurator c, Action<IResolversRuntimeConfigurator> proc) {
      proc(c.ResolversRuntime);

      return c;
    }

    public static IContext Build(this IContextConfigurator c) {
      var context = Listeria.Context();

      //TODO proc c

      context.ResolveAll();

      return context;
    }
  }

  public static class Listeria {
    public static IContext Context() {

      return new Context();
    }

    public static IContextConfigurator ArchStrucure() => new ContextConfigurator() {
      Layers = ConfigurateLayers(),
      Links = ConfigurateLinks(),
      Resolvers = ConfigurateResolvers(),
      ResolversRuntime = ConfigurateResolversRuntime(),
    };

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
  }
}
