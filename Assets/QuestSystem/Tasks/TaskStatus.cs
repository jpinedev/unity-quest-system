
namespace QuestSystem.Tasks {

  /// <summary>Represents the status of a todo item.</summary>
  public struct TaskStatus {
    public readonly string DisplayName;
    public readonly int Count;
    public readonly int Total;
    public readonly float PercentComplete;

    public TaskStatus(string displayName, int count, int total, float percentComplete) {
      DisplayName = displayName;
      Count = count;
      Total = total;
      PercentComplete = percentComplete;
    }
  }

}