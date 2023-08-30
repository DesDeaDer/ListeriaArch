using Game;
using Game.UI;
using ListeriaArch;
using ListeriaArch.FluidAPI;
using UnityEngine;

public class CoreTest : MonoBehaviour {
  IContext contextMain;
  IContext contextViews;

  IContextConfigurator GetContextMain() => Listeria
    .Create()
    .Layers(layers => layers
      .Layer<IData>(rules => rules
        .Repository<Game.Network.Data.IData>()
        .Repository<Game1.Network.Data.IData>()
        .Repository<Game2.Network.Data.IData>())
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
        .Maybe<IData>(rules => rules
          .Repository<Game.Network.Data.IData>())
        .Maybe<ISystem>()))
    .Resolvers(resolvers => resolvers
      .Add<IStorage>())
    .ResolversRuntime(resolversRuntime => resolversRuntime
      .Add<INetwork>());

  IContextConfigurator GetContextViews() => Listeria
    .Create()
      .Layers(layers => layers
        .Layer<Game.UI.IView>()
        .Layer<IViewController>(rules => rules
          .Need<Game.UI.IView>()
          .Maybe<IViewData>())
        .Layer<IViewData>())
      .ResolversRuntime(resolversRuntime => resolversRuntime
        .Add<IStorage>());

  void OnEnable() {
    contextMain = GetContextMain()
      .Links(links => links
        .Add<IAccount>()
        .Add<IAccountTest>()
        .Add<IScenes>()
        .Add<IUIs>()
        .Add<IStorage>())
      .Build()
      .Run<IEnable>(x => x.Enable);

    contextViews = GetContextViews()
      .Links(links => links
        .Add<IResourceLoader>()
        .Add<IResourceLinker>())
      .Build()
      .Run<IEnable>(x => x.Enable);
  }

  void OnDisable() {
    new [] { contextMain, contextViews}
      .Run<IDisable>(x => x.Disable)
      .Run<IRelease>(x => x.Release)
      .Release();

    contextMain = null;
    contextViews = null;
  }
}
