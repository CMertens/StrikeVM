START_CLOSURE	Any;
	PUSH			String:"RecordId";
	EXISTS;
	T_JUMP_LABEL	String:"Cl_SkipSetup";
	PUSH			String:"RecordId";
	SET;
	GET;
	DUPE;
	TYPE_EQ			Int64:0;
	T_JUMP_LABEL	String:"Cl_EndOfSetup";
	ABORT			String:"Must call with Int64 type.";
	* Stop!
	LABEL			String:"Cl_SkipSetup";	* We have the arg lingering on the stack, so eat it. (For test 
											* purposes, we'll display it, but would normally pop.)
											* If we set the value, we don't have any value on the stack
											* (consumed the boolean in the jump), so we skip this part.
	PUSH			String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	LABEL			String:"Cl_EndOfSetup";
	* Print the saved RecordId
	PUSH			String:"RecordId";
	GET;
	PUSH			String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	RETURN_CLOSURE;
END_CLOSURE;
DUPE;
PUSH	Int64:111;
SWAP;
SET_RETURN_RELATIVE	Int32:2;
CALL_CLOSURE;
DEBUG	String:"First call";
PUSH	Int64:222;
SWAP;
SET_RETURN_RELATIVE	Int32:2;
CALL_CLOSURE;
DEBUG	String:"Second call";
HALT;