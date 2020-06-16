using UnityEngine;

public class CharacterLockedAbilityPreviewer : AbilityPreviewController
{
    [SerializeField]
    Transform scalablePlane;

    float radiusScaleRatio = 2f;

    public void Setup (Transform relatedChampion, Ability relatedAbility, Vector3 offset)
    {
        base.Setup(relatedChampion, relatedAbility);
    }

    protected override void Initialize ()
    {
        base.Initialize();
        
        scalablePlane.transform.localPosition = new Vector3(0, 0, 0.5f);
        scalablePlane.transform.localRotation = Quaternion.Euler(90, 0, 0);
        scalablePlane.transform.localScale = Vector3.one * 0.1f;
    }

    protected override void CalculateTargetLocation ()
    {
        base.CalculateTargetLocation();

        target.position = champion.TransformPoint(offset);
    }

    protected override void CalculateTargetRotation ()
    {
        targetRotation = Mathf.Approximately((TargetPosition - Origin).magnitude, 0) ? Quaternion.identity : Quaternion.LookRotation(TargetPosition - Origin, Vector3.up);
    }

    protected override void SetScale()
    {
        base.SetScale();

        scalablePlane.localScale = Vector3.one * maxRange * radiusScaleRatio;
    }
}