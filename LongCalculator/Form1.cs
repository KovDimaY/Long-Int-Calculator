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
                //this.richTextBox1.Text = this.multSchool(this.inputNumber1.Text, this.inputNumber2.Text);
                this.richTextBox1.Text = this.multKaratsuba(this.inputNumber1.Text, this.inputNumber2.Text);
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
            while (input[index] == '0' && index < input.Length - 1)
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

        private String zeros(int number)
        {
            return new string('0', number);
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
            
            return this.normalizeInput(result);
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
            
            return this.normalizeInput(result);
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
                        
            return this.normalizeInput(result);
        }

        private String multKaratsuba(String number1, String number2)
        {
            int minLength = 3;
            String result = "";
            if (number1.Length < minLength && number2.Length < minLength)
            {
                result = (Int32.Parse(number1) * Int32.Parse(number2)).ToString();
            }
            else if (number2.Length < minLength)
            {
                String a = number1.Substring(0, number1.Length / 2);
                String b = number1.Substring(number1.Length / 2);

                String an = this.multKaratsuba(a, number2) + this.zeros(number1.Length - number1.Length / 2);
                String bn = this.multKaratsuba(b, number2);
                result = this.sum(an, bn);
            }
            else if (number1.Length < minLength)
            {
                String c = number2.Substring(0, number2.Length / 2);
                String d = number2.Substring(number2.Length / 2);

                String cn = this.multKaratsuba(c, number1) + this.zeros(number2.Length - number2.Length / 2);
                String dn = this.multKaratsuba(d, number1);
                result = this.sum(cn, dn);
            } else
            {
                String a = number1.Substring(0, number1.Length / 2);
                String b = number1.Substring(number1.Length / 2);
                String c = number2.Substring(0, number2.Length / 2);
                String d = number2.Substring(number2.Length / 2);

                String ac = this.multKaratsuba(a, c) + this.zeros(number1.Length - number1.Length / 2 + 
                                                                number2.Length - number2.Length / 2);
                String ad = this.multKaratsuba(a, d) + this.zeros(number1.Length - number1.Length / 2);
                String cb = this.multKaratsuba(c, b) + this.zeros(number2.Length - number2.Length / 2);
                String bd = this.multKaratsuba(b, d);

                String acd = this.sum(ac, ad);
                String cbd = this.sum(cb, bd);
             
                result = this.sum(acd, cbd);
            }
            return this.normalizeInput(result);
        }

    }
}
