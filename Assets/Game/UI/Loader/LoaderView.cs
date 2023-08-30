using Game.UI;
using UnityEngine;

public class ViewBase : MonoBehaviour, IView {
  public void Show() {
  
  }

  public void Hide() {
 
  }
}

public class LoaderView : ViewBase, ILoaderView {
  [SerializeField] 

  public void SetProgress(float progress) {
    throw new System.NotImplementedException();
  }

  public void SetText(string text) {
    throw new System.NotImplementedException();
  }
}
