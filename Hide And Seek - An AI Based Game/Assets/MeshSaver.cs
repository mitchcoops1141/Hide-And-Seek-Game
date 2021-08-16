using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSaver : MonoBehaviour
{
    public GameObject mesh;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void SetMesh(GameObject m)
    {
        mesh = m;
    }

    public GameObject GetMesh()
    {
        return mesh;
    }
}
