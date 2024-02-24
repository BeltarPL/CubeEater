using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControls : MonoBehaviour
{
    [HideInInspector] public InterfaceManager interfaceManager;

    [SerializeField] private bool keepKinematic;
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
        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        
        rb.MovePosition(new Vector3(hit.point.x, hit.point.y + objCollider.bounds.size.y / 2, hit.point.z));

        if (Input.GetMouseButtonDown(0))
        {
            if (!keepKinematic)
            {
                rb.isKinematic = false;
                
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            
            interfaceManager.SlideInPanel();
            
            Destroy(this);
        }
    }
}
