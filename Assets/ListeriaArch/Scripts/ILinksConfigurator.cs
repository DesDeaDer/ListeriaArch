namespace ListeriaArch {

  namespace Configurator {
    namespace Links {
      public interface ILinksConfigurator {
        void Register<T>();
        void Register<D, T>();
      }

      public interface IResolversConfigurator {
        void Register<T>();
      }

      public interface IResolversRuntimeConfigurator {
        void Register<T>();
      }
    }
  }
}