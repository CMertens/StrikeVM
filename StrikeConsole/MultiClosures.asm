START_BLOCK;
	START_CLOSURE	Int32:0,String:"",String:"";
		SET_VAR		String:"uid";		
		POP;
		SET_VAR		String:"LastName";	
		POP;
		SET_VAR		String:"FirstName";	
		POP;
		RETURN_CLOSURE;
	END_CLOSURE;
	RETURN_BLOCK;
END_BLOCK;
SET_VAR			String:"PersonFactory";
GET;
SET_RETURN_RELATIVE	Int32:2;
CALL_BLOCK;
SET_VAR			String:"Person_1";
POP;
PUSH			Int32:111;			** Argument 0
PUSH			String:"Smith";		** Argument 1
PUSH			String:"John";		** Argument 2
GET_VAR			String:"Person_1";
SET_RETURN_RELATIVE	Int32:2;
CALL_CLOSURE;
DEBUG;
HALT;
