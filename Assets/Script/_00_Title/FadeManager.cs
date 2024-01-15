using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] Image _fade;
    private enum State
    {
        FadeIn,
        FadeOut,
        None = 99,
    }
    private State _state;
    private float _alpha,_targetalpha;
    private float _speed;
    private void Awake()
    {
        _state = State.None;
        _alpha = 1.0f;
    }
    public void FadeIn(float r, float g, float b, float speed)
    {
        if (_state == State.None)
        {
            _fade.color = new Color(r,g,b);
            _speed = speed;
            _targetalpha = 0.0f;
            _state = State.FadeIn;
        }
    }
    public void FadeIn()
    {
        FadeIn(0, 0, 0, 1);
    }
    public void FadeIn(float speed)
    {
        FadeIn(0, 0, 0, speed);
    }
    public void FadeIn(float r,float g,float b)
    {
        FadeIn(r, g, b, 1);
    }
    
    public void FadeIn(Color color)
    {
        FadeIn(color.r, color.g, color.b, 1);
    }
    public void FadeIn(Color color, float speed)
    {
        FadeIn(color.r, color.g, color.b, speed);
    }
    public void FadeOut(float r, float g, float b, float speed)
    {
        if (_state == State.None)
        {
            _fade.color = new Color(r, g, b);
            _speed = speed;
            _targetalpha = 1.0f;
            _state = State.FadeOut;
        }
    }
    public void FadeOut()
    {
        FadeOut(0, 0, 0, 1);
    }
    public void FadeOut(float speed)
    {
        FadeOut(0, 0, 0, speed);
    }
    public void FadeOut(float r, float g, float b)
    {
        FadeOut(r, g, b, 1);

    }
    public void FadeOut(Color color)
    {
        FadeOut(color.r, color.g, color.b, 1);

    }
    public void FadeOut(Color color, float speed)
    {
        FadeOut(color.r, color.g, color.b, speed);

    }
    public void Skip()
    {
        if (_state != State.None)
        {
            AlphaChane(_targetalpha);
            _alpha = _targetalpha;
            _state = State.None;
        }
    }
    public void Skip(bool smooth)
    {
        if (_state != State.None)
        {
            AlphaChane(_targetalpha);
            if (smooth)
                _alpha = _targetalpha;
            _state = State.None;
        }
    }
    private void AlphaChane(float alpha)
    {
        _fade.color = new Color(
                _fade.color.r,
                _fade.color.g,
                _fade.color.b,
                alpha
                );
    }
    private void Update()
    {
        if(_state != State.None)
        {
            if(_alpha < _targetalpha)
            {
                _alpha += Time.deltaTime * _speed;
                if (_alpha >= _targetalpha)
                {
                    _alpha = _targetalpha;
                    _state = State.None;
                }
            }
            else
            {
                _alpha -= Time.deltaTime * _speed;
                if (_alpha <= _targetalpha)
                {
                    _alpha = _targetalpha;
                    _state = State.None;
                }
            }
            AlphaChane(_alpha);
        }
    }
}
