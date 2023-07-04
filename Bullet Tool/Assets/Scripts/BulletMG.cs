using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMG : MonoBehaviour
{
    private float shotT;

    public Sprite bulletSprite;

    public BulletMGState mgState;
    public BulletState state;

    public void StartSet(BulletMGState mGState, BulletState bState, Sprite bSprite)
    {
        mgState = mGState;
        state = bState;
        bulletSprite = bSprite;
    }

    private void Update()
    {
        shotT += Time.deltaTime;

        if(shotT >= mgState.ShotSpd)
        {

            if(mgState.Sound != null)
            {
                SoundManager.Instance.PlaySound(mgState.Sound);
            }

            Destroy(CreateBullet(), mgState.Lifespan);
            shotT = 0;
        }
    }


    private GameObject CreateBullet()
    {
        GameObject bullet = new GameObject("bullet");

        bullet.AddComponent<SpriteRenderer>().sprite = bulletSprite;
        bullet.AddComponent<Bullet>().state = state;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.AddComponent<CircleCollider2D>().isTrigger = true;

        TrailRenderer trail = bullet.AddComponent<TrailRenderer>();

        trail.material = mgState.Trail;
        trail.startWidth = 0.25f;
        trail.endWidth = 0f;
        trail.time = 0.1f;

        return bullet;
    }

}
