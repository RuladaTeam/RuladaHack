using Oculus.Interaction;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Scripts
{
    public class GrabbableWithName : MonoBehaviour
    {
        [SerializeField] public string RussianName = "Прикольни объект:)";

        public Vector3 DefaultPosition { get; private set; }
        public Quaternion DefaultRotation { get; private set; }
        public Vector3 DefaultScale { get; private set; }

        private void Start()
        {
            ResetDefaultValues();
        }

        public void SetDefaultPosition()
        {
            gameObject.transform.SetPositionAndRotation(DefaultPosition, DefaultRotation);
        }

        public void ResetDefaultValues()
        {
            DefaultPosition = gameObject.transform.position;
            DefaultRotation = gameObject.transform.rotation;
            DefaultScale = gameObject.transform.localScale;
        }
    }
}