using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _defaultItemWorkspace;
    [SerializeField] private GameObject _selectItemWorkspace;

    public event EventHandler OnDefaultItemSetActive;
    public event EventHandler OnSelectItemSetActive;
    
    private void Start()
    {
        _defaultItemWorkspace.SetActive(false);
        _selectItemWorkspace.SetActive(false);
    }
    
    public void ShowDefaultItemWorkspace()
    {
        _defaultItemWorkspace.SetActive(true);
        OnDefaultItemSetActive?.Invoke(this, EventArgs.Empty);
        if (_selectItemWorkspace.activeInHierarchy)
        {
            _selectItemWorkspace.SetActive(false);
        }
    }

    public void ShowSelectItemWorkspace()
    {
        _selectItemWorkspace.SetActive(true);
        OnSelectItemSetActive?.Invoke(this, EventArgs.Empty);
        if (_defaultItemWorkspace.activeInHierarchy)
        {
            _defaultItemWorkspace.SetActive(false);
        }
    }
}
