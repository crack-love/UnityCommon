using System;
using UnityEngine;

/// <summary>
/// 2021-06-25 금 오후 3:47:58, 4.0.30319.42000, YONG-PC, Yong
/// </summary>
namespace CommonEditor
{
    public abstract class ToolbarItem 
    {
        bool m_isSelected;

        public abstract string Name 
        { 
            get; 
        }

        public bool IsSelected
        {
            internal set => m_isSelected = value;
            get => m_isSelected;
        }

        public abstract void OnGUI();

        public virtual void Active()
        {
        }

        public virtual void DeActive()
        {
        }
    }
}