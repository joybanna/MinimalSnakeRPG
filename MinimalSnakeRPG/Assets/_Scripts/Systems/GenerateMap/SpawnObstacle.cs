using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    None = 0,
    SquareOne = 1, // 1x1
    SquareTwo = 2, // 2x2
    RectVertical = 3, // 1x2
    RectHorizontal = 4, // 2x1
}

public class SpawnObstacle : SpawnerBase
{
    [SerializeField] private Transform parent;

    [SerializeField] private ObstacleType[] obstacleTypes =
        { ObstacleType.SquareOne, ObstacleType.SquareTwo, ObstacleType.RectVertical, ObstacleType.RectHorizontal };

    [SerializeField] private List<ObstacleBase> obstacles = new List<ObstacleBase>();

    public void SpawnObstacleItem(ObstacleType obstacleType, Box box)
    {
        var prefab = LoadDataObstacle.Instance.GetObstaclePrefab(obstacleType);
        if (prefab == null)
        {
            CustomDebug.SetMessage("Can't find obstacle prefab", Color.red);
            return;
        }

        var obstacle = Instantiate(prefab, box.transform.position, Quaternion.identity, parent);
        obstacle.InitObstacle(box);
        obstacles.Add(obstacle);
    }

    public override IEnumerator Spawns(int wave)
    {
        var mod = wave % 5;
        if (mod != 3) yield break;
        ClearObstacles();
        var spawns = CalculateSpawnMap.GetObstacleSpawn(obstacleTypes, wave);
        if (spawns.IsEmptyCollection()) yield break;
        for (int i = spawns.Count - 1; i >= 0; i--)
        {
            var isBox = SpawnController.instance.GetSpawnBox(false, out var box);
            if (isBox)
            {
                SpawnObstacleItem(spawns[i], box);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void ClearObstacles()
    {
        for (var index = obstacles.Count - 1; index >= 0; index--)
        {
            var obstacle = obstacles[index];
            Destroy(obstacle.gameObject);
        }

        obstacles.Clear();
    }
}




