using UnityEngine;

public class ComparationTableWorkspace : MonoBehaviour
{

    // looks like it does not work on quest
    public void WireFrameMode()
    {
        GL.wireframe = !GL.wireframe;
    }
}
