using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    public float MinX;
    public float MaxX;

    [SerializeField] GameObject BulletPrefab;
    [SerializeField] uint Health;

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 moveDir = Vector3.zero;

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > MinX)
        {
            moveDir = Vector3.left;
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < MaxX)
        {
            moveDir = Vector3.right;
        }

        if ((Input.GetKeyDown("space")))
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y + 0.2f, 0.0f);
            Instantiate(BulletPrefab, position, Quaternion.identity);
        }

        transform.Translate (moveDir * MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy Bullet")
        {
            Debug.Log("Player Hit");
            Health -= 1;
            if (0 == Health)
            {
                Debug.Log("Player Dead");
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
