﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Shroomworld
{
    internal class Menu
    {
        // ----- Fields -----
        private const float _distanceBetweenEachMenuItem = 10;

        private readonly Button[] _items;

        private readonly Color _buttonColour;
        private readonly Color _textColour;
        private readonly Color _highlightedButtonColour;
        private readonly Color _highlightedTextColour;

        private readonly Vector2 _startPosition; // top left
        private readonly Vector2 _buttonSize;

        private Action<string, Color, Color, Vector2, Vector2> _displayBox; // msg, bgCol, txtCol, startVector2, dimensions


        // ----- Constructors -----
        /// <summary>
        /// Create new instance of <see cref="Menu"/> and calculate button positions.
        /// </summary>
        /// <param name="items">all the clickable buttons you want in the menu</param>
        /// <param name="buttonColour">background colour of each button</param>
        /// <param name="textColour">foreground colour of each button</param>
        /// <param name="highlightedButtonColour">background colour of each button when the mouse is hovering over it. if null, set to <paramref name="textColour"/></param>
        /// <param name="highlightedTextColour">foreground colour of each button when the mouse is hovering over it. if null, set to <paramref name="buttonColour"/></param>
        /// <param name="topLeftOfMenu">position of the top left corner of the first button in the menu</param>
        /// <param name="buttonSize">size of each button (x = width, y = height)</param>
        public Menu(Button[] items,
            Color buttonColour, Color textColour, Color? highlightedButtonColour, Color? highlightedTextColour,
            Vector2 topLeftOfMenu, Vector2 buttonSize)
        {
            _items = items;

            _buttonColour = buttonColour;
            _textColour = textColour;
            _highlightedButtonColour = highlightedButtonColour ?? textColour;
            _highlightedTextColour = highlightedTextColour  ?? buttonColour;

            _startPosition = topLeftOfMenu;
            _buttonSize = buttonSize;
            
            SetButtonPositions();
        }


        // ----- Methods -----
        /// <summary>
        /// Display all the buttons in the menu. They cannot be interacted with yet.
        /// </summary>
        public void DisplayMenu()
        {
            for(int i = 0; i < _items.Length; i++)
            {
                _items[i].Draw(_buttonColour, _textColour);
            }
        }
        /// <summary>
        /// Allow the user to select a button by clicking on it using the left mouse button.
        /// Highlight the button that the mouse is hovering over.
        /// </summary>
        /// <returns>the index of the selected button</returns>
        public int UserSelectsButton()
        {
            Vector2 currentMousePosition;
            bool mouseDown;
            bool prevMouseDown;
            bool mouseHasBeenReleased;
            bool mouseIsOnAButton;
            int indexOfPreviouslyHighlightedButton;
            int indexOfHighlightedButton;
            
            while(true)
            {
                indexOfPreviouslyHighlightedButton = indexOfHighlightedButton;

                // todo: get mouse state

                mouseHasBeenReleased = (!mouseDown) && prevMouseDown;
                indexOfHighlightedButton = GetIndexOfButtonContainingMouse(Shroomworld.GetCurrentMousePosition());
                mouseIsOnAButton = indexOfHighlightedButton != -1;

                if (!mouseIsOnAButton)
                {
                    continue;
                }
                if(mouseHasBeenReleased)
                {
                    return indexOfHighlightedButton;
                }
                if (indexOfHighlightedButton != indexOfPreviouslyHighlightedButton)
                {
                    _items[indexOfHighlightedButton].Draw(_highlightedButtonColour, _highlightedTextColour);
                    _items[indexOfPreviouslyHighlightedButton].Draw(_buttonColour, _textColour);
                }
            }

            // local functions
            int GetIndexOfButtonContainingMouse(Vector2 mousePosition) // returns -1 if no box
            {
                int index;
                // todo: get index of button containing mouse
                // check if out of bounds of the menu
                // divide
                // check if in the space between two boxes
                return -1;
            }
        }

        private void SetButtonPositions()
        {
            Vector2 position = new Vector2();
            for (int i = 0; i < _items.Length; i++)
            {
                position.X = _startPosition.X;
                position.Y = _startPosition.Y + (_buttonSize.Y * i) + (_distanceBetweenEachMenuItem * i);
                _items[i].Sprite.SetPosition(position);
            }
        }
    }
}
