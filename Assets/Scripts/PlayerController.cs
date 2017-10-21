﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float speed = 8f;
    public GameObject shoot;
    public float fireRate = 0.2f;
    private float lastShoot = 0f;

    public GameObject[] shoots;

    public Sprite spriteAlive;
    public Sprite spriteDead;

    private Vector2 prevVelocity;
    public bool laser = false;
    public bool explode = false;
    public bool exist = false;
    GameObject shootObject;
    private KeySequenceController combo1;
    private KeySequenceController combo2;
    private List<KeyCode> keys;
    public Slider specialBarSlider;
    public Text sequenceKeysP1;
    public Text sequenceKeysP2;
    private float dashTimer = 0;
    public float maxDashTimer;

    public int maxLifeRess = 5;
    private int currLifeRess = 0;

    public bool isActive = false;
    private float timerExceedSequenceP1 = 0;
    private float timerExceedSequenceP2 = 0;
    private enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
    private DashState dashState;

    public bool player1;
    GameManager gameManager;
    SpriteRenderer sr;
    bool invul = true;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rigidbody = this.GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager)
        {
            gameManager.started = true;

            if (player1 && !gameManager.player1)
            {
                Destroy(gameObject);
            }
            else if (!player1 && !gameManager.player2)
            {
                Destroy(gameObject);
            }

            if (player1)
            {
                sr.color = gameManager.player1Color;
            }
            else
            {
                sr.color = gameManager.player2Color;
            }
        }

        StartCoroutine(CantDie());
        combo1 = new KeySequenceController();
        combo2 = new KeySequenceController();
        keys = new List<KeyCode>();
    }

    IEnumerator CantDie()
    {
        yield return new WaitForSeconds(1f);

        invul = false;

    }

    void Update()
    {
        if (currLifeRess >= 5)
        {
            isActive = true;
            sr.sprite = spriteAlive;
            currLifeRess = 0;
        }
        if (isActive)
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                shoot = shoots[0];
                laser = false;
                explode = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                shoot = shoots[1];
                laser = false;
                explode = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                shoot = shoots[2];
                laser = true;
                explode = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                shoot = shoots[3];
                laser = false;
                explode = true;
            }
            float x = 0;
            float y = 0;
            if (!player1 && isActive)
            {
                x = Input.GetAxis("Horizontal");
                y = Input.GetAxis("Vertical");
            }
            else if (isActive)
            {
                x = Input.GetAxis("Horizontal2");
                y = Input.GetAxis("Vertical2");

            }
            Vector2 vec = Vector2.zero;
            if (new Vector2(x, y).magnitude > 1)
            {
                vec = new Vector2(x, y).normalized;
            }
            else
            {
                vec = new Vector2(x, y);
            }
            rigidbody.velocity = vec * speed;

            if (!player1)
            {
                Dash(KeyCode.L);
                Shoot(KeyCode.K);
                if (Input.GetKey(KeyCode.Z) && specialBarSlider.value == 100)
                {
                    timerExceedSequenceP2 += Time.deltaTime;

                    sequenceKeysP2.text = combo2.mKeyListP2[0] + " " + combo2.mKeyListP2[1] + " " + combo2.mKeyListP2[2];
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                        combo2.mList.Add(KeyCode.DownArrow);

                    if (Input.GetKeyDown(KeyCode.RightArrow))
                        combo2.mList.Add(KeyCode.RightArrow);

                    if (Input.GetKeyDown(KeyCode.UpArrow))
                        combo2.mList.Add(KeyCode.UpArrow);

                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        combo2.mList.Add(KeyCode.LeftArrow);
                }
                if (timerExceedSequenceP2 > 2f)
                {
                    Debug.Log("Timer: " + timerExceedSequenceP2);
                    specialBarSlider.value = 0;
                    sequenceKeysP2.text = "";
                    combo2.mList.Clear();
                    timerExceedSequenceP2 = 0;
                }
                if (combo2.mList.Count > 0)
                {
                    if (combo2.CheckP2())
                    {
                        Debug.Log("COMBOU!");
                        combo2.mList.Clear();
                        sequenceKeysP2.text = "";
                        specialBarSlider.value = 0;
                        timerExceedSequenceP2 = 0;
                    }
                    else if ((combo2.mList.Count == 3 && !combo2.CheckP2()))
                    {
                        Debug.Log("Timer: " + timerExceedSequenceP2);
                        specialBarSlider.value = 0;
                        sequenceKeysP2.text = "";
                        combo2.mList.Clear();
                        timerExceedSequenceP2 = 0;
                    }
                }
            }
            else
            {
                Dash(KeyCode.G);
                Shoot(KeyCode.F);
                if (Input.GetKey(KeyCode.X) && specialBarSlider.value == 100)
                {
                    timerExceedSequenceP1 += Time.deltaTime;
                    sequenceKeysP1.text = combo1.mKeyListP1[0] + " " + combo1.mKeyListP1[1] + " " + combo1.mKeyListP1[2];
                    if (Input.GetKeyDown(KeyCode.S))
                        combo1.mList.Add(KeyCode.S);

                    if (Input.GetKeyDown(KeyCode.D))
                        combo1.mList.Add(KeyCode.D);

                    if (Input.GetKeyDown(KeyCode.W))
                        combo1.mList.Add(KeyCode.W);

                    if (Input.GetKeyDown(KeyCode.A))
                        combo1.mList.Add(KeyCode.A);
                }
                 if (timerExceedSequenceP1 > 2f)
                {
                    Debug.Log("Timer: " + timerExceedSequenceP1);
                    specialBarSlider.value = 0;
                    sequenceKeysP1.text = "";
                    combo1.mList.Clear();
                    timerExceedSequenceP1 = 0;
                }
                if (combo1.mList.Count > 0)
                {
                    if (combo1.CheckP1())
                    {
                        Debug.Log("COMBOU!");
                        combo1.mList.Clear();
                        specialBarSlider.value = 0;
                        sequenceKeysP1.text = "";
                        timerExceedSequenceP1 = 0;
                    }
                    else if (combo1.mList.Count == 3 && !combo1.CheckP1())
                    {
                        specialBarSlider.value = 0;
                        sequenceKeysP1.text = "";
                        combo1.mList.Clear();
                        timerExceedSequenceP1 = 0;
                    }
                }
            }
        }


    }
    void Shoot(KeyCode key)
    {
        if (laser)
        {
            if (Input.GetKeyDown(key))
            {
                if (specialBarSlider.value < 100)
                    specialBarSlider.value += 1;
                shootObject = Instantiate(shoot, this.transform.position, this.transform.rotation);
                shootObject.GetComponent<Laser>().Load(this);
                lastShoot = Time.time;
            }
            else if (Input.GetKeyUp(key))
            {
                Destroy(shootObject);
            }
        }
        else if (explode)
        {
            if (Input.GetKeyDown(key) && !exist)
            {
                if (specialBarSlider.value < 100)
                    specialBarSlider.value += 20;
                shootObject = Instantiate(shoot, this.transform.position, this.transform.rotation);
                shootObject.GetComponent<ExplodeShoot>().Load(this);
                lastShoot = Time.time;
                exist = true;
            }
            else if (Input.GetKeyDown(key) && shootObject)
            {
                shootObject.GetComponent<ExplodeShoot>().Explode();
            }
        }
        else
        {
            if (Input.GetKey(key) && Time.time - lastShoot > fireRate)
            {
                if (specialBarSlider.value < 100)
                    specialBarSlider.value += 10;
                Instantiate(shoot, this.transform.position, this.transform.rotation);
                lastShoot = Time.time;
            }
        }
    }

    private void Dash(KeyCode k)
    {

        switch (dashState)
        {
            case DashState.Ready:
                var isDashKeyDown = Input.GetKeyDown(k);
                if (isDashKeyDown)
                {
                    float a, b;
                    if (transform.forward == Vector3.forward)
                        a = 10;
                    else
                        a = -10;
                    if (transform.right == Vector3.right)
                        b = 10;
                    else
                        b = -10;
                    rigidbody.AddForce(new Vector2(rigidbody.velocity.x * b, rigidbody.velocity.y * a), ForceMode2D.Impulse);
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 2;
                if (dashTimer >= maxDashTimer)
                {

                    dashTimer = maxDashTimer;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }

    public void Die()
    {
        if (!invul)
        {
            isActive = false;
            sr.sprite = spriteDead;
            rigidbody.velocity = new Vector2(0, 0);
            currLifeRess = 0;

            //Destroy(gameObject);
        }
    }

    public void setRestoreLife()
    {
        currLifeRess += 1;
        Debug.Log(currLifeRess);
    }

    void OnDestroy()
    {
        foreach (var x in FindObjectsOfType<Laser>())
        {
            if (x.player == this)
            {
                Destroy(x.gameObject);
            }
        }
    }

}

