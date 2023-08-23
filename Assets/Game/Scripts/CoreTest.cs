using Game;
using ListeriaArch;
using UnityEngine;
using Network = Game.Network;

public class CoreTest : MonoBehaviour {

  IContext context;

  void OnEnable() {
    context = new ContextConfigurator()
      .Links(links => links
        .Add<IAccountTest, AccountTest>()
        .Add<IStorage, Storage>()
        .Add<INetwork, Network>()
        .Add<IScenes, Scenes>()
        .Add<IUIs, UIs>())
      .Layers(layers => layers
        .Layer<IData>()
        .Layer<IProcess>(rules => rules
          .Maybe<IModel>(model => model
            .Without<IData>())
          .Maybe<ISystem>(system => system
            .Without<IData>()
            .Need<ISystem>())
          .Maybe<IData>())
        .Layer<IModel>(model => model
          .Maybe<IData>())
        .Layer<ISystem>(system => system
          .Maybe<IModel>()
          .Maybe<IData>()
          .Maybe<ISystem>()))
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
