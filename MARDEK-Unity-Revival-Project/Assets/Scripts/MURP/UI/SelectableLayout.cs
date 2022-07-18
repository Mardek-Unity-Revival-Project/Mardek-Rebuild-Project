using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MURP.UI
{
    [RequireComponent(typeof(GridLayoutGroup), typeof(InputReader))]
    public class SelectableLayout : MonoBehaviour
    {
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] int numFittingEntries;
        int currentScrollIndex = 0;

        int index = 0;
        int Index
        {
            get
            {
                if (Selectables.Count == 0)
                    return 0;
                return (index + Selectables.Count) % Selectables.Count;
            }
            set
            {
                if (Selectables.Count == 0)
                    index = 0;
                else
                    index = (value + Selectables.Count) % Selectables.Count;
            }
        }

        Selectable[] selectables;
        List<Selectable> Selectables
        {
            get
            {
                List<Selectable> returnList = new List<Selectable>();
                foreach (var s in selectables)
                    if (s && s.gameObject.activeSelf)
                        returnList.Add(s);
                return returnList;
            }
        }
        Selectable currentlySelected = null;
        
        GridLayoutGroup layout;
        InputReader input;

        private void Awake()
        {
            selectables = GetComponentsInChildren<Selectable>();
            layout = GetComponent<GridLayoutGroup>();
            input = GetComponent<InputReader>();
        }

        private void OnEnable()
        {
            UpdateSelectionAtIndex(false);
            if(input)
                input.enabled = true;
        }

        private void OnDisable()
        {
            if (input)
                input.enabled = false;            
        }

        void UpdateSelectionAtIndex(bool playSFX = true)
        {
            if (currentlySelected)
                currentlySelected.Deselect();
            if (Selectables.Count == 0)
                return;
            currentlySelected = Selectables[Index];
            currentlySelected.Select(playSFX);
            if (numFittingEntries > 0 && scrollRect != null)
            {
                int desiredScrollIndex = Index / layout.constraintCount;
                if (desiredScrollIndex - currentScrollIndex >= numFittingEntries) SetScrollIndex(1 + desiredScrollIndex - numFittingEntries);
                if (desiredScrollIndex - currentScrollIndex < 0) SetScrollIndex(desiredScrollIndex);
            }
        }

        void SetScrollIndex(int newScrollIndex)
        {
            currentScrollIndex = newScrollIndex;
            int numTotalEntries = Selectables.Count / layout.constraintCount;
            if (Selectables.Count % layout.constraintCount != 0) numTotalEntries += 1;

            int numNonFittingEntries = numTotalEntries - numFittingEntries;
            float scrollAmount = newScrollIndex / (float) numNonFittingEntries;
            Debug.Log("scrollAmount is " + scrollAmount);
            
            if (scrollRect.vertical) scrollRect.verticalNormalizedPosition = 1f - scrollAmount;
            else scrollRect.horizontalNormalizedPosition = scrollAmount;
        }

        public void RefreshSelectables()
        {
            this.currentlySelected = null;
            this.selectables = GetComponentsInChildren<Selectable>();
            this.UpdateSelectionAtIndex(false);
        }

        public void HandleMovementInput(InputAction.CallbackContext ctx)
        {
            if (enabled == false)
                return;
            var value = ctx.ReadValue<Vector2>();
            if (value.x == 0)
                HandleVerticalInput(value.y);
            if (value.y == 0)
                HandleHorizontalInput(value.x);
        }

        void HandleVerticalInput(float value)
        {
            if (layout.constraint == GridLayoutGroup.Constraint.FixedRowCount && layout.constraintCount == 1) return;

            if (layout.constraint == GridLayoutGroup.Constraint.FixedColumnCount && layout.constraintCount != 1)
            {
                if (value > 0) Index -= layout.constraintCount;
                else Index += layout.constraintCount;
            } else {
                if (value > 0) Index--;
                else Index++;
            }

            UpdateSelectionAtIndex();
        }

        void HandleHorizontalInput(float value)
        {
            if (layout.constraint == GridLayoutGroup.Constraint.FixedColumnCount && layout.constraintCount == 1) return;

            if (layout.constraint == GridLayoutGroup.Constraint.FixedRowCount && layout.constraintCount != 1)
            {
                if (value > 0) Index += layout.constraintCount;
                else Index -= layout.constraintCount;
            } else {
                if (value > 0) Index++;
                else Index--;
            }
            
            UpdateSelectionAtIndex();
        }
    }
}