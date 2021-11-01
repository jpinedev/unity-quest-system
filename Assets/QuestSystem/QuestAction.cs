using System;

namespace QuestSystem {

  /// <summary>Represents an event to be recorded with a todo list.</summary>
  public class QuestAction : IEquatable<QuestAction> {
    private static int index = 0;
    protected QuestAction(string value) {
      DisplayName = value;
      Id = index++;
    }

    public string DisplayName { get; private set; }
    protected int Id { get; private set; }

    public override string ToString() => DisplayName;
    public bool Equals (QuestAction other) {
      return Id == other.Id;
    }
  }

}