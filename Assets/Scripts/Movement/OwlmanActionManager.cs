using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Helpers;
using System.Linq;
using Assets.Scripts;

public class OwlmanActionManager : MonoBehaviour
{
    private OwlmanChaseMovement chaseController;
    private OwlmanPatrolMovement patrolController;

    private void Start()
    {
        chaseController = gameObject.GetComponent<OwlmanChaseMovement>();
        patrolController = gameObject.GetComponent<OwlmanPatrolMovement>();

        patrolController.QueueUpToggleIsPatrolIdle();
    }

    private void Update()
    {
        if (chaseController.ShouldChase())
        {
            chaseController.Chase();
            return;
        }
        patrolController.Patrol();
    }
}