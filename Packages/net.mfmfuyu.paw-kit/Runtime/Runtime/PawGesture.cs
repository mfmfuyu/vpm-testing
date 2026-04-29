using UnityEngine;
using VRC.SDKBase;

namespace PawKit.Runtime
{
    public class PawGesture : MonoBehaviour, IEditorOnly
    {
        public GestureType gestureType;
        public AnimationClip clip;
    }
}