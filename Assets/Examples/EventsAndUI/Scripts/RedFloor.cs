using UnityEngine;

public class RedFloor : MonoBehaviour {

  void OnTriggerEnter(Collider col) {
    DemoQuestManager.Instance.TriggerAction(DemoQuestAction.WalkedOnRed);
  }

}
