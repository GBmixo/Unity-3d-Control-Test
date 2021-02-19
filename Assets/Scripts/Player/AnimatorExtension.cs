using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public static bool isPlayingOnLayer(this Animator animator, int fullPathHash, int layer) 
    {
        return animator.GetCurrentAnimatorStateInfo(layer).fullPathHash == fullPathHash;


    }

    public static double normalizedTime(this Animator animator, System.Int32 layer) 
    {
        double time = animator.GetCurrentAnimatorStateInfo(layer).normalizedTime;
        return time > 1 ? 1 : time;
    }
}
