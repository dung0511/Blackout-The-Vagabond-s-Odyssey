using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Splines.Interpolators;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public List<GameObject> runes;
        public float lerpSpeed;

        private Color curColor, targetColor;
        private float curLight, targetLight;
        private List<SpriteRenderer> runeSprite =new(); 
        private List<Light2D> runeLight=new();

        private void Awake()
        {
            foreach(var rune in runes)
            {
                runeSprite.Add(rune.GetComponent<SpriteRenderer>()); 
                runeLight.Add(rune.GetComponent<Light2D>());
            }
            targetColor = runeSprite[0].color;
            

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            targetColor.a = 1.0f;
            targetLight = 1f;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            targetColor.a = 0.0f;
            targetLight = 0f;
        }

        private void Update()
        {
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);
            curLight = Mathf.Lerp(curLight, targetLight, lerpSpeed * Time.deltaTime);
            for(int i=0; i< runes.Count; i++)
            {
                runeSprite[i].color = curColor;
                runeLight[i].intensity = curLight;
            }
        }
    }
}
