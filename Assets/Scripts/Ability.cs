using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityConfig", menuName = "ScriptableObjects/AbilityConfig", order = 1)]
public class Ability : ScriptableObject
{
    [SerializeField]
    public readonly float[] RangePerLevel = { 5f, 10f, 15f, 20f };

    public int level = 2;

    public float Range => RangePerLevel[level - 1];

    public float DamageAreaRadius = 3;

    public bool Previewable = true;

    public float areaRadiusFloat = 3.5f;

    public int areaRadiusInt = 2;

    public float areaWidth = 2;

    public float areaLength = 4;

    public Vector3 dashArea = new Vector3(3, 1, 8);

    public Vector3 areaSizeOfOne = Vector3.one;

    public Vector2 areaVec2 = new Vector2(5, 10);
    public Vector3 areaVec3 = new Vector3(2, 1, 6);

    public float maxDistanceFromOrigin = 10;

    public float areaAngle = 45;

    public bool IsPreviewPositionValid (Vector3 position)
    {
        return !Input.GetKey(KeyCode.LeftShift);
    }
}
