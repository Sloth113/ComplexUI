using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CUI {
    //Tween manager can beused to dynamically load in tweens at run time. 
    //This uses resoures load (So having a resources folder and tweens) 
   //Singleton approach to ensure only one manager exists 
    public class TweensManager : ScriptableObject {
        private static TweensManager m_instance = null;
        private List<Tween> m_tweens;
        public static TweensManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = CreateInstance<TweensManager>();
                }
                return m_instance;
            }
        }

        public List<Tween> GetTweens()
        {
            m_tweens = new List<Tween>(Resources.LoadAll<Tween>("Tweens"));
            return m_tweens;
        }

        public void AddTween(Tween t)
        {
            m_tweens.Add(t);
        }

    }
}
