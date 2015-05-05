I had the same problem. I resolved it by just changing my package name in Manifest and updating its references. One more thing , Activty that is set to "Launcher" in android:name , go to that activity code in Oncreate method there should be some thing like:
String classNames[] = { "com.example.player.UnityPlayerActivity", "com.example.player.UnityPlayerNativeActivity" };
provide your package name here in quotation that should remove your exception.
I hope it'd work 