namespace ListeriaArch {
  public interface ILinksConfigurator {
    void RegisterType<T>();
    void RegisterType<D, T>();
  }
}
