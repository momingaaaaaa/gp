using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTaskState
{
    Wating,
    Executing,
    Completed,
    End

}
[CreateAssetMenu()]
public class GameTaskSO : ScriptableObject
{
    public GameTaskState state;
    public string[] diague;

    public ItemSO startReward;
    public ItemSO endReward;
    public int enemyCount = 10;
    public int currentEnemyCount = 0;
    public void Start()
    {
        currentEnemyCount = 0;
        state = GameTaskState.Executing;
        EventCenter.OnEnemyDied += OnEnemyDied;

    }
    private void OnEnemyDied(Enemy enemy)
    {
        if (state == GameTaskState.Completed)
        {
            return;
        }
        currentEnemyCount++;
        if (currentEnemyCount >= enemyCount)
        {
            state = GameTaskState.Completed;
            MessageUI.Instance.Show("已完成任务目标");
        }
    }
    public void End()
    {
        state = GameTaskState.End;
        EventCenter.OnEnemyDied -= OnEnemyDied;
    }
}
