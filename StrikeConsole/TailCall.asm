
**************************************
** Tail call example (factorial computation)
**
** Demonstrates how to write a tail-recursive function 
** that doesn't blow the stack. 
**************************************

*** fact(UInt64 n, UInt64 product)
*** Remember that a code block inherits environments unless it's a closure or function,
*** so if this were defined as a block, the value of 'n' would step on the calling's environment's 'n'.
START_FUNCTION		UInt64:0,UInt64:0;
	*** Get function arguments
	PUSH		String:"n";
	SET;
	POP;
	PUSH		String:"product";
	SET;
	POP;

	*** If n < 2 then jump to the return function
	PUSH		String:"n";
	GET;
	PUSH		UInt64:2;
	LT;
	T_JUMP_LABEL	String:"DoneWithFactorial";

	**** n - 1
	PUSH		String:"n";
	GET;
	PUSH		UInt64:1;
	SUBTRACT;

	**** n x product
	PUSH		String:"n";
	GET;
	PUSH		String:"product";
	GET;
	MULTIPLY;
	TAIL_CALL;	* Call itself on top of the existing environment.

	*** Return to regular control flow.
	LABEL			String:"DoneWithFactorial";
	PUSH			String:"product";
	GET;
	RETURN_FUNCTION;
	
END_FUNCTION;


PUSH			String:"fact";	
SET;								** Save function as fact()
POP;								** Toss the variable name.


PUSH			UInt64:20;			
PUSH			String:"n";
SET;
POP;

*** Push function args (UInt64 n, UInt64 product)
PUSH			String:"n";			** n
GET;
PUSH			UInt64:1;			** Product, first call is 1
PUSH			String:"fact";
GET;

SET_RETURN_RELATIVE	Int32:2;
CALL_FUNCTION;
** We always get the last value from a function's stack, if it 
** exists, so the last total from fact() is at the top right now.

PUSH			String:"n";
GET;
PUSH			String:"! is ";
STRING_APPEND;
SWAP;
STRING_APPEND;
PUSH			String:"<IO.PrintLine>";
CALL_PRIMITIVE;

HALT;