using System;
using System.Collections.Generic;

namespace ListeriaArch.Configurator.Links {
  public interface ILinksConfigurator {
    IEnumerable<ILinkConfigurator> Links { get; }

    void Register(ILinkConfigurator link);
  }

  public interface ILinkConfigurator {
    IEnumerable<Type> Links { get; }

    void Register(Type link);
  }

  public class Link : ILinkConfigurator {
    public IEnumerable<Type> Links => throw new NotImplementedException();
    
    List<Type> links = new();
  
    public void Register(Type type) => links.Add(type);
  }
}
