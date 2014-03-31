START_BLOCK;
	START_CLOSURE	Int32:0,String:"",String:"";
		SET_VAR		String:"FirstName";	
		POP;
		SET_VAR		String:"LastName";	
		POP;
		SET_VAR		String:"uid";		
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
PUSH			String:"John";		** Argument 2
PUSH			String:"Smith";		** Argument 1
PUSH			Int32:111;			** Argument 0
GET_VAR			String:"Person_1";
SET_RETURN_RELATIVE	Int32:2;
CALL_CLOSURE;
DEBUG;
