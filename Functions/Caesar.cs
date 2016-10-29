using System;
using System.IO; 

namespace Functions
{
    public static class Caesar
    {
        private static string filename, key;
        private static int key_val = 0;
        private static bool flag = true;
        public static void Encryption()
        {            
            do
            {
                Console.Write("Enter the file name to encrypt (with extension): ");
                filename = Console.ReadLine();
                if (!File.Exists(filename)) Console.WriteLine("File not found. Try again!");
            } while (!File.Exists(filename));

            flag = true;
            while (flag)
            { 
                Console.Write("Set the key (integer): ");
                key = Console.ReadLine();
                if (Int32.TryParse(key, out key_val))
                {
                    flag = false;
                    if (key_val < 0) key_val = 32 + key_val;
                }

                else
                {
                    Console.WriteLine("Key is an integer! Try again");
                    flag = true;
                }
            }
            
            Byte[] b = File.ReadAllBytes(filename); //program works with only characters that have code of Russian letters, for Bytes is 223 to 255
            byte tmp;
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] > 223)
                {
                    tmp = b[i];
                    b[i] += (byte)(key_val % 32);
                    if (b[i] < 224)
                        b[i] = (byte)(223 + (key_val % 32 - (255 - tmp)));
                }
            }
            File.WriteAllBytes(filename, b);            
        }
        public static void Decryption()
        {     
            do
            {
                Console.Write("Enter the file name to decrypt (with extension): ");
                filename = Console.ReadLine();
                if (!File.Exists(filename)) Console.WriteLine("File not found. Try again!");
            } while (!File.Exists(filename));

            flag = true;
            while (flag)
            {
                Console.Write("Set the key to decrypt the file (integer): ");
                key = Console.ReadLine();
                if (Int32.TryParse(key, out key_val))
                {
                    flag = false;
                    if (key_val < 0) key_val = 32 + key_val;
                }
                else
                {
                    Console.WriteLine("Key is an integer! Try again");
                    flag = true;
                }
            }

            Byte[] b = File.ReadAllBytes(filename);
            byte tmp;
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i] > 223)
                {
                    tmp = b[i];
                    b[i] -= (byte)(key_val % 32);
                    if (b[i] < 224)
                        b[i] = (byte)(255 - (key_val % 32 - (tmp - 223)));
                }
            }           
            File.WriteAllBytes(filename, b);
        }
        public static void Spec()
        {            
            do
            {            
                Console.Write("Enter the file name to decrypt the message (with extension): ");
                filename = Console.ReadLine();
                if (!File.Exists(filename)) Console.WriteLine("File not found. Try again!");
            } while (!File.Exists(filename));
            
            StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default);
            char ch, tmp;
            string orig_str = "", mod_str = "", file_str = "";
            while (!sr.EndOfStream)
            {
                ch = (char)sr.Read();
                orig_str += ch;
            }
            sr.Close();
            Console.WriteLine("Original word is \"{0}\"\nKEY - DECRYPTED WORD", orig_str);
            
            for (int j = 1; j < 32; j++)
            {
                for (int i = 0; i < orig_str.Length; i++)
                {
                    ch = orig_str[i];
                    if ((ch > 1071) && (ch < 1104))
                    {
                        tmp = ch;
                        ch -= (char)j;
                        if (ch < 1072)
                            ch = (char)(1103 - (j - (tmp - 1071)));
                        mod_str += ch;                        
                    }
                }
                file_str = file_str + j.ToString() + " - " + mod_str + " | ";
                Console.WriteLine("{0} - {1}", j, mod_str);
                mod_str = "";
            }

            char point;
            Console.Write("Would you like to save result to the file? Y/N ");
            point = Console.ReadKey().KeyChar;
            if (point == 'y' || point == 'Y')
            {
                File.WriteAllText("decrypted_words.txt", file_str);
                Console.WriteLine("\nFile \"decrypted_words.txt\" has been created (or re-written). Have a nice day!");
            }
            else Console.WriteLine("\nFile has not been created");
            Console.WriteLine("(press ENTER to continue)");
            Console.Read();
        }
    }
}
