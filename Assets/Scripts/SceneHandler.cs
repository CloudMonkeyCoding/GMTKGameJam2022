using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler
{
    public static bool[] activeEnemies = new bool[4];
    public static int currentEnemy;
    public static Vector3 lastPlayerLocation;

    public static bool restart = true;
}
