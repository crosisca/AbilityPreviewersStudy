using UnityEngine;

public class ScaleToMouseAbilityPreviewer : AbilityPreviewController
{
    [SerializeField]
    GameObject scalablePlane;

    [SerializeField]
    Transform scalableMeshPivot;
    
    protected override void Initialize()
    {
        base.Initialize();
        
        scalablePlane.transform.SetParent(scalableMeshPivot, false);
        scalablePlane.transform.localPosition = new Vector3(0, 0, 0.5f);
        scalablePlane.transform.localRotation = Quaternion.Euler(90, 0, 0);
        //scalablePlane.transform.localScale = Vector3.one * 0.1f;
    }

    protected override void CalculateTargetLocation()
    {
        base.CalculateTargetLocation();

        if (!MathUtils.IsInsideCircle(Origin, maxRange, mouseHitPosition))
            target.position = Origin + (mouseHitPosition - Origin).normalized * maxRange;
        else
            target.position = mouseHitPosition;
    }

    protected override void CalculateTargetRotation()
    {
        targetRotation = Mathf.Approximately((TargetPosition - Origin).magnitude, 0) ? Quaternion.identity : Quaternion.LookRotation(TargetPosition - Origin, Vector3.up);
    }

    protected override void SetPosition()
    {
        transform.position = Origin;
    }

    protected override void SetScale()
    {
        base.SetScale();

        float dist = Mathf.Min(Vector3.Distance(Origin, TargetPosition), maxRange);

        Vector3 planeLineScale = new Vector3(1, 1, Mathf.Min(dist, maxRange));

        scalableMeshPivot.localScale = planeLineScale;
    }
}