using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public enum GameDeltaTime
    {
        DEFAULT, PLAYER, ACTOR, END
    };

    private float speed = 1.0f;
    private float[] deltaTime = new float[(int)GameDeltaTime.END];
    private float actorDeltaTimeScale = 1.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deltaTime[(int)GameDeltaTime.DEFAULT] = Time.deltaTime;
        deltaTime[(int)GameDeltaTime.PLAYER] = Time.unscaledDeltaTime;
        deltaTime[(int)GameDeltaTime.ACTOR] = Time.unscaledDeltaTime * actorDeltaTimeScale;
    }

    public float GetDefaultDeltaTime()
    {
        return deltaTime[(int)GameDeltaTime.DEFAULT];
    }

    public float GetPlayerDeltaTime()
    {
        return deltaTime[(int)GameDeltaTime.PLAYER];
    }

    public float GetActorDeltaTime()
    {
        return deltaTime[(int)GameDeltaTime.ACTOR];
    }
}
