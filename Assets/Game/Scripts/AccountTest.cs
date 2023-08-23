using ListeriaArch;
using Logger;

namespace Game {
  public class AccountTest : IAccountTest, IEnable, IDisable {
    [Inject] IStorage Storage { get; set; }
    [Inject] IScenes Scenes { get; set; }
    [Inject] INetwork Network { get; set; }
    [Inject] IUIs UIs { get; set; }
    [Inject] Account Account { get; set; }

    public void Enable() {
      "Enable".Log("AccountTest");

      Account.Id.Log();
      Account.Name.Log();
    }

    public void Disable() {
      "Disable".Log("AccountTest");

      Account.Id.Log();
      Account.Name.Log();
    }
  }

  public class AccountUpdate : IAccountUpdate, IEnable, IDisable {
    [Inject] IStorage Storage { get; set; }
    [Inject] IScenes Scenes { get; set; }
    [Inject] INetwork Network { get; set; }
    [Inject] IUIs UIs { get; set; }
    [Inject] Account Account { get; set; }

    public void Enable() {
      "Enable".Log("AccountUpdate");

      Account.Id = (uint)UnityEngine.Random.value.Log();
      Account.Name = "abcdefgh".GetRandom(10);
    }

    public void Disable() {
      "Disable".Log("AccountUpdate");
    }
  }

  public static class StringUnityExtension {
    public static char GetRandom(this string arr) => arr[UnityEngine.Random.Range(0, arr.Length)];

    public static string GetRandom(this string set, int length) {
      var chrs = new char[length];

      for (int i = 0; i < length; i++) {
        chrs[i] = set.GetRandom();
      }

      return new string(chrs);
    }
  }

  public interface IEnable {
    void Enable();
  }

  public interface IDisable {
    void Disable();
  }

  public interface IAccountUpdate : IProcess { }
  public interface IAccountTest : IProcess { }

  public interface INetwork : ISystem { }
  public interface INetworkTest : INetwork { }
  public interface INetworkGame1 : INetwork { }
  public interface INetworkGame2 : INetwork { }

  public interface IStorage : ISystem { }
  public interface IStorageFile : IStorage { }
  public interface IStorageFileJson : IStorageFile { }
  public interface IStorageUnity : IStorage { }

  public interface IScenes : ISystem { }
  public interface IUIs : ISystem { }

  public class Account : IData {
    public uint Id { get; set; }
    public string Name { get; set; }
  }

  public class NetworkTest : INetworkTest { }
  public class NetworkGame1 : INetworkGame1 { }
  public class NetworkGame2 : INetworkGame2 { }

  public class Storage : IStorageFile, IStorageUnity {

  }

  public class Scenes : IScenes {

  }

  public class UIs : IUIs {
    public enum ViewID {
      None = 0,
      GameLoad = 1,
    }

    IView Get(ViewID viewID) {
      return default;
    }
  }


}