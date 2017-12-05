using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidToast {

	private AndroidJavaObject activity;
	private AndroidJavaClass UnityPlayer;
	private AndroidJavaObject context;
	private AndroidJavaClass Toast;

	private string toastMessage;

	public AndroidToast(){
		if (Application.platform.Equals (RuntimePlatform.Android)) {
			UnityPlayer = new AndroidJavaClass (Variables.UNITY_PLAYER_PATH);
			activity = UnityPlayer.GetStatic<AndroidJavaObject> (Variables.CURRENT_ACTIVITY);
			context = activity.Call<AndroidJavaObject> (Variables.GET_APPLICATION_CONTEXT);
			Toast = new AndroidJavaClass (Variables.TOAST_PATH);
		}
	}

	public void CreateToastWithMessage(string message){
		if (Application.platform.Equals (RuntimePlatform.Android)) {
			toastMessage = message;
			activity.Call ("runOnUiThread", new AndroidJavaRunnable (ShowToast));
		}
	}

	private void ShowToast(){
		AndroidJavaObject javaString=new AndroidJavaObject(Variables.JAVA_STRING_PATH,toastMessage);
		AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject> ("makeText", context, javaString, Toast.GetStatic<int>(Variables.TOAST_LENGTH));
		toast.Call ("show");
	}
}
