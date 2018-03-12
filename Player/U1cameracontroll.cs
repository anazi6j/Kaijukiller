using UnityEngine;
using System.Collections;

public class U1cameracontroll : MonoBehaviour
{
    

    public GameObject lookAt;
    public Transform camTransform;
    private Vector3 offset;
    private Vector3 defaultposition;
    private Camera cam;

    
    public float distance = 1.0f;
    public float currentX = 0.0f;
    public float currentY = 0.0f;
    private float sensitivityX = 4.0f;
    private float sensitivityY = 1.0f;
    private float lowRangeX;
    private float maxRangeX;
    private float lowRangeY;
    private float maxRangeY;
    // Use this for initialization
    void Awake()
    {
        lookAt = GameObject.FindGameObjectWithTag("Camera");
    }
    void Start()
    {

        camTransform = transform;
        cam = Camera.main;
        offset = transform.position - lookAt.transform.position;
            }
    // Update is called once per frame

    void Update()
    {
        CameraControll();
    }

    void CameraControll()
    {

        transform.position = lookAt.transform.position + offset;
        currentX = Mathf.Clamp(currentX, 0f, 60.0f);
        currentY = Mathf.Repeat(currentY, 360.0f);
        if (Input.GetAxis("DS4_RanalogHorizontal") > 0)
        {
            currentY += 10 * Input.GetAxis("DS4_RanalogHorizontal");
        }
        else if (Input.GetAxis("DS4_RanalogHorizontal") < 0)
        {
            currentY -= 10 * -Input.GetAxis("DS4_RanalogHorizontal");
        }

        if (Input.GetAxis("DS4_RanalogVertical") > 0)
        {
            currentX += 10 * Input.GetAxis("DS4_RanalogVertical");
        }
        else if (Input.GetAxis("DS4_RanalogVertical") < 0)
        {
            currentX -= 10 * -Input.GetAxis("DS4_RanalogVertical");
        }
    }

    

    void LateUpdate()
    {

        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentX, currentY, 0);
        camTransform.position = lookAt.transform.position + rotation * dir;
        camTransform.LookAt(lookAt.transform.position);
    }

    





    /*
void Update()
{



    hordistance = 3f;
    verdistance = 1f;

    float angle = rotangle;

    float horizotalangle = xangle * angle;

    horizotalangle = Mathf.Repeat(yangle, 360.0f);
    float verticalangle = yangle * angle;
    verticalangle = Mathf.Clamp(xangle, 0f, 60.0f);
    xangle = Mathf.Clamp(verticalangle, -60f, 60.0f);






    if (Input.GetAxis("DS4_RanalogHorizontal") > 0)
    {
        yangle += 20*Time.fixedDeltaTime;
    }
    else if (Input.GetAxis("DS4_RanalogHorizontal") < 0)
    {
        yangle -= 20*Time.fixedDeltaTime;
    }


    if (Input.GetAxis("DS4_RanalogVertical") > 0)
    {
        xangle += 20*Time.fixedDeltaTime;
    }
    else if (Input.GetAxis("DS4_RanalogVertical") < 0)
    {
        xangle -= 20*Time.fixedDeltaTime;
    }





    if (looktarget != null)
    {
        Vector3 lookpositon = Player.position + offset;//追従する位置=見る対象+オフセット加算(カメラがめりこまないようにする）
        Vector3 relativepos = Quaternion.Euler(verticalangle, horizotalangle, 0) * new Vector3(0,0, -hordistance) + new Vector3(0, upperangle, 0);
        //      カメラ位置  は(Z→X→Yの順で回転する) (X=立体,Y=水平,z=奥行き)                 カメラと注視点の位置 
        transform.position = Player.position + relativepos;

        transform.LookAt(Player);




    }




}

*/
}
