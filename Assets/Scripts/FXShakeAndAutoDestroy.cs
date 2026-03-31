// Assets/Scripts/FXShakeAndAutoDestroy.cs
using UnityEngine;

public class FXShakeAndAutoDestroy : MonoBehaviour
{
    [Header("Lifetime")]
    [SerializeField] private float lifeTime = 2f;

    [Header("Shake")]
    [SerializeField] private float posAmplitude = 0.08f;
    [SerializeField] private float posFrequency = 25f;
    [SerializeField] private float rotAmplitude = 8f;
    [SerializeField] private float rotFrequency = 20f;

    private Vector3 _startLocalPos;
    private Quaternion _startLocalRot;
    private float _t;

    private void Awake()
    {
        _startLocalPos = transform.localPosition;
        _startLocalRot = transform.localRotation;
    }

    private void Update()
    {
        _t += Time.deltaTime;

        float px = Mathf.Sin(_t * posFrequency) * posAmplitude;
        float py = Mathf.Cos(_t * (posFrequency * 0.9f)) * posAmplitude;
        transform.localPosition = _startLocalPos + new Vector3(px, py, 0f);

        float rz = Mathf.Sin(_t * rotFrequency) * rotAmplitude;
        transform.localRotation = _startLocalRot * Quaternion.Euler(0f, 0f, rz);

        if (_t >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}