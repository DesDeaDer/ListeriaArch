using ListeriaArch.Configurator.Links;

namespace ListeriaArch {
  namespace Configurator {

    public static class LinksConfiguratorExtension {
      public static ILinksConfigurator Add<T>(this ILinksConfigurator configurator) {
        configurator.Register<T>();

        return configurator;
      }

      public static ILinksConfigurator Add<D, T>(this ILinksConfigurator configurator) {
        configurator.Register<D, T>();

        return configurator;
      }
    }
  }
}
