using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    Coroutine coroutine;
    void Start()
    {
        Setup();
    }


    void Setup()
    {
        do { SetPosition(); } while (GameManager.instance.IsOverlapping(transform, GameManager.instance.segments) || GameManager.instance.IsOverlapping(transform, GameManager.instance.bossSkill));

        if(coroutine != null )
        {
            StopAllCoroutines();
        }

        coroutine = StartCoroutine(Move());
    }

    void SetPosition()
    {
        int x = Random.Range((int)GameManager.instance.mapCollider2D.bounds.min.x, (int)GameManager.instance.mapCollider2D.bounds.max.x + 1);
        int y = Random.Range((int)GameManager.instance.mapCollider2D.bounds.min.y, (int)GameManager.instance.mapCollider2D.bounds.max.y + 1);
        transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !GameManager.instance.isGameOver)
        {
            GameManager.instance.curBoss.GetComponent<Dengert>().Hurt();
            Setup();
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(20);

        Setup();
    }
}
