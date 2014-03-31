using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StrikeVM;
using System.Net;
using System.IO;

namespace StrikeConsole {
    public class Primitives {
        public List<FileStream> files = new List<FileStream>();
        Random rand = new Random();

        

        internal byte[] GetBytes(string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        internal string GetString(byte[] bytes) {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public void GetRandom(VirtualMachine vm, Instruction i, List<Value> args) {            
            Value v = Value.New(ValueTypes.INT_32);
            v.Int32_Value = rand.Next(args[0].Int32_Value, args[1].Int32_Value);
            vm.Stack.Push(v);
        }

        public void OpenFileWrite(VirtualMachine vm, Instruction i, List<Value> args) {
            try {
                FileStream fs = File.Open(args[0].String_Value, FileMode.OpenOrCreate);
                files.Add(fs);
                vm.Stack.Push(Value.New(ValueTypes.INT_32, (Int32)(files.Count() - 1)));
            } catch (Exception e) {
                vm.RaiseError(e.Message);
            }
        }

        public void WriteToFile(VirtualMachine vm, Instruction i, List<Value> args) {
            byte[] toWrite = GetBytes(args[0].String_Value);
            try {
                FileStream fs = files[args[1].Int32_Value];
                fs.Write(toWrite, 0, toWrite.Count());
            } catch (Exception e) {
                vm.RaiseError(e.Message);
            }
            vm.Stack.Push(args[1]);
        }

        public void CloseFile(VirtualMachine vm, Instruction i, List<Value> args) {
            try {
                FileStream fs = files[args[0].Int32_Value];
                fs.Close();
            } catch (Exception e) {
                vm.RaiseError(e.Message);
            }
        }

        public void ConsoleWriteLine(VirtualMachine vm, Instruction i, List<Value> args) {
            Console.WriteLine(args[0].Get().ToString());
        }

        public void ConsoleWrite(VirtualMachine vm, Instruction i, List<Value> args) {
            Console.Write(args[0].Get().ToString());
        }

        public void ConsoleReadLine(VirtualMachine vm, Instruction i, List<Value> args) {
            String s = Console.ReadLine();
            Value v = Value.New(ValueTypes.STRING, s);
            vm.Stack.Push(v);
        }
    }
}
