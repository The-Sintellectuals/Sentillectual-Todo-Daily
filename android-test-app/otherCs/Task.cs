﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android_test_app.otherCs
{
    public class Task
    {
        // ------------- Initialization -------------
        public int id { get; set; }
        public string TaskName { get; set; }

        public DateTime date { get; set; }

        public bool isSelected = false;

        public bool completed { get; set; }



        // ------------- Constructor -------------
        public Task (int id, string taskName, DateTime date)
        {
            this.id = id;
            this.TaskName = taskName;
            this.date = date;
        }



        // ------------- Other Functions -------------
        public void SetSelected(bool selected)
        {
            isSelected = selected;
        }

        public static List<Task> AllSelectedTasks(List<Task> taskList)
        {
            List<Task> selectedList = new List<Task>();
            for (int i = 0; i > taskList.Count; i++)
            {
                if (taskList[i].isSelected)
                {
                    selectedList.Add(taskList[i]);
                }
            }
            return selectedList;
        }

        public List<Task> childTask()
        {
            // temp code
            List<Task> tempSub = new List<Task> 
            { 
                new Task(20, "Teach my classmate", new DateTime(2002, 8, 10)),
                new Task(21, "Read the quipper guide", new DateTime(2002, 8, 10)),
                new Task(22, "Answer the quiz", new DateTime(2002, 8, 10)),
            };

            return tempSub;
        }
    }
}