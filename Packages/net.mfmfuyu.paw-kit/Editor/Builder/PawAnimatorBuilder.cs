using AnimatorAsCode.V1;
using AnimatorAsCode.V1.VRC;
using PawKit.Runtime;
using UnityEngine;

namespace PawKit.Editor.Builder
{
    public class PawAnimatorBuilder
    {
        private readonly AacFlBase aac;

        public PawAnimatorBuilder(AacFlBase aac)
        {
            this.aac = aac;
        }

        public AacFlController Build(HandSide handSide, PawGesture[] gestures)
        {
            var ctrl = aac.NewAnimatorController();

            var avatarMask = handSide.ToAvatarMask(aac);
            var layer = ctrl.NewLayer(handSide.ToString()).WithAvatarMask(avatarMask);

            var trackingElement = handSide.ToAv3TrackingElement();
            var idleState = layer.NewState("Idle").TrackingTracks(trackingElement);

            var av3GestureParameter = handSide.ToAv3Gesture(layer);
            foreach (var gesture in gestures)
            {
                var name = gesture.gestureType.ToString();

                var state = layer.NewState(name)
                    .WithAnimation(gesture.clip)
                    .TrackingAnimates(trackingElement);

                var av3Gesture = gesture.gestureType.ToAv3();

                idleState.TransitionsTo(state)
                    .When(av3GestureParameter.IsEqualTo(av3Gesture));

                state.Exits().When(av3GestureParameter.IsNotEqualTo(av3Gesture));
            }

            return ctrl;
        }
    }
}