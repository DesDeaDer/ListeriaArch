using ListeriaArch.Configurator.Resolvers;

namespace ListeriaArch.FluidAPI {
  public static class ResolversConfiguratorExtension {
    public static IResolversConfigurator Add<T>(this IResolversConfigurator c) {
      c.Register<T>();

      return c;
    }
  }
}
