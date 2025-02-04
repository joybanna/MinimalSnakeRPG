using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataObstacle", menuName = "GameData/DataObstacle")]
public class DataObstacle : ScriptableObject
{
    [SerializeField] private ObstacleBase squareOnePrefab;
    [SerializeField] private ObstacleBase squareTwoPrefab;
    [SerializeField] private ObstacleBase rectVerticalPrefab;
    [SerializeField] private ObstacleBase rectHorizontalPrefab;

    public Dictionary<ObstacleType, ObstacleBase> GetObstaclePrefabs()
    {
        return new Dictionary<ObstacleType, ObstacleBase>
        {
            { ObstacleType.SquareOne, squareOnePrefab },
            { ObstacleType.SquareTwo, squareTwoPrefab },
            { ObstacleType.RectVertical, rectVerticalPrefab },
            { ObstacleType.RectHorizontal, rectHorizontalPrefab }
        };
    }
}

public class LoadDataObstacle : Singleton<LoadDataObstacle>
{
    private DataObstacle _dataObstacle;
    private const string Path = "DataObstacle";
    private Dictionary<ObstacleType, ObstacleBase> _obstaclePrefabs = new Dictionary<ObstacleType, ObstacleBase>();

    public LoadDataObstacle()
    {
        _dataObstacle = Resources.Load<DataObstacle>(Path);
        if (_dataObstacle == null)
        {
            Debug.LogError($"Can't load data obstacle from path: {Path}");
        }
        else
        {
            _obstaclePrefabs = _dataObstacle.GetObstaclePrefabs();
        }
    }

    public ObstacleBase GetObstaclePrefab(ObstacleType type)
    {
        var isFound = _obstaclePrefabs.TryGetValue(type, out var obstacle);
        if (!isFound)
        {
            CustomDebug.SetMessage($"Can't find obstacle with type: {type}", Color.red);
            return null;
        }
        else
        {
            return obstacle;
        }
    }
}