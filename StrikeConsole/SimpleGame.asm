*********************************************
*	Adventure Cave!
*	A very simple game for the Strike VM.
***********************************************
NOP;
*** Mob Object ***
START_CLOSURE	Int32:0;
	PUSH	String:"MobTypeId";
	EXISTS;
	T_JUMP_LABEL	String:"MobDoneWithSetup";

	*** Combat Method ***
	START_BLOCK	;

		RETURN_BLOCK;
	END_BLOCK;
	PUSH	String:"DoCombat";
	SET;

	* Constructor *
	PUSH	String:"MobTypeId";
	SET;
	GET;
	PUSH	Int32:0;
	EQ;
	T_JUMP_LABEL	String:"MobSetupImp";
	PUSH	String:"MobTypeId";
	GET;
	PUSH	Int32:1;
	T_JUMP_LABEL	String:"MobSetupSkeletoon";
	PUSH	String:"MobTypeId";
	GET;
	PUSH	Int32:2;
	T_JUMP_LABEL	String:"MobSetupZombro";
	PUSH	String:"MobTypeId";
	GET;
	PUSH	Int32:3;
	T_JUMP_LABEL	String:"MobSetupIllich";

	ABORT	String:"Impossible value in MobSetup";

	LABEL	String:"MobSetupImp";
		PUSH	String:"A Pushy Little Imp";
		PUSH	String:"MobName";
		SET;
		POP;
		JUMP_LABEL	String:"MobDoneWithSetup";
	LABEL	String:"MobSetupSkeletoon";
		PUSH	String:"A Rubber-Boned Skeletoon";
		PUSH	String:"MobName";
		SET;
		POP;
		JUMP_LABEL	String:"MobDoneWithSetup";
	LABEL	String:"MobSetupZombro";
		PUSH	String:"A Natty Light-Drinking Zombro";
		PUSH	String:"MobName";
		SET;
		POP;
		JUMP_LABEL	String:"MobDoneWithSetup";
	LABEL	String:"MobSetupIllich";
		PUSH	String:"DOMEKULUS the NECRODEMANDER!!!!!!";
		PUSH	String:"MobName";
		SET;
		POP;
		JUMP_LABEL	String:"MobDoneWithSetup";

	ABORT	String:"Impossible value in Mob Setup (How did I get here?)";

	LABEL	String:"MobDoneWithSetup";

	RETURN_CLOSURE;
END_CLOSURE;


**** create player ***
NEW;
PUSH	String:"PlayerData";
SET;
POP;	**** Wipe the stack ****

PUSH	String:"You are in a maze of twisty little passages, all alike.";
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;

***** Get player name ****
PUSH	String:"Your Name?>";
PUSH	String:"<IO.Print>";
CALL_PRIMITIVE;
PUSH	String:"<IO.ReadLine>";
CALL_PRIMITIVE;
PUSH	String:"PlayerData";
GET;
PUSH	String:"Name";
SETPROP;	**** SETPROP calls value,refc,propname,end from the stack *****

**** SETPROP leaves the actual object reference on the stack, not the variable name. *****
PUSH	String:"Name";
GETPROP;
PUSH	String:", welcome to ADVENTURE CAVE!";
PUSH	Int32:0;
STRING_SPLICE;
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;


***** Set up character class *****
PUSH	Int32:0;
PUSH	Int32:4;
PUSH	String:"<Random.GetInt>";
CALL_PRIMITIVE;
PUSH	String:"1";	**** We'll use numbers to denote temporary registers. ****
SET;
GET;
PUSH	String:"PlayerData";
GET;
PUSH	String:"Type";
SETPROP;
POP;	**** Clear it off the stack ****

**** Grab the variable and do some basic comparisons for char class *****
PUSH	String:"1";
GET;
PUSH	Int32:0;
EQ;
T_JUMP_LABEL	String:"BerserkerCharDesc";
PUSH	String:"1";
GET;
PUSH	Int32:1;
EQ;
T_JUMP_LABEL	String:"DuelistCharDesc";
PUSH	String:"1";
GET;
PUSH	Int32:2;
EQ;
T_JUMP_LABEL	String:"RogueCharDesc";
PUSH	String:"1";
GET;
PUSH	Int32:3;
EQ;
T_JUMP_LABEL	String:"ScholarCharDesc";
ABORT	String:"Got unexpected result from character creation";

LABEL	String:"BerserkerCharDesc";
	PUSH	String:"You are a Berserker. You are strong at ATTACKS but weak at DODGING.";
	PUSH	String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	JUMP_LABEL	String:"DoneWithChars";

LABEL	String:"DuelistCharDesc";
	PUSH	String:"You are a Duelist. You are strong at COUNTERING but weak at MAGIC.";
	PUSH	String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	JUMP_LABEL	String:"DoneWithChars";
LABEL	String:"RogueCharDesc";
	PUSH	String:"You are a Rogue. You are strong at DODGING but weak at ATTACKS.";
	PUSH	String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	JUMP_LABEL	String:"DoneWithChars";

LABEL	String:"ScholarCharDesc";
	PUSH	String:"You are a Scholar. You are strong at MAGIC but weak at COUNTERING.";
	PUSH	String:"<IO.PrintLine>";
	CALL_PRIMITIVE;
	JUMP_LABEL	String:"DoneWithChars";

LABEL	String:"DoneWithChars";
PUSH	String:"";
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;
PUSH	String:"You stand at the entrance to the ancient lair of DOMEKULUS the NECRODEMANDER.";
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;
PUSH	String:"Your torchlight flickers. Warily, you take your first step towards fame ... or DEATH.";
PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;
	
**************************************************
*	0: Outside the cave (Down:1)
*	1: Antechamber		(Up:0,Left:2,Right:3)
*	2: Guardroom		(Right:1,Forward:4)
*	3: Torturama		(Left:1)
*	4: Lair				(Back:2)
**************************************************
PUSH	Boolean:True;
PUSH	String:"KeepRunning";
SET;
GET;
POP;
LABEL	String:"GameLoop";
PUSH	String:"KeepRunning";
GET;
F_JUMP_LABEL	String:"DoneWithGame";


JUMP_LABEL	String:"GameLoop";
LABEL	String:"DoneWithGame";
	PUSH	String:"Thanks for playing!";
	PUSH	String:"<IO.PrintLine>";
CALL_PRIMITIVE;
HALT;
