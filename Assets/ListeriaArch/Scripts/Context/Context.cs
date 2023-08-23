using System;
using System.Collections.Generic;
using ListeriaArch.Configurator.Layers;

namespace ListeriaArch {
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
}
