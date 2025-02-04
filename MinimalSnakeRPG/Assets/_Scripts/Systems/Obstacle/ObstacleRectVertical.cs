using UnityEngine;

public class ObstacleRectVertical : ObstacleBase
{
    [SerializeField] private SpriteRenderer box_1x1;
    [SerializeField] private SpriteRenderer box_1x2;

    private Box _box_1x1;
    private Box _box_1x2;

    public override void InitObstacle(Box box)
    {
        _box_1x1 = box;
        SetBox(_box_1x1, box_1x1);

        _box_1x2 = GridBoxesCollector.instance.FindBox(box.Grid.x, box.Grid.y + 1, out var box1x2) ? box1x2 : null;
        SetBox(_box_1x2, box_1x2);
    }
}