using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StrikeVM;
using StrikeAsm;

namespace StrikeConsole {
    class StrikeConsole {

        

        static void Main(string[] args) {
            if (args.Count() < 1 || System.IO.File.Exists(args[0]) == false) {
                Console.Write("Filename>");
                args = new String[1];
                args[0] = Console.ReadLine();
            }
            Assembler asm = new Assembler();
            String code = System.IO.File.ReadAllText(args[0]);
            List<Instruction> bc = asm.Compile(code, false);
            VirtualMachine vm = new VirtualMachine(bc);
            Primitives prims = new Primitives();

            vm.AddPrimitive("<IO.PrintLine>", prims.ConsoleWriteLine, new List<Value>() { Value.New(ValueTypes.ANY_TYPE) });
            vm.AddPrimitive("<IO.Print>", prims.ConsoleWrite, new List<Value>() { Value.New(ValueTypes.ANY_TYPE) });
            vm.AddPrimitive("<IO.ReadLine>", prims.ConsoleReadLine, new List<Value>());

            vm.AddPrimitive("<IO.File.OpenForWriting>", prims.OpenFileWrite, new List<Value>() { Value.New(ValueTypes.STRING) });
            vm.AddPrimitive("<IO.File.Write>", prims.WriteToFile, new List<Value>() { Value.New(ValueTypes.STRING), Value.New(ValueTypes.INT_32) });
            vm.AddPrimitive("<IO.File.Close>", prims.CloseFile, new List<Value>() { Value.New(ValueTypes.INT_32) });

            vm.Start();
            
            // We've actually run a full cycle here, but this
            // indicates 
            while (vm.CanBeRestarted || vm.IsAborted) {
                if (vm.IsDebugging || vm.IsAborted) {
                    Console.WriteLine("Debug mode at PC " + vm.ProgramCounter + " (" + vm.DebugText + ")");
                    bool debugMode = true;
                    while (debugMode) {
                        Console.Write(">");
                        String cmd = Console.ReadLine().ToUpperInvariant();
                        switch (cmd) {
                            case "HELP":
                            case "?":
                                Console.WriteLine(@"
COMMANDS:
CLO/SURES:  Dump closure stack.
C/ONT/INUE: Leave debug mode and continue execution.
G/LO/BALS: Dump global variables.
EN/V/IRONMENT: Dump the current environment.
E/RR/ORS: Dump errors, if any.
P/RIM/ITIVES: Dump primitives.
Q/UIT: Quit.
S/TACK: Dump the stack.
STAT/US: Dump status.
");
                                
                                break;
                            
                            case "Q":
                            case "QUIT":
                                System.Environment.Exit(111);
                                break;
                            case "STAT":
                            case "STATUS":
                                vm.DumpOperation(Console.Out);
                                break;
                            case "CLO":
                            case "CLOSURES":
                                vm.DumpClosures(Console.Out);
                                break;
                            case "ERRORS":
                            case "ERR":
                            case "E":
                                vm.DumpErrors(Console.Out);
                                break;
                            case "GLOBALS":
                            case "GLO":
                            case "G":
                                vm.DumpGlobals(Console.Out);
                                break;
                            case "V":
                            case "ENVIRONMENT":
                            case "ENV":
                                vm.DumpCurrentEnvironment(Console.Out);
                                break;
                            case "P":
                            case "PRIM":
                            case "PRIMITIVES":
                                vm.DumpPrimitives(Console.Out);
                                break;
                            case "STACK":
                            case "S":
                                vm.DumpStack(Console.Out);
                                break;
                            case "CONTINUE":
                            case "CONT":
                            case "C":
                                debugMode = false;
                                break;
                            default:
                                break;
                        }
                        
                        
                    }
                }
                vm.Run();
            }
            if (vm.IsAborted) {
                vm.DumpOperation(Console.Out);
                vm.DumpErrors(Console.Out);
                vm.DumpCurrentEnvironment(Console.Out);
                vm.DumpStack(Console.Out);
            }
        }

 
    }
}
