using UnityEngine;
using UnityEngine.AI;

/*	
	This is for all objects that the player can
	interact with such as enemies, items etc. It is to be used as a base class.
*/

[RequireComponent(typeof(ColorOnHover))]
public class Interactable : MonoBehaviour {

	public float radius = 3f;
	//public Transform interactionTransform;

	bool isFocus = false;	
	Transform player;		

	bool hasInteracted = false;	

	void Update ()
	{
		if (isFocus)	// If currently being focused
		{
			float distance = Vector3.Distance(player.position, transform.position);
		
			if (!hasInteracted && distance <= radius)
			{
				// Interact with the object
				hasInteracted = true;
				Interact();
			}
		}
	}

	// Called when the object is being focused
	public void OnFocused (Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
        hasInteracted = false;
    }

	// Called when the object is no longer focused
	public void OnDefocused ()
	{
		isFocus = false;
		hasInteracted = false;
		player = null;
	}

	// To be overwritten
	public virtual void Interact ()
	{
        Debug.Log("Interacting with " + transform.name);
	}

	void OnDrawGizmosSelected ()
	{
        //if (interactionTransform == null)
        //    interactionTransform = transform;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radius);
	}

}
