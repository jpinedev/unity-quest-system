using QuestSystem;
using QuestSystem.Quests;
using QuestSystem.Tasks;

/// <summary>Singleton Quest Manager to keep and update a queue of Quests.</summary>
public class DemoQuestManager : QuestManager<DemoQuestAction> {

  public void Start() {
    base.Start();

    // Queuing some example quests for the Quest System Demo
    quests.Enqueue(Quest<DemoQuestAction>.Builder.BuildActive(
      ATask<DemoQuestAction>.Builder.Build(DemoQuestAction.WalkedOnRed),
      ATask<DemoQuestAction>.Builder.Build(DemoQuestAction.WalkedOnBlue, 3)
    ));
    quests.Enqueue(Quest<DemoQuestAction>.Builder.Build(
      ATask<DemoQuestAction>.Builder.Build(DemoQuestAction.WalkedOnRed, 10)
    ));
  }

}