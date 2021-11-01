using System.Collections.Generic;

using QuestSystem.Tasks;

namespace QuestSystem.Quests {

  /// <summary>Represents a todo list.</summary>
  public class Quest<T> where T : QuestAction {
    private bool active;
    private ATask<T>[] tasks;

    private Quest(ATask<T>[] tasks) {
      this.tasks = tasks;
    }

    /// <summary>Allow progress to be made for the todo list.</summary>
    public void Activate() {
      foreach (ATask<T> task in tasks) {
        task.Activate();
      }
    }
    /// <summary>Halt progress for the todo list.</summary>
    public void Deactivate() {
      foreach (ATask<T> task in tasks) {
        task.Deactivate();
      }
    }

    /// <returns>Is the todo list completed?</returns>
    public bool IsComplete() {
      foreach (ATask<T> task in tasks) {
        if (!task.IsComplete()) return false;
      }
      return true;
    }
    /// <returns>Percent completion of the todo list.</returns>
    public float PercentComplete() {
      float percent = 0;
      foreach (ATask<T> task in tasks) {
        percent += task.PercentComplete();
      }
      return percent / (float) tasks.Length;
    }

    /// <returns>The number of todo item in the list.</returns>
    public int TaskCount() => tasks.Length;
    /// <returns>The number of todo items in the list that are completed.</returns>
    public int TasksComplete() {
      int count = 0;
      foreach (ATask<T> task in tasks) {
        if (task.IsComplete()) count++;
      }
      return count;
    }
    /// <returns>The status of the todo list.</returns>
    public QuestStatus Status() {
      List<ATask<T>> taskList = new List<ATask<T>>(tasks);
      return new QuestStatus(
        taskList.ConvertAll<TaskStatus>(task => task.Status()).ToArray()
      );
    }

    /// <summary>Todo list builder in the Builder Pattern.</summary>
    public static class Builder {
      /// <summary>Create a todo list that allows progress.</summary>
      public static Quest<T> BuildActive(params ATask<T>[] tasks) {
        Quest<T> quest = new Quest<T>(tasks);
        quest.Activate();
        return quest;
      }

      /// <summary>Create a todo list that halts progress.</summary>
      public static Quest<T> Build(params ATask<T>[] tasks) {
        return new Quest<T>(tasks);
      }
    }
  }
    
}
