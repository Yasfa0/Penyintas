using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

//[ExecuteAlways]
public class WaterShapeController : MonoBehaviour
{
    [SerializeField] private float springStiffness = 0.1f;
    [SerializeField] private float dampening = 0.03f;
    [SerializeField] private List<WaterSpring> springs = new List<WaterSpring>();
    public float spread = 0.006f;

    private int cornerCount = 2;
    [SerializeField] private SpriteShapeController spriteShapeController;
    [Range(1,100)] [SerializeField] private int wavesCount = 6;
    [SerializeField] private GameObject wavePointPref;
    [SerializeField] private GameObject wavePoints;

    private float lastSplash;
    float splashCD = 2f;

    private void Start()
    {
        SetWaves();
        CreateSprings(spriteShapeController.spline);
        lastSplash = Time.time;
    }

    /*private void OnValidate()
    {
        StartCoroutine(CreateWaves());
    }*/

    IEnumerator CreateWaves()
    {
        foreach (Transform child in wavePoints.transform)
        {
            StartCoroutine(Destroy(child.gameObject));
        }
        yield return null;
        SetWaves();
        yield return null;
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }

    private void FixedUpdate()
    {
        foreach (WaterSpring spring in springs)
        {
            spring.WaveSpringUpdate(springStiffness,dampening);
            spring.WavePointUpdate();
        }

        UpdateSprings();

       
    }

    private void Update()
    {
        if(Time.time-lastSplash >= splashCD)
        {
            lastSplash = Time.time;
            Splash(Random.Range(0,springs.Count-1), 0.05f);
        }
    }




    public void Splash(int index,float speed)
    {
        if (index >= 0 && index < springs.Count)
        {
            springs[index].velocity += speed;
        }
    }

    private void Smoothen(Spline waterSpline,int index)
    {
        Vector3 position = waterSpline.GetPosition(index);
        Vector3 positionPrev = position;
        Vector3 positionNext = position;

        if(index > 1)
        {
            positionPrev = waterSpline.GetPosition(index - 1);
        }
        if(index-1 <= wavesCount)
        {
            positionNext = waterSpline.GetPosition(index + 1);
        }

        Vector3 forward = gameObject.transform.forward;

        float scale = Mathf.Min((positionNext - position).magnitude, (positionPrev - position).magnitude) * 0.33f;
        Vector3 leftTangent = (positionPrev - position).normalized * scale;
        Vector3 rightTangent = (positionNext - position).normalized * scale;
        SplineUtility.CalculateTangents(position, positionPrev, positionNext, forward, scale, out rightTangent, out leftTangent);

        waterSpline.SetLeftTangent(index, leftTangent);
        waterSpline.SetRightTangent(index, rightTangent);
    }

    private void CreateSprings(Spline waterSpline)
    {
        springs = new List<WaterSpring>();
        for (int i = 0; i <= wavesCount+1; i++)
        {
            int index = i + 1;

            Smoothen(waterSpline, index);
            GameObject wavePoint = Instantiate(wavePointPref,wavePoints.transform, false);
            wavePoint.transform.localPosition = waterSpline.GetPosition(index);
            WaterSpring waterSpring = wavePoint.GetComponent<WaterSpring>();
            waterSpring.Init(spriteShapeController);
            springs.Add(waterSpring);
        }
    }

    public void SetWaves()
    {
        Spline waterSpline = spriteShapeController.spline;
        int waterPointsCount = waterSpline.GetPointCount();

        for (int i = cornerCount; i < waterPointsCount - cornerCount; i++)
        {
            waterSpline.RemovePointAt(cornerCount);
        }

        Vector3 waterTopLeftCorner = waterSpline.GetPosition(1);
        Vector3 waterTopRightCorner = waterSpline.GetPosition(2);
        float waterWidth = waterTopRightCorner.x - waterTopLeftCorner.x;

        float spacingPerWave = waterWidth / (wavesCount + 1);

        for (int i = wavesCount; i > 0; i--)
        {
            int index = cornerCount;

            float xPosition = waterTopLeftCorner.x + (spacingPerWave * i);
            Vector3 wavePoint = new Vector3(xPosition,waterTopLeftCorner.y,waterTopLeftCorner.z);
            waterSpline.InsertPointAt(index,wavePoint);
            waterSpline.SetHeight(index, 0.1f);
            waterSpline.SetCorner(index, false);
            waterSpline.SetTangentMode(index, ShapeTangentMode.Continuous);
        }
    }

    public void UpdateSprings()
    {
        int count = springs.Count;
        float[] leftDeltas = new float[count];
        float[] rightDeltas = new float[count];
        for (int i = 0; i < count; i++)
        {
            if (i > 0)
            {
                leftDeltas[i] = spread * (springs[i].height - springs[i - 1].height);
                springs[i - 1].velocity += leftDeltas[i];
            }
            if(i < springs.Count - 1)
            {
                rightDeltas[i] = spread * (springs[i].height - springs[i + 1].height);
                springs[i + 1].velocity += rightDeltas[i];
            }
        }
    }
}
