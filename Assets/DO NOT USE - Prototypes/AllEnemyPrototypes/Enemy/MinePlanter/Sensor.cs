using UnityEngine;

namespace PlaceHolders
{
    public class Sensor : MonoBehaviour
    {
        public bool activated = false;
        [SerializeField]
        float refreshRate = 0.1f, distance = 1f;

        void Start()
        {
            InvokeRepeating(nameof(CheckState), 0, refreshRate);
        }

        private void Update()
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward * distance, activated ? Color.green : Color.red);
        }

        void CheckState()
        {
            if(Physics.Raycast(transform.position, transform.forward, distance))
            {
                activated = true;
            }
            else activated = false;
        }
    }
}
