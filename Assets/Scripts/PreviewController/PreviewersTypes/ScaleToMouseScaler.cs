using UnityEngine;

public class ScaleToMouseScaler : PreviewScaler
{
    Transform scalableMeshPivot;

    [SerializeField, AbilityDatabaseValue]
    string areaSizeVar;

    [SerializeField]
    bool limitMaxRange;

    [SerializeField]
    bool limitMinRange;

    public override void Initialize ()
    {
        base.Initialize();

        scalableMeshPivot = new GameObject("Pivot").transform;
        scalableMeshPivot.SetParent(positioner.PositionerTransform); ;
        scalableMeshPivot.localPosition = Vector3.zero;
        scalableMeshPivot.localRotation = Quaternion.identity;

        scalableQuad.name = "AimToMouseScalableQuad";
        scalableQuad.SetParent(scalableMeshPivot, false);
        scalableQuad.localPosition = new Vector3(0, 0, 0.5f);
        scalableQuad.localRotation = Quaternion.Euler(90, 0, 0);
    }
    
    public override void SetScale ()
    {
        float originalMaxDistance = previewConfig.GetValue<Vector3>(areaSizeVar).z;

        Vector3 newScale = previewConfig.GetValue<Vector3>(areaSizeVar);

        float mouseDistance = Vector3.Distance(positioner.Origin, positioner.TargetPosition);
        newScale.z = mouseDistance;

        if (limitMaxRange)
            newScale.z = Mathf.Min(mouseDistance, originalMaxDistance);

        if (limitMinRange)
            newScale.z = Mathf.Max(mouseDistance, originalMaxDistance);

        scalableMeshPivot.localScale = newScale;
    }
}