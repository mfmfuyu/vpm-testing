using System;
using PawKit.Runtime;
using UnityEditor;
using UnityEngine;

namespace PawKit.Editor
{
    public class PawKitEditor
    {
        [MenuItem("GameObject/PawKit/Template")]
        public static void CreateTemplate()
        {
            var parent = Selection.activeGameObject;

            var go = new GameObject("PawKit");
            if (parent != null) go.transform.SetParent(parent.transform, false);

            foreach (GestureType g in Enum.GetValues(typeof(GestureType)))
            {
                var child = new GameObject();
                child.transform.SetParent(go.transform, false);
                child.name = g.ToString();

                var gesture = child.AddComponent<PawGesture>();
                gesture.gestureType = g;
            }
        }
    }
}