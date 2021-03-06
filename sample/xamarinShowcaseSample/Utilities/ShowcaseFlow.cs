﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using ShowcaseView.Interfaces;
using ShowcaseView.ShowcaseExtensions;
using System;
using System.Collections.Generic;

namespace xamarinShowcaseSample.Utilities
{
    class ShowcaseFlow
    {
        private List<View> focusViews = new List<View>();
        private Context context;

        /// <summary>
        /// Access the currently stored queue.
        /// </summary>
        public ShowCaseQueue Queue { get; set; }

        public ShowcaseFlow(Context context)
        {
            this.context = context;
        }

        public void Flow(List<View> focuses, TimeSpan autoDelaySpan)
        {
            focusViews = focuses;

            // initialise a showcase queue
            Queue = new ShowCaseQueue();

            int count = 0;

            foreach (var item in focusViews)
            {
                ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(context as Activity)
                .FocusOn(item)
                .CloseOnTouch(false)
                .BackgroundColor(Color.DarkRed)
                .FocusBorderColor(Color.White)
                .FocusBorderSize(15)
                .Title(count.ToString())
                .TitleGravity((int)(GravityFlags.Top|GravityFlags.Right))
                .FocusCircleRadiusFactor(1.5)
                .Build();

                Queue.Add(showcase);

                count++;
            }

            // set auto run delay of ten seconds if required
            Queue.AutoRunDelay = autoDelaySpan;

            // set an action once the queue completes.
            Queue.QueueCompleted += ShowCaseQueue_QueueCompleted;

            // Start the queue
            Queue.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowCaseQueue_QueueCompleted(object sender, EventArgs e)
        {
            Toast.MakeText(context, "Showcase flow completed", ToastLength.Long).Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowCaseQueue_FlowCompleted(object sender, EventArgs e)
        {
            //
        }
    }
}