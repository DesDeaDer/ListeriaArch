using ListeriaArch;
using Logger;

namespace Game {
  public class GameTest : IGame, IEnable, IDisable {
    [Inject] IStorage Storage { get; set; }
    [Inject] IScenes Scenes { get; set; }
    [Inject] INetwork Network { get; set; }
    [Inject] IUIs UIs { get; set; }
    [Inject] Account Account { get; set; }

    public void Enable() {
      "GameTest Enable".Log("+");
      
      Account.Id.Log();
      Account.Name.Log();


      "GameTest Enable".Log("-");
    }

    public void Disable() {
      "GameTest Disable".Log("+");

      
      "GameTest Disable".Log("-");
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

  public interface IGame : IProcess { }
  public interface INetwork : ISystem { }
  public interface IStorage : ISystem { }
  public interface IScenes : ISystem { }
  public interface IUIs : ISystem { }

  public class Account : IData {
    [Inject<IStorage> ]public uint Id { get; set; }
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