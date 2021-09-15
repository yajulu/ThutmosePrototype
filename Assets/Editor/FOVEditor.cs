using UnityEngine;
using UnityEditor;


[CustomEditor (typeof (EnemyFieldOfView))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyFieldOfView fov = (EnemyFieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngleA = fov.DirFromAngle(-fov.angle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.angle / 2, false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.radius);

    }
}
