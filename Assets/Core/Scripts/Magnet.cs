using System;
using System.Collections.Generic;
using Core.Scripts;
using TMPro;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private Transform _anchor;
    [SerializeField] private TextMeshProUGUI _objectsList;
    
    private bool _isActive;
    private List<string> _objectsInMagnet = new ();

    private void Update()
    {
        string text = string.Empty;
        foreach (var name in _objectsInMagnet)
        {
            text += name + ", ";
        }
        
        if (text != "")
        {
            text = text.Substring(0, text.Length - 2);
        }
        _objectsList.text = text;
    }

    private void OnTriggerEnter(Collider other)
    {
        AnchorModel(other);
    }

    private void OnTriggerStay(Collider other)
    {
        AnchorModel(other);
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.layer == LayerMask.NameToLayer(Config.DESTRUCTABLE_LAYER_MASK)
            || obj.layer == LayerMask.NameToLayer(Config.REFERENCE_LAYER_MASK))
        {
            GrabbableWithName grabbableWithName = obj.GetComponent<GrabbableWithName>();
            if (_objectsInMagnet.Contains(grabbableWithName.RussianName))
            {
                _objectsInMagnet.Remove(grabbableWithName.RussianName);
            }
        }
    }

    private void AnchorModel(Collider other)
    {
        
        GameObject obj = other.gameObject;
        if (!_isActive) return;
        if (obj.layer == LayerMask.NameToLayer(Config.DESTRUCTABLE_LAYER_MASK))
        {
            other.transform.rotation = _anchor.rotation;
            Bounds modelBounds = obj.GetComponent<MeshFilter>().mesh.bounds;
            Vector3 offset = obj.transform.position - obj.transform.TransformPoint(modelBounds.center );
            obj.transform.position = _anchor.position + offset;
            GrabbableWithName grabbableWithName = obj.GetComponent<GrabbableWithName>();
            if (!_objectsInMagnet.Contains(grabbableWithName.RussianName))
            {
                _objectsInMagnet.Add(grabbableWithName.RussianName);
            }
        } 
        else if (obj.layer == LayerMask.NameToLayer(Config.REFERENCE_LAYER_MASK))
        {
            obj.GetComponentInChildren<Transform>().localRotation = new Quaternion(0,0,0,0);
            obj.transform.position = _anchor.position;
            GrabbableWithName grabbableWithName = obj.GetComponent<GrabbableWithName>();
            if (!_objectsInMagnet.Contains(grabbableWithName.RussianName))
            {
                _objectsInMagnet.Add(grabbableWithName.RussianName);
            }
        }
    }
    
    public void SetActive()
    {
        _isActive = !_isActive;
    }
}
