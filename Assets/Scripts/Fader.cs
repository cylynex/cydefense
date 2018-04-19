using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour {
    
    public Image image;
    public float speedValue = 0.5f;
    public AnimationCurve animCurve;

    void Start() {
        StartCoroutine(FadeIn());
    }


    public void FadeTo(string scene) {
        StartCoroutine(FadeOut(scene));
    }
                               
    IEnumerator FadeIn() {
        float alphaVal = 1f;

        while (alphaVal > 0f) {
            alphaVal -= Time.deltaTime * speedValue;
            float curveVal = animCurve.Evaluate(alphaVal);
            image.color = new Color(0f, 0f, 0f, alphaVal);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene) {
        float alphaVal = 0f;

        while (alphaVal < 1f) {
            alphaVal += Time.deltaTime * speedValue;
            float curveVal = animCurve.Evaluate(alphaVal);
            image.color = new Color(0f, 0f, 0f, alphaVal);
            yield return 0;
        }

        // Load the scene now
        SceneManager.LoadScene(scene);
    }

}