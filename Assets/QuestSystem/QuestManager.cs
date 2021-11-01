using System;
using System.Collections.Generic;
using UnityEngine;

using QuestSystem.Quests;
using QuestSystem.Tasks;

namespace QuestSystem {

  /// <summary>Singleton Quest Manager to keep and update a queue of Quests.</summary>
  public class QuestManager<T> : MonoBehaviour where T : QuestAction {
    private Dictionary<T, Action> actionDictionary;
    protected Queue<Quest<T>> quests;

    private static QuestManager<T> _instance;
    public static QuestManager<T> Instance { get { return _instance; } }

    public bool HasActiveQuest { get { return !(quests == null || quests.Count == 0); } }
    public Quest<T> ActiveQuest { get { return (HasActiveQuest ? quests.Peek():null); } }

    /// <summary>Ensures only one QuestManager exists at a time (maintains Singleton Pattern).</summary>
    private void Awake() {
      if (_instance != null && _instance != this) {
        Destroy(this.gameObject);
      } else {
        _instance = this;
      }
    }

    public void Start() {
      actionDictionary = new Dictionary<T, Action>();

      quests = new Queue<Quest<T>>();
    }

    /// <summary>
    /// Updates any active active quest. If the active quest is completed, 
    /// automatically activates next quest in the queue.
    /// </summary>
    ///
    /// <param name="questAction">Quest Action that occurred</param>
    public void TriggerAction(T questAction) {
      actionDictionary[questAction]?.Invoke();
      if (ActiveQuest != null && ActiveQuest.IsComplete()) {
        Debug.Log("You have completed a quest!");
        ActiveQuest.Deactivate();
        quests.Dequeue();
        ActiveQuest?.Activate();
      }
    }

    /// <summary>
    /// Subscribe to the Quest Action Event.
    /// Adds action to Action Dictionary if not already.
    /// </summary>
    ///
    /// <param name="questAction">Quest Action Event to listen for.</param>
    /// <param name="subscriber">Subscriber to the Event. Listens for Quest Action Event.</param>
    public void SubscribeToAction(T questAction, Action subscriber) {
      if (!actionDictionary.ContainsKey(questAction)) actionDictionary.Add(questAction, delegate() {});
      actionDictionary[questAction] += subscriber;
    }
    
    /// <summary>
    /// Unsubscribe to the Quest Action Event.
    /// </summary>
    ///
    /// <param name="questAction">Quest Action Event to listen for.</param>
    /// <param name="action">Quest Action Event Subscriber to be unsubscribed.</param>
    public void UnsubscribeToAction(T questAction, Action subscriber) {
      actionDictionary[questAction] -= subscriber;
    }

  }

}
