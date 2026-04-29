using System;
using AnimatorAsCode.V1;
using AnimatorAsCode.V1.VRC;
using UnityEngine;

namespace PawKit.Editor
{
    public enum HandSide
    {
        Left,
        Right
    }

    public static class SideExtensions
    {
        public static AvatarMask ToAvatarMask(this HandSide handSide, AacFlBase aac)
        {
            return handSide switch
            {
                HandSide.Left => aac.VrcAssets().LeftHandAvatarMask(),
                HandSide.Right => aac.VrcAssets().RightHandAvatarMask(),
                _ => throw new ArgumentOutOfRangeException(nameof(handSide))
            };
        }

        public static AacAv3.Av3TrackingElement ToAv3TrackingElement(this HandSide handSide)
        {
            return handSide switch
            {
                HandSide.Left => AacAv3.Av3TrackingElement.LeftFingers,
                HandSide.Right => AacAv3.Av3TrackingElement.RightFingers,
                _ => throw new ArgumentOutOfRangeException(nameof(handSide))
            };
        }

        public static AacFlEnumIntParameter<AacAv3.Av3Gesture> ToAv3Gesture(this HandSide handSide, AacFlLayer layer)
        {
            return handSide switch
            {
                HandSide.Left => layer.Av3().GestureLeft,
                HandSide.Right => layer.Av3().GestureRight,
                _ => throw new ArgumentOutOfRangeException(nameof(handSide))
            };
        }
    }
}