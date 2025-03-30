using System.Collections.Generic;
using Core.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class TableWorkspace : MonoBehaviour
{
    [SerializeField] private GameObject _referenceObject;
    [SerializeField] private GameObject _sliderForScale;
    
    private List<GameObject> _objectsInZone = new ();
    private Slider _scaleSlider;

    private void Start()
    {
        _objectsInZone.Add(_referenceObject);
        _scaleSlider = _sliderForScale.GetComponent<Slider>();
    }
    
    public void ResetObjectPosition()
    {
        _referenceObject.GetComponent<GrabbableWithName>().SetDefaultPosition();
    }

    public void SetObjectScale()
    {
        foreach (GameObject obj in _objectsInZone)
        {
            GrabbableWithName grabbable = obj.GetComponent<GrabbableWithName>();
            obj.transform.localScale =
                grabbable.DefaultScale + grabbable.DefaultScale * _scaleSlider.value;
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _referenceObject) return;
        if (other.gameObject.layer == LayerMask.NameToLayer(Config.DESTRUCTABLE_LAYER_MASK)
            || other.gameObject.layer == LayerMask.NameToLayer(Config.REFERENCE_LAYER_MASK))
        {
            other.gameObject.GetComponent<GrabbableWithName>().ResetDefaultValues();
            _objectsInZone.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _referenceObject) return;
        if (other.gameObject.layer == LayerMask.NameToLayer(Config.DESTRUCTABLE_LAYER_MASK)
            || other.gameObject.layer == LayerMask.NameToLayer(Config.REFERENCE_LAYER_MASK))
        {
            _objectsInZone.Remove(other.gameObject);
        }
    }
}
