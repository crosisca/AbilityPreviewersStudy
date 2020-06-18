using UnityEngine;

public class ScaleToMouseScaler : PreviewScaler
{
    [SerializeField]
    bool canScale = true;

    Transform scalableMeshPivot;

    [SerializeField]
    string areaSizeVar;

    [SerializeField]
    bool limitMaxRange;

    [SerializeField]
    bool limitMinRange;

    public override void Initialize(AbilityPreviewer previewer, PreviewConfig<object> previewConfig)
    {
        base.Initialize(previewer, previewConfig);

        scalableMeshPivot = new GameObject("Pivot").transform;
        scalableMeshPivot.SetParent(positioner.PositionerTransform); ;
        scalableMeshPivot.localPosition = Vector3.zero;
        scalableMeshPivot.localRotation = Quaternion.identity;
        scalableMeshPivot.localScale = (Vector3)previewConfig.Variables[areaSizeVar];

        Transform scalablePlane = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
        scalablePlane.name = "AimToMouseQuad";
        scalablePlane.SetParent(scalableMeshPivot, false);
        scalablePlane.localPosition = new Vector3(0, 0, 0.5f);
        scalablePlane.localRotation = Quaternion.Euler(90, 0, 0);
        scalablePlane.GetComponent<Renderer>().material = previewConfig.Material;
    }

    public override void SetScale ()
    {
        if (!canScale)
            return;

        float originalMaxDistance = ((Vector3) previewConfig.Variables[areaSizeVar]).z;

        Vector3 newScale = (Vector3)previewConfig.Variables[areaSizeVar];

        float mouseDistance = Vector3.Distance(positioner.Origin, positioner.TargetPosition);
        newScale.z = mouseDistance;

        if (limitMaxRange)
            newScale.z = Mathf.Min(mouseDistance, originalMaxDistance);

        if (limitMinRange)
            newScale.z = Mathf.Max(mouseDistance, originalMaxDistance);

        scalableMeshPivot.localScale = newScale;
    }
}