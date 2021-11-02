using NUnit.Framework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class EventManagerTests
{
    private readonly string TEST_EVENT = "TestEvent";

    [UnityTest]
    public IEnumerator ShouldBroadcastEvent()
    {
        AddEventManagerToScene();

        // add event listener to event manager
        EventManager.AddEventListener(TEST_EVENT, () => { Assert.Pass("Event listener successfully added and event broadcast responded to."); });

        yield return null;

        // attempt to broadcast event
        EventManager.Broadcast(TEST_EVENT);
    } //end method ShouldBroadcastEvent

    [UnityTest]
    public IEnumerator ShouldRemoveEventListener()
	{
        AddEventManagerToScene();

        // add event listener to event manager
        EventManager.AddEventListener(TEST_EVENT, NegativeListenerResponse);

        yield return null;

        // remove event listener and attempt to broadcast
        EventManager.RemoveEventListener(TEST_EVENT, NegativeListenerResponse);

        // should throw exception as listener no longer exists
        Assert.Throws<Exception>(delegate { EventManager.Broadcast(TEST_EVENT); });
    } //end method ShouldRemoveEventListener
    
    [UnityTest]
    public IEnumerator ShouldRemoveAllEventListeners()
    {
        AddEventManagerToScene();

        // add multiple event listeners to event manager
        EventManager.AddEventListener(TEST_EVENT, NegativeListenerResponse);
        EventManager.AddEventListener(TEST_EVENT, () => { Assert.Fail("Event should not have been responded to."); });


        yield return null;

        // remove all event listeners and attempt to broadcast
        EventManager.RemoveAllListeners(TEST_EVENT);

        // should throw exception as listeners no longer exists
        Assert.Throws<Exception>(delegate { EventManager.Broadcast(TEST_EVENT); });
    } //end method ShouldRemoveAllEventListeners
    
    private void AddEventManagerToScene()
	{
        GameObject obj = new GameObject();
        obj.AddComponent<EventManager>();
	}

    private void NegativeListenerResponse()
	{
        Assert.Fail("Event should not have been responded to.");
	} //end method NegativeListenerResponse
} //end class EventManagerTests
