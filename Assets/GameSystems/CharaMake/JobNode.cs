using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Parameter;
using System;
using SelectView;

namespace CharaMake {
    public class JobNode : MonoBehaviour, INode<Job> {
        /// <summary> 名前を表示するテキスト </summary>
        public Text name;

        /// <summary> 担当する職業 </summary>
        private Job job;

        public Job getElement() {
            return job;
        }

        /// <summary>
        /// 職業を設定します
        /// </summary>
        /// <param name="job">職業</param>
        public void setJob(Job job) {
            this.job = job;
            name.text = job.getName();
        }
    }
}
