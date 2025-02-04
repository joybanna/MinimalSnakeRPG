using UnityEngine;

public class ObstacleSquareOne : ObstacleBase
{
    [SerializeField] private SpriteRenderer box_1x1;
    private Box _box_1x1;

    public override void InitObstacle(Box box)
    {
        _box_1x1 = box;
        SetBox(_box_1x1, box_1x1);
    }
}