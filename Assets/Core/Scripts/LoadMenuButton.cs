using System.Collections;
using System.Collections.Generic;
using Core.Scripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadMenuButton : MonoBehaviour
{

    [SerializeField] private Vector3 _startPositionOnTable;
    
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
            List<GameObject> objects = new List<GameObject>();
            foreach (Mesh mesh in meshes)
            {
                SpawnMesh(mesh, out MeshFilter meshFilter, out GameObject obj);
                meshFilters.Add(meshFilter);
                objects.Add(obj);
            }
            
            GameObject combinedObj = MeshCombiner.CombineMeshes(meshFilters, _startPositionOnTable);
            combinedObj.transform.localScale *= 0.001f;
            
            // Optionally, destroy the original objects
            foreach (var ob in objects)
            {
                if (ob != null)
                {
                    Destroy(ob
                    );
                }
            }
        }
    }

    private void SpawnMesh(Mesh mesh, out MeshFilter meshFilter, out GameObject obj)
    {
        obj = new GameObject("LoadedObjectMesh");
        meshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        
        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));
    }
}