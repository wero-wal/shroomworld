using System;
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
        private const float _distanceBetweenEachButton = 10;

        private readonly Button[] _items;

        private readonly Color _buttonColour;
        private readonly Color _textColour;
        private readonly Color _highlightedButtonColour;
        private readonly Color _highlightedTextColour;

        private readonly Vector2 _startPosition; // top left
        private readonly Vector2 _buttonSize;

        private Action<string, Color, Color, Vector2, Vector2> _displayBox; // msg, bgCol, txtCol, startVector2, dimensions


        // ----- Constructors -----
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
        public void DisplayMenu()
        {
            for(int i = 0; i < _items.Length; i++)
            {
                _items[i].Draw(_buttonColour, _textColour);
            }
        }
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
                // Update user inputs
                indexOfPreviouslyHighlightedButton = indexOfHighlightedButton;

                // todo: get mouse state

                mouseHasBeenReleased = (!mouseDown) && prevMouseDown;
                indexOfHighlightedButton = GetIndexOfButtonContainingMouse(mousePosition: Shroomworld.GetCurrentMousePosition());
                mouseIsOnAButton = indexOfHighlightedButton != -1;

                // Display
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
                    _items[indexOfHighlightedButton].Draw(buttonColour: _highlightedButtonColour, textColour: _highlightedTextColour);
                    _items[indexOfPreviouslyHighlightedButton].Draw(buttonColour: _buttonColour, textColour: _textColour);
                }
            }

            // Local Functions
            int GetIndexOfButtonContainingMouse(Vector2 mousePosition) // returns -1 if no box
            {
                const int DefaultReturnValue = -1;
                
                // Check if out of bounds of the menu
                if ((mousePosition.Y < _startPosition.Y) || (mousePosition.X < _startPosition.X)
                || (mousePosition.Y > (_startPosition.Y + (_buttonSize.Y + _distanceBetweenEachButton) * _items.Length))
                || (mousePosition.X > (_startPosition.X + _buttonSize.X)))
                {
                    return DefaultReturnValue;
                }

                // Calculate index
                int index = (int)(mousePosition.Y / (_buttonSize.Y + _distanceBetweenEachButton));

                // Check if in between two buttons
                float bottomOfButtonAtIndex = _startPosition.Y + (_buttonSize.Y * index) + (_distanceBetweenEachButton * (index - 1));
                if (mousePosition.Y > bottomOfButtonAtIndex)
                {
                    return DefaultReturnValue;
                }
                return index;
            }
        }

        private void SetButtonPositions()
        {
            Vector2 position = new Vector2();
            for (int i = 0; i < _items.Length; i++)
            {
                position.X = _startPosition.X;
                position.Y = _startPosition.Y + (_buttonSize.Y * i) + (_distanceBetweenEachButton * i);
                _items[i].Sprite.SetPosition(position);
            }
        }
    }
}
