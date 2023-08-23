namespace ListeriaArch {
  public interface IProvider<ID, V> {
    V this[ID id] { get; }
  }
}
