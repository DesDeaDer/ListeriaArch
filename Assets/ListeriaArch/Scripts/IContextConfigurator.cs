namespace ListeriaArch {
  public interface IContextConfigurator {
    ILinksConfigurator Links { get; }
    ILayersConfigurator Layers { get; }
  }
}
