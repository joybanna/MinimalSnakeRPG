using UnityEngine;

public class ObstacleSquareTwo : ObstacleBase
{
    [SerializeField] private SpriteRenderer box_1x1;
    [SerializeField] private SpriteRenderer box_1x2;
    [SerializeField] private SpriteRenderer box_2x1;
    [SerializeField] private SpriteRenderer box_2x2;

    private Box _box_1x1;
    private Box _box_1x2;
    private Box _box_2x1;
    private Box _box_2x2;

    public override void InitObstacle(Box box)
    {
        _box_1x1 = box;
        SetBox(_box_1x1, box_1x1);

        _box_1x2 = GridBoxesCollector.instance.FindBox(box.Grid.x, box.Grid.y + 1, out var box1x2) ? box1x2 : null;
        SetBox(_box_1x2, box_1x2);

        _box_2x1 = GridBoxesCollector.instance.FindBox(box.Grid.x + 1, box.Grid.y, out var box2x1) ? box2x1 : null;
        SetBox(_box_2x1, box_2x1);

        _box_2x2 = GridBoxesCollector.instance.FindBox(box.Grid.x + 1, box.Grid.y + 1, out var box2x2) ? box2x2 : null;
        SetBox(_box_2x2, box_2x2);
    }
    
}