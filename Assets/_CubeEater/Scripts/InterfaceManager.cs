using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private ListContentManager listContentManager;
    [SerializeField] private Transform listContentParent;
    [SerializeField] private TMP_InputField filterInputField;

    private readonly List<Transform> listContent = new();
    
    private void Start()
    {
        CreateList();
    }

    private void CreateList()
    {
        foreach (Element element in listContentManager.listContent)
        {
            Transform newElement = Instantiate(element.listElementPrefab, listContentParent);
            newElement.name = element.name.ToLower();
            newElement.GetComponent<Button>().onClick.AddListener(() => InstantiateSceneElement(element.sceneElementPrefab));
            
            listContent.Add(newElement);
        }
    }

    private void InstantiateSceneElement(Transform element)
    {
        Instantiate(element);
    }
    
    public void FilterList()
    {
        string filter = filterInputField.text.ToLower();

        foreach (Transform element in listContent)
        {
            element.gameObject.SetActive(element.name.Contains(filter));
        }
    }
}
