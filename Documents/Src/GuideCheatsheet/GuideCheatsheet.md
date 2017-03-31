
<link href="style.css" type="text/css" rel="stylesheet"/>

<table width="100%">
<tr>
<td class="title">SolidCP Coding Guidelines for C# 3.0, 4.0 and 5.0 Cheat Sheet</td>
<td rowspan="2" style="text-align:right">![logo](images/logo.png)</td>
</tr>
<tr>
<td><div class="subTitle">Design & Maintainability</div> (level 1 and 2 only)</td>
</tr>
</table>

<table width="100%">
<tr>
<td class="column" markdown="1">
<div markdown="1" class="sidebar">
**Basic Principles**

* The Principle of Least Surprise   
* Keep It Simple Stupid   
* You Ain’t Gonne Need It  
* Don’t Repeat Yourself
</div>

**Class Design**

*  A class or interface should have a single purpose (SCP1000)
*  An interface should be small and focused (SCP1003)  
*  Use an interface to decouple classes from each other (SCP1005)  
*  Don’t hide inherited members with the `new` keyword (SCP1010)  
*  It should be possible to treat a derived object as if it were a base class object (SCP1011)  
*  Don’t refer to derived classes from the base class (SCP1013)  
*  Avoid exposing the objects an object depends on (SCP1014)  
*  Avoid bidirectional dependencies (SCP1020)  
*  Classes should have state and behavior (SCP1025)  

<br/>
**Member Design**  

* Allow properties to be set in any order (SCP1100)  
* Don’t use mutual exclusive properties (SCP1110)  
* A method or property should do only one thing (SCP1115)  
* Don’t expose stateful objects through static members (SCP1125)   
* Return an `IEnumerable<T>` or `ICollection<T>` instead of a concrete collection class (SCP1130)   
* Properties, methods and arguments representing strings or collections should never be `null` (SCP1135)  
* Define parameters as specific as possible (SCP1137)   
</td>
<td class="column">
**Miscellaneous Design**  

* Throw exceptions rather than returning status values (SCP1200)  
* Provide a rich and meaningful exception message text (SCP1202)  
* Don’t swallow errors by catching generic exceptions  (SCP1210)  
* Always check an event handler delegate for `null` (SCP1220)  
* Properly handle exceptions in asynchronous code (SCP1215)  
* Use a protected virtual method to raise each event (SCP1225)  
* Don’t pass `null` as the sender parameter when raising an event (SCP1235)  
* Use generic constraints if applicable (SCP1240)  
* Evaluate the result of a LINQ expression before returning it (SCP1250)  

<br/>
**Maintainability**  

* Methods should not exceed 7 statements (SCP1500)  
* Make all members `private` and types `internal` by default (SCP1501)  
* Avoid conditions with double negatives (SCP1502)  
* Don’t use "magic numbers" (SCP1515)  
* Only use `var` when the type is very obvious (SCP1520)  
* Declare and initialize variables as late as possible (SCP1521)  
* Favor Object and Collection Initializers over separate statements (SCP1523)  
* Don’t make explicit comparisons to `true` or `false` (SCP1525)  
* Don’t change a loop variable inside a `for` or `foreach` loop (SCP1530)  
* Avoid nested loops (SCP1532)  
</td>
<td class="column">
* Always add a block after keywords such `if`, `else`, `while`, `for`, `foreach` and `case` (SCP1535)  
* Always add a `default` block after the last `case` in a `switch` statement (SCP1536)  
* Finish every `if`-`else`-`if `statement with an `else`-part (SCP1537)  
* Be reluctant with multiple `return` statements (SCP1540)  
* Don’t use `if`-`else` statements instead of a simple (conditional) assignment  (SCP1545)  
* Encapsulate complex expressions in a method or property (SCP1547)  
* Call the most overloaded method from other overloads (SCP1551)  
* Only use optional arguments to replace overloads (SCP1553)  
* Avoid using named arguments (SCP1555)  
* Don’t allow methods and constructors with more than three parameters (SCP1561)  
* Don’t use `ref` or `out` parameters (SCP1562)  
* Avoid methods that take a `bool` flag (SCP1564)  
* Always check the result of an `as` operation (SCP1570)  
* Don’t comment-out code (SCP1575)  

<br/>
**Framework Guidelines**  

* Use C# type aliases instead of the types from the `System` namespace (SCP2201)  
* Build with the highest warning level (SCP2210)  
* Use Lambda expressions instead of delegates (SCP2221)  
* Only use the `dynamic` keyword when talking to a dynamic object (SCP2230)  
* Favor `async`/`await` over the `Task` (SCP2235)  
</td>
<tr>

<table width="100%" class="footer">
<tr>
<td>
  Rich Bailo    
  (%commitdate%)  
</td>
<td style="text-align:right">
  [solidcp.com](http://solidcp.com)  
</td>
</tr>
</table>
<br />
<hr />
<br />
<table width="100%" style="page-break-before: always;">
 <tr>
  <td class="title">Coding Guidelines for C# 3.0, 4.0 and 5.0 Cheat Sheet</td>
  <td markdown="1" rowspan="2" style="text-align:right">![logo](images/logo.png)</td>
 </tr>
 <tr>
 <td><div class="subTitle">Naming & Layout</div> (level 1 and 2 only)</td>
 </tr>
</table>

<table width="100%">
<tr>
<td class="column" markdown="1">
<div class="sidebar">

|**Pascal Casing**||
|:-------------------------------|-----------|
|Class, Struct					|`AppDomain`|
|Interface 						|`IBusinessService`|
|Enumeration type 				|`ErrorLevel`
|Enumeratiion values			|`FatalError`
|Event 							|`Click`
|Protected field 				|`MainPanel`
|Const field 					|`MaximumItems`
|Read-only static field&nbsp;&nbsp;			|`RedValue`	
|Method 						|`ToString`
|Namespace 						|`System.Drawing`
|Property 						|`BackColor`
|Type Parameter					|`TEntity`
| |  
|<br/>**Camel Casing**||
|Private field					|`listItem`
|Variable 						|`listOfValues`
|Const variable					|`maximumItems`
|Parameter 						|`typeName`

</div>

<br/>
**Naming**  

* Use US English (SCP1701)
* Don’t include numbers in variables, parameters and type members  (SCP1704)
* Don’t prefix fields (SCP1705)
* Don’t use abbreviations (SCP1706)
* Name members, parameters or variables according its meaning and not its type (SCP1707)
* Name types using nouns, noun phrases or adjective phrases (SCP1708)
* Don’t repeat the name of a class or enumeration in its members (SCP1710)
* Avoid short names or names that can be mistaken with other names (SCP1712)
* Name methods using verb-object pair (SCP1720)
* Name namespaces using names, layers, verbs and features (SCP1725)
</td>
<td class="column">

* Use an underscore for irrelevant lambda parameters (SCP1739)


**Documentation**  

* Write comments and documentation in US English (SCP2301)
* Document all public, protected and internal types and members (SCP2305)
* Avoid inline comments (SCP2310)
* Only write comments to explain complex algorithms or decisions (SCP2316)
* Don’t use comments for tracking work to be done later (SCP2318)
<br/>

**Layout**

* Maximum line length is 130 characters.
* Indent 4 spaces, don’t use Tabs
* Keep one white-space between keywords like `if` and the expression, but don’t add white-spaces after `(` and before `)`.
* Add a white-space around operators, like `+`, `-`, `==`, etc.
* Always add parentheses after keywords `if`, `else`, `do`, `while`, `for` and `foreach`
* Always put opening and closing parentheses on a new line.
* Don’t indent object initializers and initialize each property on a new line.
* Don’t indent lambda statements
* Put the entire LINQ statement on one line, or start each keyword at the same indentation.
* Add braces around comparison conditions, but don’t add braces around a singular condition. 
</td>
<td class="column">

<div markdown="1" class="sidebar">
**Empty lines**

* Between members
* After the closing parentheses
* Between multi-line statements
* Between unrelated code blocks 
* Around the `#region` keyword
* Between the `using` statements of different root namespaces.
</div>

<div class="sidebar">
**Member order**

1.	Private fields and constants
1.	Public constants
1.	Public read-only static fields
1.	Factory Methods
1.	Constructors and the Finalizer
1.	Events 
1.	Public Properties
1.	Other methods and private properties in calling order
</div>

<div markdown="1" class="sidebar">
**Important Note**

These coding guidelines are an extension to Visual Studio's Code Analysis functionalty, so make sure you enable that for all your projects. Check the full document for more details.
</div>

<td/>
<tr>

<table width="100%" class="footer">
<tr>
 <td>
   Rich Bailo    
   (%commitdate%)   
 </td>
 <td style="text-align:right">
  [solidcp.com](http://solidcp.com)  
  </td>
</tr>
</table>
