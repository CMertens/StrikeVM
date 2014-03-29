using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StrikeVM;

namespace StrikeConsole {
    class BuiltIns {
        static List<Instruction> CreateStackExample() {
            List<Instruction> bc = new List<Instruction>();
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.INT_32)));
            bc.Add(new Instruction(OpCodeTypes.POP));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.INT_32)));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.INT_32)));
            bc.Add(new Instruction(OpCodeTypes.POP));
            bc.Add(new Instruction(OpCodeTypes.POP));

            bc.Add(new Instruction(OpCodeTypes.SET_TOP));
            return bc;
        }
        static List<Instruction> CreateArrayExample() {
            List<Instruction> bc = new List<Instruction>();
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.ARRAY)));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "x")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.LETSET));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.GET_LOCAL));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "This")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "Ready to swap")));
            bc.Add(new Instruction(OpCodeTypes.SWAP));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "About to append")));
            bc.Add(new Instruction(OpCodeTypes.APPEND));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "Post-append")));
            bc.Add(new Instruction(OpCodeTypes.HALT));
            return bc;
        }

        static List<Instruction> CreateTestExample() {
            List<Instruction> bc = new List<Instruction>();
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.INT_32, 100)));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.FLOAT, 100)));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.EQ));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.HALT));
            return bc;
        }

        static List<Instruction> CreateJumpExample() {
            List<Instruction> bc = new List<Instruction>();
            bc.Add(new Instruction(OpCodeTypes.JUMP_LABEL, Value.New(ValueTypes.STRING, "JumpHere")));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "HIDDEN")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "This should never be seen.")));
            bc.Add(new Instruction(OpCodeTypes.LABEL, Value.New(ValueTypes.STRING, "JumpHere")));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "NOT HIDDEN")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.HALT));
            return bc;
        }

        static List<Instruction> CreateFuncExample() {
            List<Instruction> bc = new List<Instruction>();

            bc.Add(new Instruction(OpCodeTypes.START_BLOCK));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "This is a return value")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "Within function")));
            bc.Add(new Instruction(OpCodeTypes.RETURN_BLOCK));
            bc.Add(new Instruction(OpCodeTypes.END_BLOCK));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "Code block created")));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "MyFunction")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "Ready to letset")));
            bc.Add(new Instruction(OpCodeTypes.LETSET));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "LetSet")));
            bc.Add(new Instruction(OpCodeTypes.GET_LOCAL));
            bc.Add(new Instruction(OpCodeTypes.SET_RETURN_RELATIVE, Value.New(ValueTypes.INT_32, 2)));
            bc.Add(new Instruction(OpCodeTypes.CALL_BLOCK));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "x")));
            bc.Add(new Instruction(OpCodeTypes.LETSET));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.GET_LOCAL));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.HALT));
            return bc;
        }

        static List<Instruction> CreateTypeExample() {
            List<Instruction> bc = new List<Instruction>();

            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "Test")));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.INT_32, 123123)));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.TYPE_EQ));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            return bc;
        }

        static List<Instruction> CreateObjectExample() {
            List<Instruction> bc = new List<Instruction>();

            bc.Add(new Instruction(OpCodeTypes.NEW));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "MyObj")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.LETSET));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.GET_LOCAL));   // Replace the variable name with the ObjectReference
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.UINT_64, 1000000)));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.SWAP));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "Amount")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.SETPROP));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            return bc;
        }

        static List<Instruction> CreateSimpleExample() {
            List<Instruction> bc = new List<Instruction>();
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.INT_64, 100)));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.INT_32, 222)));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.ADD));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.INT_32, 78)));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.ADD));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.INT_32)));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "x")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.LET));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "let x :: <Int32>;")));
            bc.Add(new Instruction(OpCodeTypes.SET));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "set x = 400;")));
            bc.Add(new Instruction(OpCodeTypes.GET_LOCAL));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.DECIMAL, 15.0m)));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.DIVIDE));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.POP));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.NEW));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.OBJECT)));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "y")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "let y :: <object> = new <object>;")));
            bc.Add(new Instruction(OpCodeTypes.LET));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.SET));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.GET_LOCAL));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.CLONE));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "z")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.LETSET));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.GET_LOCAL));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "IsThisValue")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.SWAP));
            bc.Add(new Instruction(OpCodeTypes.DEBUG, Value.New(ValueTypes.STRING, "Swapping values for SETPROP")));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "MyProp")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.SETPROP));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.PUSH, Value.New(ValueTypes.STRING, "MyProp")));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.GETPROP));
            bc.Add(new Instruction(OpCodeTypes.DEBUG));
            bc.Add(new Instruction(OpCodeTypes.HALT));

            return bc;
        }
    }
}
