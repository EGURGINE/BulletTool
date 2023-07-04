using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletState state;
    private Vector2 dir = Vector2.zero;
    private bool isTargetting = false;

    private void Start()
    {
        switch (state.Dir)
        {
            case EBulletDir.UP: dir = Vector2.up; break;
            case EBulletDir.DOWN: dir = Vector2.down; break;
            case EBulletDir.LEFT: dir = Vector2.left; break;
            case EBulletDir.RIGHT: dir = Vector2.right; break;
        }

        if(state.Target != null ) isTargetting = true;
    }



    void Update()
    {
        if (isTargetting == true)
        {
            TargetMove();
        }
        else
        {
            DirMove();
        }
    }

    private void TargetMove()
    {
        Vector2 dir = state.Target.position - transform.position;

        transform.Translate(dir * Time.deltaTime * state.Spd);
    }

    private void DirMove()
    {
        transform.Translate(dir * Time.deltaTime * state.Spd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            Hit();
        }
    }
    private void Hit()
    {
        if (state.AudioEffect != null)
        {
            SoundManager.Instance.PlaySound(state.AudioEffect);
        }

        print("Enter");

        Destroy(gameObject);
    }
}
