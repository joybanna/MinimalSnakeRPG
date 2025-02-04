using System.Collections;
using UnityEngine;

public abstract class SpawnerBase : MonoBehaviour
{
    public abstract IEnumerator Spawns(int wave);
}