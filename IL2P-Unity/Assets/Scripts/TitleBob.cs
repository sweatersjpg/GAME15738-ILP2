using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBob : MonoBehaviour
{

    public float min;
    public float max;
    public float duration;

    public AnimationCurve heightCurve;

    float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        float height = Mathf.Lerp(min, max, heightCurve.Evaluate(timeElapsed/duration));
        transform.transform.transform.transform.transform.localPosition = new Vector2(0, height);

    }
}
