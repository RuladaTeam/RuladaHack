using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ComparisonTableWorkspace : MonoBehaviour
{
    [SerializeField] private GameObject _defaultObject;
    [SerializeField] private GameObject _sliderForScale;
    [SerializeField] private GameObject _sliderForCutPlane;
    [SerializeField] private GameObject _referenceObject;
    [SerializeField] private GameObject _objectSpawn;
    
    private GameObject _loadedObject;
    private Vector3 _defaultObjectPosition;
    private Vector3 _defaultLoadedObjectPosition;
    private Quaternion _defaultObjectRotation;
    private Quaternion _defaultLoadedObjectRotation;
    private Vector3 _defaultObjectScale;
    private Vector3 _defaultLoadedObjectScale;
    private Slider _scaleSlider;
    private Slider _cutPlaneSlider;

    private void Start()
    {
        _scaleSlider = _sliderForScale.GetComponent<Slider>();
        _cutPlaneSlider = _sliderForCutPlane.GetComponent<Slider>();
        _cutPlaneSlider.value = 0f;
        SetCutPlaneStrength();
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


    public void SetCutPlaneStrength()
    {
        List<Transform> spawnedObjects = new List<Transform>(_objectSpawn.GetComponentsInChildren<Transform>());
        spawnedObjects.Add(_referenceObject.transform);

        foreach (Transform child in spawnedObjects)
        {
            if(child == _objectSpawn.transform) continue;
            Material material = child.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            if (child == _referenceObject.transform)
            {
                material.SetFloat("_flow",-1f * (_cutPlaneSlider.value * 1.75f - 2.25f));
            }
            else
            {
                material.SetFloat("_flow", -1f * (_cutPlaneSlider.value * 215 - 150));
            }
        }
        
    }
    
}
