using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushManager : MonoBehaviour {

	void Start () {
		// Enable line below to enable logging if you are having issues setting up OneSignal. (logLevel, visualLogLevel)
		// OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.INFO, OneSignal.LOG_LEVEL.INFO);

		OneSignal.StartInit("fc0b804c-7547-443f-90ea-6bf6ef5d4460")
			.HandleNotificationOpened(HandleNotificationOpened)
			.EndInit();

		// Call syncHashedEmail anywhere in your app if you have the user's email.
		// This improves the effectiveness of OneSignal's "best-time" notification scheduling feature.
		// OneSignal.syncHashedEmail(userEmail);
	}

	// Gets called when the player opens the notification.
	private static void HandleNotificationOpened(OSNotificationOpenedResult result) {
	}
}
