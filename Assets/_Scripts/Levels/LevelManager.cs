using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-1)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    public LevelData Data { get; private set; }

    [SerializeField] private TextMeshProUGUI _levelNumberText;
    
    [SerializeField] private int _timeBeforeStart = 5;
    private int _timerIntCounter;
    private float _timerBeforeStart;

    [SerializeField] private GameObject _countdownPanel;
    [SerializeField] private TextMeshProUGUI _countdownText;
    [SerializeField] private float _slideDuration;
    [SerializeField] private float _bouncyScale;
    [SerializeField] private float _bounceDuration;

    private float _currentRate;
    private float _spawnTimer;
    private Vector3 _currentOdds;

    private List<Vector3> _enemiesTutorialSpawnData;

    private int _busCounter = 0;

    [HideInInspector] public bool ReadyToPlay = false;
    private bool _started = false;
    public bool CountDownEnded { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this;

        _timerIntCounter = _timeBeforeStart;

        Data = SceneHandler.Instance.GetCurrentLevelData();
    }

    private void Start()
    {
        _levelNumberText.text = Data.LevelIndex == 0 ? "TUTORIAL" : "Level " + Data.LevelIndex;
    }

    private void Update()
    {
        if (!ReadyToPlay) return;

        if (ReadyToPlay && !_started)
        {
            _started = true;
            
            _currentRate = Data.PassengersSpawnRatePerBus[_busCounter];
            _currentOdds = Data.PassengersColorRatesPerBus[_busCounter];

            _spawnTimer = 1 / _currentRate;

            if (Data.IsTutorial)
            {
                _spawnTimer = 0;
                _enemiesTutorialSpawnData = new List<Vector3>(Data.PassengersColorRatesPerBus);
            }
        }

        if (!CountDownEnded)
        {
            HandleCountdown();
            return;
        }

        if (Data.IsTutorial)
            TutorialLogic();
        else
            RegularLogic();
        
    }

    private void RegularLogic()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer > 0) return;

        _spawnTimer = 1 / _currentRate;
        
        float randomValue = Random.Range(0f, 100f);
        PassengerColor color;

        if (randomValue < _currentOdds.x)
            color = PassengerColor.Red;
        else if (randomValue < _currentOdds.x + _currentOdds.y)
            color = PassengerColor.Green;
        else
            color = PassengerColor.Blue;
        
        PassengerManager.Instance.SpawnPassenger(LaneManager.Instance.Lanes[0], color);
    }

    private void TutorialLogic()
    {
        if (_enemiesTutorialSpawnData.Count <= 0) return;
        
        _spawnTimer += Time.deltaTime;

        for (int index = _enemiesTutorialSpawnData.Count - 1; index >= 0; index--)
        {
            Vector3 data = _enemiesTutorialSpawnData[index];

            if (data.x > _spawnTimer) continue;

            PassengerManager.Instance.SpawnPassenger(LaneManager.Instance.Lanes[0], (PassengerColor)(int)data.y, (int)data.z);

            _enemiesTutorialSpawnData.RemoveAt(index);
        }
    }

    private void HandleCountdown()
    {
        if (_timerIntCounter == _timeBeforeStart) 
            SlideCountdownText();

        if (_timerIntCounter > 0)
        {
            _timerBeforeStart -= Time.deltaTime;

            if (!(_timerBeforeStart <= 0)) return;
                
            _timerIntCounter--;
            _timerBeforeStart++;

            if (_timerIntCounter > 4) return;
                
            StartCoroutine(CoroutineUtils.BouncyScale(_countdownText.rectTransform, _bouncyScale, _bounceDuration, true));
            _countdownText.text = _timerIntCounter <= 1 ? "GO !" : (_timerIntCounter - 1).ToString();

            return;
        }

        _countdownPanel.SetActive(false);
        CountDownEnded = true;

        return;
    }

    public void ChangeRateAndOdds(Vehicle vehicle)
    {
        if (Data.PassengersSpawnRatePerBus[_busCounter] != 0)
            _currentRate = Data.PassengersSpawnRatePerBus[_busCounter];
        if (Data.PassengersColorRatesPerBus[_busCounter] != Vector3.zero)
            _currentOdds = Data.PassengersColorRatesPerBus[_busCounter];
        _busCounter++;
    }

    public void SlideCountdownText()
    {
        StartCoroutine(CoroutineUtils.EaseInOutMoveUI(_countdownText.rectTransform, Vector2.zero, _slideDuration));
    }
}
