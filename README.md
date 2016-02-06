# handsetfree
Notification mirroring and SMS reply from desktop

This SLN contains Three projects. 
1.) Android application to handle notification listening, sending notifications to the server,
    and that will receive and reply to notifications sent by replying from desktop.
2.) Chrome Extension generated using the following tool:
    (https://visualstudiogallery.msdn.microsoft.com/c11cd639-2abb-4243-96d2-153c0adb494a) 
    that will receive notifications from the server and display them to a computer with chrome open. 
    It will also handle replying to notifications from Desktop
2.) WCF Service that will serve as the brains of the operation. 
    It will receive messages via GCM or WebDav and push them to the Chrome Extension
    It will then receive messages from the chrome extension that it will push to the android app to 
    send. To make sure that multiple different android devices can use this same server, there will be 
    some database for associating virtual folders with the gmail address tied to the android device the 
    app is installed on. 
