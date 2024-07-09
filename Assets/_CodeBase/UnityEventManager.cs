using System;
using UnityEngine;

namespace _CodeBase
{
  public class UnityEventManager : MonoBehaviour
  {
    public Action UpdateEvent => _updateEvent;
    private Action _updateEvent;

    private void Update() => 
      UpdateEvent?.Invoke();
  }
}