grammar CLUBS;

// PARSER RULES

prog 
	: NL*	SETUP setupBlock=blockStmt NL* EOF
	;

blockStmt
	: NL* '{' NL* (stmt NL+)*  NL* '}' NL*
	;

stmt
	: dclStmt
	| controlStructureStmt
	| actionStmt
	| blockStmt
	| assignStmt
	;

dclStmt
	: set=SETOF? type ':' id=ID ('=' assignment=expr)?
	;

assignStmt
	: left=idExpr '=' right=expr
	;

expr
	: atom
	| idExpr
	| valueExpr
	| infixExpr
	| randomExpr
	;

atom
	: kind=INUM
	| kind=BOOL_VAL
	| kind=STRING_VAL
	;

idExpr
  : id=ID																													#id
	| parent=idExpr member=dotExpr																	#idDot
	| functionId=ID'('parameterId=ID? (',' parameterId=ID)* ')'			#idFunction
	;

leftExpr
	: idExpr
	| atom
	;

dotExpr
	: '.'idExpr																								        #idDotReference
	| '.'COUNT																												#countDotReference
	;

valueExpr
	: ('['parent=idExpr ':' child=idExpr']')+      #cardValueExpr
	| '['(setElement)+ id=ID']'										 #setValueExpr
	| '['ID']'('['ID']')*													 #deckValueExpr
	;

randomExpr
	: RANDOM lower=leftExpr TO upper=expr
	;

setElement
	: id=ID order
	;

order 
	: kind=LT
	| kind=COMMA 
	;

infixExpr
	: infixArithmeticExpr
	| infixBooleanExpr
	;

infixArithmeticExpr
	: left=leftExpr op=( MUL | DIV ) right=expr
	| left=leftExpr op=( ADD | SUB ) right=expr
	;

infixBooleanExpr
	: left=leftExpr op=(LT | GT) right=expr									#infixComparison
	| left=infixBooleanExpr op=AND right=infixBooleanExpr		#infixAnd
	| left=infixBooleanExpr op=OR right=infixBooleanExpr		#infixOr
	| left=leftExpr op=IS right=expr												#infixIs
	;

type
	: kind=CARD
	| kind=CARDVALUE
	| kind=PLAYER
	| kind=INT
	| kind=STRING
	| kind=BOOL
	;

controlStructureStmt
	: forAllStmt
	| ifStmt
	| whileStmt
	;

forAllStmt
	: FORALL child=ID IN parent=idExpr blockStmt
	;

ifStmt
	: IF expr ifBlock=blockStmt elseIfStmt* (ELSE elseBlock=blockStmt)?
	;

elseIfStmt
	: ELSE IF expr blockStmt
	;

whileStmt
	: WHILE predicate=expr blockStmt
	;

actionStmt
	: putActionStmt
	| ownsActionStmt
	| printActionStmt
	;

putActionStmt
	: FROM source=idExpr takeActionStmt PUT target=idExpr		#takeStmt
	| source=idExpr PUT target=idExpr												#putStmt
	;

takeActionStmt
	: TAKE quantity=expr														#takeQuantityStmt
	| TAKE quantity=expr WHERE query								#takeQueryStmt
	| TAKE quantity=expr AT index=expr							#takeIndexStmt
	| TAKE ALL																			#takeAllStmt
	;

query 
	: infixBooleanExpr
	;

ownsActionStmt
	: type OWNS '['(dclStmt',')* dclStmt']'
	;

printActionStmt
	: PRINT printContent ('+' printContent)*
	;

printContent
	: content=STRING_VAL															 #printString
	| idExpr																					 #printId
	;

// -----------------------------------------------------

// LEXER RULES

IF : 'IF' | 'if' ;
ELSE : 'ELSE' | 'else' ;
FORALL : 'FORALL' | 'forall' ;
WHILE : 'WHILE' | 'while' ;
IN : 'IN' | 'in' ;

SETUP : 'setup' ;
PRINT : 'PRINT' | 'print' ;
SETOF : 'Set OF ' | 'Set of' ;

CARD : 'Card' ;
CARDVALUE : 'CardValue' ;
PLAYER : 'Player' ;
INT : 'Int' ;
BOOL : 'Bool' ;
STRING : 'String' ;

LT : '<' ;
GT : '>' ;
COMMA : ',' ;
COLON : ':' ;
EQUAL : '=' ;

ADD : '+' ;
SUB : '-' ;
MUL : '*' ;
DIV : '/' ;

LPAREN : '(' ;
RPAREN : ')' ;
LSQUARE : '[' ;
RSQUARE : ']' ;
LBRACK : '{' ;
RBRACK : '}' ;

FROM : 'FROM' | 'from' ;
TAKE : 'TAKE' | 'take' ;
PUT : 'PUT' | 'put' ;
AT : 'AT' | 'at' ;
ALL : 'ALL' | 'all' ;
AND : 'AND' | 'and' ;
IS : 'IS' | 'is' ;
OR : 'OR' | 'or' ;
WHERE : 'WHERE' | 'where' ;
OWNS : 'OWNS' | 'owns' ;

RANDOM : 'RANDOM' | 'random' ;
TO : 'TO' | 'to' ;

COUNT : 'count' ;

NL : [\n] ;

INUM : [0-9]+ ;
BOOL_VAL : 'true' | 'false' ;
ID  : [a-zA-Z]+[a-zA-Z0-9]* ; // Should be below all other letter rules (least prioritized).

WS  : [' '| '\t' | '\r']+ -> skip ;

LINE_COMMENT : '//' ~[\n]* -> skip ;
MULTI_LINE_COMMENT : '/*'  ~['*/']* '*/' -> skip  ;

/*STRING : '"'[a-zA-Z ]+[a-zA-Z0-9 ]*'"' ;*/

STRING_VAL : '"' ~('"')* '"' ;
