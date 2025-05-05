using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskNPCObject : InteractableObject

{
    public string npcName;
    public GameTaskSO gameTaskSO;
    public string[] contentInTaskExecuting;
    public string[] contentInTaskCompleted;

    public string[] contentInTaskEnd;
    private void Start()
    {
        gameTaskSO.state = GameTaskState.Wating;
    }

    protected override void Interact()
    {
        print("你是来帮忙的吗");
        switch (gameTaskSO.state)
        {
            case GameTaskState.Wating:
                DialogueUI.Instance.Show(npcName, gameTaskSO.diague, OnDiagueEnd);
                break;
            case GameTaskState.Executing:
                DialogueUI.Instance.Show(npcName, contentInTaskExecuting, OnDiagueEnd);
                break;
            case GameTaskState.Completed:
                DialogueUI.Instance.Show(npcName, contentInTaskCompleted, OnDiagueEnd);
                break;
            case GameTaskState.End:
                DialogueUI.Instance.Show(npcName, contentInTaskEnd, OnDiagueEnd);
                break;
        }

    }
    public void OnDiagueEnd()
    {
        print("get startReward");

        switch (gameTaskSO.state)
        {
            case GameTaskState.Wating:

                //gameTaskSO.state = GameTaskState.Executing;
                gameTaskSO.Start();
                InventoryManager.Instance.AddItem(gameTaskSO.startReward);
                MessageUI.Instance.Show("你接受了一个任务");
                break;
            case GameTaskState.Executing:
                break;
            case GameTaskState.Completed:
                gameTaskSO.End();
                InventoryManager.Instance.AddItem(gameTaskSO.endReward);
                MessageUI.Instance.Show("任务已提交");
                break;
            case GameTaskState.End:
                break;
        }
    }
}
