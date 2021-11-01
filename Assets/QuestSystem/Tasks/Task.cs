using UnityEngine;

namespace QuestSystem.Tasks {

  /// <summary>Represents a todo item.</summary>
  public abstract class ATask<T> where T : QuestAction {
    protected bool active;

    protected ATask(T questAction) {
      active = false;
      this.questAction = questAction;
    }

    /// <summary>The todo item to be done.</summary>
    public T questAction { get; private set; }

    /// <summary>Allow progress to be made for the todo item.</summary>
    public void Activate() {
      if (IsComplete()) return;
      active = true;
      QuestManager<T>.Instance.SubscribeToAction(questAction, CompleteAction);
    }
    /// <summary>Halt progress for the todo item.</summary>
    public void Deactivate() {
      if (!active) return;
      active = false;
      QuestManager<T>.Instance.UnsubscribeToAction(questAction, CompleteAction);
    }

    /// <returns>Is the todo item completed?</returns>
    public abstract bool IsComplete();
    /// <returns>Percent completion of the todo item.</returns>
    public abstract float PercentComplete();
    /// <returns>The status of the todo item.</returns>
    public abstract TaskStatus Status();

    /// <summary>Do the todo item.</summary>
    protected abstract void CompleteAction();


    /// <summary>Todo item builder in the Builder Pattern.</summary>
    public static class Builder {

      /// <summary>Create a todo item that that can be done only once.</summary>
      public static ATask<T> Build(T questAction) {
        return new ActionTask<T>(questAction);
      }

      /// <summary>Create a todo item that that can be done multiple times.</summary>
      public static ATask<T> Build(T questAction, int count) {
        return new CountedTask<T>(questAction, count);
      }

    }
  }
  
  class ActionTask<T> : ATask<T> where T : QuestAction {
    bool actionComplete;

    public ActionTask(T questAction) : base(questAction) {
      actionComplete = false;
    }

    override protected void CompleteAction() {
      if (!active) return;
      actionComplete = true;

      if (IsComplete()) Deactivate();
    }

    override public bool IsComplete() => actionComplete;
    override public float PercentComplete() => (IsComplete() ? 100f : 0f);
    override public TaskStatus Status() {
      if (IsComplete()) return new TaskStatus(questAction.DisplayName, 1, 1, 100f);
      return new TaskStatus(questAction.DisplayName, 0, 1, 0f);
    }
  }

  class CountedTask<T> : ATask<T> where T : QuestAction {
    int goalCount;
    int currentCount;

    public CountedTask(T questAction, int goalCount) : base(questAction) {
      this.goalCount = goalCount;
      currentCount = 0;
    }

    override protected void CompleteAction() {
      if (!active) return;
      currentCount++;

      if (IsComplete()) Deactivate();
    }
    
    override public bool IsComplete() => currentCount >= goalCount;
    override public float PercentComplete() => 100f * (float) Mathf.Min(currentCount, goalCount) / (float) goalCount;
    override public TaskStatus Status() => new TaskStatus(questAction.DisplayName, currentCount, goalCount, PercentComplete());
  }

}
