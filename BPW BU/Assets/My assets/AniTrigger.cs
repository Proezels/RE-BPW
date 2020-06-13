using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AniTrigger : MonoBehaviour
{
    public PlayableDirector timeline;
    bool cutscene = false;

    // Start is called before the first frame update
    void Start()
    {
        timeline = timeline.GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider col){
        if (col.CompareTag ("Player") && cutscene == false ) {
            timeline.Play();
            cutscene = true;
            Debug.Log("trigger");
        }
    }
}
