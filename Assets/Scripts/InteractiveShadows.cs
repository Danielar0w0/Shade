using UnityEngine;
using System.Linq;
using System;


public class InteractiveShadows : MonoBehaviour
{
    public Transform shadowTransform;

    public Transform lightTransform;
    private LightType lightType;

    public LayerMask targetLayerMask;

    public Vector3 extrusionDirection = Vector3.zero;

    private Vector3[] objectVertices;

    private Mesh shadowColliderMesh;
    private MeshCollider shadowCollider;

    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private Vector3 previousScale;

    private bool canUpdateCollider = true;

    [Range(0.02f, 1f)] 
    public float shadowColliderUpdateTime = 0.08f;

    public bool isTriggered = false;

    void Start()
    {
        InitializeShadowCollider();

        lightType = lightTransform.GetComponent<Light>().type;
        objectVertices = transform.GetComponent<MeshFilter>().mesh.vertices.Distinct().ToArray();

        shadowColliderMesh = new Mesh();
    }

    void Update()
    {
        shadowTransform.position = transform.position;
    }

    void FixedUpdate()
    {
        if (TransformHasChanged() && canUpdateCollider)
        {
            Invoke("UpdateShadowCollider", shadowColliderUpdateTime);
            canUpdateCollider = false;
        }

        previousPosition = transform.position;
        previousRotation = transform.rotation;
        previousScale = transform.localScale;
    }

    void InitializeShadowCollider()
    {
        GameObject shadowGameObject = shadowTransform.gameObject;

        // Add MeshCollider
        shadowCollider = shadowGameObject.AddComponent<MeshCollider>();

        // Change name
        shadowGameObject.name = "ShadowCollider";

        shadowCollider.convex = true;
        shadowCollider.isTrigger = true;

        // Add Rigidbody
        Rigidbody rb = shadowGameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    internal void UpdateShadowCollider()
    {
        shadowColliderMesh.vertices = ComputeShadowColliderMeshVertices();
        shadowCollider.sharedMesh = shadowColliderMesh;
        canUpdateCollider = true;
    }

    Vector3[] ComputeShadowColliderMeshVertices()
    {
        Vector3[] points = new Vector3[2 * objectVertices.Length];

        Vector3 raycastDirection = lightTransform.forward;

        int n = objectVertices.Length;

        for (int i = 0; i < n; i++)
        {
            Vector3 point = transform.TransformPoint(objectVertices[i]);

            if (lightType == LightType.Directional || isTriggered)
            {
                if (lightType != LightType.Directional)
                {
                    raycastDirection = point - lightTransform.position;
                }

                points[i] = ComputeIntersectionPoint(point, raycastDirection);
                
                points[n + i] = ComputeExtrusionPoint(point, points[i]);
            }
        }



        return points;
    }

    Vector3 ComputeIntersectionPoint(Vector3 vertex, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(vertex, direction, out hit, Mathf.Infinity, targetLayerMask))
        {
            return hit.point - transform.position;
        }

        return vertex + 100 * direction - transform.position;
    }

    Vector3 ComputeExtrusionPoint(Vector3 objectVertexPosition, Vector3 shadowPointPosition)
    {
        if (extrusionDirection.sqrMagnitude == 0)
        {
            return objectVertexPosition - transform.position;
        }

        return shadowPointPosition + extrusionDirection;
    }

    bool TransformHasChanged()
    {
        return previousPosition != transform.position || previousRotation != transform.rotation || previousScale != transform.localScale;
    }
}