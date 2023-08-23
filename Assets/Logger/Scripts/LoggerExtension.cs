using System.Runtime.CompilerServices;

namespace Logger {
  public static class LoggerExtension {

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Log<T>(this T obj) {
#if LOGGINING
      UnityEngine.Debug.Log(obj);
#endif

      return obj;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Log<T>(this T obj, object prompt) {
#if LOGGINING
      UnityEngine.Debug.Log($"{prompt}[{obj}]");
#endif

      return obj;
    }
  }
}