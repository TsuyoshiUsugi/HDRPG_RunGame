using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StoryTimeLineManager : MonoBehaviour
{
    PlayableDirector _playableDirector;
    float _skipDur = 5;
    int _clikedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _playableDirector);
    }

    public void Skip()
    {
        _clikedTime++;

        //if (_playableDirector.duration < _playableDirector.time + _skipDur) return;
        _playableDirector.time = _clikedTime * _skipDur;
    }
}
