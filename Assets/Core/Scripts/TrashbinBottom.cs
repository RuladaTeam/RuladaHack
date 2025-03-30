using Core.Scripts;
using UnityEngine;

public class TrashbinBottom : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Config.DESTRUCTABLE_LAYER_MASK))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer(Config.REFERENCE_LAYER_MASK))
        {
            other.gameObject.GetComponent<GrabbableWithName>().SetDefaultPosition();
        }
    }
}
