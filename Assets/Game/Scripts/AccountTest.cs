using System;
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
      Account.Name;
    }

    public void Disable() {
      "Disable".Log("AccountUpdate");
    }
  }


  public interface IEnable {
    void Enable();
  }

  public interface IDisable {
    void Disable();
  }

  public interface IRelease {
    void Release();
  }
  
  public interface IAccountUpdate : IProcess { }
  public interface IAccountTest : IProcess { }
  
  public interface INetwork : ISystem { }
  public interface IStorage : ISystem { }
  public interface IScenes : ISystem { }
  public interface IUIs : ISystem { }

  public class Account : IData {
    public uint Id { get; set; }
    public string Name { get; set; }
  }

  public class Network : INetwork {

  }

  public class Storage : IStorage {

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