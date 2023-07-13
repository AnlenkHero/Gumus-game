using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    [SerializeField]
    private float timerDuration = 3f * 60f; //Duration of the timer in seconds
    [SerializeField]
    private bool countDown = true;
    private float _timer;
    private bool _isCounting = true;
    public TextMeshProUGUI TimerValue => timerText;
    /*[SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI separator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;*/

    //Use this for a single text object
    [SerializeField]
    private TextMeshProUGUI timerText;

    private float _flashTimer;
    [SerializeField]
    private float flashDuration = 1f; //The full length of the flash

    private void OnEnable()
    {
        PlayerController.OnGameOver += StopCount;
    }

    private void OnDisable()
    {
        PlayerController.OnGameOver -= StopCount;
    }

    private void Start() {
        ResetTimer();
    }

    private void ResetTimer() {
        if (countDown) {
            _timer = timerDuration;
        } else {
            _timer = 0;
        }
        SetTextDisplay(true);
    }

    void Update() 
    {
        if (_isCounting)
        {
            if (countDown && _timer > 0)
            {
                _timer -= Time.deltaTime;
                UpdateTimerDisplay(_timer);
            }
            else if (!countDown && _timer < timerDuration)
            {
                _timer += Time.deltaTime;
                UpdateTimerDisplay(_timer);
            }
        }

    }

    private void StopCount()
    {
        _isCounting = false;
    }

    private void UpdateTimerDisplay(float time) {
        if (time < 0) {
            time = 0;
        }

        if (time > 3660) {
            Debug.LogError("Timer cannot display values above 3660 seconds");
            ErrorDisplay();
            return;
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60); 

        string currentTime = string.Format("{00:00:}{01:00}", minutes, seconds);
    /*    firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();*/

        //Use this for a single text object
        timerText.text = currentTime.ToString();
    }

    private void ErrorDisplay() {
     /*   firstMinute.text = "8";
        secondMinute.text = "8";
        firstSecond.text = "8";
        secondSecond.text = "8";*/


        //Use this for a single text object
        timerText.text = "88:88";
    }

    private void FlashTimer() {
        if(countDown && _timer != 0) {
            _timer = 0;
            UpdateTimerDisplay(_timer);
        }

        if(!countDown && _timer != timerDuration) {
            _timer = timerDuration;
            UpdateTimerDisplay(_timer);
        }

        if(_flashTimer <= 0) {
            _flashTimer = flashDuration;
        } else if (_flashTimer <= flashDuration / 2) {
            _flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
        } else {
            _flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        }
    }

    private void SetTextDisplay(bool enabled) {
      /*  firstMinute.enabled = enabled;
        secondMinute.enabled = enabled;
        separator.enabled = enabled;
        firstSecond.enabled = enabled;
        secondSecond.enabled = enabled;*/

        //Use this for a single text object
        timerText.enabled = enabled;
    }
}