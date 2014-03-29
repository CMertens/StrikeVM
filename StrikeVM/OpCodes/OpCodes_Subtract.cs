using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrikeVM {
    public partial class OpCodes {
        internal static void Subtract(VirtualMachine vm, Instruction i) {
            Value b = vm.Stack.Shift();
            Value a = vm.Stack.Shift();

            if (a.IsNumeric() == false || b.IsNumeric() == false) {
                vm.RaiseError("Tried to add one or more non-numeric values");
                return;
            }
            Value v = new Value();
            switch (a.Type) {
                case ValueTypes.INT_16:
                    switch (b.Type) {
                        case ValueTypes.INT_16:
                            var val1 = a.Int16_Value - b.Int16_Value;
                            v.Type = ValueTypes.INT_32;
                            v.Int32_Value = val1;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_32:
                            var val2 = a.Int16_Value - b.Int32_Value;
                            v.Type = ValueTypes.INT_32;
                            v.Int32_Value = (Int32)val2;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_64:
                            var val3 = a.Int16_Value - b.Int64_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val3;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_16:
                            var val4 = a.Int16_Value - b.UInt16_Value;
                            v.Type = ValueTypes.INT_32;
                            v.Int32_Value = val4;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_32:
                            var val5 = a.Int16_Value - b.UInt32_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val5;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_64:
                            if (b.UInt64_Value <= Int64.MaxValue) {
                                var val6 = a.Int16_Value - (long)b.UInt64_Value;
                                v.Type = ValueTypes.INT_64;
                                v.Int64_Value = val6;
                                vm.Stack.Push(v);
                            } else {
                                vm.RaiseError("Overflow error");
                            }
                            return;
                        case ValueTypes.DECIMAL:
                            decimal val7 = a.Int16_Value - b.Decimal_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val7;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DOUBLE:
                            double val8 = a.Int16_Value - b.Double_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val8;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.FLOAT:
                            float val9 = a.Int16_Value - b.Float_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val9;
                            vm.Stack.Push(v);
                            return;
                    }
                    vm.RaiseError("Incommensurate value error");
                    break;
                case ValueTypes.INT_32:
                    switch (b.Type) {
                        case ValueTypes.INT_16:
                            var val1 = a.Int32_Value - b.Int16_Value;
                            v.Type = ValueTypes.INT_32;
                            v.Int32_Value = (Int32)val1;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_32:
                            var val2 = a.Int32_Value - b.Int32_Value;
                            v.Type = ValueTypes.INT_32;
                            v.Int32_Value = (Int32)val2;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_64:
                            var val3 = a.Int32_Value - b.Int64_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val3;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_16:
                            var val4 = a.Int32_Value - b.UInt16_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val4;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_32:
                            var val5 = a.Int32_Value - b.UInt16_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val5;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_64:
                            if (b.UInt64_Value <= Int64.MaxValue) {
                                var val6 = a.Int32_Value - (long)b.UInt64_Value;
                                v.Type = ValueTypes.INT_64;
                                v.Int64_Value = val6;
                                vm.Stack.Push(v);
                            } else {
                                vm.RaiseError("Overflow error");
                            }
                            return;
                        case ValueTypes.DECIMAL:
                            decimal val7 = a.Int32_Value - b.Decimal_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val7;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DOUBLE:
                            double val8 = a.Int32_Value - b.Double_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val8;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.FLOAT:
                            float val9 = a.Int32_Value - b.Float_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val9;
                            vm.Stack.Push(v);
                            return;
                    }
                    vm.RaiseError("Incommensurate value error");
                    break;
                case ValueTypes.INT_64:
                    switch (b.Type) {
                        case ValueTypes.INT_16:
                            var val1 = a.Int64_Value - b.Int16_Value;
                            v.Type = ValueTypes.INT_32;
                            v.Int32_Value = (Int32)val1;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_32:
                            var val2 = a.Int64_Value - b.Int32_Value;
                            v.Type = ValueTypes.INT_32;
                            v.Int32_Value = (Int32)val2;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_64:
                            var val3 = a.Int64_Value - b.Int64_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val3;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_16:
                            var val4 = a.Int64_Value - b.UInt16_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val4;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_32:
                            var val5 = a.Int64_Value - b.UInt32_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val5;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_64:
                            if (b.UInt64_Value <= Int64.MaxValue) {
                                var val6 = a.Int64_Value - (long)b.UInt64_Value;
                                v.Type = ValueTypes.INT_64;
                                v.Int64_Value = val6;
                                vm.Stack.Push(v);
                            } else {
                                vm.RaiseError("Overflow error");
                            }
                            return;
                        case ValueTypes.DECIMAL:
                            decimal val7 = a.Int64_Value - b.Decimal_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val7;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DOUBLE:
                            double val8 = a.Int64_Value - b.Double_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val8;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.FLOAT:
                            float val9 = a.Int64_Value - b.Float_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val9;
                            vm.Stack.Push(v);
                            return;
                    }
                    vm.RaiseError("Incommensurate value error");
                    break;
                case ValueTypes.UINT_16:
                    switch (b.Type) {
                        case ValueTypes.INT_16:
                            var val1 = a.UInt16_Value - b.Int16_Value;
                            v.Type = ValueTypes.INT_32;
                            v.Int32_Value = val1;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_32:
                            var val2 = a.UInt16_Value - b.Int32_Value;
                            v.Type = ValueTypes.INT_32;
                            v.Int32_Value = val2;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_64:
                            var val3 = a.UInt16_Value - b.Int64_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val3;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_16:
                            var val4 = a.UInt16_Value - b.UInt16_Value;
                            v.Type = ValueTypes.INT_32;
                            v.Int32_Value = val4;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_32:
                            var val5 = a.UInt16_Value - b.UInt32_Value;
                            v.Type = ValueTypes.UINT_32;
                            v.UInt32_Value = val5;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_64:
                            var val6 = a.UInt16_Value - b.UInt64_Value;
                            v.Type = ValueTypes.UINT_64;
                            v.UInt64_Value = val6;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DECIMAL:
                            decimal val7 = a.UInt16_Value - b.Decimal_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val7;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DOUBLE:
                            double val8 = a.UInt16_Value - b.Double_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val8;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.FLOAT:
                            float val9 = a.UInt16_Value - b.Float_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val9;
                            vm.Stack.Push(v);
                            return;
                    }
                    vm.RaiseError("Incommensurate value error");
                    break;
                case ValueTypes.UINT_32:
                    switch (b.Type) {
                        case ValueTypes.INT_16:
                            var val1 = a.UInt32_Value - b.Int16_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val1;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_32:
                            var val2 = a.UInt32_Value - b.Int32_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val2;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_64:
                            var val3 = a.UInt32_Value - b.Int64_Value;
                            v.Type = ValueTypes.INT_64;
                            v.Int64_Value = val3;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_16:
                            var val4 = a.UInt32_Value - b.UInt16_Value;
                            v.Type = ValueTypes.UINT_32;
                            v.UInt32_Value = val4;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_32:
                            var val5 = a.UInt32_Value - b.UInt32_Value;
                            v.Type = ValueTypes.UINT_32;
                            v.UInt32_Value = val5;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_64:
                            var val6 = a.UInt32_Value - b.UInt64_Value;
                            v.Type = ValueTypes.UINT_64;
                            v.UInt64_Value = val6;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DECIMAL:
                            decimal val7 = a.UInt32_Value - b.Decimal_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val7;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DOUBLE:
                            double val8 = a.UInt32_Value - b.Double_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val8;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.FLOAT:
                            float val9 = a.UInt32_Value - b.Float_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val9;
                            vm.Stack.Push(v);
                            return;
                    }
                    vm.RaiseError("Incommensurate value error");
                    break;
                case ValueTypes.UINT_64:
                    switch (b.Type) {
                        case ValueTypes.INT_16:
                            vm.RaiseError("Incommensurate value error: Int16 to UInt64");
                            return;
                        case ValueTypes.INT_32:
                            vm.RaiseError("Incommensurate value error: Int32 to UInt64");
                            return;
                        case ValueTypes.INT_64:
                            vm.RaiseError("Incommensurate value error: Int64 to UInt64");
                            return;
                        case ValueTypes.UINT_16:
                            var val1 = a.UInt64_Value - b.UInt16_Value;
                            v.Type = ValueTypes.UINT_64;
                            v.UInt64_Value = val1;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_32:
                            var val2 = a.UInt64_Value - b.UInt32_Value;
                            v.Type = ValueTypes.UINT_64;
                            v.UInt64_Value = val2;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_64:
                            var val3 = a.UInt64_Value - b.UInt64_Value;
                            v.Type = ValueTypes.UINT_64;
                            v.UInt64_Value = val3;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DECIMAL:
                            decimal val4 = a.UInt64_Value - b.Decimal_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val4;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DOUBLE:
                            decimal val5 = a.UInt64_Value - Convert.ToDecimal(b.Double_Value);
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val5;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.FLOAT:
                            decimal val6 = a.UInt64_Value - Convert.ToDecimal(b.Float_Value);
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val6;
                            vm.Stack.Push(v);
                            return;
                    }
                    vm.RaiseError("Incommensurate value error");
                    break;
                case ValueTypes.DECIMAL:
                    switch (b.Type) {
                        case ValueTypes.INT_16:
                            decimal val1 = a.Decimal_Value - b.Int16_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val1;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_32:
                            decimal val2 = a.Decimal_Value - b.Int32_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val2;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_64:
                            decimal val3 = a.Decimal_Value - b.Int64_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val3;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_16:
                            decimal val4 = a.Decimal_Value - b.UInt16_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val4;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_32:
                            decimal val5 = a.Decimal_Value - b.UInt32_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val5;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_64:
                            decimal val6 = a.Decimal_Value - b.UInt64_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val6;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DECIMAL:
                            decimal val7 = a.Decimal_Value - b.Decimal_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val7;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DOUBLE:
                            decimal val8 = a.Decimal_Value - Convert.ToDecimal(b.Double_Value);
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val8;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.FLOAT:
                            decimal val9 = a.Decimal_Value - Convert.ToDecimal(b.Float_Value);
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val9;
                            vm.Stack.Push(v);
                            return;
                    }
                    vm.RaiseError("Incommensurate value error");
                    break;
                case ValueTypes.FLOAT:
                    switch (b.Type) {
                        case ValueTypes.INT_16:
                            var val1 = a.Float_Value - b.Int16_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val1;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_32:
                            var val2 = a.Float_Value - b.Int32_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val2;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_64:
                            var val3 = a.Float_Value - b.Int64_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val3;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_16:
                            var val4 = a.Float_Value - b.UInt16_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val4;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_32:
                            var val5 = a.Float_Value - b.UInt32_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val5;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_64:
                            var val6 = a.Float_Value - b.UInt64_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val6;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DECIMAL:
                            var val7 = Convert.ToDecimal(a.Float_Value) - b.Decimal_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val7;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DOUBLE:
                            var val8 = Convert.ToDouble(a.Float_Value) - b.Double_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val8;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.FLOAT:
                            var val9 = a.Float_Value - b.Float_Value;
                            v.Type = ValueTypes.FLOAT;
                            v.Float_Value = val9;
                            vm.Stack.Push(v);
                            return;
                    }
                    vm.RaiseError("Incommensurate value error");
                    break;
                case ValueTypes.DOUBLE:
                    switch (b.Type) {
                        case ValueTypes.INT_16:
                            var val1 = a.Double_Value - b.Int16_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val1;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_32:
                            var val2 = a.Double_Value - b.Int32_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val2;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.INT_64:
                            var val3 = a.Double_Value - b.Int64_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val3;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_16:
                            var val4 = a.Double_Value - b.UInt16_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val4;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_32:
                            var val5 = a.Double_Value - b.UInt32_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val5;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.UINT_64:
                            var val6 = a.Double_Value - b.UInt64_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val6;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DECIMAL:
                            var val7 = Convert.ToDecimal(a.Double_Value) - b.Decimal_Value;
                            v.Type = ValueTypes.DECIMAL;
                            v.Decimal_Value = val7;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.DOUBLE:
                            var val8 = a.Double_Value - b.Double_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val8;
                            vm.Stack.Push(v);
                            return;
                        case ValueTypes.FLOAT:
                            var val9 = a.Double_Value - b.Float_Value;
                            v.Type = ValueTypes.DOUBLE;
                            v.Double_Value = val9;
                            vm.Stack.Push(v);
                            return;
                    }
                    vm.RaiseError("Incommensurate value error");
                    break;
            }
        }
    }
}
