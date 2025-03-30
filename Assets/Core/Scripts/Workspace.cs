using System;
using UnityEngine;

public class Workspace : MonoBehaviour
{
    [Header("DefaultItemWorkspace")] 
    [SerializeField] private GameObject _defaultItemWorkspace;
    [Space(10)] 
    [Header("DefaultCanvas")] 
    [SerializeField] private DefaultCanvas _defaultCanvas;

    // [Space(10)]
    // [Header("ComparisonTableWorkspace")]
    [Space(10)]
    [Header("LoadItemWorkspace")]
    [SerializeField] private GameObject _loadItemWorkspace;
    // [Space(10)]
    // [SerializeField]

    
    public static Workspace Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

}
