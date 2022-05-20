using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public BoxCollider2D explosionArea;
    public GameObject alert;
    public GameObject explosion;
    public GameObject smoke;

    int totalExplosions=0;
    private void FixedUpdate()
    {
       StartCoroutine(SetExplosion());
    }
    IEnumerator SetExplosion()
    {

        yield return new WaitForSeconds(5f);
        StartCoroutine(MakeExplosion());
    }
    
    IEnumerator MakeExplosion()
    {
        if (totalExplosions <= 5)
        {
            totalExplosions++;
            Bounds bounds = explosionArea.bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector3 explosionPos = new Vector3(x, y, 0);
            GameObject explosionAlert = Instantiate(alert, explosionPos, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            GameObject ExplosionEffect = Instantiate(explosion, explosionAlert.transform.position, Quaternion.identity);
            SoundManager.Instance().PlaySFX(Sound.missileExplosion);
            GameObject SmokeEffect = Instantiate(smoke, ExplosionEffect.transform.position, Quaternion.identity);
            Destroy(explosionAlert);
            Destroy(ExplosionEffect, 2f);
            totalExplosions--;
            Destroy(SmokeEffect, 5f);
        }
    }
}
