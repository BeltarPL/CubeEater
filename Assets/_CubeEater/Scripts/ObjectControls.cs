using UnityEngine;

public class ObjectControls : MonoBehaviour
{
    [HideInInspector] public InterfaceManager interfaceManager;

    [SerializeField] private bool isCubeEater;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider objCollider;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            interfaceManager.SlideInPanel();
            
            Destroy(gameObject);
        }
        
        MoveObject();

        if (Input.GetMouseButtonDown(0))
        {
            FinishPlacingObject();
        }
    }

    private void MoveObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        
        rb.MovePosition(new Vector3(hit.point.x, hit.point.y + objCollider.bounds.size.y / 2, hit.point.z));
    }

    private void FinishPlacingObject()
    {
        rb.isKinematic = false;
            
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        if (!isCubeEater)
        {
            CubeEatersManager.targets.Add(transform);
        }
        else
        {
            CubeEaterController cubeEaterController = transform.GetComponent<CubeEaterController>();
            CubeEatersManager.cubeEaters.Add(cubeEaterController);
            cubeEaterController.isPlaced = true;
        }
            
        CubeEatersManager.RefreshCubeEatersTargets();
        interfaceManager.SlideInPanel();
            
        Destroy(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (CubeEatersManager.targets.Contains(other.transform))
        {
            CubeEatersManager.RefreshCubeEatersTargets();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (CubeEatersManager.targets.Contains(other.transform))
        {
            CubeEatersManager.RefreshCubeEatersTargets();
        }
    }
}
