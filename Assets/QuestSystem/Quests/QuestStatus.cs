using QuestSystem.Tasks;

namespace QuestSystem.Quests {
  
  /// <summary>Represents the status of a Quest's todo list.</summary>
  public struct QuestStatus {
    public readonly TaskStatus[] Tasks;

    public QuestStatus(TaskStatus[] tasks) {
      Tasks = tasks;
    }
  }

}