using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Util;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Service.Notification;

namespace HandsetFree
{
    public class NotificationListenerServiceWrapper : NotificationListenerService
    {
        public NotificationListenerServiceWrapper() { }
        
        private NLSServiceReceiver nlservicereceiver = new NLSServiceReceiver();

        public override void OnCreate()
        {
            base.OnCreate();
            nlservicereceiver = new NLSServiceReceiver();
            IntentFilter filter = new IntentFilter();
            filter.AddAction("com.dpwebdev.handsetfree.NOTIFICATION_LISTENER_SERVICE_EXAMPLE");
            RegisterReceiver(nlservicereceiver, filter);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterReceiver(nlservicereceiver);
        }

        public override void OnNotificationPosted(StatusBarNotification sbn)
        {
            base.OnNotificationPosted(sbn);
            Log.Info("NLService", "**********  onNotificationPosted");
            Log.Info("NLService", "ID :" + sbn.Id + "\t" + sbn.Notification.TickerText + "\t" + sbn.PackageName);
            Intent i = new Intent("com.dpwebdev.handsetfree.NOTIFICATION_LISTENER_EXAMPLE");
            i.PutExtra("notification_event", "onNotificationPosted :" + sbn.PackageName + "\n");
            SendBroadcast(i);
        }

        public override void OnNotificationRemoved(StatusBarNotification sbn)
        {
            Log.Info("NLService", "********** onNOtificationRemoved");
            Log.Info("NLService", "ID :" + sbn.Id + "\t" + sbn.Notification.TickerText + "\t" + sbn.PackageName);
            Intent i = new Intent("com.dpwebdev.handsetfree.NOTIFICATION_LISTENER_EXAMPLE");
            i.PutExtra("notification_event", "onNotificationRemoved :" + sbn.PackageName + "\n");
            SendBroadcast(i);
        }

        [BroadcastReceiver(Enabled = true)]
        [IntentFilter(new[] { "com.dpwebdev.handsetfree.NOTIFICATION_LISTENER_EXAMPLE" })]
        public class NLSServiceReceiver : BroadcastReceiver
        {
            public NLSServiceReceiver() { }

            NotificationListenerServiceWrapper NLSService = new NotificationListenerServiceWrapper();

            public override void OnReceive(Context context, Intent intent)
            {


                if (intent.GetStringExtra("command").Equals("clearall"))
                {
                    NLSService.CancelAllNotifications();
                }
                else if (intent.GetStringExtra("command").Equals("list"))
                {
                    Intent i1 = new Intent("com.dpwebdev.handsetfree.NOTIFICATION_LISTENER_EXAMPLE");
                    i1.SetFlags(ActivityFlags.NewTask);
                    i1.PutExtra("notification_event", "=====================");
                    //SendBroadcast(i1);
                    NLSService.SendBroadcast(i1);

                    StatusBarNotification[] statusBarNotifications = NLSService.GetActiveNotifications();

                    var i = 0;
                    foreach (StatusBarNotification sbn in statusBarNotifications)
                    {
                        Intent i2 = new Intent("com.kpbird.nlsexample.NOTIFICATION_LISTENER_EXAMPLE");
                        i2.SetFlags(ActivityFlags.NewTask);
                        i2.PutExtra("notification_event", i + " " + sbn.Notification + "n");
                        NLSService.SendBroadcast(i2);

                        i++;
                    }
                    
                    Intent i3 = new Intent("com.dpwebdev.handsetfree.NOTIFICATION_LISTENER_EXAMPLE");
                    i3.SetFlags(ActivityFlags.NewTask);
                    i3.PutExtra("notification_event", "===== Notification List ====");
                    NLSService.SendBroadcast(i3);


                }


            }

            
        }
    }
}