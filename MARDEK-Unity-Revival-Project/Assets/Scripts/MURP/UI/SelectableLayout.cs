using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MURP.UI
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public abstract class SelectableLayout : MonoBehaviour
    {
        [SerializeField] int index = 0;
        int Index
        {
            get { return index; }
            set
            {
                index = (value + selectables.Length) % selectables.Length;
                UpdateSelected();
            }
        }
        protected Selectable[] selectables { get; private set; }
        GridLayoutGroup layout;

        private void OnValidate()
        {
            selectables = GetComponentsInChildren<Selectable>();
            layout = GetComponent<GridLayoutGroup>();
        }

        private void OnEnable()
        {
            UpdateSelected();
        }

        void UpdateSelected()
        {
            selectables[index].Select();
        }

        public void HandleMovementInput(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();
            if (value.x == 0)
                HandleVerticalInput(value.y);
            if (value.y == 0)
                HandleHorizontalInput(value.x);
        }
        void HandleVerticalInput(float value)
        {
            if (layout.constraint == GridLayoutGroup.Constraint.FixedRowCount)
                if (layout.constraintCount == 1)
                    return;
            if (value > 0)
                Index--;
            else
                Index++;
        }
        void HandleHorizontalInput(float value)
        {
            if (layout.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
                if (layout.constraintCount == 1)
                    return;
            if (value > 0)
                Index++;
            else
                Index--;
        }
    }
}