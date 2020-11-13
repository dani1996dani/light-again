using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Fade
{
    class FadeInOnStart : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        float smoothTime = Settings.TimeTillMoonDustCollectionTriggerIsActive;
        float velocity = 0.0f;
        float alpha;
        float alphaTarget = 1;
        float acceptableOffest = 0.1f;

        private void Start()
        {
            spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            SetAlpha(0);
        }

        private void Update()
        {
            alpha = Mathf.SmoothDamp(alpha, alphaTarget, ref velocity, smoothTime);
            Debug.Log("alpha " + alpha);
            SetAlpha(alpha);
            if(alphaTarget - acceptableOffest <= alpha)
            {
                SetAlpha(alphaTarget);
                this.enabled = false;
            }
        }

        private void SetAlpha(float newAlpha)
        {
            Color color = spriteRenderer.color;
            color.a = newAlpha;
            spriteRenderer.color = color;
        }
    }
}
