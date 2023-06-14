using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class SceneLoadManager : SingletonMonobehavior<SceneLoadManager>
{
    [SerializeField] Image _fadeImage;
    [SerializeField] float _fadeDuration;

    private void Start()
    {
        _fadeImage.gameObject.SetActive(true);
        _fadeImage.color = new Color(0, 0, 0, 1);
        _fadeImage.DOFade(0, _fadeDuration).SetLink(_fadeImage.gameObject);
    }

    public void LoadScene(string sceneName)
    {
        if (!_fadeImage) SceneManager.LoadScene(sceneName);
        _fadeImage.DOFade(1, _fadeDuration).OnComplete(() => SceneManager.LoadScene(sceneName)).SetLink(_fadeImage.gameObject);
    }

    public async UniTask OnStartScene()
    {
        if (!_fadeImage) return;
        await _fadeImage.DOFade(0, _fadeDuration).SetLink(_fadeImage.gameObject);
    }
}
