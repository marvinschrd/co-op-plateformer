﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicPlatform : MonoBehaviour
{
    bool isSmall = false;
    bool isBig = false;
    bool checkIfSmall = false;
    bool checkIfBig = false;
    Shrinking shrinking;
    ScaleChange scaleChange;
    SpriteRenderer sprite;
    Vector3 growingScale;
    Vector3 shrinkingScale;
    Vector3 normalScale;
    bool grow = false;
    bool player1On = false;
    bool player2On = false;
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = FindObjectOfType<ScaleChange>();
        shrinking = FindObjectOfType<Shrinking>();
        normalScale = transform.localScale;
        growingScale = new Vector3(normalScale.x * 2.5f, normalScale.y, normalScale.z);
        shrinkingScale = new Vector3(normalScale.x /2, normalScale.y, normalScale.z);
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isSmall = shrinking.ShowState();
        isBig = scaleChange.ShowState();
        switch(state)
        {
            case State.NORMAL:
                BackToNormal();
                if(player1On&&isBig)
                {
                    state = State.GROW;
                }
                if(player2On&&isSmall)
                {
                    state = State.SHRINK;
                }
                break;
            case State.GROW:
                Grow();
                if(!player1On||!isBig)
                {
                    state = State.NORMAL;
                }
                break;
            case State.SHRINK:
                Shrink();
                if (!player2On || !isSmall)
                {
                    state = State.NORMAL;
                }
                break;
        }

    }

    enum State
    {
        NORMAL,
        GROW,
        SHRINK
    }
    State state = State.NORMAL;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player1On = true;
        }
        if (collision.gameObject.tag == "Player2")
        {
            player2On = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player1On = false;
        }
        if (collision.gameObject.tag == "Player2")
        {
            player2On = false;
        }
    }

    void Grow()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, growingScale, Time.deltaTime);
        sprite.color = Color.blue;
    }

    void BackToNormal()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, normalScale, Time.deltaTime);
        sprite.color = Color.white;
    }
    void Shrink()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, shrinkingScale, Time.deltaTime);
        sprite.color = Color.green;
    }
}
