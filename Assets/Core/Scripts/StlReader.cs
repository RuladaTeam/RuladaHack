using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class StlReader 
{
    private static int MaxVertexCount = 64500;
    private static uint TrianglesCount { get; set; }
    private static List<Mesh> Meshes = new ();

    /// <summary>
    /// Reads binary STL data and creates meshes within the vertex limit.
    /// </summary>
    /// <param name="data">Binary STL data as a byte array.</param>
    /// <returns>List of meshes.</returns>
    public static List<Mesh> Read(byte[] data)
    {
        Meshes.Clear();
        var reader = new BinaryReader(new MemoryStream(data));
        reader.ReadBytes(80); // Skip the 80-byte header

        var trianglesCount = reader.ReadUInt32();
        TrianglesCount = trianglesCount;
        Debug.Log($"Triangles Count: {trianglesCount}");

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var triangleIndex = 0;

        for (var i = 0; i < trianglesCount; i++)
        {
            // Skip the normal vector (not used in Unity Mesh)
            reader.ReadSingle(); // Normal X
            reader.ReadSingle(); // Normal Y
            reader.ReadSingle(); // Normal Z

            // Read the three vertices of the triangle
            var vertex1 = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            var vertex2 = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            var vertex3 = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            vertices.Add(vertex1);
            vertices.Add(vertex2);
            vertices.Add(vertex3);

            triangles.Add(triangleIndex++);
            triangles.Add(triangleIndex++);
            triangles.Add(triangleIndex++);

            reader.ReadUInt16(); // Skip the attribute byte count (usually 0)

            // Check if the vertex count exceeds the limit
            if (vertices.Count <= MaxVertexCount - 1)
            {
                continue;
            }

            AddMesh(vertices, triangles);

            vertices.Clear();
            triangles.Clear();
            triangleIndex = 0;
        }

        // Add any remaining vertices and triangles
        if (vertices.Count > 0)
        {
            AddMesh(vertices, triangles);
        }

        return Meshes;
    }

    /// <summary>
    /// Adds a new mesh to the list.
    /// </summary>
    /// <param name="vertices">List of vertices.</param>
    /// <param name="triangles">List of triangle indices.</param>
    private static void AddMesh(List<Vector3> vertices, List<int> triangles)
    {
        var mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray()
        };

        mesh.RecalculateNormals();
        mesh.Optimize();
        Meshes.Add(mesh);
    }
}