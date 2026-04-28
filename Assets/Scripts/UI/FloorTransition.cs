using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FloorTransition : MonoBehaviour {
    public static FloorTransition Instance;
    [SerializeField] private Image fadeImage; // Drag a black UI Image here
    [SerializeField] private float fadeSpeed = 1f;

    void Awake() {
        Instance = this;
    }

    public IEnumerator FadeOut() {
        float alpha = 0;
        while (alpha < 1) {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    public IEnumerator FadeIn() {
        float alpha = 1;
        while (alpha > 0) {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0);
    }
}