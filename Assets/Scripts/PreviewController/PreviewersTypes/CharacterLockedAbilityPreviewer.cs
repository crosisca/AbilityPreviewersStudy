﻿using UnityEngine;

public class CharacterLockedAbilityPreviewer : MonoBehaviour, IAbilityPreviewer
{
    [SerializeField]
    Material scalableQuadMaterial;

    Transform scalableQuad;

    AbilityPreviewController controller;

    Transform target;
    public Vector3 TargetPosition => target.position;
    public Quaternion TargetRotation => target.rotation;

    float radiusScaleRatio = 2f;

    public void Initialize ()
    {
        controller = GetComponentInParent<AbilityPreviewController>();

        target = new GameObject("target").transform;
        target.SetParent(transform);

        scalableQuad = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
        scalableQuad.name = "ScalableQuad";
        scalableQuad.SetParent(transform);
        scalableQuad.transform.localPosition = new Vector3(0, 0, 0.5f);
        scalableQuad.transform.localRotation = Quaternion.Euler(90, 0, 0);
        scalableQuad.transform.localScale = Vector3.one * radiusScaleRatio;

        scalableQuad.GetComponent<Renderer>().material = scalableQuadMaterial;
    }

    public void CalculateTargetLocation ()
    {
        target.position = controller.champion.TransformPoint(controller.offset);
    }

    public void CalculateTargetRotation ()
    {
        if (Mathf.Approximately((TargetPosition - controller.Origin).magnitude, 0))
            target.rotation = Quaternion.identity;
        else
            target.rotation = Quaternion.LookRotation(TargetPosition.FlattenY() - controller.Origin.FlattenY(), Vector3.up);
    }

    public void SetPosition ()
    {
        transform.position = target.position;
    }

    public void SetRotation ()
    {
        transform.rotation = controller.canRotate ? target.rotation : Quaternion.identity;
    }

    public void SetScale ()
    {
        if (controller.maxRange != Mathf.Infinity)
            scalableQuad.localScale = Vector3.one * controller.maxRange * radiusScaleRatio;
    }
}