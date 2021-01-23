using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteadyHandVersion5
{
    //Пользовательский элемент управления. Используется в качестве мишени в ShootingRange.
    public class Target : Control
    {
        //Функция bool, проверяющая, попадает ли курсор при нажатии в вписанную в элемент окружность.
        //Для проверки используется уравнение окружности. Требуется, чтобы X и -X в нём были меньше другой части уравнения. В качестве X и Y используются xRelated и yRelated, являющиеся координатами курсора относительно центра элемента.
        public bool CircleHit(MouseEventArgs e)
        {
            int xMiddle = Height / 2;
            int yMiddle = Width / 2;
            int xRelated = e.X - xMiddle;
            int yRelated = e.Y - yMiddle;
            return (xRelated <= Math.Sqrt(((Height / 2) * (Height / 2)) - ((yRelated) * (yRelated))) &&
                -xRelated <= Math.Sqrt(((Height / 2) * (Height / 2)) - ((yRelated) * (yRelated))));
        }

        //В этой версии функции указывается, насколько радиус окружности уменьшен. Радиус умножается на входное число.
        public bool CircleHit(MouseEventArgs e, double scale)
        {
            double scaledHeight = Height * scale;
            double xMiddle = Height / 2;
            double yMiddle = Width / 2;
            double xRelated = e.X - xMiddle;
            double yRelated = e.Y - yMiddle;
            return (xRelated <= Math.Sqrt(((scaledHeight / 2) * (scaledHeight / 2)) - ((yRelated) * (yRelated))) &&
                -xRelated <= Math.Sqrt(((scaledHeight / 2) * (scaledHeight / 2)) - ((yRelated) * (yRelated))));
        }

        //При создании объекта ему задаётся размер, поддержка прозрачности и прозрачный цвет.
        public Target()
        {
            Size = new Size(80, 80);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }
    }
}