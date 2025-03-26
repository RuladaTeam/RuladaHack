using UnityEngine;

public class DefaultCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _defaultItemWorkspace;

    public void ShowDefaultItemWorkspace()
    {
        if (_defaultItemWorkspace.activeInHierarchy)
        {
            _defaultItemWorkspace.SetActive(false);
        }
        else
        {
            _defaultItemWorkspace.SetActive(true);
        }
    }
}
