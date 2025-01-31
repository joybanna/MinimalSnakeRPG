using UnityEngine;

public class HpBar : MonoBehaviour
{
    private static readonly int CURRENT_HP = Shader.PropertyToID("_currentHp");
    [SerializeField] private SpriteRenderer hpBar;


    public void SetHpBar(int currentHp, int maxHp)
    {
        var hpPercent = (float)currentHp / maxHp;
        hpBar.material.SetFloat(CURRENT_HP, hpPercent);
    }
}