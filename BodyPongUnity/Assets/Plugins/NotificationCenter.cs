//    NotificationCenter is used for handling messages between GameObjects.
 
//    GameObjects can register to receive specific notifications.  When another objects sends a notification of that type, all GameObjects that registered for it and implement the appropriate message will receive that notification.
 
//    Observing GameObjetcs must register to receive notifications with the AddObserver function, and pass their selves, and the name of the notification.  Observing GameObjects can also unregister themselves with the RemoveObserver function.  GameObjects must request to receive and remove notification types on a type by type basis.
 
//    Posting notifications is done by creating a Notification object and passing it to PostNotification.  All receiving GameObjects will accept that Notification object.  The Notification object contains the sender, the notification type name, and an option hashtable containing data.
 
//    To use NotificationCenter, either create and manage a unique instance of it somewhere, or use the static NotificationCenter.
 
 
// We need a static method for objects to be able to obtain the default notification center.
// This default center is what all objects will use for most notifications.  We can of course create our own separate instances of NotificationCenter, but this is the static one used by all.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class NotificationCenter : MonoBehaviour
{
    private static NotificationCenter defaultCenter;
    public static NotificationCenter DefaultCenter {
		get {
			if (!defaultCenter) {
				GameObject notificationObject = new GameObject ("Default Notification Center");
 
				defaultCenter = notificationObject.AddComponent<NotificationCenter> ();
			}
 
            return defaultCenter; 
		}
    }
 
 
 
    // Our hashtable containing all the notifications.  Each notification in the hash table is an ArrayList that contains all the observers for that notification.
	Hashtable notifications = new Hashtable();
	//Hashtable notificationFunctions = new Hashtable();
 
	
	public void AddObserver (Component observer, string name) { AddObserver(observer, name, null); }
	public void AddObserver (Component observer, string name, string functionName) {
		// If the name isn't good, then throw an error and return.
		if (string.IsNullOrEmpty (name)) { Debug.Log ("Null name specified for notification in AddObserver."); return; }

		// If this specific notification doesn't exist yet, then create it.
		if (notifications[name] == null) {		
			notifications[name] =  new List<Hashtable>();
		}

		Hashtable hash = new Hashtable();

		hash["observer"] = observer;
		
		if(functionName != null) hash["functionName"] = functionName;
		else{
			hash["functionName"] = name;
		}

		List<Hashtable> notifyList = notifications[name] as List<Hashtable>;
 
		if(!notifyList.Contains(hash)){
			notifyList.Add(hash);
		}

	}
 
 
	public void RemoveObserver (Component observer, string name)
	{
		// REMOVES ALL INSTANCES OF A COMPONENT observer LISTENING FOR A STRING name
		List<Hashtable> notifyList = notifications[name] as List<Hashtable>;
 
		// Assuming that this is a valid notification type, remove the observer from the list.
		// If the list of observers is now empty, then remove that notification type from the notifications hash. This is for housekeeping purposes.
		if (notifyList == null) return;

		//Debug.Log("NOTIFICATIONCENTER: REMOVE"+name);
		//Debug.Log("notifyList COUNT: "+notifyList.Count);

		List<Hashtable> observersToRemove = new List<Hashtable>();
		foreach (Hashtable hash in notifyList){
			Component anObserver = (Component) hash["observer"];
			if (anObserver == observer) { 
				//Debug.Log(name+" REMOVE: "+observer);
				observersToRemove.Add(hash); 
			}
		}
		// Remove all the invalid observers
		if(observersToRemove.Count > 0){
			foreach (Hashtable hash in observersToRemove) {
				notifyList.Remove(hash);
				//Debug.Log(name+" COUNT: "+notifyList.Count);
			}	
		}

		if (notifyList.Count == 0) { 
			notifications.Remove(name); 
		}
	}
 
	// PostNotification sends a notification object to all objects that have requested to receive this type of notification.
	// A notification can either be posted with a notification object or by just sending the individual components.
 
	public void PostNotification (Component aSender, string aName) { PostNotification(aSender, aName, null); }
    public void PostNotification (Component aSender, string aName, object aData) { PostNotification(new Notification(aSender, aName, aData)); }
	public void PostNotification (Notification aNotification)
	{
		// First make sure that the name of the notification is valid.
		//Debug.Log("sender: " + aNotification.name);
		if (string.IsNullOrEmpty (aNotification.name)) {
			Debug.Log ("Null name sent to PostNotification.");
			return;
		}

		//Debug.Log("NOTIFICATIONCENTER POST: "+aNotification.name);

		// Obtain the notification list, and make sure that it is valid as well
		List<Hashtable> notifyList = (List<Hashtable>)notifications[aNotification.name];
		if (notifyList == null) return;
		
 
		// Create an array to keep track of invalid observers that we need to remove
		List<Hashtable> observersToRemove = new List<Hashtable> ();
 
		// Itterate through all the objects that have signed up to be notified by this type of notification.
		for (int i=0; i<notifyList.Count; i++) {	
			Hashtable hash = (Hashtable)notifyList[i];
			// If the observer isn't valid, then keep track of it so we can remove it later.
			// We can't remove it right now, or it will mess the for loop up.
			Component observer = (Component) hash["observer"];
			if (!observer) { 
				//Debug.Log(aNotification.name+" REMOVE: "+observer);
				observersToRemove.Add(hash); 
			}
			else {
				// If the observer is valid, then send it the notification. The message that's sent is the name of the notification.
				string functionName = (string) hash["functionName"];				
				//Debug.Log(aNotification.name+" POST: "+observer+" "+functionName);
				observer.SendMessage(functionName, aNotification, SendMessageOptions.DontRequireReceiver);
			}
		}
 
		// Remove all the invalid observers
		for(int i=0; i<observersToRemove.Count; i++) {
			Hashtable hash = observersToRemove[i];
			notifyList.Remove(hash);
		}	
	}
}

// The Notification class is the object that is send to receiving objects of a notification type.
// This class contains the sending GameObject, the name of the notification, and optionally a hashtable containing data.
public class Notification {

		//public Notification (GameObject aSender, string aName, object aData)
		//{
		//	throw new System.NotImplementedException ();
		//}
 
    public Component sender;
    public string name;
    public object data;
    public Notification (Component aSender, string aName) { sender = aSender; name = aName; data = null; }
    public Notification (Component aSender, string aName, object aData) { sender = aSender; name = aName; data = aData; }
}

public class StatusNotification : Notification{
	public StatusNotificationState state;
	public float value;

	public StatusNotification(Component aSender, string aName, object aData, // REQUIRED
								StatusNotificationState aState = StatusNotificationState.success, // OPTIONAL
								float aValue=0.0f)
	: base(aSender, aName, aData) { // BASE CLASS CONSTRUCTION
		state = aState;				// DERIVED CONSTRUCTION
		value = aValue;
	}
}

public class PurchaseNotification : StatusNotification{
	public object purchaseData; // REFERENCE DATA

	public PurchaseNotification(Component aSender, string aName, object aData, object aPurchaseData, // REQUIRED
								StatusNotificationState aState = StatusNotificationState.success, // OPTIONAL
								float aValue=0.0f)
	: base(aSender, aName, aData, aState, aValue) {
		purchaseData = aPurchaseData;
	}
}

public enum StatusNotificationState
{
	failure,
	success,
	cancel
}