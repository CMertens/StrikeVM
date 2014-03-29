PUSH	String:"0123789";
PUSH	String:"x";
SET;
POP;
PUSH	String:"a";
PUSH	String:"x";
GET;
PUSH	Int32:0;
ARRAY_SET;
PUSH	String:"x";
SET;
GET;
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

PUSH	String:"b";
PUSH	String:"x";
GET;
PUSH	Int32:1;
ARRAY_SET;
PUSH	String:"x";
SET;
GET;
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

PUSH	String:"c";
PUSH	String:"x";
GET;
PUSH	Int32:2;
ARRAY_SET;
PUSH	String:"x";
SET;
GET;
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

PUSH	String:"456";
PUSH	String:"x";
GET;
PUSH	Int32:4;
STRING_SPLICE;
PUSH	String:"x";
SET;
GET;
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

PUSH	String:"012";
PUSH	String:"x";
GET;
PUSH	Int32:3;
STRING_SPLICE;
PUSH	String:"x";
SET;
GET;
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

