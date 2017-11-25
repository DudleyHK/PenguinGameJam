using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour 
{
    public enum FadeState
    {
        Off,
        FadeIn,
        FadeOut
    }
    public FadeState fadeState;
    public float alpha = 0;
    public  BoatManager boatManager;
    private Animator boatAnimator;


    private void Start()
    {
        boatAnimator = boatManager.GetComponent<boatAnimator>();
    }



    private void Update()
    {
        boatAnimator.SetInteger("AnimState", alpha);
    }

}
