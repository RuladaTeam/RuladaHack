using Core.Scripts;
using UnityEngine;

public class TrashbinBottom : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Config.DESTRUCTABLE_LAYER_MASK))
        {
            Destroy(other.gameObject);
        }
    }
}
