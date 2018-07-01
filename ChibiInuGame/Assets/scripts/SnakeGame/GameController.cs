using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [Header("Snake Parts")]
    public GameObject snakePrefab;
    public Snake head;
    public Snake tail;
    [Space]
    public int direction;
    public Vector2 nextPos;
    public int currentSize;
    public int maxSize;
    public float stepSpeed;
    [Space]
    public Sprite leadSprite;
    public Sprite bodyLook;

    Vector2 globalScale;
    public bool facingRight;

    private void OnEnable()
    {
        Snake.hit += hit;
    }

    // Use this for initialization
    void Start () {
        facingRight = false;
        InvokeRepeating("TimerInvoke", 0, stepSpeed);	
	}

    private void OnDisable()
    {
        Snake.hit -= hit;
    }
    // Update is called once per frame
    void Update () {
        ChangeDir();
        
        
	}

    public void TimerInvoke()
    {
        Movement();
        if (currentSize >= maxSize)
        {
            Tail();
        }
        else
        {
            currentSize++;
        }
    }

    public void Movement()
    {
        GameObject temp;
        nextPos = head.transform.position;

        switch (direction)
        {
            case 0:
                nextPos = new Vector2(nextPos.x, nextPos.y + .5f); //UP
                break;
            case 1:
                nextPos = new Vector2(nextPos.x + .5f, nextPos.y); //Right
                break;
            case 2:
                nextPos = new Vector2(nextPos.x, nextPos.y - .5f); //Down
                break;
            case 3:
                nextPos = new Vector2(nextPos.x - .5f, nextPos.y); //Left
                break;
        }
        temp = (GameObject)Instantiate(snakePrefab, nextPos, transform.rotation);

        SpriteRenderer bodySprite = head.GetComponent<SpriteRenderer>();
        bodySprite.sprite = bodyLook;

        head.SetNext(temp.GetComponent<Snake>());
        head.gameObject.tag = "snake";
        head = temp.GetComponent<Snake>();

        SpriteRenderer headSprite = head.GetComponent<SpriteRenderer>();
        headSprite.sprite = leadSprite;
        headSprite.tag = "Player";

        Transform dog = head.GetComponent<Transform>();
        Vector2 localScale = dog.transform.localScale;
       
        if(direction == 1)
        {
            facingRight = false;
            localScale.x = -1;
            dog.transform.localScale = localScale;
        }
        else if(direction == 3)
        {
            facingRight = true;
            localScale.x = 1;
            dog.transform.localScale = localScale;
        }
        else if (direction == 2)
        {
            
            if (facingRight)
            { 
                localScale.x = 1;
                dog.transform.localScale = localScale;
            }
            else if(!facingRight)
            {
                
                localScale.x = -1;
                dog.transform.localScale = localScale;
            }
        }
        else if (direction == 0)
        {
            if (facingRight)
            {
                localScale.x = 1;
                dog.transform.localScale = localScale;
            }
            else if (!facingRight)
            {
                localScale.x = -1;
                dog.transform.localScale = localScale;
            }
        }
        return;
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;

        Transform dog = head.GetComponent<Transform>();
        Vector2 localScale = dog.transform.localScale;
        localScale.x *= -1;
        dog.transform.localScale = localScale; ;
    }

    public void ChangeDir()
    {
        if(direction != 2 && Input.GetKey(KeyCode.UpArrow))
        {
            direction = 0;
        }

        else if (direction != 0 && Input.GetKey(KeyCode.DownArrow))
        {
            direction = 2;
        }

        else if (direction != 3 && Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1;
        }

        else if (direction != 1 && Input.GetKey(KeyCode.LeftArrow))
        {
            direction = 3;
        }
    }

    void Tail()
    {
        Snake tempSnake = tail;
        tail = tail.GetNext();
        tempSnake.RemoveTail();
    }

    void hit(string message)
    {
        if(message == "food")
        {
            maxSize++;
        }

        if(message == "Player" || message == "snake")
        {
            Debug.Log("touched");
            CancelInvoke("TimerInvoke");
        }
    }
}
