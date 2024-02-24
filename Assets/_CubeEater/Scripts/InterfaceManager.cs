using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private ListContentManager listContentManager;
    [SerializeField] private Transform listContentParent;
    
    private void Start()
    {
        CreateList();
    }

    private void CreateList()
    {
        foreach (Element element in listContentManager.listContent)
        {
            Transform newElement = Instantiate(element.listElementPrefab, listContentParent);
            newElement.name = element.name;
            newElement.GetComponent<Button>().onClick.AddListener(() => InstantiateSceneElement(element.sceneElementPrefab));
        }
    }

    private void InstantiateSceneElement(Transform element)
    {
        Instantiate(element);
    }
}
