using UnityEngine;
using System.Collections;

public class CameraTransition : MonoBehaviour {
    public static CameraTransition Instance;
    [SerializeField] private float slideDuration = 0.4f;

    void Awake() {
        Instance = this;
    }

    public IEnumerator SlideToRoom(Vector3 targetPosition, System.Action onComplete = null) {
        Vector3 startPosition = transform.position;
        targetPosition.z = startPosition.z; // Maintain camera depth

        float timer = 0f;
        while (timer < slideDuration) {
            timer += Time.deltaTime;
            float t = timer / slideDuration;
            
            // SmoothStep makes the transition feel more polished
            t = t * t * (3f - 2f * t); 

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        
        transform.position = targetPosition;
        onComplete?.Invoke();
    }
}