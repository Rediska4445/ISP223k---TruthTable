using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace truth_table
{
    public partial class Form1 : Form
    {
        // da v pizdu
        private BoolExpr expression1;
        private BoolExpr expression2;
        private bool error1 = false;
        private bool error2 = false;
        private bool errors = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Input1_TextChanged(object sender, EventArgs e)
        {
            checkInputs();
        }

        private void Input2_TextChanged(object sender, EventArgs e)
        {
            checkInputs();
        }

        private void checkInputs()
        {
            try
            {
                expression1 = new BoolExpr(Input1.Text);
                expression1.Solve();
            }
            catch (Exception) {}
        }

        private void GenBtn_Click(object sender, EventArgs e)
        {
        //    if (!validateInputs()) return;

            TruthTableView.Columns.Clear();
            TruthTableView.Rows.Clear();

            List<char> boolVars;

            boolVars = expression1.GetBoolVars();

            bool[,] inputTable = GenInputTable(boolVars.Count);

            foreach (char c in boolVars)
            {
                TruthTableView.Columns.Add(c.ToString(), c.ToString());
            }

            bool[] answer1 = new bool[inputTable.GetLength(0)];
            for (int i = 0; i < inputTable.GetLength(0); i++)
            {
                for (int j = 0; j < inputTable.GetLength(1); j++)
                {
                    expression1.SetValue(boolVars[j], inputTable[i, j]);
                }
                answer1[i] = expression1.Solve();
            }

            TruthTableView.Columns.Add("#1", "#1");
            for (int i = 0; i < inputTable.GetLength(0); i++)
            {
                TruthTableView.Rows.Add(getBoolStr(inputTable[i, 0]), getBoolStr(inputTable[i, 0]));
                for (int j = 1; j < inputTable.GetLength(1); j++)
                {
                    TruthTableView.Rows[i].Cells[j].Value = getBoolStr(inputTable[i, j]);
                }
                TruthTableView.Rows[i].Cells[TruthTableView.Rows[i].Cells.Count - 1].Value = getBoolStr(answer1[i]);
            }

            // U can introduce my head? Inside his regins empty and silence
            //label1.Text = "Это закон поглощения";                     label1.Text = "";

            bool law = false;

            for (int i = 0; i < Input1.Text.Length; i++)
            {
                try
                {
                    if (Input1.Text[i] == '(' && GetBoolVar(Input1.Text[i - 2]) && GetBoolVar(Input1.Text[i + 1])
                        && GetBoolExpressOperation(Input1.Text[i - 1].ToString(), Input1.Text[i + 2].ToString())
                        && Input1.Text[i - 2] == Input1.Text[i + 1])
                    {
                        law = true;
                    }
                } catch (Exception) { }
            }

            if (law) label1.Text = "Это закон поглощения";
            else label1.Text = ""; 
        }

        private bool GetBoolExpressOperation(string _0, string _1)
        {
            return _0.Equals("&") && _1.Equals("|")
                || _0.Equals("|") && _1.Equals("&");
        }

        private bool GetBoolVar(char text)
        {
            return (text != '&'
                    || text != '|'
                    || text != '='
                    || text != '\''
                    || text != '^'
                    || text != '('
                    || text != ')');
        }

        private string getBoolStr(bool b)
        {
            return b ? "1" : "0";
        }

        private bool validateInputs()
        {
            if (Input1.Text == "")
            {
                return false;
            }

            return true;
        }

        private bool[,] GenInputTable(int col)
        {
            bool[,] table;
            int row = (int)Math.Pow(2, col);

            table = new bool[row, col];

            int divider = row;

            for (int c = 0; c < col; c++)
            {
                divider /= 2;
                bool cell = false;
                for (int r = 0; r < row; r++)
                {
                    table[r, c] = cell;
                    if ((divider == 1) || ((r + 1) % divider == 0))
                    {
                        cell = !cell;
                    }
                }
            }

            return table;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Input1.Text = comboBox1.Text;
        }
    }

    class BoolExpr
    {
        private List<Token> Expression;

        public BoolExpr(string input)
        {
            Expression = Token.Tokenize(input);
            InsertANDs();

            Expression = ShuntingYard(Expression);
        }

        public bool Solve()
        {
            Stack<bool> stack = new Stack<bool>();

            foreach (Token t in Expression)
            {
                if (t.Category == Token.TokenCategory.Bool)
                {
                    stack.Push(t.BoolVal);
                }
                else if (t.Category == Token.TokenCategory.Op)
                {
                    if (t.ArgCount > stack.Count)
                    {
                        throw new Exception();
                    }

                    switch (t.Symbol)
                    {
                        case '|':
                            stack.Push(stack.Pop() | stack.Pop());
                            break;
                        case '=':
                            stack.Push(stack.Pop() == stack.Pop());
                            break;
                        case '&':
                            stack.Push(stack.Pop() & stack.Pop());
                            break;
                        case '\'':
                            stack.Push(!stack.Pop());
                            break;
                        default:
                            throw new Exception();
                    }
                }
            }

            if (stack.Count > 1) throw new Exception();

            return stack.Pop();
        }

        private List<Token> ShuntingYard(List<Token> list)
        {
            List<Token> outputQueue = new List<Token>();
            Stack<Token> operatorStack = new Stack<Token>();

            foreach (Token t in list)
            {
                if (t.Category == Token.TokenCategory.Bool)
                {
                    outputQueue.Add(t);
                }
                else if (t.Category == Token.TokenCategory.Op)
                {
                    while ((operatorStack.Count > 0) &&
                        (operatorStack.Peek().Category == Token.TokenCategory.Op) &&
                        (
                        (t.IsLeftAssoc && t.Precedence <= operatorStack.Peek().Precedence) ||
                        (!t.IsLeftAssoc && t.Precedence < operatorStack.Peek().Precedence)
                        ))
                    {
                        outputQueue.Add(operatorStack.Pop());
                    }
                    operatorStack.Push(t);
                }
                else if (t.Category == Token.TokenCategory.LeftParen)
                {
                    operatorStack.Push(t);
                }
                else if (t.Category == Token.TokenCategory.RightParen)
                {
                    try
                    {
                        while (operatorStack.Peek().Category != Token.TokenCategory.LeftParen)
                        {
                            outputQueue.Add(operatorStack.Pop());
                        }
                        operatorStack.Pop();
                    }
                    catch (InvalidOperationException)
                    {
                        throw new Exception();
                    }
                }
            }

            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek().Category == Token.TokenCategory.LeftParen ||
                    operatorStack.Peek().Category == Token.TokenCategory.RightParen)
                {
                    throw new Exception();
                }
                outputQueue.Add(operatorStack.Pop());
            }

            return outputQueue;
        }

        private void InsertANDs()
        {
            int a = 0; int b = 1;
            while (b < Expression.Count)
            {
                if ((Expression[a].Category == Token.TokenCategory.Bool &&
                     Expression[b].Category == Token.TokenCategory.LeftParen)
                     ||
                    (Expression[a].Category == Token.TokenCategory.Bool &&
                     Expression[b].Category == Token.TokenCategory.Bool)
                     ||
                    (Expression[a].Category == Token.TokenCategory.RightParen &&
                     Expression[b].Category == Token.TokenCategory.LeftParen)
                     ||
                    (Expression[a].Category == Token.TokenCategory.RightParen &&
                     Expression[b].Category == Token.TokenCategory.Bool)
                     || 
                    (Expression[a].Symbol == '\'' &&
                     Expression[b].Category == Token.TokenCategory.Bool)
                     ||
                    (Expression[a].Symbol == '\'' &&
                     Expression[b].Category == Token.TokenCategory.LeftParen)
                    )
                {
                    Expression.Insert(b, new Token('&', Token.TokenCategory.Op, 4));
                    a++; b++;
                }
                a++; b++;
            }
        }

        public List<char> GetBoolVars()
        {
            List<char> boolVars = new List<char>();
            foreach (var t in Expression)
            {
                if (t.Category == Token.TokenCategory.Bool && t.isVariable)
                {
                    boolVars.Add(t.Symbol);
                }
            }
            boolVars = boolVars.Distinct().ToList();
            boolVars.Sort();

            return boolVars;
        }

        public bool SetValue(char c, bool val)
        {
            bool success = false;
            char ch = Char.ToUpper(c);
            foreach (var t in Expression)
            {
                if (t.Symbol == ch)
                {
                    t.BoolVal = val;
                    success = true;
                }
            }

            return success;
        }
    }

    class Token
    {
        public enum TokenCategory
        {
            Undefined,
            Bool,
            Op,
            LeftParen,
            RightParen
        };

        public int Precedence { get; private set; }
        public TokenCategory Category { get; private set; }
        public char Symbol { get; private set; } 
        public bool IsLeftAssoc { get; private set; }
        public bool BoolVal { get; set; }
        public bool isVariable { get; private set; }
        public int ArgCount { get; private set; }

        public Token(char symbol,
            TokenCategory cat = TokenCategory.Undefined,
            int precedence = -1,
            int argCount = 0,
            bool isLeftAssoc = true)
        {
            if (cat == TokenCategory.Bool) isVariable = true;
            this.Category = cat;
            this.Symbol = symbol;
            this.Precedence = precedence;
            this.IsLeftAssoc = isLeftAssoc;
            this.ArgCount = argCount;
        }

        public Token(char symbol, bool boolVal)
        {
            this.Category = TokenCategory.Bool;
            this.BoolVal = boolVal;
            this.Symbol = symbol;
        }

        public override string ToString()
        {
            return String.Format("Category: {0}\nValue: {1}\nPrecedence: {2}\nAssociativity: {3}\n",
                Category.ToString(),
                Symbol == 0 ? BoolVal.ToString() : Symbol.ToString(),
                Precedence,
                IsLeftAssoc ? "left" : "right");
        }

        public static List<Token> Tokenize(string input)
        {
            List<Token> result = new List<Token>();
            input = Regex.Replace(input, @"\s+", "");
            foreach (char c in input)
            {
                if (Char.IsLetter(c))
                {
                    result.Add(new Token(Char.ToUpper(c), TokenCategory.Bool));
                    continue;
                }
                switch (c)
                {
                    case '0':
                        result.Add(new Token(c, false));
                        break;
                    case '1':
                        result.Add(new Token(c, true));
                        break;
                    case '(':
                        result.Add(new Token(c, TokenCategory.LeftParen));
                        break;
                    case ')':
                        result.Add(new Token(c, TokenCategory.RightParen));
                        break;
                    case '|':
                        result.Add(new Token(c, TokenCategory.Op, 2, 2));
                        break;
                    case '=':
                        result.Add(new Token(c, TokenCategory.Op, 3, 2));
                        break;
                    case '&':
                        result.Add(new Token(c, TokenCategory.Op, 4, 2));
                        break;
                    case '\'':
                        result.Add(new Token(c, TokenCategory.Op, 5, 1, false));
                        break;
                    default:
                        throw new Exception();
                }
            }
            return result;
        }
    }
}
