using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewerPlayerController : MonoBehaviour
{
    //This is the assignment section
    public Transform Player;
    public CharacterController Controller;
    public Transform Camera;
    public Transform ObjectInHand;
    public RectTransform MiniMap;
    public Animation Animator;
    public float MovementSpeed = 10f;
    public float MouseSpeed = 10f;
    public float ControllerSpeed = 10f;
    public float Gravity = -9.18f;
    public float JumpHeight = 10f;

    //delegate to focus on our Interactables
    public delegate void OnFocusChanged(Interactable newFocus);
    public OnFocusChanged onFocusChangedCallback;

    public Interactable focus;

    //private stuff that doesn't need to be in the inspector
    float xRotation = 0f;
    bool MiniMapOpen = false;
    public Camera cam;
    Vector3 AppliedGravity = new Vector3();
    bool Crouching = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        
    }

    // Update is called once per frame
    void Update()
    {


        //First im collecting the Inputdata and print them out for Debug
        float moveX = Input.GetAxis("Horizontal") * MovementSpeed;
        float moveZ = Input.GetAxis("Vertical") * MovementSpeed;
        float mouseX = Input.GetAxis("Mouse X") * MouseSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSpeed;
        float contX = Input.GetAxis("Controller X") * ControllerSpeed;
        float contY = Input.GetAxis("Controller Y") * ControllerSpeed;

        //Now im processing the Input

        if(Input.GetKeyDown(KeyCode.M))
        {
            if(!MiniMapOpen)
            {
                MiniMap.sizeDelta = new Vector2(800, 800);
                MiniMap.position = new Vector3(564, 317,0);
                MiniMapOpen = true;
            } else
            {
                MiniMap.sizeDelta = new Vector2(200, 200);
                MiniMap.position = new Vector3(-400, -200, 0);
                MiniMapOpen = false;
            }
        }
        if(moveZ < 0 || moveZ > 0)
        {
            if (!Animator.isPlaying)
            {
                Animator.Play("Walk");
            }
        }

        if(Input.GetButtonDown("Crouch") && Controller.isGrounded)
        {
            if(!Crouching)
            {
                Camera.position -= new Vector3(0,.8f,0);
                Crouching = true;
            } else
            {
                Camera.position += new Vector3(0,.8f,0);
                Crouching = false;
            }
        }

        if (Input.GetButton("Run"))
        {
            moveZ = moveZ * 5;
            if(!Animator.IsPlaying("Run"))
            {
                Animator.Stop();
            }
            if(!Animator.isPlaying)
            {
                Animator.Play("Run");
            }
        }

        if (moveZ == 0)
        {
            Animator.Stop();
        }
        Vector3 move = Player.forward * moveZ + Player.right * moveX;
        xRotation -= mouseY;
        xRotation -= contY;
        xRotation = Mathf.Clamp(xRotation,-90,90);
        
        if(Controller.isGrounded)
        {
            AppliedGravity.y = -2f;
        } else
        {
            AppliedGravity.y += Gravity * Time.deltaTime;
            Animator.Stop();
        }

        if(Input.GetButtonDown("Jump") && Controller.isGrounded)
        {
            AppliedGravity.y += JumpHeight;
        }

        //Here i'm applying the movement
        Camera.localRotation = Quaternion.Euler(xRotation,0,0);
        Player.Rotate(0, mouseX, 0);
        Player.Rotate(0, contX, 0);
        Controller.Move(move * Time.deltaTime);
        Controller.Move(AppliedGravity * Time.deltaTime);

        
        //Animation


        //Interactable
        // If we press right mouse
        if (Input.GetMouseButtonDown(1))
        {
            // Shoot out a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 
           
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
                  
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (onFocusChangedCallback != null)
            onFocusChangedCallback.Invoke(newFocus);

        // If our focus has changed
        if (focus != newFocus && focus != null)
        {
            // Let our previous focus know that it's no longer being focused
            focus.OnDefocused();
        }

        // Set our focus to what we hit
        // If it's not an interactable, simply set it to null
        focus = newFocus;

        if (focus != null)
        {
            // Let our focus know that it's being focused
            focus.OnFocused(Player.transform);
        }

    }

      
}
