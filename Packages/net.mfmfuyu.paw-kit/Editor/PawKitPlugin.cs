using System.Linq;
using AnimatorAsCode.V1;
using AnimatorAsCode.V1.ModularAvatar;
using nadena.dev.ndmf;
using PawKit.Editor;
using PawKit.Editor.Builder;
using PawKit.Runtime;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

[assembly: ExportsPlugin(typeof(PawKitPlugin))]

namespace PawKit.Editor
{
    public class PawKitPlugin : Plugin<PawKitPlugin>
    {
        private const string SystemName = "PawKit";
        public override string QualifiedName => "net.mfmfuyu.paw-kit";
        public override string DisplayName => "PawKit";

        protected override void Configure()
        {
            InPhase(BuildPhase.Generating).Run($"Generate {DisplayName}", Generate);
        }

        private void Generate(BuildContext ctx)
        {
            var pawGestures = ctx.AvatarRootTransform.GetComponentsInChildren<PawGesture>(true)
                .GroupBy(c => c.gestureType)
                .Select(g => g.First())
                .ToArray();
            
            if (pawGestures.Length == 0) return;

            var aac = AacV1.Create(new AacConfiguration
            {
                SystemName = SystemName,
                AnimatorRoot = ctx.AvatarRootTransform,
                DefaultValueRoot = ctx.AvatarRootTransform,
                AssetKey = GUID.Generate().ToString(),
                AssetContainer = ctx.AssetContainer,
                DefaultsProvider = new AacDefaultsProvider()
            });

            var coreGameObject = new GameObject(SystemName)
            {
                transform = { parent = ctx.AvatarRootTransform }
            };

            var modularAvatar = MaAc.Create(coreGameObject);

            var animatorBuilder = new PawAnimatorBuilder(aac);

            var left = animatorBuilder.Build(HandSide.Left, pawGestures);
            modularAvatar.NewMergeAnimator(left.AnimatorController, VRCAvatarDescriptor.AnimLayerType.Gesture);
            var right = animatorBuilder.Build(HandSide.Right, pawGestures);
            modularAvatar.NewMergeAnimator(right.AnimatorController, VRCAvatarDescriptor.AnimLayerType.Gesture);
        }
    }
}