using UnityEngine;

public class ParabolaPreviewer : MonoBehaviour, IAbilityPreviewer
{
    [SerializeField]
    Material scalablePlaneMaterial;

    [SerializeField]
    float gravity = 60;
    [SerializeField]
    float angle = 45;
    [SerializeField]
    float meshThickness = 0.2f;
    [SerializeField]
    Vector3 offset = Vector3.up * 1.6f;
    [SerializeField]
    int amountOfPoints = 40;

    Transform target;
    public Vector3 TargetPosition => target.position;
    public Quaternion TargetRotation => target.rotation;

    AbilityPreviewController controller;

    ParabolaMesh trajectoryParabola;

    public void Initialize ()
    {
        this.controller = GetComponentInParent<AbilityPreviewController>();

        target = new GameObject("target").transform;
        target.SetParent(transform);

        trajectoryParabola = GameObject.CreatePrimitive(PrimitiveType.Quad).AddComponent<ParabolaMesh>();
        trajectoryParabola.Initialize(controller.champion, target, gravity, angle, meshThickness, offset, scalablePlaneMaterial, amountOfPoints);
    }

    public void CalculateTargetLocation()
    {
        if (!MathUtils.IsInsideCircle(controller.Origin, controller.maxRange, controller.MouseHitPosition))
            target.position = controller.Origin + (controller.MouseHitPosition - controller.Origin).normalized * controller.maxRange;
        else
            target.position = controller.MouseHitPosition;
    }

    public void CalculateTargetRotation()
    {
        //Parabola mesh already fixes rotation
    }
    
    public void SetPosition()
    {
        transform.position = controller.Origin;
    }

    public void SetRotation ()
    {
        //Parabola mesh already fixes rotation
    }

    public void SetScale()
    {
        //Parabola mesh scales acordingly
    }
}