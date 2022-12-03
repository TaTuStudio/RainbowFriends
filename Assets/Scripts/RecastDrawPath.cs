using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RecastDrawPath : MonoBehaviour
{
    public Transform target;
    public Vector3 targetPos;

    public List<Vector3> movePath = new List<Vector3>();

    private void Update()
    {
        _GetPath();
    }

    void _GetPath()
    {
        if(targetPos != target.position)
        {
            targetPos = target.position;

            _GetMoveToTargetPath();
        }
    }

    public void _GetMoveToTargetPath()
    {
        Debug.Log("Get path");

        var p = ABPath.Construct(transform.position, targetPos, OnPathComplete);

        // Start the path by calling the AstarPath component directly
        // AstarPath.active is the active AstarPath instance in the scene
        AstarPath.StartPath(p);
    }

    public void OnPathComplete(Path p)
    {
        movePath.Clear();

        // We got our path back
        if (p.error)
        {
            // Nooo, a valid path couldn't be found

            //Debug.Log("Nooo, a valid path couldn't be found");
        }
        else
        {
            // Yay, now we can get a Vector3 representation of the path
            // from p.vectorPath

            //Debug.Log("Yay, now we can get a Vector3 representation of the path");

            movePath.AddRange(p.vectorPath);

            movePath.Remove(movePath[0]);
        }
    }

    private void OnDrawGizmos()
    {
        if(movePath.Count > 0)
        {
            Gizmos.color = Color.red;

            for (int i = 1; i < movePath.Count; i++)
            {
                Gizmos.DrawLine(movePath[i - 1], movePath[i]);
            }
        }
    }
}
