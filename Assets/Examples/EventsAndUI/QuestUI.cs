using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using QuestSystem;
using QuestSystem.Quests;
using QuestSystem.Tasks;

public class QuestUI : MonoBehaviour {

  [SerializeField] private TMPro.TextMeshProUGUI text;

  void Update() {
    if (!DemoQuestManager.Instance.HasActiveQuest) {
      text.text = "No active quest.";
      return;
    }
    QuestStatus status = DemoQuestManager.Instance.ActiveQuest.Status();

    StringBuilder questStatus = new StringBuilder();

    foreach (TaskStatus task in status.Tasks) {
      questStatus.AppendFormat("{0}: {1} of {2}\n", task.DisplayName, task.Count, task.Total);
      // questStatus.AppendFormat("{0}: {1:0.#}%\n", task.DisplayName, task.PercentComplete);
    }
    text.text = questStatus.ToString();
  }

}
