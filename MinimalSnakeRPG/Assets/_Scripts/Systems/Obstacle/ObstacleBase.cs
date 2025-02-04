using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour
{
    public abstract void InitObstacle(Box box);

    protected void SetBox(Box box, SpriteRenderer spriteRenderer)
    {
        if (box == null || box.BoxStatus != BoxStatus.Empty)
        {
            spriteRenderer.gameObject.SetActive(false);
        }
        else
        {
            box.BoxStatus = BoxStatus.Obstacle;
            spriteRenderer.gameObject.SetActive(true);
        }
    }
}