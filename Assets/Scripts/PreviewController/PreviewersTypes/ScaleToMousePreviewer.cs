using UnityEngine;

public class ScaleToMousePreviewer : MonoBehaviour, IAbilityPreviewer
{
    [SerializeField]
    Material scalableQuadMaterial;

    [SerializeField]
    float scalableQuadWidth = 1;

    Transform scalableQuad;
    
    Transform scalableMeshPivot;

    AbilityPreviewController controller;

    Transform target;
    public Vector3 TargetPosition => target.position;
    public Quaternion TargetRotation => target.rotation;
    
    public void Initialize()
    {
        controller = GetComponentInParent<AbilityPreviewController>();

        target = new GameObject("target").transform;
        target.SetParent(transform);

        scalableMeshPivot = new GameObject("Pivot").transform;
        scalableMeshPivot.SetParent(transform);;
        scalableMeshPivot.localPosition = Vector3.zero;
        scalableMeshPivot.localRotation = Quaternion.identity;
        scalableMeshPivot.localScale = Vector3.one;

        scalableQuad = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
        scalableQuad.name = "ScalableQuad";

        scalableQuad.SetParent(scalableMeshPivot, false);
        scalableQuad.localPosition = new Vector3(0, 0, 0.5f);
        scalableQuad.localRotation = Quaternion.Euler(90, 0, 0);

        scalableQuad.GetComponent<Renderer>().material = scalableQuadMaterial;
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
        if (Mathf.Approximately((target.position - controller.Origin).magnitude, 0))
            target.rotation = Quaternion.identity;
        else
            target.rotation = Quaternion.LookRotation(target.position.FlattenY() - controller.Origin.FlattenY(), Vector3.up);
    }

    public void SetPosition ()
    {
        transform.position = controller.Origin;
    }

    public void SetRotation ()
    {
        transform.rotation = target.rotation;
    }

    public void SetScale()
    {
        float dist = Mathf.Min(Vector3.Distance(controller.Origin, target.position), controller.maxRange);

        Vector3 planeLineScale = new Vector3(scalableQuadWidth, 1, Mathf.Min(dist, controller.maxRange));

        scalableMeshPivot.localScale = planeLineScale;
    }
}