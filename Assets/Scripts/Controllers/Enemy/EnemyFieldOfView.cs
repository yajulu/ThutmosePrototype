using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;
using TMPro;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    GameObject prefabCircle;
    [SerializeField]
    [Range(0,360)]
    public float  angle;
    [SerializeField]
    [Range(0, 20)]
    public float radius;
    [SerializeField]
    [Range(0,10)]
    float meshResolution;

    Transform playerTransform;

    [SerializeField]
    LayerMask obstaclesLayer, targetLayer;

    [SerializeField]
    MeshFilter fovMeshFilter;
    Mesh fovMesh;

    [HideInInspector]
    public bool isPlayerDetected;

    private void Start()
    {
        fovMesh = new Mesh();
        fovMesh.name = "FOV Mesh";
        fovMeshFilter.mesh = fovMesh;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(DetectionRoutine());
    }

    private void Update()
    {
        DrawFOV();
    }

    

    void FindVisibleTarget()
    {
        Collider[] playerCollier = Physics.OverlapSphere(transform.position, radius,targetLayer);
        if (playerCollier.Length > 0)
        {
            Transform playerTransform = playerCollier[0].transform;
            Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < angle / 2)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstaclesLayer))
                {
                    isPlayerDetected = true;
                    //Debug.Log("Player Detected!");
                    text.text= "PLAYER DETECTED1";
                }
                else
                {
                    isPlayerDetected = false;
                    //Debug.Log("Player Vanished! 1");
                    text.text = "PLAYER GOT COVER";
                }
                    
            }
            else
            {
                isPlayerDetected = false;

                text.text = "";
            }
         }
    }

    void DrawFOV()
    {
        int stepCount = Mathf.RoundToInt(angle * meshResolution);
        float stepAngleSize = angle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for(int i = 0; i<=stepCount; i++)
        {
            float angleToDraw = transform.eulerAngles.y - angle / 2 + stepAngleSize * i;
            ViewCastInfo viewCastInfo = ViewCast(angleToDraw);
            viewPoints.Add(viewCastInfo.point );
            //Debug.DrawLine(transform.position, transform.InverseTransformPoint(viewCastInfo.point), Color.red);
            //Debug.DrawLine(transform.position, transform.position + DirFromAngle(angleToDraw, true) * radius, Color.red);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2) * 3];
        vertices[0] = Vector3.zero;
        for(int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            //GameObject.Instantiate(prefabCircle, viewPoints[i], Quaternion.identity);

            if(i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        fovMesh.Clear();
        fovMesh.vertices = vertices;
        fovMesh.triangles = triangles;
        fovMesh.RecalculateNormals();
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool isAngleGlobal)
    {
        if (!isAngleGlobal)
            angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
       
        if (Physics.Raycast(transform.position, dir, out hit, radius, obstaclesLayer))
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        else
            return new ViewCastInfo(false, transform.position + dir * radius, radius, globalAngle);
    }
    IEnumerator DetectionRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            FindVisibleTarget();
        }

    }


}
