using Sirenix.OdinInspector;
using UnityEngine;

public class ParabolaScaler : PreviewScaler
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
        trajectoryParabola = scalableMesh.gameObject.AddComponent<ParabolaMesh>();
        trajectoryParabola.Initialize(previewer.Champion, positioner.Target, gravity, angle, meshWidth, offset, Material, amountOfPoints);
    }
    
    public override void SetScale()
    {
        trajectoryParabola.ForceUpdate();
    }
}