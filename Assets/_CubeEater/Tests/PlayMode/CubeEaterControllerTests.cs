using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CubeEaterControllerTests
{
    private GameObject cubeEater;
    private CubeEaterController cubeEaterController;
    private GameObject target;
    
    [SetUp]
    public void SetUp()
    {
        cubeEater = new();
        cubeEater.transform.position = new Vector3(0, 0, 0);
        cubeEater.transform.eulerAngles = Vector3.forward;
        cubeEaterController = cubeEater.AddComponent<CubeEaterController>();
        Rigidbody cubeEaterRb = cubeEater.AddComponent<Rigidbody>();
        cubeEaterRb.useGravity = false;
        
        FieldInfo rbField = typeof(CubeEaterController).GetField("rb", BindingFlags.NonPublic | BindingFlags.Instance);
        rbField.SetValue(cubeEaterController, cubeEaterRb); 
        
        target = new();
        target.transform.position = new Vector3(0.5f, 0, 0);
        target.transform.eulerAngles = Vector3.back;

        cubeEaterController.target = target.transform;
    }
    
    [UnityTest]
    public IEnumerator CubeEaterControllerMoveCubeEater()
    {
        MethodInfo moveCubeEater = typeof(CubeEaterController).GetMethod("MoveCubeEater", BindingFlags.NonPublic | BindingFlags.Instance);

        while (Vector3.Distance(cubeEater.transform.position, target.transform.position) > 0.1f)
        {
            moveCubeEater.Invoke(cubeEaterController, null);

            yield return null;
        }
        
        Assert.IsTrue(Vector3.Distance(cubeEater.transform.position, target.transform.position) < 0.1f);
    }
    
    [UnityTest]
    public IEnumerator CubeEaterControllerRotateCubeEater()
    {
        MethodInfo rotateCubeEater = typeof(CubeEaterController).GetMethod("RotateCubeEater", BindingFlags.NonPublic | BindingFlags.Instance);

        while (Quaternion.Angle(cubeEater.transform.rotation, target.transform.rotation) > 2f)
        {
            rotateCubeEater.Invoke(cubeEaterController, null);

            yield return null;
        }

        Assert.IsTrue(Quaternion.Angle(cubeEater.transform.rotation, target.transform.rotation) < 2f);
    }
    
    [TearDown]
    public void TearDown()
    {
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            Object.Destroy(obj);
        }
    }
}
