using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float TargetY;
    public float MoveSpeed;
    public float MinX;
    public float MaxX;

    bool movingLeft;

    [SerializeField] GameObject BulletPrefab;
    [SerializeField] float BulletDelay;
    [SerializeField] uint Health;
    
    private float BulletTimeEllapse;

    private void Start()
    {
        // Randomly start moving either left or right
        movingLeft = Random.Range (0.0f, 1.0f) < 0.5f;
    }

    private void Update()
    {
        if (transform.position.y > TargetY)
        {
            // Enemy starts offscreen, so move down until it's in the right place
            transform.position -= Vector3.up * Time.deltaTime * MoveSpeed;

            BulletTimeEllapse = 0.0f;
            return;
        }
        else
        {
            BulletTimeEllapse += Time.deltaTime;

            if (BulletTimeEllapse >= BulletDelay)
            {
                Vector3 position = new Vector3(transform.position.x, transform.position.y - 0.2f, 0.0f);
                Instantiate(BulletPrefab, position, Quaternion.identity);
                BulletTimeEllapse = 0.0f;
            }
        }

        // Move
        Vector3 movementDirection = movingLeft ? Vector3.left : Vector3.right;
        transform.position += movementDirection * Time.deltaTime * MoveSpeed;

        // If moved to the left/right all the way, turn around
        if (transform.position.x < MinX || transform.position.x > MaxX)
        {
            movingLeft = !movingLeft;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player Bullet")
        {
            Debug.Log("Enemy Hit");
            Health -= 1;
            if (0 == Health)
            {
                Debug.Log("Enemy Dead");
                Destroy(gameObject);
                return;
            }

            IEnumerator coroutine = Hit();
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator Hit()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color32(243, 107, 107, 255);
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
    }
}
