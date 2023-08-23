using ListeriaArch.Configurator.ResolversRuntime;

namespace ListeriaArch.FluidAPI {
  public static class ResolversRuntimeConfiguratorExtension {
    public static IResolversRuntimeConfigurator Add<T>(this IResolversRuntimeConfigurator c) {
      c.Register<T>();

      return c;
    }
  }
}
