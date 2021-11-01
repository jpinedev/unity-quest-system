# Collision Event Triggers and UI Integration Demo

This is an example of using the Quest System to count how many times the the player capsule walks over the designated locations. The player can mover the capsule left and right with the horizontal axis input (`W` and `A` or `Left Arrow` and `Right Arrow`). 

## Implementation Walkthrough
#### Queuing Quests
Upon starting the game, two quests are queued in the QuestManager in `DemoQuestManager.cs`. These quests are built using the Builder Pattern for Quests and Tasks. Lets take a look at the first quest:
```cs
Quest<DemoQuestAction>.Builder.BuildActive(
  ATask<DemoQuestAction>.Builder.Build(DemoQuestAction.WalkedOnRed),
  ATask<DemoQuestAction>.Builder.Build(DemoQuestAction.WalkedOnBlue, 3)
);
```
Since the DemoQuestManager uses our local extention of the `QuestAction` class, `DemoQuestAction`, we must reflect that type in the Generic for `Quests` and `Tasks`. Here we are building a quest with two tasks. The first task only needs to be completed once while the second task needs to be completed three times. Once all tasks have been fully completed, the quest is completed.

#### Specifying In-game Actions
Next let's take a look at our local extention of the `QuestAction` class, `DemoQuestAction`:
```cs
public class DemoQuestAction : QuestAction {
  protected DemoQuestAction(string value): base(value) { }

  // Example Event Definitions
  public static DemoQuestAction WalkedOnRed = new DemoQuestAction("Walk on Red");
  public static DemoQuestAction WalkedOnBlue = new DemoQuestAction("Walk on Blue");
  // ...
  // End Definitions
}
```
Here we can see the definitions for the `DemoQuestAction`s that resemble in game actions. The two definitions of `DemoQuestAction` (`WalkedOnRed` and `WalkedOnBlue`) are the actions we used when building the first quest. In our demo, these actions are triggered by the player walking over two colored reigons of the floor.

#### Collision Event Triggers
We can see how we tell the `DemoQuestManager` that an action has occurred by looking at either the `RedFloor.cs` or `BlueFloor.cs`. We will be taking a look at `RedFloor.cs`, but the implementation is essentially the same for both.
```cs
public class RedFloor : MonoBehaviour {
  void OnTriggerEnter(Collider col) {
    DemoQuestManager.Instance.TriggerAction(DemoQuestAction.WalkedOnRed);
  }
}
```
Here we have collision detection for when any object (in this case the player capsule) enters the `Trigger Collider` above the `RedFloor` instance in game.
> Note:
> The `RedFloor` object has access to the `DemoQuestManager` via the Singlton Pattern. The `DemoQuestManager` recieves the action and sends an event to all subscribers of the action via the Subscriber Pattern.

#### Quest UI
Finally, let's take a look at displaying the active quest in the UI. For this we will look at the `QuestUI.cs` file.
```cs
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
```
When there is no active quest, we display that to the player. We use the `QuestStatus` object from the current active `Quest` to get the information about the quest's tasks. Here we are displaying the action name, the amount of times the action has been completed, and the total times the action needs to be completed to complete the task. Commented out is a similar message that displays the action name and the completion percentage of the task.