using System;
using UnityEngine;

namespace _CodeBase.LevelModule.ProgressSaver.Entities
{
  [Serializable]
  public class SaveCardData
  {
    public int ID;
    public Sprite SpriteName;
    public float PosX;
    public float PosY;
    public Vector3 LocalScale;
    public bool IsActive;
  }
}