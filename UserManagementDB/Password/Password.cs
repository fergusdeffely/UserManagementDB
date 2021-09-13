using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * The password must meet the following strength requirements:
 *    (1) Minimum 8 characters
 *    (2) Maximum 16 characters
 *    (3) Have at least one upper case (A - Z) character
 *    (4) Have at least one lower case (a - z) character
 *    (5) Have at least one number (0 - 9)
 *    (6) Have at least one symbol or special character from this list:
 *           ~ ! @ # $ % ^ & * ( ) _ + - = { } | [ ] \ : " ; ' < > ? , . /
 *    (7) Must not contain whitespace such as spaces or tabs
 *    (8) Must be free of consecutive identical characters
 *
 * Any password entered by the user must conform to the requirements above.
 */

namespace UserManagement
{
    public class Password
    {
        private Password() { }

        private const int MIN_LENGTH = 8;
        private const int MAX_LENGTH = 16;

        private const int CHARACTER_COUNT = 0;
        private const int CAPITAL_COUNT = 1;
        private const int LOWERCASE_COUNT = 2;
        private const int NUMBER_COUNT = 3;
        private const int SYMBOL_COUNT = 4;
        private const int OTHER_COUNT = 5;
        private const int REPEAT_SEQUENCE_COUNT = 6;

        private static String errorMessages = "";

        public static bool IsPassword(String pw)
        {
            int[] counts = GetCharacterCounts(pw);
            bool valid = true;
            errorMessages = "";
            if (counts[CHARACTER_COUNT] < MIN_LENGTH)
            {
                valid = false;
                errorMessages = "Password must have 8 or more characters.";
            }
            else if (counts[CHARACTER_COUNT] > MAX_LENGTH)
            {
                valid = false;
                errorMessages = "Password must have 16 or fewer characters.";
            }
            else if (counts[CAPITAL_COUNT] < 1)
            {
                valid = false;
                errorMessages = "Password must have at least one upper case (A - Z) character.";
            }
            else if (counts[LOWERCASE_COUNT] < 1)
            {
                valid = false;
                errorMessages = "Password must have at least one lower case (a - z) character.";
            }
            else if (counts[NUMBER_COUNT] < 1)
            {
                valid = false;
                errorMessages = "Password must have at least one number (0 - 9).";
            }
            else if (counts[SYMBOL_COUNT] < 1)
            {
                valid = false;
                errorMessages = "Password must have at least one symbol or special character from this list:";
                errorMessages = errorMessages + "\n" + "*~! @ # $ % ^ & * ( ) _ + - = { } | [ ] \\ : \" ; \' < > ? , . /";
            }
            else if (counts[OTHER_COUNT] > 0)
            {
                valid = false;
                errorMessages = "Password must not contain whitespace such as spaces or tabs.";
            }
            else if (counts[REPEAT_SEQUENCE_COUNT] > 0)
            {
                valid = false;
                errorMessages = "Password must be free of consecutive identical characters.";
            }
            if (valid == true)
            {
                errorMessages = "Password is valid.";
            }
            return valid;
        }

        public static String GetLastErrorMessage()
        {
            return errorMessages;
        }

        private static int[] GetCharacterCounts(String pw)
        {
            int[] counts = new int[7];
            counts[CHARACTER_COUNT] = pw.Length;
            bool validChar = false;
            char current = '0';
            //char next = '0';
            for (int i = 0; i < counts[CHARACTER_COUNT]; i++)
            {
                validChar = true;
                current = pw[i];
                if (Char.IsUpper(current)) counts[CAPITAL_COUNT]++;
                else if (Char.IsLower(current)) counts[LOWERCASE_COUNT]++;
                else if (Char.IsDigit(current)) counts[NUMBER_COUNT]++;
                else if (IsSymbol(current)) counts[SYMBOL_COUNT]++;
                else
                {
                    counts[OTHER_COUNT]++;
                    validChar = false;
                }
                if (validChar && (i < (counts[CHARACTER_COUNT] - 1)))
                {
                    if (current == pw[i + 1]) counts[REPEAT_SEQUENCE_COUNT]++;
                }
            }
            return counts;
        }

        private static bool IsSymbol(char ch)
        {
            switch (ch)
            {
                case '~':
                case '!':
                case '@':
                case '#':
                case '$':
                case '%':
                case '^':
                case '&':
                case '*':
                case '(':
                case ')':
                case '_':
                case '+':
                case '-':
                case '=':
                case '{':
                case '}':
                case '|':
                case '[':
                case ']':
                case '\\':
                case ':':
                case '"':
                case ';':
                case '\'':
                case '<':
                case '>':
                case '?':
                case ',':
                case '.':
                case '/':
                    return true;
                default:
                    return false;
            }
        }

    }
}
