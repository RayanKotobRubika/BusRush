using UnityEngine;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-1)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [SerializeField] public LevelData Data;
    
    [SerializeField] private float _timeBeforeStart;

    private float _currentRate;
    private float _spawnTimer;
    private Vector3 _currentOdds;

    private int _busCounter = 0;

    public bool ReadyToPlay = false;
    private bool _started = false;
    private bool _countDownEnded = false;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this; 
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
        }

        if (!_countDownEnded)
        {
            _timeBeforeStart -= Time.deltaTime;
            
            if (_timeBeforeStart < 0)
                _countDownEnded = true;

            return;
        }
        
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

    public void ChangeRateAndOdds(Vehicle vehicle)
    {
        if (Data.PassengersSpawnRatePerBus[_busCounter] != 0)
            _currentRate = Data.PassengersSpawnRatePerBus[_busCounter];
        if (Data.PassengersColorRatesPerBus[_busCounter] != Vector3.zero)
            _currentOdds = Data.PassengersColorRatesPerBus[_busCounter];
        _busCounter++;
    }
}
