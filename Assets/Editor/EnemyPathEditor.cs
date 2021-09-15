using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(EnemyController))]
public class EnemyPathEditor : Editor
{
    private void OnSceneGUI()
    {
        EnemyController enemyController = (EnemyController)target;
        if(enemyController.pathPoints.Length == 0 && enemyController.pathParent.childCount > 0)
        {
            int j = 0;
            enemyController.pathPoints = new Transform[enemyController.pathParent.childCount];
            if (enemyController.pathParent.childCount > 0)
            {

                foreach (Transform pathPoint in enemyController.pathParent)
                {
                    enemyController.pathPoints[j] = pathPoint;
                    j++;
                }
            }
            else
                Debug.LogWarning("This enemy has no path to follow");
        }
        

      
        Handles.color = Color.red;
        int i = 0;
        if (enemyController.pathPoints.Length > 0)
        {
            foreach (Transform point in enemyController.pathPoints)
            {
                if (point != null)
                {
                    if (i > 0)
                    {
                        Handles.color = Color.blue;
                        Handles.DrawDottedLine(enemyController.pathPoints[i - 1].position, point.position, 1);
                    }
                    Handles.color = Color.red;
                    Handles.DrawWireDisc(point.transform.position, Vector3.up, 0.25f);
                    i++;
                }

            }
        }
        
    }
}
