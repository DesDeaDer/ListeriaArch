using System;
using System.Collections.Generic;

namespace ListeriaArch.FluidAPI {
  public static class ContextExtension {
    public static IContext Run<T>(this IContext c, Func<T, Action> getProcessing) {
      c.Run(getProcessing);

      return c;
    }

    public static IEnumerable<IContext> Run<T>(this IEnumerable<IContext> contexts, Func<T, Action> getProcessing) {
      foreach (var context in contexts) {
        context.Run(getProcessing);
      }
      
      return contexts;
    }

    public static IEnumerable<IContext> Release(this IEnumerable<IContext> contexts) {
      foreach (var context in contexts) {
        context.Release();
      }

      return contexts;
    }
  }
}
