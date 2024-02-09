using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class optionjoueurmouvement : MonoBehaviour
{
    const float MIN_SIZE = 0.1f;
    const float MAX_SIZE = 0.5f;
    Transform parent_transform;
    // Start is called before the first frame update
    void Start()
    {
        parent_transform = GetComponentInParent<Transform>();
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
