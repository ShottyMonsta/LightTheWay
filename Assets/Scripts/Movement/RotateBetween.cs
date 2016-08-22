using UnityEngine;
using System.Collections;

public class RotateBetween : MonoBehaviour {


    

    public float rotationSpeed = 5;

    public float startAngle;
    public float endAngle;

    public float hangTime = 0f;

    public bool active = true;

    private float elapsedTime;

    private bool isLight = false;

    private BeamLight lightBeam;

    public float angle;

    private float direction = 1;

    private float hangClock;

    private ConeLight lightCone;

    public bool constantRotation = false;
	// Use this for initialization
	void Start () {

        lightBeam = GetComponent<BeamLight>();
        lightCone = GetComponent<ConeLight>();

	}


	
	// Update is called once per frame
	void Update () {

        if(active)
        {

            if (constantRotation)
            {
                angle += rotationSpeed;
                angle = angle % 360;
            }
            else
            {
                angle = Mathf.LerpAngle(startAngle, endAngle, elapsedTime / rotationSpeed);

                hangClock += Time.deltaTime;

                if (hangTime < hangClock)
                {
                    elapsedTime += (Time.deltaTime * direction);
                }

                if (elapsedTime >= rotationSpeed && direction > 0)
                {
                    direction *= -1;
                    hangClock = 0;
                }
                else if (elapsedTime <= 0 && direction < 0)
                {

                    direction *= -1;
                    hangClock = 0;
                }

            }


            if (lightCone)
            {
                lightCone.angle = angle;
            }
            else if (lightBeam)
            {
                lightBeam.angle = angle;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            
        }
	
	}
}
