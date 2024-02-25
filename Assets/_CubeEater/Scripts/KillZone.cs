using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (CubeEatersManager.targets.Contains(other.transform))
        {
            CubeEatersManager.targets.Remove(other.transform);
            Destroy(other.gameObject);
        }
        else if (CubeEatersManager.cubeEaters.Contains(other.transform.GetComponent<CubeEaterController>()))
        {
            CubeEatersManager.cubeEaters.Remove(other.transform.GetComponent<CubeEaterController>());
            Destroy(other.gameObject);
        }
        
        CubeEatersManager.RefreshCubeEatersTargets();
    }
}