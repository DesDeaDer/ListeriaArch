using Game;
using ListeriaArch;
using UnityEngine;
using Network = Game.Network;

public class CoreTest : MonoBehaviour {

  IContext context;

  void OnEnable() {
    context = new Context()
      .Links(x => x
        .Add<IGame, GameTest>()
        .Add<IStorage, Storage>()
        .Add<INetwork, Network>()
        .Add<IScenes, Scenes>()
        .Add<IUIs, UIs>())
      .LayerRule<IData>()
      .LayerRule<IProcess>(rule => rule
        .Add<IModel>()
        .Add<IData>()
        .Add<ISystem>())
      .LayerRule<IProcess>(rule => rule
        .Add<IModel>()
        .Add<IData>()
        .Add<ISystem>())
      .LayerRule<ISystem>(rule => rule
        .Add<IModel>()
        .Add<IData>()
        .Add<ISystem>())
      .Resolve()
      .Run<IEnable>(x => x.Enable);
  }

  void OnDisable() {
    context
      .Run<IDisable>(x => x.Disable)
      .Run<IRelease>(x => x.Release)
      .Release();

    context = null;
  }
}
