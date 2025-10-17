using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE_SHMUP
{
    public class ButtonHelper
    {
        private int _selected;
        public Button[] Buttons { get; set; }


        public void IncrementSelection()
        {
            _selected = GetSelected();

            Buttons[_selected].Selected = false;

            if (_selected == Buttons.Length - 1)
            {
                Buttons[0].Selected = true;
            }
            else
            {
                Buttons[_selected + 1].Selected = true;
            }
        }

        public void DecrementSelection()
        {
            _selected = GetSelected();

            Buttons[_selected].Selected = false;

            if (_selected == 0)
            {
                Buttons[Buttons.Length - 1].Selected = true;
            }
            else
            {
                Buttons[_selected - 1].Selected = true;
            }
        }

        public int GetSelected()
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                if (Buttons[i].Selected == true)
                {
                    _selected = i;
                    return i;
                }
            }
            return -1;
        }
    }
}
