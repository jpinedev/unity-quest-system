using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    void Update() {
        float dx = Input.GetAxis("Horizontal");
        transform.position += new Vector3(dx / 5, 0);
    }
}
