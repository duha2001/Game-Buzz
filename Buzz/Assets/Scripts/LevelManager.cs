using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public Player Player { get; private set; }
    public CameraController Camera { get; private set; }
    public TimeSpan RunningTime { get { return DateTime.UtcNow - _started; } }

    public int CurrentTimeBonus
    {
        get
        {
            var secondDifference = (int)(BonusCutoffSeconds - RunningTime.TotalSeconds);
            return Mathf.Max(0, secondDifference) * BonusSecondMultipler;
        }
    }

    private List<Checkpoint> _checkpoints;
    private int _currentCheckpointIndex;
    private DateTime _started;
    private int _savePoinnts;

    public Checkpoint DebugSpawn;
    public int BonusCutoffSeconds;
    public int BonusSecondMultipler;

    public void Awake()
    {
        _savePoinnts = GameManager.Instance.Points;
        Instance = this;
    }

    public void Start() // xuat hien nhan vat tai diem checkpoint(0)
    {
        _checkpoints = FindObjectsOfType<Checkpoint>().OrderBy(t => t.transform.position.x).ToList();
        _currentCheckpointIndex = _checkpoints.Count > 0 ? 0 : -1;

        Player = FindObjectOfType<Player>();
        Camera = FindObjectOfType<CameraController>();


        _started = DateTime.UtcNow;

        var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerRespawnListener>();
        foreach (var listener in listeners)
        {
            for (var i = _checkpoints.Count - 1; i >= 0; i--)
            {
                var distance = ((MonoBehaviour)listener).transform.position.x - _checkpoints[i].transform.position.x;
                if (distance < 0)
                    continue;

                _checkpoints[i].AssignObjectToCheckpoint(listener);
                break;
            }
        }

#if UNITY_EDITOR
        if (DebugSpawn != null) // xuat hien nhan vat tai diem checkpoint(0)
            DebugSpawn.SpawnPlayer(Player);
        else if(_currentCheckpointIndex != -1)
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
#else                                                     // su dung doc lap
        if(_currentCheckpointIndex != -1)
           _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
#endif

    }

    public void Update()
    {
        var isAtLastCheckpoint = _currentCheckpointIndex + 1 >= _checkpoints.Count;

        if (isAtLastCheckpoint)
            return;

        var distanceToNextCheckpoint = _checkpoints[_currentCheckpointIndex + 1].transform.position.x - Player.transform.position.x;
        if (distanceToNextCheckpoint >= 0)
            return;

        _checkpoints[_currentCheckpointIndex].PlayerLeftCheckpoint();
        _currentCheckpointIndex++;
        _checkpoints[_currentCheckpointIndex].playerHitCheckpoint();

        //time bonus
        GameManager.Instance.AddPoints(CurrentTimeBonus);
        _savePoinnts = GameManager.Instance.Points;
        _started = DateTime.UtcNow;

        //var listener = FindObjectsOfType<IPlayerRespawnListener>();

        ////var listeners = FindObjectsOfType<MonoBehaviour>().OfType<IPlayerRespawnListener>();
        ////foreach(var listener in listeners)
        ////{
        ////    for(var i = _checkpoints.Count - 1; i >= 0; i--)
        ////    {
        ////        var distance = ((MonoBehaviour)listener).transform.position.x - _checkpoints[i].transform.position.x;
        ////        if(distance < 0)
        ////            continue;

        ////        _checkpoints[i].AssignObjectToCheckpoint(listener);
        ////        break;
        ////    }
        ////}
    }

    public void GoToNextLevel(string levelName)
    {
        StartCoroutine(GoToNextLevelCo(levelName));
    }

    private IEnumerator GoToNextLevelCo(string levelName)
    {
        Player.FinishLevel();
        GameManager.Instance.AddPoints(CurrentTimeBonus);

        FloatingText.Show("Level Complete", "CheckPointText", new CenterTextPositioner(.2f));
        yield return new WaitForSeconds(1);

        FloatingText.Show(string.Format("{0} POINT", GameManager.Instance.Points), "CheckPointText", new CenterTextPositioner(.1f));
        yield return new WaitForSeconds(2f);

        if (string.IsNullOrEmpty(levelName))
            Application.LoadLevel("StartScreen");
        else
            Application.LoadLevel(levelName);
    }

    public void KillPlayer()
    {
        StartCoroutine(KillPlayerCo());
    }

    private IEnumerator KillPlayerCo()
    {
        Player.Kill();
        Camera.IsFollowing = false;
        yield return new WaitForSeconds(2f);

        Camera.IsFollowing = true;

        if(_currentCheckpointIndex != -1)
        {
            _checkpoints[_currentCheckpointIndex].SpawnPlayer(Player);
        }

        //points
        _started = DateTime.UtcNow;
        GameManager.Instance.ResetPoints(_savePoinnts);
    }

}
