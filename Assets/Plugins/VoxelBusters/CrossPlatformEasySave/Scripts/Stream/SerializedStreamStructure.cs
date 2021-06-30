// serialized data structure
/* 
 * ------------------------------
 * -----------HEADER------------
 * ------------------------------
 * |serialized-version-value	|		(byte)	
 * |encryption-type-value		|		(byte)	
 * |packet-length				|		(byte)	
 * |header-length				|		(byte)
 * |data-length				____|__		(hex)
 * |assembly-data-length			|	(hex)
 * |type-data-length			shared	(hex)
 * |object-data-length		________|	(hex)
 * |reserved					|		(byte)	
 * |reserved					|		(byte)	
 * |reserved					|		(byte)	
 * ------------------------------
 * ------------DATA-------------
 * ------------------------------
 * |type-element				|		(element)	
 * |BLOCK-LENGTH				|		(hex)	
 * |name1						|		(string)
 * |type-id						|		(long)	
 * |reserved					|		(element)	
 * |reserved					|		(element)	
 * |reserved					|		(element)	
 * |var(1)-type-element			|		(element)	
 * |BLOCK-LENGTH				|		(hex)	
 * |var(1)-name					|		(hex)	
 * |var(1)-typecode				|		(hex)	
 * |var(1)-value				|
 * |.							|
 * |.							|
 * |var(n)-type-element			|		(element)	
 * |BLOCK-LENGTH				|		(hex)	
 * |var(n)-name					|
 * |var(n)-value				|
 * ------------------------------
 * --------SHARED DATA-----------
 * ------------------------------
 * |assembly0-id				|
 * |assembly0-name				|
 * |reserved				 Assemblies
 * |reserved					|
 * |reserved					|
 * ------------------------------
 * |type0-id					|
 * |type0-name					|
 * |assembly0-id			   Types
 * |reserved					|
 * |reserved					|
 * |reserved					|
 * ------------------------------
 * |object-ref-id				|
 * |BLOCK-LENGTH				|
 * |type-id						|
 * |reserved					|
 * |reserved					|
 * |reserved			Object Reference
 * |data0-name				   Data	
 * |BLOCK-LENGTH				|
 * |type-id						|
 * |data0-value					|
 * |data1-name					|
 * |BLOCK-LENGTH				|
 * |type-id						|
 * ------------------------------
 */