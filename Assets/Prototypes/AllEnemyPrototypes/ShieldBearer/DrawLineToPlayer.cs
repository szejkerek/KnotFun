using System.Linq;
using UnityEngine;

namespace PlaceHolders
{
    public class DrawLineToPlayer : MonoBehaviour
    {
        public Color lineColor = Color.green;

        void OnDrawGizmos()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 0) return;

            GameObject nearestPlayer = players.OrderBy(p => Vector3.Distance(transform.position, p.transform.position)).First();
            if (nearestPlayer != null)
            {
                Gizmos.color = lineColor;
                Gizmos.DrawLine(transform.position, nearestPlayer.transform.position);
            }
        }
    }

}
