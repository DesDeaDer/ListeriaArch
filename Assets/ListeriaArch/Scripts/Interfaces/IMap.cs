using System.Collections.Generic;

namespace ListeriaArch {
  public interface IMap<K, V> {
    IReadOnlyDictionary <K, V> Map { get; }
    V this[K k] { get; }
  }
}
