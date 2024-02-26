using NUnit.Framework;
using UnityEngine;

public class CubeEatersManagerTests
{
    [SetUp]
    public void SetUp()
    {
        CubeEatersManager.targets.Clear();
        CubeEatersManager.cubeEaters.Clear();
    }
    
    [Test]
    public void RefreshCubeEatersTargetsEmptyTargets()
    {
        GameObject cubeEater = new();
        CubeEaterController cubeEaterController = cubeEater.AddComponent<CubeEaterController>();
        CubeEatersManager.cubeEaters.Add(cubeEaterController);
        
        CubeEatersManager.RefreshCubeEatersTargets();
        
        Assert.IsNull(CubeEatersManager.cubeEaters[0].target);
    }
    
    [Test]
    public void RefreshCubeEatersTargetsClosestTarget()
    {
        Transform target1 = new GameObject().transform;
        target1.position = new Vector3(5, 0, 0);
        CubeEatersManager.targets.Add(target1);

        Transform target2 = new GameObject().transform;
        target2.position = new Vector3(10, 0, 0);
        CubeEatersManager.targets.Add(target2);
        
        GameObject cubeEater = new();
        cubeEater.transform.position = new Vector3(0, 0, 0);
        CubeEaterController cubeEaterController = cubeEater.AddComponent<CubeEaterController>();
        CubeEatersManager.cubeEaters.Add(cubeEaterController);
        
        CubeEatersManager.RefreshCubeEatersTargets();
        
        Assert.AreEqual(target1, CubeEatersManager.cubeEaters[0].target);
    }
    
    [TearDown]
    public void TearDown()
    {
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            Object.DestroyImmediate(obj);
        }
    }


}
