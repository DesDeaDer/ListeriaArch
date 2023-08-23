using System;

namespace ListeriaArch.FluidAPI {
  public static class ContextExtension {

    public static IContext Run<T>(this IContext c, Func<T, Action> getProcessing) {
      c.Run(getProcessing);

      return c;
    }
  }
}
