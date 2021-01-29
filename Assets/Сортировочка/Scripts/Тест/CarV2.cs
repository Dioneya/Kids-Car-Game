using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarV2 : MonoBehaviour, IDropHandler
{
    public ItemSlot[] items { get; private set; }

    private WheelRotate wheels;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource stopEngine;
    private bool SpeedUp = false;
    private bool SpeedDown = false;
    private bool ToCenter = false;
    private bool ToPreviousPosition = false;
    Vector3 previousPosition = new Vector3();
    [SerializeField] private AudioClip[] motorSounds = new AudioClip[3];
    [SerializeField] private GameObject carObj;
    [SerializeField] private GameObject camera;
    [SerializeField] private SpeedBtn speed;
    void Awake()
    {
        items = gameObject.GetComponentsInChildren<ItemSlot>();
        wheels = GetComponent<WheelRotate>();
    }

    void Start()
    {
        ChangeMotorSound(0);
        DifficultCheck();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.name == "Pistol")
        {
            eventData.pointerDrag.GetComponent<Pistol>().isWork = true;
            eventData.pointerDrag.GetComponent<Glowing>().AddGlow();
        }
    }
    public void CarStop() 
    {
        stopEngine.Play();
    }
    public void CheckForCarAvaible()
    {
        bool isFull = true;
        foreach (ItemSlot itemSlot in items)
        {
            if (itemSlot.isEmpty)
            {
                isFull = false;
                break;
            }
        }
        Debug.LogWarning(isFull);
        if (isFull)
        {
            LevelManager.isStart = true;
            //touriste.GetComponent<Animator>().Play("Walk");
        }
    }

    private void DifficultCheck()
    {
        if (Settings.difficult == Settings.Difficult.Hard)
        {
            foreach (ItemSlot itemSlot in items) 
            {
                Image image = itemSlot.GetComponent<Image>();
                var c = image.color;
                c.a = 0;
                image.color = c;
            }
        }
    }

    public void CheckDisableDetailTriggers(Detail detail)
    {
        foreach (ItemSlot item in items)
        {
            Image image = item.gameObject.GetComponent<Image>();
            image.raycastTarget = item.nameOfCorrectDetail == detail.nameOfDetail && item.isEmpty;
        }
    }
    private void FixedUpdate()
    {
        if (LevelManager.isMoved)
        {
            wheels.enabled = true;
            audioSource.mute = false;
        }
        else 
        {
            wheels.enabled = false;
            audioSource.mute = true;
        }

        if (SpeedUp)
        {
            if(ToCenter)
                carObj.transform.position += Vector3.right * 1f * Time.deltaTime; 
            else
                carObj.transform.position += Vector3.right * .5f * Time.deltaTime;
            if (ToCenter && gameObject.transform.position.x >= camera.transform.position.x+4f)
            {
                SpeedUp = false;
                ToCenter = false;
            }
        }


        else if (SpeedDown) 
        {
            carObj.transform.position += Vector3.left * 1f * Time.deltaTime;
            if (ToPreviousPosition && carObj.transform.position.x <= previousPosition.x)
            {
                SpeedDown = false;
                ToPreviousPosition = false;
                speed.isWork = true;
            }
        }
            

    }

    public void ChangeMotorSound(int soundIndx) 
    {
        audioSource.clip = motorSounds[soundIndx];
        audioSource.Play();
    }

    public IEnumerator MoveForward() 
    {
        SpeedUp = true;
        yield return new WaitForSeconds(.5f);
        SpeedUp = false;
    }

    public IEnumerator MoveBack()
    {
        SpeedDown = true;
        ToCenter = false;
        yield return new WaitForSeconds(.5f);
        SpeedDown = false;
    }

    public void MoveToCenter() 
    {
        SpeedUp = true;
        ToCenter = true;
        SpeedDown = false;
        previousPosition = carObj.transform.position;
    }
    public void MoveToPrevPos() 
    {
        speed.isWork = false;
        ToPreviousPosition = true;
        SpeedDown = true;
    }
}
