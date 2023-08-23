using Game;
using ListeriaArch;
using ListeriaArch.FluidAPI;
using UnityEngine;

public class CoreTest : MonoBehaviour {

  IContext context;

  void OnEnable() {
    context = Listeria
      .Create()
      .Layers(layers => layers
        .Layer<IData>()
        .Layer<IProcess>(rules => rules
          .Maybe<IModel>(models => models
            .Without<IData>())
          .Maybe<ISystem>(systems => systems
            .Without<IData>()
            .Need<ISystem>())
          .Maybe<IData>())
        .Layer<IModel>(models => models
          .Maybe<IData>())
        .Layer<ISystem>(systems => systems
          .Maybe<IModel>()
          .Maybe<IData>()
          .Maybe<ISystem>()))
      .Links(links => links
        .Add<IAccountTest>()
        .Add<IScenes>()
      .Add<IUIs>()
        .Add<IStorage>())
      .Resolvers(resolvers => resolvers
        .Add<INetwork>())
      .ResolversRuntime(resolversRuntime => resolversRuntime
        .Add<INetwork>())
      .Build()
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
