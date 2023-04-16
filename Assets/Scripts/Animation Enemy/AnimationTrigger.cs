using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    Animator anim;
    public bool offRadar;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.name == "Tampak Samping E")
        {
            AnimasiDuduk();
        }

        if(this.gameObject.name == "Tampak Samping E (1)")
        {
            AnimasiNgobrol();
        }

        if(this.gameObject.tag == "P1")
        {
            if(offRadar)
            {
                anim.SetTrigger("off");
            }
            else if(!offRadar)
            {
                anim.SetTrigger("on");
            }
        }

        if(transform.parent.name == "Prajurit 2")
        {
            anim.SetTrigger("idle2");
        }

        if(transform.parent.name == "Prajurit 1")
        {
            anim.SetTrigger("idle1");
        }
    }

    void AnimasiDuduk()
    {
        anim.SetTrigger("duduk");
    }

    void AnimasiNgobrol()
    {
        anim.SetTrigger("ngobrol");
    }
}
