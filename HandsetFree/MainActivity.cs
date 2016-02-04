using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Service.Notification;

namespace HandsetFree
{

    [Activity(Label = "HandsetFree", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public TextView txtView;
        private NotificationReceiver nReceiver;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            txtView = (TextView)FindViewById(Resource.Id.tbNotificationInfo);
            //txtView = (TextView)FindViewById(Resource.Id.textView1);
            nReceiver = new NotificationReceiver();
            IntentFilter filter = new IntentFilter();
            filter.AddAction("com.dpwebdev.handsetfree.NOTIFICATION_LISTENER_EXAMPLE");
            RegisterReceiver(nReceiver, filter);

            // Get our button from the layout resource,
            // and attach an event to it
            Button createButton = FindViewById<Button>(Resource.Id.MyButton);
            Button catchButton = FindViewById<Button>(Resource.Id.catchNotification);

            createButton.Click += delegate
            {
                NotificationManager nManager = (NotificationManager)GetSystemService(NotificationListenerService.NotificationService);
                Notification.Builder ncomp = new Notification.Builder(this);
                ncomp.SetContentTitle("My Notification");
                ncomp.SetContentText("Notification Listener Service Example");
                ncomp.SetTicker("Notification Listener Service Example");
                ncomp.SetSmallIcon(Resource.Drawable.Icon);
                ncomp.SetAutoCancel(true);
                nManager.Notify(DateTime.Now.Millisecond, ncomp.Build());

                

            };

            catchButton.Click += delegate
            {
                
                Intent i = new Intent("com.dpwebdev.handsetfree.NOTIFICATION_LISTENER_SERVICE_EXAMPLE");
                i.PutExtra("command", "list");
                SendBroadcast(i);
            };

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterReceiver(nReceiver);
        }
        [BroadcastReceiver]
        public class NotificationReceiver : BroadcastReceiver
        {
            public override void OnReceive(Context context, Intent intent)
            {

                String temp = intent.GetStringExtra("notification_event") + "\n";
            }
        }


    }
}

