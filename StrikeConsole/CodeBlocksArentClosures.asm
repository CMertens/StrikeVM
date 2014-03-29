PUSH	String:"Starting program";
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

START_BLOCK;	
	PUSH		String:"x";
	EXISTS;
	T_JUMP_LABEL	String:"Post-Define";
	PUSH		Int32:0;
	PUSH		String:"x";	
	SET;
	POP;
	LABEL		String:"Post-Define";
	PUSH		String:"x";
	GET;
	PUSH		String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	
	
	PUSH		String:"x";
	GET;
	PUSH		Int32:1;
	ADD;

	PUSH		String:"x";
	SET;
	GET;
	PUSH		Int32:100;
	MODULO;
	PUSH		Int32:0;
	EQ;
	T_JUMP_LABEL	String:"DropIntoDebug";

	PUSH		String:"z";
	GET;
	PUSH		String:"<IO.Print>";
	CALL_PRIMITIVE;

	RETURN_BLOCK;
	LABEL		String:"DropIntoDebug";
	DEBUG;
	RETURN_BLOCK;
END_BLOCK;

PUSH	String:"Outside Variable: ";
PUSH	String:"z";
SET;
POP;

PUSH	String:"MyBlock";
SET;
GET;

SET_RETURN_RELATIVE	Int32:0;
PUSH	String:"MyBlock";
GET;
CALL_BLOCK;

PUSH	String:"You'll never see this.";
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

HALT;