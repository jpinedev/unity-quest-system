# Quest System in Unity
This is a project I made as a system that can be reused in different game development projects. The Quest System functions as a todo list for the player.

To see an example of its use, I provided a demo project with a walkthrough of its integration with the Quest System.
[Go to Demo Project](Assets/Examples/EventsAndUI/README.md)

## Quest System Overview
### Tasks
- Tasks have an associated in-game action or trigger.
- Tasks have a number of times the action must be completed for the task to be considered complete.
- If the Task is active and not complete, the counter will increase every time the associated action is triggered.

### Quests
- Quests are a list of Tasks (much like a todo list)
- When all Tasks in a quest are completed, the quest is completed

### QuestManager
- QuestManager uses the Singleton Pattern so exactly one QuestManager should ever exist at one time.
- QuestManager awaits in-game events to update active quests and their contained tasks.
- QuestManager also contains a Queue for Quests that can be used as a quest line.
