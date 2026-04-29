using System;
using AnimatorAsCode.V1.VRC;
using PawKit.Runtime;

namespace PawKit.Editor
{
    public static class GestureTypeExtensions
    {
        public static AacAv3.Av3Gesture ToAv3(this GestureType gestureType)
        {
            var id = (int)gestureType;
            if (Enum.IsDefined(typeof(GestureType), id)) return (AacAv3.Av3Gesture)gestureType;

            throw new ArgumentOutOfRangeException("Unknown gesture type: " + gestureType);
        }
    }
}