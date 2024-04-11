using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dengert : Boss
{
    public GameObject cactus;
    public GameObject sand;

    private new void Start()
    {
        maxHp = 50;
        bossNickname = "Dangerous Sphinx";
        bossName = "Dengert";

        base.Start();
        sand = Instantiate(sand, new Vector3(0, -3, 0), Quaternion.identity);
        sand.SetActive(false);
    }

    public override void Hurt()
    {
        base.Hurt();

        if(!GameManager.instance.isGameOver)
        {
            if (curHp % 10 == 0)
            {
                Skill2();
            }
            else if (curHp % 3 == 0)
            {
                for(int i = 0; i < (curHp < (maxHp / 2) ? 2 : 1); i++)
                    Skill1();
            }
        }
    }

    void Skill1()
    {
        anim.SetTrigger("Skill1");

        Transform spawnpoint = SetSpawnCactusPoint();
        Vector3Int tilePosition = GameManager.instance.tilemap.WorldToCell(new Vector3Int((int)spawnpoint.position.x , (int)spawnpoint.position.y, 0));
        GameManager.instance.tilemap.SetTile(tilePosition, GameManager.instance.skillTile);
        StartCoroutine(SpawnCactus(spawnpoint, tilePosition));
    }

    Transform SetSpawnCactusPoint()
    {
        int x;
        int y;
        Transform spawnpoint = new GameObject("cactus").transform;

        do {
            x = UnityEngine.Random.Range((int)GameManager.instance.mapCollider2D.bounds.min.x + 1, (int)GameManager.instance.mapCollider2D.bounds.max.x);
            y = UnityEngine.Random.Range((int)GameManager.instance.mapCollider2D.bounds.min.y + 1, (int)GameManager.instance.mapCollider2D.bounds.max.y);

            spawnpoint.position = new Vector3(x, y, 0);
        } while (GameManager.instance.IsOverlapping(spawnpoint, GameManager.instance.segments) || GameManager.instance.IsOverlapping(spawnpoint, GameManager.instance.bossSkill) || GameManager.instance.IsOverlapping(spawnpoint, new List<Transform> { GameManager.instance.capsule.transform}));
        
        GameManager.instance.bossSkill.Add(spawnpoint);

        return spawnpoint;
    }

    IEnumerator SpawnCactus(Transform spawn, Vector3Int tilePosition)
    {
        yield return new WaitForSeconds(2);

        GameObject spawnCactus = Instantiate(cactus, spawn.position, Quaternion.identity);
        spawnCactus.GetComponent<SpriteRenderer>().flipX = (curHp % 2 == 0);
        GameManager.instance.tilemap.SetTile(tilePosition, GameManager.instance.baseTile);
    }

    void Skill2()
    {
        anim.SetTrigger("Skill2");
        StartCoroutine(ShowSand());
    }

    IEnumerator ShowSand()
    {
        sand.SetActive(true);
        yield return new WaitForSeconds((maxHp - curHp) / 2.5f);
        sand.SetActive(false);
    }
}
