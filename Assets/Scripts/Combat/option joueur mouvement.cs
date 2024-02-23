using System;
using UnityEngine;

public class optionjoueurmouvement : MonoBehaviour
{
    const float MIN_SIZE = 0.1f;
    const float MAX_SIZE = 0.5f;
    //Transform parent_transform;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        // https://www.desmos.com/calculator/fykzf0shnv?lang=fr fonction sigmoid
        transform.localScale = Vector3.one * (1 / (1 + Mathf.Pow(MathF.E, (20 - transform.position.x - 15) * -0.2f)));

        //if (Random.Range(0, 100) == 0)
        //    Debug.Log(transform.position.x);
    }

    // Update is called once per frame
    public void ChangeSize(bool big)
    {
        float scale;
        if (big)
            scale = MAX_SIZE;
        else
            scale = MIN_SIZE;
        transform.localScale = Vector3.one * scale;
    }
}
