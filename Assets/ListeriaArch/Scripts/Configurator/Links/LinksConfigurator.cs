using System.Collections.Generic;

namespace ListeriaArch.Configurator.Links {
  public class LinksConfigurator : ILinksConfigurator {
    List<ILinkConfigurator> links = new();
    public IEnumerable<ILinkConfigurator> Links => links;

    public void Register(ILinkConfigurator link) => links.Add(link);
  }
}
