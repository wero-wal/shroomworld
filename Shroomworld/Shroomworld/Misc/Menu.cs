using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class Menu
    {
        // ----- Enums -----
        public enum IndexingOptions
        {
            Numbered,
            LowerCaseLettered,
            UpperCaseLettered,
            LowerCaseRomanNumeraled,
            UpperCaseRomanNumeraled,
            None,
            Count,
        }
        public enum SelectBy
        {
            Index,
            MouseOrCursor,
            Count,
        }

        // ----- Properties -----
        // ----- Fields -----
        private readonly string[] _items;

        private readonly Color _backgroundColour;
        private readonly Color _textColour;
        private readonly Color _selectedBackgroundColour;
        private readonly Color _selectedTextColour;

        private readonly Vector2 _topLeftOfMenu;
        private readonly Vector2 _bottomRightOfMenu;

        private readonly Key _confirmSelection;

        //---
        private SelectBy _selectBy;
        private IndexingOptions _indexingOption;
        private string _bullet;
        private string _characterToPutAfterIndexers;
        private int _lengthOfLongestIndex;

        private Action<string, Color, Color, Vector2, Vector2> _displayBox; // msg, bgCol, txtCol, startVector2, dimensions
        private Func<Vector2> Shroomworld.GetMousePosition;
        private Func<Key> _getInput;

        private Vector2 _dimensionsOfOneBox;
        private int _indexOfSelectedItem = 0;

        // ----- Constructors -----
        public Menu(string[] items,
            Color backgroundColour, Color textColour, Color? selectedBackgroundColour, Color? selectedTextColour,
            Vector2 topLeftOfMenu, Vector2? bottomRightOfMenu,
            Key confirmSelection,
            Action<string, Color, Color, Vector2, Vector2> displayBox,
            SelectBy selectBy = SelectBy.MouseOrCursor, IndexingOptions indexingOption = IndexingOptions.None)
        {
            _items = items;
            _lengthOfLongestIndex = items.Length.ToString().Length;

            _backgroundColour = backgroundColour;
            _textColour = textColour;
            _selectedBackgroundColour = selectedBackgroundColour ?? textColour;
            _selectedTextColour = selectedTextColour  ?? backgroundColour;

            _topLeftOfMenu = topLeftOfMenu;
            _bottomRightOfMenu = bottomRightOfMenu ?? topLeftOfMenu;

            _confirmSelection = confirmSelection;

            _displayBox = displayBox;
        }

        // ----- Methods -----
        public void DisplayMenu(IndexingOptions indexingOption, char? characterToPutAfterIndex = '.', char? bullet = null)
        {
            if (((indexingOption == IndexingOptions.LowerCaseLettered) || (indexingOption == IndexingOptions.UpperCaseLettered)) && (_items.Length > 26))
            {
                throw new ArgumentException("Can't use lettered indexing because the number of items is greater than the number of letters in the alphabet.");
            }

            _characterToPutAfterIndexers = ((characterToPutAfterIndex is null) ? "" : characterToPutAfterIndex.ToString()) + " ";
            _bullet = (bullet is null) ? null : (bullet.ToString() + " ");

            if (bullet is not null)
            {
                ApplyBullet();
            }
            if (indexingOption != IndexingOptions.None)
            {
                ApplyIndexingOption();
            }
        }
        public int SelectBox(SelectBy selectBy, out string selectedItem)
        {
            // get input
            // check if cursor is in box
            // select that box

            selectedItem = _items[_indexOfSelectedItem];
            return _indexOfSelectedItem;
        }

        private void DetermineDimensionsOfOneBox()
        {
            // get longest string in items
            // check ( against the topLeft and bottomRight of menu ) if it needs to be wrapped
            // determine the height and width from that
            // also check menuHeight / wrappedBoxHeight to see if it fits
        }
        private void ApplyBullet()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i] = _bullet + _items[i];
            }
        }
        private void ApplyIndexingOption()
        {
            switch (_indexingOption)
            {
                case IndexingOptions.Numbered:
                    for (int i = 0; i < _items.Length; i++)
                    {
                        InsertIndexer(i, (i + 1).ToString().PadLeft(_lengthOfLongestIndex));
                    }
                    break;

                case IndexingOptions.LowerCaseLettered:
                    for (int i = 0; i < _items.Length; i++)
                    {
                        InsertIndexer(i, Convert.ToChar('a' + i).ToString());
                    }
                    break;

                case IndexingOptions.UpperCaseLettered:
                    for (int i = 0; i < _items.Length; i++)
                    {
                        InsertIndexer(i, Convert.ToChar('A' + i).ToString());
                    }
                    break;

                case IndexingOptions.LowerCaseRomanNumeraled:
                    int longestNumeral = 0;
                    int current;
                    for (int i = 0; i < _items.Length; i++)
                    {
                        current = IntToRoman(i).ToString().Length;
                        if (current > longestNumeral)
                        {
                            longestNumeral = current;
                        }
                    }
                    for (int i = 0; i < _items.Length; i++)
                    {
                        InsertIndexer(i, IntToRoman(i).ToLower().PadLeft(longestNumeral));
                    }
                    break;

                case IndexingOptions.UpperCaseRomanNumeraled:
                    for (int i = 0; i < _items.Length; i++)
                    {
                        InsertIndexer(i, IntToRoman(i));
                    }
                    break;
            }
        }
        private void InsertIndexer(int itemIndex, string indexer)
        {
            _items[itemIndex] = _items[itemIndex].Insert(_bullet.Length, indexer + _characterToPutAfterIndexers);
        }
        private string IntToRoman(int number) // code from: https://www.c-sharpcorner.com/article/convert-numbers-to-roman-characters-in-c-sharp/
        {
            string romanResult = string.Empty;
            string[] romanLetters = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] romanNumbers = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

            int i = 0;
            while (number != 0)
            {
                if (number >= romanNumbers[i])
                {
                    number -= romanNumbers[i];
                    romanResult += romanLetters[i];
                }
                else
                {
                    i++;
                }
            }
            return romanResult;
        }
		
		// Interface
    }
}
