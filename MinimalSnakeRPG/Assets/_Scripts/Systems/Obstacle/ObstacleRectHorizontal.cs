using UnityEngine;

public class ObstacleRectHorizontal : ObstacleBase
{
    [SerializeField] private SpriteRenderer box_1x1;
    [SerializeField] private SpriteRenderer box_2x1;

    private Box _box_1x1;
    private Box _box_2x1;

    public override void InitObstacle(Box box)
    {
        _box_1x1 = box;
        SetBox(_box_1x1, box_1x1);

        _box_2x1 = GridBoxesCollector.instance.FindBox(box.Grid.x + 1, box.Grid.y, out var box2x1) ? box2x1 : null;
        SetBox(_box_2x1, box_2x1);
    }
}