using UnityEngine;
using UnityEngine.UI;

public class ComparisonTableWorkspace : MonoBehaviour
{
    [SerializeField] private GameObject _defaultObject;
    [SerializeField] private GameObject _slider;
    
    private GameObject _loadedObject;
    private Vector3 _defaultObjectPosition;
    private Vector3 _defaultLoadedObjectPosition;
    private Quaternion _defaultObjectRotation;
    private Quaternion _defaultLoadedObjectRotation;
    private Vector3 _defaultObjectScale;
    private Vector3 _defaultLoadedObjectScale;
    private Slider _scaleSlider;

    private void Start()
    {
        _scaleSlider = _slider?.GetComponent<Slider>();
    }
    
    // looks like it does not work on quest
    public void WireFrameMode()
    {
        GL.wireframe = !GL.wireframe;
    }

    public void SetLoadedObject(GameObject loadedObject)
    {
        _loadedObject = loadedObject;
        _defaultLoadedObjectPosition = loadedObject.transform.position;
        _defaultLoadedObjectRotation = loadedObject.transform.rotation;
        _defaultObjectScale = loadedObject.transform.localScale;
    }
    
    public void SetDefaultObjectParameters()
    {
        if (_defaultObject == null) return;
        _defaultObjectPosition = _defaultObject.transform.position;
        _defaultObjectRotation = _defaultObject.transform.rotation;
        _defaultObjectScale = _defaultObject.transform.localScale;
    }
    
    public void ResetObjectPosition()
    {
        _defaultObject.transform.position = _defaultObjectPosition;
        _defaultObject.transform.rotation = _defaultObjectRotation;
        
        if (_loadedObject == null)  return;
        _loadedObject.transform.position = _defaultLoadedObjectPosition;
        _loadedObject.transform.rotation = _defaultLoadedObjectRotation;
    }

    public void SetObjectScale()
    {
        _defaultObject.transform.localScale = _defaultObjectScale + _defaultObjectScale * _scaleSlider.value;
        if (_loadedObject == null) return;
        _loadedObject.transform.localScale = _defaultLoadedObjectScale + _defaultLoadedObjectScale * _scaleSlider.value;
    }


    public void Penis()
    {
        Debug.Log("EBAT YA VAHUI");
    }
    
}
