using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormaPoloneza
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int PrioritateOp(char c)
        {
            switch (c)
            {
                case '+': return 1;
                case '-': return 1;
                case '*': return 2;
                case '/': return 2;
                case '^': return 3;
                default: return 0;

            }

        }
        public bool IsParanteza(char c)
        {
            if (c == '(' || c == ')')
                return true;
            return false;

        }
        public String FormaPoloneza(String Expresie)
        {

            int l = Expresie.Length;
            int i = 0;
            Stack<Char> Operatori = new Stack<Char>();
            StringBuilder FP = new StringBuilder();
            while (i < l)
            {
                if (Expresie[i] == '(')
                {
                    Operatori.Push('(');
                    i++;
                }
                else
                if (Expresie[i] == ')')
                {
                    while (Operatori.Count > 0 && Operatori.Peek() != '(')        
                        FP.Append(Operatori.Pop());      
                    i++;
                    Operatori.Pop();
                }
                else
                if (PrioritateOp(Expresie[i]) == 0)
                {
                    while (PrioritateOp(Expresie[i]) == 0 && Expresie[i]!=')' && Expresie[i]!='(')
                    {
                        FP.Append(Expresie[i]);
                        i++;
                        if (i == l)
                            break;
                    }
                    
                    FP.Append(' ');

                }
                else
                if (PrioritateOp(Expresie[i]) != 0)
                {
                    while (Operatori.Count > 0 && (char)Operatori.Peek() != '(' && PrioritateOp(Expresie[i]) < PrioritateOp((char)Operatori.Peek()))
                    { 
                        char k = (char)Operatori.Pop();
                        FP.Append(k);
                    }
                   
                    Operatori.Push(Expresie[i]);
                    i++;
                }
            }
            while (Operatori.Count > 0)
                FP.Append(Operatori.Pop());
            return FP.ToString();

        }
        public double Ordonata(String Expresie, double x)
        {
            String Fun = FormaPoloneza(Expresie);
            Stack<Double> Functie = new Stack<double>();
            int n = Fun.Length;
            for(int i=0; i<n;)
            {
                if (Char.IsDigit(Fun[i]) == true)
                {
                    double nr = 0;
                    while ((Char.IsDigit(Fun[i]) == true))
                    {
                        nr = nr * 10 + Fun[i] - '0';
                        i++;
                    }
                    Functie.Push(nr);
                }

                else
                if (Fun[i] == 'x')
                {
                    Functie.Push(x);
                    i++;
                }

                else
                if (Fun[i] == ' ')
                    i++;

                else
                if(PrioritateOp(Fun[i])!=0 && Fun[i]!='x' && Char.IsDigit(Fun[i])==false)
                {
                    if(Fun[i]=='^')
                    {
                        double t1, t2;
                        t1 = Functie.Pop();
                        t2 = Functie.Pop();
                        Functie.Push(Math.Pow(t2,t1));
                    }
                    else 
                    if(Fun[i]=='+')
                    {
                        double t1, t2;
                        t1 = Functie.Pop();
                        t2 = Functie.Pop();
                        Functie.Push(t1+t2);
                    }
                    else
                    if(Fun[i] == '-')
                    {
                        double t1, t2;
                        t1 = Functie.Pop();
                        t2 = Functie.Pop();
                        Functie.Push(t2-t1);
                    }
                    else
                    if(Fun[i] == '*')
                    {
                        double t1, t2;
                        t1 = Functie.Pop();
                        t2 = Functie.Pop();
                        Functie.Push(t2*t1);
                    }
                    else
                    if (Fun[i] == '/')
                    {
                        double t1, t2;
                        t1 = Functie.Pop();
                        t2 = Functie.Pop();
                        Functie.Push((double)t2/t1);
                    }
                    i++;
                }
            }
            double rezultat = Functie.Pop();
            return rezultat;   
        }
            
  

        private void button1_Click(object sender, EventArgs e)
        {
            String Expr = textBox_expr.Text.ToString();
            String Rezultat = FormaPoloneza(Expr);
            // label1.Text = Rezultat;
            double x = Convert.ToDouble(textBox_x.Text);
            double rez = Ordonata(Expr, x);
            label1.Text = rez.ToString();

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
