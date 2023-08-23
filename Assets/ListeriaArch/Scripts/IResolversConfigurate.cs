namespace ListeriaArch {
  public interface IResolversConfigurate {
    void RegisterResolver<T>();
    void RegisterResolver<D, T>();
  }
}
