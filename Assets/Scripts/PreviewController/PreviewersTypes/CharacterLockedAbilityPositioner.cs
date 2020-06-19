using UnityEngine;

public class CharacterLockedAbilityPositioner : PreviewPositioner
{
    [SerializeField, AbilityDatabaseValue]
    string offsetVector3Var;

    [SerializeField]
    bool canRotate = true;

    public override void CalculateTargetLocation ()
    {
        Target.position = previewer.Champion.TransformPoint(previewConfig.GetValue<Vector3>(offsetVector3Var));
    }

    public override void CalculateTargetRotation ()
    {
        if (Mathf.Approximately((TargetPosition.FlattenY() - Origin.FlattenY()).magnitude, 0.1f))
            Target.rotation = Quaternion.identity;
        else
            Target.rotation = Quaternion.LookRotation(TargetPosition.FlattenY() - Origin.FlattenY(), Vector3.up);
    }

    public override void SetPosition ()
    {
        PositionerTransform.position = Target.position;
    }

    public override void SetRotation ()
    {
        PositionerTransform.rotation = canRotate ? Target.rotation : Quaternion.identity;
    }
}