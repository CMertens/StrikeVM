using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public enum OpCodeTypes {
        /// <summary>
        /// Do nothing but increment the PC.
        /// </summary>
        NOP = 1,

        ADD = 2 ,
        SUBTRACT = 3,
        MULTIPLY = 4,
        DIVIDE = 5,
        MODULO = 6,            

        EQ = 20,
        NEQ = 21,
        LT = 22, 
        LTE = 23,
        GT = 24,
        GTE = 25,

        /// <summary>
        /// Pushes one or more values onto the stack, in LIFO format.
        /// (end) | a (...n) (end)
        /// </summary>
        PUSH = 50,

        /// <summary>
        /// Pops a value off the stack.
        /// </summary>
        POP = 51,

        /// <summary>
        /// 
        /// </summary>
        SWAP = 52,

        /// <summary>
        /// NOT WORKING
        /// </summary>
        DUPE = 53,

        /// <summary>
        /// Checks whether a variable exists
        /// </summary>
        EXISTS = 58,

        /// <summary>
        /// Checks the local environment for a variable.
        /// </summary>
        GET_LOCAL = 60,

        /// <summary>
        /// 
        /// </summary>
        SET = 61,

        /****************
        LET = 62,

        LETSET = 63,
        ****************/
        TYPESET = 64,
        /****************
        TYPELET = 65,

        TYPELETSET = 66,
        ****************/
        /// <summary>
        /// Attempts to get a variable in the current or all parent environments. Useful for closures.
        /// </summary>
        GET = 67,

        /// <summary>
        /// Gets a global (VM-level) variable value by name.
        /// </summary>
        GET_TOP = 68,

        /// <summary>
        /// Sets a global (VM-level) variable.
        /// </summary>
        SET_TOP = 69,

        /// <summary>
        /// Sets a global (VM-level) variable with type checking.
        /// </summary>
        TYPESET_TOP = 70,

        SET_NEAR = 71,

        TYPESET_NEAR = 72,


        DELETE = 73,

        /// <summary>
        /// Pushes boolean indicating whether the top two values on the stack are of the same type.
        /// </summary>
        TYPE_EQ = 80,
        
        /// <summary>
        /// Pushes boolean indicating whether the top two values on the stack are numerics.
        /// </summary>
        NUMERIC_EQ = 81,

        IS_TYPE = 82,
        
        MAKE_REF = 101,
        SET_REF = 102,
        GET_REF = 103,
        TYPESET_REF = 104,

        /// <summary>
        /// Creates an empty prototype in the current environment and pushes an ObjectReference back onto the stack.
        /// </summary>
        NEW = 110,

        /// <summary>
        /// Clones a prototype pointed to by an ObjectReference. Pushes the clone onto the stack.
        /// a (end) | b (end)
        /// (ObjectReference) (end) | (ObjectReference) (end)
        /// </summary>
        CLONE = 111,


        /// <summary>
        /// NOT IMPLEMENTED
        /// </summary>
        COPY = 112,

        /// <summary>
        /// Get a property on the current object or, if the object does not have the property, will attempt to
        /// find it on its parents. Will raise an error if the property doesn't exist anywhere in the 
        /// inheritance chain.
        /// </summary>
        GETPROP = 113,

        /// <summary>
        /// Set a property on the current object.
        /// </summary>
        SETPROP = 114,

        /// <summary>
        /// A type-safe property setter; if the variable is set on an object or its parents, this ensures
        /// that the types match.
        /// </summary>
        TYPESETPROP = 115,

        DELETEPROP = 116,

        GET_VAR = 117,

        SET_VAR = 118,

        TYPESET_VAR = 119,

        /// <summary>
        /// 
        /// </summary>
        LENGTH = 150,


        /// <summary>
        /// Appends a single value to the end of an array.
        /// (value) (array) (end) | (array) (end)
        /// </summary>
        APPEND = 200,

        /// <summary>
        /// Appends all the values of array_source to array_target.
        /// (array_source) (array_target) (end) | (array_target) (end)
        /// </summary>
        APPEND_RANGE = 201,

        /// <summary>
        /// Pops an array and index off the stack and returns the value at the index.
        /// (array) (index) (end) | (value) (end)
        /// </summary>
        ARRAY_GET = 202,

        /// <summary>
        /// 
        /// (value) (array) (index) (end) | (array) (end)
        /// </summary>
        ARRAY_SET = 203,

        STRING_SPLICE = 204,

        /// <summary>
        /// Pushes a return address onto the return stack, using arg[0] as a relative offset.
        /// </summary>
        SET_RETURN_RELATIVE = 500,
        SET_RETURN_ABSOLUTE = 501,
        SET_RETURN_LABEL = 502,

        RETURN_BLOCK = 510,

        /// <summary>
        /// Operates like a return, but instead of shifting to the parent for the current operating environment,
        /// pops off the top of the closure return stack in the vm.
        /// </summary>
        RETURN_CLOSURE = 511,

        START_BLOCK = 700,
        END_BLOCK = 701,

        START_CLOSURE = 702,
        END_CLOSURE = 703,

        CALL_BLOCK = 750,
        CALL_PRIMITIVE = 751,
        CALL_CLOSURE = 752,
        

        /// <summary>
        /// Uses a string arg[0] to indicate a code location that can be label-jumped to. Label positions are cached
        /// at VM start.
        /// </summary>
        LABEL = 554,

        /// <summary>
        /// Jumps to an absolute position in the bytecode.
        /// </summary>
        JUMP_ABSOLUTE = 555,

        /// <summary>
        /// Jumps to the labeled location.
        /// </summary>
        JUMP_LABEL = 556,

        /// <summary>
        /// Jumps to a relative position in the bytecode.
        /// </summary>
        JUMP_RELATIVE = 557,

        /// <summary>
        /// Does the same thing as above, but branches on True or False booleans at the top of the stack.
        /// </summary>
        T_JUMP_ABSOLUTE = 558,
        T_JUMP_LABEL = 559,
        T_JUMP_RELATIVE = 560,
        F_JUMP_ABSOLUTE = 561,
        F_JUMP_LABEL = 562,
        F_JUMP_RELATIVE = 563,

        /// <summary>
        /// 
        /// </summary>
        CLOCK_UTC_TICKS = 600,

        /// <summary>
        /// Tells the VM to break into debug mode.
        /// </summary>
        DEBUG = 9977,

        /// <summary>
        /// Indicates an explicit raise of an error condition. If arg0 exists, it will be raised as an error.
        /// </summary>
        ABORT = 9988,

        /// <summary>
        /// Indicates a correct halt of the program.
        /// </summary>
        HALT = 9999

    }
}
