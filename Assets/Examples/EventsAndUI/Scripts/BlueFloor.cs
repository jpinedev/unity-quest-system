using UnityEngine;

public class BlueFloor : MonoBehaviour {

  void OnTriggerEnter(Collider col) {
    DemoQuestManager.Instance.TriggerAction(DemoQuestAction.WalkedOnBlue);
  }

}
