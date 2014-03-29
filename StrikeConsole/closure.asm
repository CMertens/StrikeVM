PUSH	String:"Starting program";
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

START_CLOSURE;	
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
	RETURN_CLOSURE;
	LABEL		String:"DropIntoDebug";
	DEBUG;
	RETURN_CLOSURE;
END_CLOSURE;

PUSH	String:"MyClosure";
SET;
GET;

SET_RETURN_RELATIVE	Int32:0;
PUSH	String:"MyClosure";
GET;
CALL_CLOSURE;

PUSH	String:"You'll never see this.";
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

HALT;