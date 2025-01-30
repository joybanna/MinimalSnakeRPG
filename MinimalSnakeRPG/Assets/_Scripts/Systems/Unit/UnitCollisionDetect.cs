using System;
using UnityEngine;

public class UnitCollisionDetect : MonoBehaviour
{
    private string _myTag;

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
        }
    }
}