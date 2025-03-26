using UnityEngine;

public class DefaultCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _defaultItemWorkspace;
    [SerializeField] private GameObject _selectItemWorkspace;

    private void Start()
    {
        _defaultItemWorkspace.SetActive(false);
        _selectItemWorkspace.SetActive(false);
    }
    
    public void ShowDefaultItemWorkspace()
    {
        _defaultItemWorkspace.SetActive(true);
        if (_selectItemWorkspace.activeInHierarchy)
        {
            _selectItemWorkspace.SetActive(false);
        }
    }

    public void ShowSelectItemWorkspace()
    {
        _selectItemWorkspace.SetActive(true);
        if (_defaultItemWorkspace.activeInHierarchy)
        {
            _defaultItemWorkspace.SetActive(false);
        }
    }
}
