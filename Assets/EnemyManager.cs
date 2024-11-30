using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   private List<Enemy> sceneEnemies = new();
   private void Start()
   {
      sceneEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
   }
}
