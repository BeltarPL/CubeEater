using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private ListContentManager listContentManager;
    [SerializeField] private RectTransform panel;
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
        SlideOutPanel();
        
        Instantiate(element, Vector3.up * 10, Quaternion.identity).GetComponent<ObjectControls>().interfaceManager = this;
    }
    
    public void FilterList()
    {
        string filter = filterInputField.text.ToLower();

        foreach (Transform element in listContent)
        {
            element.gameObject.SetActive(element.name.Contains(filter));
        }
    }
    
    private void SlideOutPanel()
    {
        float duration = (1 - Mathf.Abs(panel.anchoredPosition.x / (panel.rect.width * panel.localScale.x))) * 0.5f;
        
        DOTween.Kill("SlideInPanel");
        panel.DOAnchorPosX(-panel.rect.width * panel.localScale.x, duration, true).SetId("SlideOutPanel");
    }
    
    public void SlideInPanel()
    {
        float duration = Mathf.Abs(panel.anchoredPosition.x / (panel.rect.width * panel.localScale.x)) * 0.5f;
        
        DOTween.Kill("SlideOutPanel");
        panel.DOAnchorPosX(0, duration, true).SetId("SlideInPanel");;
    }
}
