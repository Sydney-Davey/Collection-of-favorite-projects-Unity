using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HONKHONKKK : MonoBehaviour
{
    [Header("Detection Settings")]
    public float distance = 23.4f;       // sphere radius
    public float angle = 7.37f;          // cone half-angle
    public float height = 1f;
    public int scanFrequency = 30;
    public LayerMask layers;

    [Header("Targets")]
    public List<Transform> specificTargets;   // Objects the AI should detect

    [Header("AI Car Reference")]
    public AICars car;                // Drag the AI car here
    public float brakeSpeed = 0f;     // Speed when stopping

    [Header("Debug")]
    public Color meshColor = Color.red;

    [Header("Runtime")]
    public Collider[] colliders;
    int count;
    float scanInterval;
    float scanTimer;
    Mesh mesh;

    void Start()
    {
        colliders = new Collider[20]; // max hits
        scanInterval = 1f / scanFrequency;
    }

    void Update()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }

    private void Scan()
    {
        bool targetDetected = false;

        // detect colliders in range
        count = Physics.OverlapSphereNonAlloc(
            transform.position,
            distance,
            colliders,
            layers,
            QueryTriggerInteraction.Collide
        );

        for (int i = 0; i < count; i++)
        {
            Collider c = colliders[i];
            if (c == null) continue;

            Transform hitRoot = c.transform.root;

            // Only detect specific targets (In edit menu)
            if (!specificTargets.Contains(hitRoot))
                continue;

            // Cone check
            Vector3 dir = (hitRoot.position - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, dir);

            if (dot > Mathf.Cos(angle * Mathf.Deg2Rad))
            {
                Debug.Log("Detected object in cone: " + hitRoot.name);
                targetDetected = true;
                break; // stop after first detection
            }
        }

        // Car Breaking logic
        if (car != null)
        {
            if (targetDetected)
            {
                car.speed = 0f;
            }
            else
            {
                car.speed = car.defaultSpeed; // restore Inspector speed
            }
        }
    }

    Mesh CreateMesh()
    {
        // meshs
        Mesh mesh = new Mesh();
        int segments = 10;
        int numTri = (segments * 4) + 2 + 2;
        int numVert = numTri * 3;

        Vector3[] vertices = new Vector3[numVert];
        int[] triangles = new int[numVert];

        // Sphere

        Vector3 bCenter = Vector3.zero;
        Vector3 bLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 tCenter = bCenter + Vector3.up * height;
        Vector3 tLeft = bLeft + Vector3.up * height;
        Vector3 tRight = bRight + Vector3.up * height;

        int vert = 0;

        // Cone 

        // left side
        vertices[vert++] = bCenter;
        vertices[vert++] = bLeft;
        vertices[vert++] = tLeft;

        vertices[vert++] = tLeft;
        vertices[vert++] = tCenter;
        vertices[vert++] = bCenter;

        // right side
        vertices[vert++] = bCenter;
        vertices[vert++] = tCenter;
        vertices[vert++] = tRight;

        vertices[vert++] = tRight;
        vertices[vert++] = bRight;
        vertices[vert++] = bCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        for (int i = 0; i < segments; ++i)
        {
            bLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            tLeft = bLeft + Vector3.up * height;
            tRight = bRight + Vector3.up * height;

            // far side
            vertices[vert++] = bLeft;
            vertices[vert++] = bRight;
            vertices[vert++] = tRight;

            vertices[vert++] = tRight;
            vertices[vert++] = tLeft;
            vertices[vert++] = bLeft;

            // top
            vertices[vert++] = tCenter;
            vertices[vert++] = tLeft;
            vertices[vert++] = tRight;

            // bottom
            vertices[vert++] = bCenter;
            vertices[vert++] = bRight;
            vertices[vert++] = bLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVert; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate()
    {
        mesh = CreateMesh();
        scanInterval = 1f / scanFrequency;
    }

    private void OnDrawGizmos()
    {
        // draw in the colors (can be changed in edit menu
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distance);

        for (int i = 0; i < count; i++)
        {
            if (colliders[i] != null)
                Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);
        }
    }
}

