using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public enum ButtonState { Zero, One, Two };


    public interface IPlataform
    {
        int numbOfButtons();
        void paintButton(int position, ButtonState buttonstate);
        void resetButton(int position);
        void paintAllButtons(ButtonState buttonstater);
        void resetAllButtons();
    }
}
