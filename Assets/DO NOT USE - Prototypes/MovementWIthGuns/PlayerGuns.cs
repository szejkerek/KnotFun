using System.Collections.Generic;
using UnityEngine;

namespace PlaceHolders
{
    // Define the gun interface
    public interface IGun
    {
        void Use();
    }

    public class PlayerGuns : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> guns; // Assign these in the Inspector. Each should have a component that implements IGun.

        private int currentGunIndex = 0;

        private void Start()
        {
            // Ensure that only the current gun is active at start.
            UpdateActiveGun();
        }

        // Call this method when scrolling up
        public void OnScrollUp()
        {
            if (guns == null || guns.Count == 0)
                return;

            currentGunIndex = (currentGunIndex + 1) % guns.Count;
            UpdateActiveGun();
        }

        // Call this method when scrolling down
        public void OnScrollDown()
        {
            if (guns == null || guns.Count == 0)
                return;

            // Wrap around using modulo arithmetic
            currentGunIndex = (currentGunIndex - 1 + guns.Count) % guns.Count;
            UpdateActiveGun();
        }

        // Call this method to use the currently selected gun.
        public void OnUse()
        {
            if (guns == null || guns.Count == 0)
            {
                Debug.LogWarning("No guns available to use.");
                return;
            }

            GameObject currentGunObject = guns[currentGunIndex];

            if (currentGunObject == null)
            {
                Debug.LogWarning("Current gun GameObject is null.");
                return;
            }

            // Try to get a component that implements IGun.
            IGun gunComponent = currentGunObject.GetComponent<IGun>();
            if (gunComponent != null)
            {
                gunComponent.Use();
            }
            else
            {
                Debug.LogWarning("The current gun does not have a component that implements IGun.");
            }
        }

        // Activates only the currently selected gun and deactivates all others.
        private void UpdateActiveGun()
        {
            for (int i = 0; i < guns.Count; i++)
            {
                if (guns[i] != null)
                {
                    // Only the currently selected gun remains active.
                    guns[i].SetActive(i == currentGunIndex);
                }
            }
        }
    }
}
