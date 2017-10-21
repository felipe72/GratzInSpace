using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : MonoBehaviour
{

    bool isDead = false;
    public GameObject shoot;
    private GameObject shootObject;
    //public float fireRate = 10f;
    float timeStop = 0f;

    float maxTimeStop = 1f;
    private float lastShoot = 0f;
    private float shootingCD =0.5f;
    bool stop = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Shoot();
            if (!stop)
            {
                CheckDistance();
                Vector2 nextPosition = new Vector2(this.transform.position.x - 0.05f, this.transform.position.y);
                this.transform.position = nextPosition;
            }
            else
            {
                timeStop += Time.deltaTime;
            }
            if (timeStop >= maxTimeStop)
            {
                timeStop -= 2;
                if (timeStop <= 0)
                {
                    timeStop = 0;
                    stop = false;
                }
            }
            if (this.transform.position.x < -15f)
                {
                    Destroy(gameObject);
                }
        }
    }

    void Shoot()
    {
        Debug.Log(Time.deltaTime - lastShoot);
        if (Time.time - lastShoot > shootingCD)
        {
            Debug.Log("ATIRANDO");
            shootObject = Instantiate(shoot, this.transform.position, this.transform.rotation);
            lastShoot = Time.time;
            if(shootObject.transform.position.x < -15f)
                Destroy(shootObject.gameObject);
            //shootObject.GetComponent<BasicEnemyShootController>().Load(this);
            //lastShoot += Time.deltaTime;
        }
        

    }
    private void CheckDistance()
    {
        Vector3 pos = this.transform.position;
        if (pos.x <= 6f && pos.x >= 5.99f)
            stop = true;
    }

    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
