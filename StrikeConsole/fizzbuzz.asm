PUSH	Int32:1;
PUSH	String:"x";
SET;
POP;
LABEL	String:"StartLoop";
PUSH	String:"x";
GET;
PUSH	Int32:100;
GT;
T_JUMP_LABEL	String:"EndLoop";

	LABEL	String:"Test Mod 3";
	PUSH	String:"x";
	GET;
	PUSH	Int32:3;
	MODULO;
	PUSH	Int32:0;
	EQ;
	F_JUMP_LABEL	String:"Test Mod 5";
	PUSH	String:"x";
	GET;
	PUSH	Int32:5;
	MODULO;
	PUSH	Int32:0;
	EQ;
	T_JUMP_LABEL	String:"Fizzbuzz";
	PUSH		String:"Fizz";
	PUSH		String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	JUMP_LABEL	String:"ReLoop";

	LABEL		String:"Fizzbuzz";
	PUSH		String:"FizzBuzz!";
	PUSH		String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	JUMP_LABEL	String:"ReLoop";	

	LABEL		String:"Test Mod 5";
	PUSH		String:"x";
	GET;
	PUSH		Int32:5;
	MODULO;
	PUSH		Int32:0;
	EQ;
	F_JUMP_LABEL	String:"Fallthrough";
	PUSH		String:"Buzz";
	PUSH		String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	JUMP_LABEL	String:"ReLoop";

	
	LABEL	String:"Fallthrough";
	PUSH	String:"x";
	GET;
	PUSH	String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	
	LABEL		String:"ReLoop";
	PUSH		String:"x";
	GET;
	PUSH		Int32:1;
	ADD;
	PUSH		String:"x";
	SET;
	POP;
	JUMP_LABEL	String:"StartLoop";
LABEL	String:"EndLoop";

HALT;