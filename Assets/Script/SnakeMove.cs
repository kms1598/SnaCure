using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float moveTime = 0.1f;
    Vector2 moveDirection = Vector2.left;
    Vector2 inputDirection = Vector2.left;

    [Header("Segments")]
    [SerializeField]
    Transform segmentPrefab;
    int segmentCount = 2;

    IEnumerator Start()
    {
        Setup();

        while(!GameManager.instance.isGameOver)
        {
            MoveSegments();

            yield return new WaitForSeconds(moveTime);
        }
    }

    void Update()
    {
        if (moveDirection.x != 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                inputDirection = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                inputDirection = Vector2.down;
            }
        }
        else if (moveDirection.y != 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                inputDirection = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                inputDirection = Vector2.right;
            }
        }
    }

    void Setup()
    {
        GameManager.instance.segments.Add(transform); //머리를 리스트에 저장

        for (int i = 0; i < segmentCount - 1; i++)
            AddSegment();
    }

    void AddSegment()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = GameManager.instance.segments[GameManager.instance.segments.Count - 1].position;
        GameManager.instance.segments.Add(segment);
    }

    void MoveSegments()
    {
        moveDirection = inputDirection;

        if(moveDirection == Vector2.up)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(moveDirection == Vector2.down)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(moveDirection == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(moveDirection == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }

        for (int i = GameManager.instance.segments.Count - 1; i > 0; i--)
        {
            GameManager.instance.segments[i].position = GameManager.instance.segments[i - 1].position;
        }

        transform.position = (Vector2)transform.position + moveDirection;

        if(transform.position.x < GameManager.instance.mapCollider2D.bounds.min.x || transform.position.x > GameManager.instance.mapCollider2D.bounds.max.x
            || transform.position.y < GameManager.instance.mapCollider2D.bounds.min.y || transform.position.y > GameManager.instance.mapCollider2D.bounds.max.y)
        {
            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Capsule")
        {
            AddSegment();
            
        }
        else if (collision.tag == "Wall")
        {
            GameOver();
        }
    }

    void GameOver()
    {
        GameManager.instance.playerSmoke.GetComponent<Transform>().position = new Vector3(transform.position.x, transform.position.y - 6.5f, 0);
        GameManager.instance.playerSmoke.SetActive(true);
        GameManager.instance.GameOver();
    }
}
