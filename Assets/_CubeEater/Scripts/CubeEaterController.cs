using UnityEngine;

public class CubeEaterController : MonoBehaviour
{
    [HideInInspector] public bool isPlaced;
    [HideInInspector] public Transform target;

    [SerializeField] private float moveSpeed = 5f; 
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Rigidbody rb;

    private void Update()
    {
        if (target == null) return;
        
        MoveCubeEater();
        RotateCubeEater();
    }

    private void MoveCubeEater()
    {
        Vector3 movePosition = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        rb.MovePosition(new Vector3(movePosition.x, transform.position.y, movePosition.z));
    }

    private void RotateCubeEater()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isPlaced) return;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        if (CubeEatersManager.targets.Contains(other.transform))
        {
            CubeEatersManager.targets.Remove(other.transform);
            Destroy(other.gameObject);
        }
        
        CubeEatersManager.RefreshCubeEatersTargets();
    }

    private void OnCollisionStay(Collision other)
    {
        if (!isPlaced) return;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        if (CubeEatersManager.targets.Contains(other.transform))
        {
            CubeEatersManager.targets.Remove(other.transform);
            Destroy(other.gameObject);
        }
        
        CubeEatersManager.RefreshCubeEatersTargets();
    }
}
