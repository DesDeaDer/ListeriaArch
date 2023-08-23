using System;
using System.Collections.Generic;

namespace ListeriaArch {

  public class InjectAttribute : System.Attribute { }
  public class InjectAttribute<T> : System.Attribute { }

  //-------------------------

  public class Links {

  }

  public class Injector {
    public void Inject(...) { }
  }

  //-------------------------

  public interface ILayerConfigurator {
    void AddInjectType<T>();
  }

  public static class LayerConfigurator : ILayerConfigurator {
    public void AddInjectType<T>() {
      throw new NotImplementedException();
    }
  }

  public static class LayerConfiguratorExtension {
    public static ILayerConfigurator Add<Layer>(this ILayerConfigurator configurator) {
      configurator.AddInjectType<Layer>();
      return configurator;
    }
  }

  public interface ILinksConfigurator {
    void RegisterType<T>();
    void RegisterType<D, T>();
  }

  public static class LinksConfiguratorExtension {
    public static ILinksConfigurator Add<T>(this ILinksConfigurator configurator) {
      configurator.RegisterType<T>();
      return configurator;
    }

    public static ILinksConfigurator Add<D, T>(this ILinksConfigurator configurator) {
      configurator.RegisterType<D, T>();
      return configurator;
    }
  }

  public interface IContextConfigurator {
    void AddLinks(ILinksConfigurator linksConfigurator);
    void AddLayers(ILayerConfigurator linksConfigurator);
  }

  public interface IContext {
    void ResolveAll();
    void Release();
  }

  public class ContextConfigurator : IContextConfigurator {


    public ILayerConfigurator CreateLayerRule<T>() {
    }

    public ILinksConfigurator CreateLinks() {
    }

    public void Process<T>(Func<T, Action> proc) {
    }

    public void Release() {
    }
  }

  public class Context : IContext {
    public ILayerConfigurator CreateLayerRule<T>() {
      throw new NotImplementedException();
    }

    public void Process<T>(Func<T, Action> getProcessing) { //TODO: use code Tree for parce static version of method
      var objs = default(IEnumerable<T>); //TODO

      foreach (var obj in objs) {
        getProcessing(obj)();
      }
    }

    public void Register<P>(IProvider<P, Type> dependencies) {
      throw new NotImplementedException();
    }

    public void Release() {
      throw new NotImplementedException();
    }

    public void ResolveAll() {
      throw new NotImplementedException();
    }
  }

  public static class ContextChainExtension {
    public static IContext Links(this IContext c, Action<ILinksConfigurator> proc) {
      var links = c.CreateLinks();

      proc(links);

      return c;
    }

    public static IContext LayerRule<L>(this IContext c, Action<ILayerConfigurator> proc) {
      var layer = c.CreateLayerRule<L>();

      proc(layer);

      return c;
    }

    public static IContext LayerRule<L>(this IContext c) {
      var layer = c.CreateLayerRule<L>();

      //TODO

      return c;
    }

    public IContext Run<T>(this IContext c, Func<T, Action> getProcessing) {


      return c;
    }
  }

  public interface IProvider<ID, V> {
    V this[ID id] { get; }
  }

}
