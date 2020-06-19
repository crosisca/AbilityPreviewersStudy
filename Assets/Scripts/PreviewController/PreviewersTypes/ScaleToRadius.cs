using UnityEngine;

public class ScaleToRadius : PreviewScaler
{
    [SerializeField, AbilityDatabaseValue]
    string radiusVar;

    Transform scalableQuad;

    const float radiusScaleRatio = 2f;

    public override void Initialize ()
    {
        base.Initialize();

        scalableQuad = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
        scalableQuad.name = "ScaleToRadiusQuad";
        scalableQuad.SetParent(positioner.PositionerTransform);
        scalableQuad.transform.localPosition = Vector3.zero;
        scalableQuad.transform.localRotation = Quaternion.Euler(90, 0, 0);

        scalableQuad.GetComponent<Renderer>().material = previewConfig.Material;
    }

    public override void SetScale ()
    {
        scalableQuad.localScale = Vector3.one * previewConfig.GetValue<float>(radiusVar) * radiusScaleRatio;
    }
}