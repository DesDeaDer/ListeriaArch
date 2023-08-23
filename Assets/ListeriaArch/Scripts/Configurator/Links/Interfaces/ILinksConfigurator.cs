﻿namespace ListeriaArch {

  namespace Configurator {
    namespace Links {
      public interface ILinksConfigurator {
        void Register<T>();
        void Register<D, T>();
      }
    }
  }
}