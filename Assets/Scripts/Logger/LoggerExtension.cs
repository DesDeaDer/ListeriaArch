using UnityEngine;

namespace Logger {
  public static class LoggerExtension {
    public static T Log<T>(this T obj) {
      Debug.Log(obj);

      return obj;
    }

    public static T Log<T>(this T obj, object prompt) {
      Debug.Log(prompt + obj);

      return obj;
    }
  }
}