using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject curBoss;
    public TMP_Text bossNickname;
    public TMP_Text bossName;
    public TMP_Text bossHp;
    public GameObject bossSmoke;
    public GameObject playerSmoke;

    public GameObject player;
    public GameObject capsule;
    public BoxCollider2D mapCollider2D;
    public Tilemap tilemap;
    public TileBase baseTile;
    public TileBase skillTile;
    public List<Transform> segments = new List<Transform>();
    public List<Transform> bossSkill = new List<Transform>();
    public GameObject gameOverPanel;

    public bool isGameOver = false;

    void Start()
    {
        instance = this;

        segments.Clear();
        bossSkill.Clear();
        curBoss = Instantiate(BossList.instance.boss[BossList.instance.bossIdx], new Vector3(0, 6.5f, 0), Quaternion.identity);
        player = Instantiate(player, new Vector3(0, -3, 0), Quaternion.identity);
        capsule = Instantiate(capsule, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public bool IsOverlapping(Transform target, List<Transform> list)
    {
        Vector2 targetPosition = new Vector2(target.position.x, target.position.y);

        foreach (Transform t in list)
        {
            if (Vector2.Distance(t.position, targetPosition) < 0.5f)
                return true;
        }

        return false;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = curBoss.GetComponent<Boss>().curHp == 0 ? "You Win!" : "Defeated...";
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
