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
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].Sprite.SetPosition(_startPosition + (_buttonSize * i) + (Vector2.UnitY * i * _distanceBetweenEachMenuItem));
            }
        }
    }
}
