using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListContentManager", menuName = "Custom/ListContentManager")]
public class ListContentManager : ScriptableObject
{
    public Element[] listContent;
}

[Serializable]
public class Element
{
    public string name;
    public Transform listElementPrefab;
    public Transform sceneElementPrefab;
}
