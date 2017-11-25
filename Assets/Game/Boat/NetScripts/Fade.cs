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
    public float fadeDownSpeed = 0.5f;
    public float fadeUpSpeed = 0.25f;
    public  BoatManager boatManager;
    public SpriteRenderer netRenderer;
    private Animator boatAnimator;



    private void Start()
    {
        boatAnimator = boatManager.GetComponent<Animator>();
        alpha = netRenderer.color.a;
    }



    private void Update()
    { 
       // var colour = netRenderer.material.color;
       var colour = netRenderer.color;
        switch(fadeState)
        {
            case FadeState.Off:
                alpha = 0f;
                break;
            case FadeState.FadeIn:
                alpha = Mathf.Lerp(alpha, 1, fadeDownSpeed * Time.deltaTime);
                break;
            case FadeState.FadeOut:
                alpha = Mathf.Lerp(alpha, 0, fadeUpSpeed * Time.deltaTime);
                break;
        }
        colour = new Color(colour.r, colour.g, colour.b, alpha);
        netRenderer.color = colour;

        boatAnimator.SetFloat("Alpha", alpha);
    }

}
