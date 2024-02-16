using UnityEngine;
using System;
using System.Collections;

namespace TyeUtils
{
    public class TweenUtils
    {
        // Accessible Variables
        public event Action TweenFinished;
        public bool isTweening;
        public float tweenProgress;

        AnimationCurve defaultCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        Coroutine currentTweenCoroutine;
        Coroutine currentTweenCoroutine2;
        Coroutine currentTweenCoroutine3;

        public void StartFloatTween(MonoBehaviour monoBehaviour, float initialValue, float targetValue, Action<float> setProperty, float tweenTime = 1f, AnimationCurve tweenCurve = null)
        {
            // Stop tween if already tweening
            if (currentTweenCoroutine != null) StopTween(monoBehaviour, currentTweenCoroutine);

            // Establish default curve
            if (tweenCurve == null) tweenCurve = defaultCurve;

            currentTweenCoroutine = monoBehaviour.StartCoroutine(FloatTweenCoroutine(initialValue, targetValue, setProperty, tweenTime, tweenCurve));
        }

        public void StartVector3Tween(MonoBehaviour monoBehaviour, Vector3 initialValue, Vector3 targetValue, Action<Vector3> setProperty, float tweenTime = 1f, AnimationCurve tweenCurve = null)
        {
            //Stop tween if already tweening
            if (currentTweenCoroutine != null) StopTween(monoBehaviour, currentTweenCoroutine);
            if (currentTweenCoroutine2 != null) StopTween(monoBehaviour, currentTweenCoroutine2);
            if (currentTweenCoroutine3 != null) StopTween(monoBehaviour, currentTweenCoroutine3);

            //Establish default curve
            if (tweenCurve == null) tweenCurve = defaultCurve;

            //Create temp callbacks for each value
            Action<float> setX = (x) => { initialValue.x = x; };
            Action<float> setY = (y) => { initialValue.y = y; };
            Action<float> setZ = (z) => { initialValue.z = z; setProperty(initialValue); };

            //Start 3 tweens for each value
            currentTweenCoroutine = monoBehaviour.StartCoroutine(FloatTweenCoroutine(initialValue.x, targetValue.x, setX, tweenTime, tweenCurve));
            currentTweenCoroutine2 = monoBehaviour.StartCoroutine(FloatTweenCoroutine(initialValue.y, targetValue.y, setY, tweenTime, tweenCurve));
            currentTweenCoroutine3 = monoBehaviour.StartCoroutine(FloatTweenCoroutine(initialValue.z, targetValue.z, setZ, tweenTime, tweenCurve));
        }

        public void StopAllTweens(MonoBehaviour monoBehaviour)
        {
            StopTween(monoBehaviour, currentTweenCoroutine);
            StopTween(monoBehaviour, currentTweenCoroutine2);
            StopTween(monoBehaviour, currentTweenCoroutine3);
        }

        void StopTween(MonoBehaviour monoBehaviour, Coroutine current)
        {
            if (currentTweenCoroutine != null)
            {
                monoBehaviour.StopCoroutine(current);
                currentTweenCoroutine = null;
                isTweening = false;
            }
        }

        IEnumerator FloatTweenCoroutine(float initialValue, float targetValue, Action<float> setProperty, float tweenTimeMax, AnimationCurve tweenCurve)
        {
            isTweening = true;
            float elapsedTime = 0;

            while (elapsedTime < tweenTimeMax)
            {
                tweenProgress = Mathf.Clamp01(elapsedTime / tweenTimeMax);
                float currentValue = Mathf.Lerp(initialValue, targetValue, tweenCurve.Evaluate(tweenProgress));
                setProperty(currentValue);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            isTweening = false;
            TweenFinished?.Invoke();
        }
    }
}
