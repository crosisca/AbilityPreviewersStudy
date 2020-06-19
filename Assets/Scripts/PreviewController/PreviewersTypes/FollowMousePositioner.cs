using UnityEngine;

public class FollowMousePositioner : PreviewPositioner
{
    [SerializeField]
    bool canRotate = true;
    
    [SerializeField, AbilityDatabaseValue]
    string maxDistanceVar;


    //TODO add offset
    //[SerializeField]
    //string offsetVar;

    public override void CalculateTargetLocation ()
    {
        float maxRange = previewConfig.GetValue<float>(maxDistanceVar);

        if (!MathUtils.IsInsideCircle(Origin, maxRange, previewer.MouseHitPosition))
            Target.position = Origin + (previewer.MouseHitPosition - Origin).normalized * maxRange;
            //Target.position = Origin + (previewer.MouseHitPosition - Origin).normalized * maxRange + (previewer.Champion.rotation * previewConfig.GetValue<Vector3>(offsetVar));
        else
            Target.position = previewer.MouseHitPosition;
    }

    public override void CalculateTargetRotation ()
    {
        if(Mathf.Approximately((Target.position.FlattenY() - Origin.FlattenY()).magnitude, 0.1f))
            Target.rotation = Quaternion.identity;
        else
            Target.rotation = Quaternion.LookRotation(Target.position.FlattenY() - Origin.FlattenY(), Vector3.up);
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