using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaMesh : MonoBehaviour
{
    public Transform target;

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

    List<Vector3> newVertices = new List<Vector3>();
    List<Vector3> newNormals = new List<Vector3>();
    List<Vector2> newUV = new List<Vector2>();
    List<int> newTriangles = new List<int>();

    Mesh mesh;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;

    [SerializeField]
    Material meshMaterial;

    List<Vector3> currentCalculatedPositions = new List<Vector3>();

    Transform transformToAttach;

    private void Awake()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        meshFilter.mesh = mesh;
    }
    
    public void Initialize(Transform transformToAttach, Transform target, float gravity, float angle, float meshThickness, Vector3 offset, Material material, int amountOfPoints = 40)
    {
        transform.position = transformToAttach.position + transformToAttach.rotation * offset;

        this.transformToAttach = transformToAttach;
        this.offset = offset;
        this.target = target;
        this.angle = angle;
        this.meshThickness = meshThickness;
        this.amountOfPoints = amountOfPoints;
        this.gravity = gravity;

        if (meshMaterial == null)
        {
            meshMaterial = material;
            meshRenderer.material = meshMaterial;
        }
    }
    
    public void ForceUpdate()
    {
        transform.position = transformToAttach.position + transformToAttach.rotation * offset;

        CheckIfSettingsHasChanged();
        GetParabolaPositions();
        UpdateMesh();
    }
    
    void UpdateMesh()
    {
        for (int positionIndex = 1, mesh4Index = 0, triangleIndex = 0; positionIndex < currentCalculatedPositions.Count; positionIndex++, mesh4Index+=4, triangleIndex+=6)
        {
            newVertices[mesh4Index + 0] = new Vector3(-meshThickness, currentCalculatedPositions[positionIndex-1].y, currentCalculatedPositions[positionIndex-1].z);
            newVertices[mesh4Index + 1] = new Vector3(meshThickness, currentCalculatedPositions[positionIndex-1].y, currentCalculatedPositions[positionIndex-1].z);
            newVertices[mesh4Index + 2] = new Vector3(-meshThickness, currentCalculatedPositions[positionIndex].y, currentCalculatedPositions[positionIndex].z);
            newVertices[mesh4Index + 3] = new Vector3(meshThickness, currentCalculatedPositions[positionIndex].y, currentCalculatedPositions[positionIndex].z);

            newNormals[mesh4Index + 0] = -Vector3.forward;
            newNormals[mesh4Index + 1] = -Vector3.forward;
            newNormals[mesh4Index + 2] = -Vector3.forward;
            newNormals[mesh4Index + 3] = -Vector3.forward;

            newUV[mesh4Index + 0] = new Vector2(0, 0);
            newUV[mesh4Index + 1] = new Vector2(1, 0);
            newUV[mesh4Index + 2] = new Vector2(0, 1);
            newUV[mesh4Index + 3] = new Vector2(1, 1);

            newTriangles[triangleIndex + 0] = mesh4Index + 0;
            newTriangles[triangleIndex + 1] = mesh4Index + 2;
            newTriangles[triangleIndex + 2] = mesh4Index + 1;

            newTriangles[triangleIndex + 3] = mesh4Index + 2;
            newTriangles[triangleIndex + 4] = mesh4Index + 3;
            newTriangles[triangleIndex + 5] = mesh4Index + 1;
        }
        
        mesh.vertices = newVertices.ToArray();
        mesh.normals = newNormals.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.triangles = newTriangles.ToArray();
    }
    
    void CheckIfSettingsHasChanged()
    {
        if(amountOfPoints != currentCalculatedPositions.Count)
            UpdateLists();
    }

    void UpdateLists()
    {
        // Mesh things
        newVertices.Clear();
        newNormals.Clear();
        newUV.Clear();
        newTriangles.Clear();

        for (int i = 0; i < amountOfPoints; i++)
        {
            // Mesh things
            for (int v = 0; v < 4; v++)
            {
                newVertices.Add(Vector3.zero);
                newNormals.Add(Vector3.zero);
                newUV.Add(Vector2.zero);
            }

            for (int t = 0; t < 6; t++)
                newTriangles.Add(0);
        }
    }

    List<Vector3> GetParabolaPositions()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        float velocity = distanceToTarget / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity);

        float velocityZ = Mathf.Sqrt(velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
        float velocityY = Mathf.Sqrt(velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);

        float flightDuration = distanceToTarget / velocityZ;
        
        transform.rotation = Quaternion.LookRotation(target.position - transform.position);
        
        List<Vector3> positions = new List<Vector3>();

        Vector3 lastPosition = Vector3.zero;
        float lastPointOfTime = 0;
        for (int i = 0; i <= amountOfPoints; i++)
        {
            float currentPointOfTime = (i * flightDuration) / amountOfPoints;
            float delta = currentPointOfTime - lastPointOfTime;
            
            Vector3 position = lastPosition + new Vector3(0, (velocityY - (gravity * currentPointOfTime)) * delta, velocityZ * delta);
            positions.Add(position);

            lastPosition = position;
            lastPointOfTime = currentPointOfTime;
        }

        currentCalculatedPositions = positions;
        return positions;
    }
}
