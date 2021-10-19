using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathController : MonoBehaviour
{
    [SerializeField]
    public Transform pathParent;
    [SerializeField]
    public Transform[] pathPoints;
    NavMeshAgent agent;
    int currPointInPath;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currPointInPath = 0;
    }
    public void GoToNextPoint()
    {
        agent.SetDestination(pathPoints[currPointInPath].position);
        currPointInPath++;
        currPointInPath %= pathPoints.Length;
    }

 

}
