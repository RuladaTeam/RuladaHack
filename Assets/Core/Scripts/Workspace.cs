using System;
using UnityEngine;

public class Workspace : MonoBehaviour
{
    [Header("DefaultItemWorkspace")] 
    [SerializeField] private GameObject _defaultItemWorkspace;
    [Space(10)] 
    [Header("DefaultCanvas")] 
    [SerializeField] private DefaultCanvas _defaultCanvas;
    [Space(10)]
    [Header("TableWorkspace")]
    [SerializeField] private GameObject _uiForDefault;
    [SerializeField] private GameObject _uiForSelected;

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

    private void Start()
    {
        
        DisableUI();
        _defaultCanvas.OnDefaultItemSetActive += DefaultCanvas_OnDefaultItemSetActive;
        _defaultCanvas.OnSelectItemSetActive += DefaultCanvas_OnSelectItemSetActive;
    }

    private void DefaultCanvas_OnSelectItemSetActive(object sender, EventArgs e)
    {
        _uiForSelected.SetActive(true);
        _uiForDefault.SetActive(false);
    }

    private void DefaultCanvas_OnDefaultItemSetActive(object sender, EventArgs e)
    {
        _uiForSelected.SetActive(false);
        _uiForDefault.SetActive(true);
    }

    public void DisableUI()
    {
        _uiForDefault.SetActive(false);
        _uiForSelected.SetActive(false);
        _defaultItemWorkspace.SetActive(false);
        _loadItemWorkspace.SetActive(false);
    }
}
