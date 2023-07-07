using UnityEngine;

namespace Utilities
{
    public class ObjectTypes : MonoBehaviour
    {
      public enum Type
      {
          None,
          Character,
          DeadParticleEffect,
          Coin
          
      }

      public Type objectType;
    }
}
