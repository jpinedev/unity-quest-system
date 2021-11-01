using QuestSystem;

public class DemoQuestAction : QuestAction {

  protected DemoQuestAction(string value): base(value) { }

  // Example Event Definitions
  public static DemoQuestAction WalkedOnRed = new DemoQuestAction("Walk on Red");
  public static DemoQuestAction WalkedOnBlue = new DemoQuestAction("Walk on Blue");
  // ...

  // End Definitions

}