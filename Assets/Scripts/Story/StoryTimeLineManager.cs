using System;
using UniRx;
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
        Observable.Interval(TimeSpan.FromSeconds(_skipDur))
            .Subscribe(_ => _clikedTime++)
            .AddTo(this);
    }

    public void Skip()
    {
        _clikedTime++;

        _playableDirector.time = _clikedTime * _skipDur;
    }

    public void Pose()
    {
        if (_playableDirector.state == PlayState.Paused)
        {
            _playableDirector.Play();
        }
        else
        {
            _playableDirector.Pause();
        }
    }
}
