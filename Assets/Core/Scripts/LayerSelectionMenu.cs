using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LayerSelectionMenu : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private GameObject _skinLayer;
    [SerializeField] private GameObject _glandLayer;
    [SerializeField] private GameObject _tracheaLayer;
    [SerializeField] private GameObject _lymphLayer;
    [SerializeField] private GameObject _veinsLayer;
    [SerializeField] private GameObject _arteryLayer;
    [SerializeField] private GameObject _bonesLayer;
    [Space(5f)]
    [Header("Sliders")]
    [SerializeField] private Slider _skinSlider;
    [SerializeField] private Slider _organSlider;
    [SerializeField] private Slider _lymphSlider;
    [SerializeField] private Slider _veinsSlider;
    [SerializeField] private Slider _arterySlider;
    [SerializeField] private Slider _bonesSlider;

    private void SetLayerActive(GameObject obj, bool isActive)
    {
        obj.SetActive(isActive);
    }

    private void SetLayerTransparency(GameObject obj, float value)
    {
        Material material = obj.GetComponent<MeshRenderer>().sharedMaterial;;
        Color materialColor = material.color;
        materialColor.a = value;
        material.color = materialColor;
        obj.GetComponentInChildren<MeshRenderer>().sharedMaterial = material;
    }
    
    public void SetSkinActive(bool isActive)
    {
        SetLayerActive(_skinLayer, isActive);
    }
    
    public void SetOrganActive(bool isActive)
    {
        SetLayerActive(_tracheaLayer, isActive);
        SetLayerActive(_glandLayer, isActive);
    }
    
    public void SetLymphActive(bool isActive)
    {
        SetLayerActive(_lymphLayer, isActive);
    }
    
    public void SetVeinsActive(bool isActive)
    {
        SetLayerActive(_veinsLayer, isActive);
    }
    
    public void SetArteryActive(bool isActive)
    {
        SetLayerActive(_arteryLayer, isActive);
    }
    
    public void SetBonesActive(bool isActive)
    {
        SetLayerActive(_bonesLayer, isActive);
    }

    public void SetSkinLayerTransparency()
    {
        SetLayerTransparency(_skinLayer, _skinSlider.value);
    }

    public void SetOrganLayerTransparency()
    {
        SetLayerTransparency(_tracheaLayer, _organSlider.value);
        SetLayerTransparency(_glandLayer, _organSlider.value);
    }

    public void SetLymphLayerTransparency()
    {
        SetLayerTransparency(_lymphLayer, _lymphSlider.value);
    }

    public void SetVeinsLayerTransparency()
    {
        SetLayerTransparency(_veinsLayer, _veinsSlider.value);
    }

    public void SetArteryLayerTransparency()
    {
        SetLayerTransparency(_arteryLayer, _arterySlider.value);
    }

    public void SetBonesLayerTransparency()
    {
        SetLayerTransparency(_bonesLayer, _bonesSlider.value);
    }
}
