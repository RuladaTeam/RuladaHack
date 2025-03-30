using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class TableUI : MonoBehaviour
{
    [SerializeField] private GameObject _objectOnTable;
    [SerializeField] private GameObject _slider;

    private Vector3 _defaultObjectPosition;
    private Quaternion _defaultObjectRotation;
    private Vector3 _defaultObjectScale;
    private Slider _scaleSlider;

    private void Start()
    {
        _scaleSlider = _slider?.GetComponent<Slider>();
    }
    
    public void SetDefaultObjectParameters()
    {
        if (_objectOnTable == null) return;
        _defaultObjectPosition = _objectOnTable.transform.position;
        _defaultObjectRotation = _objectOnTable.transform.rotation;
        _defaultObjectScale = _objectOnTable.transform.localScale;
    }

    public void ResetObjectPosition()
    {
        _objectOnTable.transform.position = _defaultObjectPosition;
        _objectOnTable.transform.rotation = _defaultObjectRotation;
    }

    public void SetObjectScale()
    {
        _objectOnTable.transform.localScale = _defaultObjectScale + _defaultObjectScale * _scaleSlider.value;
    }
}
