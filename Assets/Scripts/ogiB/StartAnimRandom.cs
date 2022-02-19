using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimRandom : MonoBehaviour
{
    [SerializeField] private Animator anim;
    void Start()
    {
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, -1, Random.Range(0, 4f));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
