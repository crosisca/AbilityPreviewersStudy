using UnityEngine;

public class ParabolaPreviewer : PreviewScaler
{
    [SerializeField]
    float gravity = 60;
    [SerializeField]
    float angle = 45;
    [SerializeField]
    float meshWidth = 0.2f;
    [SerializeField]
    Vector3 offset = Vector3.up * 1.6f;
    [SerializeField]
    int amountOfPoints = 40;
    
    ParabolaMesh trajectoryParabola;

    public override void Initialize ()
    {
        base.Initialize();
        trajectoryParabola = scalableQuad.gameObject.AddComponent<ParabolaMesh>();
        trajectoryParabola.Initialize(previewer.Champion, positioner.Target, gravity, angle, meshWidth, offset, previewConfig.Material, amountOfPoints);
    }
    
    public override void SetScale()
    {
        trajectoryParabola.ForceUpdate();
    }
}