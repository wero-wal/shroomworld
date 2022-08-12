using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld_Console
{
    public class Menu
    {
        //---Enums---
        public enum SelectionStyle
        {
            Mouse, ArrowKeys
        }

        //---Constants---
        //---Accessors---
        public int SelectedItemIndex { get => _currently_selected_item_index; }

        //---Variables---
        private List<string> _items;
        private bool _first_item_is_header;
        private int _currently_selected_item_index;
        private SelectionStyle _selection_style;

        //---Constructors---
        public Menu(List<string> items, bool firstItemIsHeader, SelectionStyle selectionStyle, )
        {
            _items = items;
            _first_item_is_header = firstItemIsHeader;
            _currently_selected_item_index = 0;
            _selection_style = selectionStyle;
        }

        //---Methods---
        public void Run() // displays the menu and allows the user to select an item
        {
            switch (_selection_style)
            {
                case SelectionStyle.Mouse:
                    break;
                case SelectionStyle.ArrowKeys:
                    break;
            }
        }
        private void Run_Arrow_Key_Menu()
        {
            Game.Input input = Game.Get_Input();            
        }
        private void Show_Which_Item_Is_Selected()
        {
            // change the size or colour of the item
        } 
    }
}
