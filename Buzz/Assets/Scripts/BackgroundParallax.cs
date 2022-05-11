using UnityEngine;
//using UnityEditor;

public class BackgroundParallax : MonoBehaviour
{
    public Transform[] Backgrounds;
    public float ParallaxScale;
    public float ParallaxReductionFactor;
    public float Smoothing;

    private Vector3 _lastposition;

    public void Start()
    {
        _lastposition = transform.position;
    }

    public void Update()
    {
        var parallax = (_lastposition.x - transform.position.x) * ParallaxScale;

        for(var i = 0; i < Backgrounds.Length; i++)
        {
            var backgroundTargetPotision = Backgrounds[i].position.x + parallax * (i * ParallaxReductionFactor + 1);
            Backgrounds[i].position = Vector3.Lerp(
                Backgrounds[i].position, //from
                new Vector3(backgroundTargetPotision, Backgrounds[i].position.y, Backgrounds[i].position.z), //to
                Smoothing * Time.deltaTime);
        }

        _lastposition = transform.position;
    }
}