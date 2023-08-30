using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Logger;
using UnityEditor;
using Assembly = System.Reflection.Assembly;

namespace UITools {
  public class CA {
    public static void Do(object o, ref object o1, in object o2, out object o3) {
      o3 = default;
      "-".Log();
    }
  }

  public class CB {
    public static void Do(object o, ref object o1, in object o2, out object o3) {
      o3 = default;
      "+".Log();
    }
  }

  public static class InvokeProxy {
    [MenuItem("AAAAA/AAA")]
    public static void A() {
      var i = default(object);

      var d = typeof(CA)
        .GetMethod<DDo>("CB", CA.Do);

      d(i, ref i, in i, out i);
    }

    delegate void DDo(object o, ref object o1, in object o2, out object o3);

    internal static TD GetMethod<TD>(this Type assemblyTypeAny, string typeName, TD d) where TD : Delegate {
      var assembly = Assembly.GetAssembly(assemblyTypeAny);
      var type = assembly.GetType($"{assembly.GetName().Name}.{typeName}");
      var m = d.GetMethodInfo();
      var ps = m
        .GetParameters()
        .Select(x => x.ParameterType)
        .ToArray();
      var mnew = type.SearchSome(m);
      var mres = (MethodInfo)mnew;

      return mres.AsMethod<TD>(ps);
    }

    public static T AsMethod<T>(this MethodInfo methodInfo, params Type[] paramTypes)
      where T : Delegate {
      var methodParams = paramTypes.Select(Expression.Parameter).ToArray();

      return Expression
        .Lambda<T>(Expression.Call(methodInfo, methodParams), methodParams)
        .Compile();
    }
  }

  public static class MemberInfoExtension {
    public static MemberInfo SearchSome(this Type type, MemberInfo m) => type
      .GetMember(m.Name, m.MemberType, m.GetBindingFlags())
      ?[0]
      ?? default;

    public static BindingFlags GetBindingFlags(this MemberInfo m) => 
      BindingFlags.DeclaredOnly
      | BindingFlags.Instance
      | BindingFlags.Static
      | BindingFlags.Public
      | BindingFlags.NonPublic
      | BindingFlags.FlattenHierarchy
      | BindingFlags.InvokeMethod
      | BindingFlags.CreateInstance
      | BindingFlags.GetField
      | BindingFlags.SetField
      | BindingFlags.GetProperty
      | BindingFlags.SetProperty
      | BindingFlags.PutDispProperty
      | BindingFlags.PutRefDispProperty
      | BindingFlags.ExactBinding
      | BindingFlags.SuppressChangeType
      | BindingFlags.OptionalParamBinding
      | BindingFlags.IgnoreReturn;
  }
}
