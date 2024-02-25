using System.Collections.Generic;
using UnityEngine;

public static class CubeEatersManager
{
    public static List<Transform> targets = new();
    public static List<CubeEaterController> cubeEaters = new();

    public static void RefreshCubeEatersTargets()
    {
        if (targets == null || targets.Count == 0)
        {
            return;
        }

        foreach (CubeEaterController cubeEater in cubeEaters)
        {
            Transform closest = null;
            float closestDistance = Mathf.Infinity;
            
            foreach (Transform element in targets)
            {
                float distance = Vector3.Distance(element.position, cubeEater.transform.position);

                if (distance > closestDistance) continue;
                
                closestDistance = distance;
                closest = element;
            }

            cubeEater.target = closest;
        }
    }
}
