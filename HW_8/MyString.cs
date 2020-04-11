using System;
using System.Collections.Generic;
using System.Text;

namespace HW_8
{
    class MyString
    {
        public char[] Text { get; private set; }
        public int Length => Text.Length;

        /// <summary>
        /// Initiliazed string by array
        /// </summary>
        /// <param name="value"></param>
        public MyString(char[] value)
        {
            Text = new char[value.Length];

            for (int i = 0; i < Length; i++)
            {
                Text[i] = value[i];
            }
        }

        /// <summary>
        /// Initiliazed string by specifed symbol repeat specifed number of time
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="count"></param>
        public MyString(char ch, int count)
        {
            Text = new char[count];

            for (int i = 0; i < count; i++)
            {
                Text[i] = ch;
            }
        }

        /// <summary>
        /// Initiliazed string by string
        /// </summary>
        /// <param name="value"></param>
        public MyString(ReadOnlySpan<char> value)
        {
            Text = new char[value.Length];

            for (int i = 0; i < Length; i++)
            {
                Text[i] = value[i];
            }
        }

        public char this[int index]
        {
            get
            {
                while (index < 0) index += Length;
                while (index >= Length) index -= Length;

                return Text[index];
            }
        }

        public override string ToString()
        {
            string res = "";
            for (int i = 0; i < Length; i++)
            {
                res += Text[i];
            }
            return res;
        }

        /// <summary>
        /// Содержится ли подстрока в строке
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool Contains(string str)
        {
            for (int i = 0; i < Length; i++)
            {
                if (str.Length + i > Length) return false;
                bool wrong = false;
                for (int j = 0; j < str.Length; j++)
                {
                    if (Text[i + j] != str[j])
                    {
                        wrong = true;
                        break;
                    }
                }
                if (wrong)
                {
                    continue;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Поиск первого вхождения в строку символа
        /// Возвращает -1, если нет вхождения
        /// </summary>
        /// <returns>-1 если нет вхождения</returns>
        public int IndexOf(char c)
        {
            return IndexOf(c, 0);
        }

        /// <summary>
        /// Поиск первого вхождения в строку символа с позиции start
        /// Возвращает -1, если нет вхождения
        /// </summary>
        /// <returns>-1 если нет вхождения</returns>
        public int IndexOf(char c, int start)
        {
            int position = -1;
            if (start < 0 || start >= Length) return position;

            for (int i = start; i < Length; i++)
            {
                if (Text[i] == c)
                {
                    position = i;
                    break;
                }
            }
            return position;
        }

        /// <summary>
        /// Поиск первого вхождения в строку подстроки с позиции start
        /// Возвращает -1, если нет вхождения
        /// </summary>
        /// <returns>-1 если нет вхождения</returns>
        public int IndexOf(string str, int start)
        {
            int position = -1;
            if (start < 0 || start >= Length) return position;

            for (int i = start; i < Length; i++)
            {
                if (str.Length + i > Length) return position;
                bool wrong = false;
                for (int j = 0; j < str.Length; j++)
                {
                    if (Text[i + j] != str[j])
                    {
                        wrong = true;
                        break;
                    }
                }
                if (wrong)
                {
                    continue;
                }
                return i;
            }
            return position;
        }

        /// <summary>
        /// Поиск первого вхождения в строку подстроки
        /// Возвращает -1, если нет вхождения
        /// </summary>
        /// <returns>-1 если нет вхождения</returns>
        public int IndexOf(string str)
        {
            int position = -1;

            for (int i = 0; i < Length; i++)
            {
                if (str.Length + i > Length) return position;
                bool wrong = false;
                for (int j = 0; j < str.Length; j++)
                {
                    if (Text[i + j] != str[j])
                    {
                        wrong = true;
                        break;
                    }
                }
                if (wrong)
                {
                    continue;
                }
                return i;
            }
            return position;
        }

        public string IndexOfToString(int num)
        {
            if (num == -1) return "none";
            return num.ToString();
        }
    }
}
