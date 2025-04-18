using System;
using System.Collections.Generic;
using Core.Scripts;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ComparisonTableWorkspace : MonoBehaviour
{
    [SerializeField] private GameObject _defaultObject;
    [SerializeField] private GameObject _sliderForScale;
    [SerializeField] private GameObject _sliderForCutPlane;
    [SerializeField] private GameObject _referenceObject;
    [SerializeField] private GameObject _objectSpawn;

    private Slider _scaleSlider;
    private Slider _cutPlaneSlider;
    private List<GameObject> _objectsInZone = new();

    private void Start()
    {
        _objectsInZone.Add(_referenceObject);
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


    public void SetCutPlaneStrength()
    {
        List<Transform> spawnedObjects = new List<Transform>(_objectSpawn.GetComponentsInChildren<Transform>());
        spawnedObjects.Add(_referenceObject.transform);

        foreach (Transform child in spawnedObjects)
        {
            if (child == _objectSpawn.transform) continue;
            Material material = child.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            if (child == _referenceObject.transform)
            {
                material.SetFloat("_flow", -1f * (_cutPlaneSlider.value * 1.74f - 2.25f));
            }
            else
            {
                material.SetFloat("_flow", -1f * (_cutPlaneSlider.value * 215 - 150));
            }
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
