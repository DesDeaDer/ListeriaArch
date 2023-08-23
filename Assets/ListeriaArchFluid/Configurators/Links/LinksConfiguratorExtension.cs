using ListeriaArch.Configurator.Links;

namespace ListeriaArch.FluidAPI {
  public static class LinksConfiguratorExtension {
    public static ILinksConfigurator Add<T>(this ILinksConfigurator configurator) {
      var link = Listeria.ConfigurateLink();

      link.Register(typeof(T));

      configurator.Register(link);

      return configurator;
    }

    public static ILinksConfigurator Add<D, T>(this ILinksConfigurator configurator) {
      var link = Listeria.ConfigurateLink();

      link.Register(typeof(D));
      link.Register(typeof(T));

      configurator.Register(link);

      return configurator;
    }

    public static ILinksConfigurator Add(this ILinksConfigurator configurator, params ILinkConfigurator[] links) {
      foreach (var link in links) {
        configurator.Register(link);
      }

      return configurator;
    }

    public static ILinksConfigurator Add(this ILinksConfigurator configurator, ILinkConfigurator link) {
      configurator.Register(link);

      return configurator;
    }
  }
}
