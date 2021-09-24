using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmesh_desired_path : MonoBehaviour
{
  public Transform source;
  public Transform target;
  private NavMeshPath path;
  private float elapsed = 0.0f;
  void Start()
  {
      path = new NavMeshPath();
      elapsed = 0.0f;
  }

  void Update()
  {
      // Update the way to the goal every second.
      elapsed += Time.deltaTime;
      if (elapsed > 1.0f)
      {
          elapsed -= 1.0f;
          NavMesh.CalculatePath(source.position, target.position, NavMesh.AllAreas, path);
      }
      for (int i = 0; i < path.corners.Length - 1; i++){
          Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
      }
      Debug.Log(path.corners);
  }
}
