PUSH	String:"";
PUSH	String:"x";
SET;
POP;
PUSH	String:"Type 'quit' to quit program.";
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

LABEL	String:"LoopForever";

	PUSH	String:">";
	PUSH	String:"<IO.Print>";
	CALL_PRIMITIVE;

	PUSH	String:"<IO.ReadLine>";
	CALL_PRIMITIVE;
	PUSH	String:"x";
	SET;
	GET;
	PUSH	String:"quit";
	EQ;
	T_JUMP_LABEL	String:"Stop";

	PUSH	String:"x";
	GET;
	PUSH	String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	JUMP_LABEL	String:"LoopForever";

LABEL		String:"Stop";
PUSH		String:"Got quit command.";
PUSH		String:"<IO.PrintLine>";
CALL_PRIMITIVE;
HALT;