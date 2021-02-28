using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    bool airborn;
    bool stunned;

    float pVely;

    Transform arrow;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        arrow = transform.Find("arrow");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 dir = mousePosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        arrow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        arrow.gameObject.SetActive(!airborn && rb.velocity.x==0);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y == 0 && pVely == 0)
        {
            airborn = false;
            stunned = false;
        }
        else
        {
            airborn = true;
        }

        if (rb.velocity.x < 0) sr.flipX = true;
        if (rb.velocity.x > 0) sr.flipX = false;

        anim.SetBool("Airborn", airborn);
        anim.SetBool("Stunned", stunned);
        anim.SetFloat("YVelocity", rb.velocity.y);

        pVely = rb.velocity.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (airborn)
        {
            stunned = true;
        }
    }

    private void Launch(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
