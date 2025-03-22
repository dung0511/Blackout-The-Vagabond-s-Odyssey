using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Cainos.PixelArtTopDown_Basic
{
    //animate the sprite color base on the gradient and time
    public class SpriteColorAnimation : MonoBehaviour
    {
        public Gradient gradient;
        public float time;
        public bool lightOnlyAlpha = false;

        private SpriteRenderer sr;
        private Light2D l;
        private float timer;

        private void Start()
        {
            timer = time * Random.value;
            sr = GetComponent<SpriteRenderer>();
            l = GetComponent<Light2D>();
        }

        private void Update()
        {
            if (sr)
            {
                timer += Time.deltaTime;
                if (timer > time) timer = 0.0f;

                sr.color = gradient.Evaluate(timer / time);
                if (lightOnlyAlpha) 
                    l.color = new Color(l.color.r, l.color.g, l.color.b, sr.color.a); 
                else
                    l.color = sr.color;
            }
        }
    }
}
