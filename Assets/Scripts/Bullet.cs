using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] float Speed;
    [SerializeField] float MinY;
    [SerializeField] float MaxY;
    [SerializeField] string Tag;
    
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
        if (MaxY < transform.position.y || MinY > transform.position.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Tag == other.tag)
        {
            Destroy(gameObject);
        }
    }
}
