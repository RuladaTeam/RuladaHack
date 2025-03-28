using System.Collections.Generic;
using UnityEngine;

public static class MeshCombiner
{
    public static bool useSharedMesh = false;  // Use shared meshes (faster but modifies original meshes)
    public static bool generateNewMaterial = true; // Combine materials into a single material
    
    public static GameObject CombineMeshes(List<MeshFilter> meshFilters, Vector3 startPosition, Material material)
    {
        // Create a new GameObject to hold the combined mesh
        GameObject combinedObject = new GameObject("LoadedObject");
        combinedObject.transform.position = startPosition;

        // Add a MeshFilter and MeshRenderer to the new object
        MeshFilter combinedMeshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer combinedMeshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // Prepare CombineInstance array
        CombineInstance[] combineInstances = new CombineInstance[meshFilters.Count];
        for (int i = 0; i < meshFilters.Count; i++)
        {
            if (meshFilters[i] == null || meshFilters[i].sharedMesh == null)
            {
                Debug.LogWarning($"MeshFilter at index {i} is null or has no mesh.");
                continue;
            }

            combineInstances[i].mesh = meshFilters[i].sharedMesh;
            combineInstances[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        // Create a new mesh and combine the meshes
        Mesh combinedMesh = new Mesh
        {
            indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
        };
        combinedMesh.CombineMeshes(combineInstances, generateNewMaterial);

        // Assign the combined mesh to the new object
        combinedMeshFilter.mesh = combinedMesh;
        
        //combinedObject.GetComponent<MeshFilter>().mesh = combinedMesh;

        // Handle materials
        // if (generateNewMaterial)
        // {
        //     Material[] materials = new Material[meshFilters.Count];
        //     for (int i = 0; i < meshFilters.Count; i++)
        //     {
        //         if (meshFilters[i].GetComponent<MeshRenderer>() != null)
        //         {
        //             materials[i] = meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial;
        //         }
        //     }
        //     combinedMeshRenderer.material = materials[0];
        // }
        // else
        // {
        //     combinedMeshRenderer.materials = new[] { material };
        // }
        combinedMeshRenderer.materials = new[] { material };
        return combinedObject;
    }
}