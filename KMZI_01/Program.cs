using System;
using Functions;

namespace KMZI_01
{
    class Program
    {
        static void Main(string[] args)
        {
            char point;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("[---CAESAR CIPHER---]");
                Console.WriteLine("MENU");
                Console.Write("1 - file encryption\n2 - file decryption\n3 - key fitting for decryption of the specific message (variant 9)\n4 - exit\nSelect the option (press the proper number): ");
                point = Console.ReadKey().KeyChar;
                Console.Clear();
                if (point == '1' || point == '2' || point == '3' || point == '4')
                {                    
                    switch (point)
                    {
                        case '1':
                            Caesar.Encryption();
                            Console.Clear();
                            break;
                        case '2':
                            Caesar.Decryption();
                            Console.Clear();
                            break;
                        case '3':
                            Caesar.Spec();
                            Console.Clear();                          
                            break;
                        case '4':
                            flag = false;
                            break;
                    }
                }
                else Console.WriteLine("Input data is incorrect! You entered \'{0}\'", point);
            }           
        }
    }
}
