using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitCollisionDetect : MonoBehaviour
{
    private string _myTag;
    [SerializeField] private Collider2D myCollider;

    private void Start()
    {
        _myTag = gameObject.tag;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CustomDebug.SetMessage("Enemy Collision", Color.yellow);
        }

        if (other.CompareTag("Hero"))
        {
            CustomDebug.SetMessage("Hero Collision : Recruit Hero", Color.yellow);
            var unitMain = other.GetComponent<UnitMain>();
            HeroHeadGroup.instance.RecruitHero(unitMain);
        }
    }

    public void EnableCollisionDetect()
    {
        enabled = true;
        myCollider.enabled = true;
    }

    public void DisableCollisionDetect()
    {
        enabled = false;
        myCollider.enabled = false;
    }
}