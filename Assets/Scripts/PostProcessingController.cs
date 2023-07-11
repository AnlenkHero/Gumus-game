using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class PostProcessingController : MonoBehaviour
    {
        public PostProcessVolume volume;
        private Grain _grain;
        private LensDistortion _lensDistortion;
        private ChromaticAberration _chromaticAberration;
        private ColorGrading _colorGrading;
        private DepthOfField _depthOfField;

        [SerializeField]
        private float minIntensity = 0f;
        [SerializeField]
        private float maxIntensity = 1f;
        [SerializeField]
        private float lensMinIntensity = -80f;
        [SerializeField]
        private float lensMaxIntensity = 80f;
        [SerializeField]
        private float minSize = 0.3f;
        [SerializeField]
        private float maxSize = 3f;
        [SerializeField]
        private float minXMultiplier = 0f;
        [SerializeField]
        private float maxXMultiplier = 1f;
        [SerializeField]
        private float minYMultiplier = 0f;
        [SerializeField]
        private float maxYMultiplier = 1f;
        [SerializeField]
        private float minCenterX = 0f;
        [SerializeField]
        private float maxCenterX = 1f;
        [SerializeField]
        private float minCenterY = 0f;
        [SerializeField]
        private float maxCenterY = 1f;
        [SerializeField]
        private float minScale = 0.4f;
        [SerializeField]
        private float maxScale = 1.5f;
        [SerializeField]
        private float minDepthOfFieldFocalLength = 1f;
        [SerializeField]
        private float maxDepthOfFieldFocalLength = 300f;

        void Start()
        {
            if (volume == null)
            {
                Debug.LogError("PostProcessVolume is not assigned in the inspector.");
                return;
            }
            
            if (!volume.profile.TryGetSettings(out _grain)) Debug.LogError("Grain effect not found");
            if (!volume.profile.TryGetSettings(out _lensDistortion)) Debug.LogError("Lens Distortion effect not found");
            if (!volume.profile.TryGetSettings(out _chromaticAberration))
                Debug.LogError("Chromatic Aberration effect not found");
            if (!volume.profile.TryGetSettings(out _colorGrading))
                Debug.LogError("Color Grading effect not found");
            if (!volume.profile.TryGetSettings(out _depthOfField))
                Debug.LogError("Depth Of Field effect not found");
        }

        public  void RandomizeEffects()
        {

                _grain.active = true;// (Random.value > 0.5f);
                _lensDistortion.active = true;//(Random.value > 0.5f);
                _chromaticAberration.active =true;// (Random.value > 0.5f);
                _colorGrading.active = true;
                _depthOfField.active = true;

                if (_grain.active && _grain != null)
                {
                    RandomizeGrain();
                }

                if (_lensDistortion.active && _lensDistortion != null)
                {
                    RandomizeLensDistortion();
                }
                if (_colorGrading.active && _colorGrading != null)
                {
                    RandomizeColorGrading();
                }
                if (_depthOfField.active && _depthOfField != null)
                {
                    RandomizeDepthOfField();
                }
                
        }

        private void RandomizeGrain()
        {
            _grain.intensity.value = Random.Range(minIntensity, maxIntensity);
            _grain.size.value = Random.Range(minSize, maxSize);
        }

        private void RandomizeLensDistortion()
        {
            _lensDistortion.intensity.value = Random.Range(lensMinIntensity, lensMaxIntensity);
            _lensDistortion.intensityX.value = Random.Range(minXMultiplier, maxXMultiplier);
            _lensDistortion.intensityY.value = Random.Range(minYMultiplier, maxYMultiplier);
            _lensDistortion.centerX.value = Random.Range(minCenterX, maxCenterX);
            _lensDistortion.centerY.value = Random.Range(minCenterY, maxCenterY);
            _lensDistortion.scale.value = Random.Range(minScale, maxScale);
        }

        private void RandomizeColorGrading()
        {
            _colorGrading.gain.value = new Vector4(Random.Range(-1f, 2f), Random.Range(-1f, 2f), 
                Random.Range(-1f, 2f), Random.Range(-1f, 2f));
        }

        private void RandomizeDepthOfField()
        {
            _depthOfField.focalLength.value =
                Random.Range(minDepthOfFieldFocalLength, maxDepthOfFieldFocalLength);
        }

        public void DeactivatePostProcessingEffect()
        {
            _grain.active = false;
            _lensDistortion.active = false;
            _chromaticAberration.active =false;
            _colorGrading.active = false;
            _depthOfField.active = false;
        }
    }
}