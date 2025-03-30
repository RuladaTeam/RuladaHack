using System.Collections;
using System.Collections.Generic;
using Core.Scripts;
using Oculus.Interaction;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadMenuButton : MonoBehaviour
{
    [SerializeField] private Vector3 _startPositionOnTable;
    [SerializeField] private Material _loadedObjectMaterial;

    private const string URL_BASE = Config.URL + "/archive?name=";

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        StartCoroutine(DownloadFile());
    }

    private IEnumerator DownloadFile()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL_BASE + gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            byte[] stlData = www.downloadHandler.data;
            List<Mesh> meshes = StlReader.Read(stlData);
            List<MeshFilter> meshFilters = new List<MeshFilter>();
            List<GameObject> objectSeparated = new List<GameObject>();
            foreach (Mesh mesh in meshes)
            {
                SpawnMesh(mesh, out MeshFilter meshFilter, out GameObject obj);
                meshFilters.Add(meshFilter);
                objectSeparated.Add(obj);
            }

            GameObject spawn = GameObject.FindWithTag("Spawn");
            GameObject combinedObj = MeshCombiner.CombineMeshes(
                meshFilters, spawn.transform, _startPositionOnTable, _loadedObjectMaterial);
            SetupObject(combinedObj);
            

            foreach (var obj in objectSeparated)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
        }
    }

    private void SpawnMesh(Mesh mesh, out MeshFilter meshFilter, out GameObject obj)
    {
        obj = new GameObject("LoadedObjectMesh");
        meshFilter = obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
    }

    private void SetupObject(GameObject combinedObj)
    {
        combinedObj.layer = LayerMask.NameToLayer(Config.DESTRUCTABLE_LAYER_MASK);
        combinedObj.transform.localScale *= 0.001f;
        combinedObj.AddComponent<Rigidbody>()
            .AddComponent<BoxCollider>()
            .AddComponent<Grabbable>()
            .AddComponent<GrabInteractable>();
        Grabbable grabbable = combinedObj.GetComponent<Grabbable>();
        combinedObj.GetComponent<GrabInteractable>().InjectOptionalPointableElement(grabbable);
        combinedObj.GetComponent<GrabInteractable>().InjectRigidbody(combinedObj.GetComponent<Rigidbody>());
        combinedObj.GetComponent<Rigidbody>().useGravity = false;
        combinedObj.GetComponent<Rigidbody>().isKinematic = true;
        combinedObj.GetComponent<BoxCollider>().isTrigger = true;
        combinedObj.AddComponent<GrabbableWithName>();
        
        string objectType = string.Empty;
        if (combinedObj.name[^1] == 'I')
        {
            objectType = "МРТ";
        }
        else if (combinedObj.name[^1] == 'T')
        {
            objectType = "КТ";
        }
        combinedObj.GetComponent<GrabbableWithName>().RussianName = $"Загруженный объект {objectType}";

    }

}