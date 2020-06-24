using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterLockedAbilityPositioner : PreviewPositioner
{
    public override void Setup(AbilityPreviewer previewer, PreviewConfig previewConfig)
    {
        base.Setup(previewer, previewConfig);

        PositionerTransform.rotation = Quaternion.identity;
    }

    public override void CalculateTargetLocation ()
    {
        Target.position = OriginPosition;
    }
    
    public override void SetPosition ()
    {
        PositionerTransform.position = TargetPosition;
    }
}