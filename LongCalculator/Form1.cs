using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LongCalculator
{
    public partial class divBtn : Form
    {
        public divBtn()
        {
            InitializeComponent();
        }

        private void sumBtn_Click(object sender, EventArgs e)
        {
            if (this.inputDataCorrect())
            {
                this.richTextBox1.Text = this.sum(this.inputNumber1.Text, this.inputNumber2.Text);
            }
            else
            {
                this.richTextBox1.Text = "Your numbers are incorrect! Enter two positive integers and try again!";
            }
        }

        private void multBtn_Click(object sender, EventArgs e)
        {
            if (this.inputDataCorrect())
            {
                this.richTextBox1.Text = this.multSchool(this.inputNumber1.Text, this.inputNumber2.Text);
            }
            else
            {
                this.richTextBox1.Text = "Your numbers are incorrect! Enter two positive integers and try again!";
            }
        }

        private bool inputDataCorrect()
        {
            if (this.inputNumber1.Text.Length < 1 || this.inputNumber2.Text.Length < 1)
            {
                return false;
            }
            else
            {
                this.inputNumber1.Text = this.normalizeInput(this.inputNumber1.Text);
                this.inputNumber2.Text = this.normalizeInput(this.inputNumber2.Text);

                for (int i = 0; i < this.inputNumber1.Text.Length; i++)
                {
                    if (!System.Char.IsDigit(this.inputNumber1.Text[i])) return false;
                }
                for (int i = 0; i < this.inputNumber2.Text.Length; i++)
                {
                    if (!System.Char.IsDigit(this.inputNumber2.Text[i])) return false;
                }
            }            
            return true;
        }

        private String normalizeInput(String input)
        {
            int index = 0;
            while (input[index] == '0' && index < input.Length-1)
            {
                index++;
            }
            return input.Substring(index);
        }

        private int[] convertToArray(String number, int length)
        {
            int[] result = new int[length];
            for (int i = 0; i < length; i++)
            {
                if (i < number.Length)
                {
                    result[i] = Int32.Parse(number[number.Length - i - 1].ToString());
                }
                else
                {
                    result[i] = 0;
                }
            }
            return result;
        }

        private String sum(String number1, String number2)
        {
            int newLength;
            if (number1.Length > number2.Length)
            {
                newLength = number1.Length + 1;
            }
            else
            {
                newLength = number2.Length + 1;
            }

            int[] num1 = this.convertToArray(number1, newLength);
            int[] num2 = this.convertToArray(number2, newLength);
            String result = "";
            int temp = 0;

            for (int i = 0; i < newLength; i++)
            {
                result = ((num1[i] + num2[i] + temp) % 10).ToString() + result;
                temp = (num1[i] + num2[i] + temp) / 10;
            }

            if (result[0] == '0')
            {
                result = result.Substring(1);
            }
            return result;
        }
                        
        private String multByDigit(String number, int digit)
        {
            int newLength = number.Length + 1;
            int[] num = this.convertToArray(number, newLength);
            String result = "";

            int temp = 0;

            for (int i = 0; i < newLength; i++)
            {
                result = ((num[i] * digit + temp) % 10).ToString() + result;
                temp = (num[i] * digit + temp) / 10;
            }

            if (result[0] == '0')
            {
                result = result.Substring(1);
            }
            return result;
        }

        private String multSchool(String number1, String number2)
        {
            int[] num2 = this.convertToArray(number2, number2.Length);
            String result = this.multByDigit(number1, num2[0]);
            String temp;
            String shift = "0";

            for (int i = 1; i < number2.Length; i++)
            {
                temp = this.multByDigit(number1, num2[i]);
                result = this.sum(result, temp + shift);
                shift += "0";
            }

            if (result[0] == '0')
            {
                result = result.Substring(1);
            }
            return result;
        }

    }
}
