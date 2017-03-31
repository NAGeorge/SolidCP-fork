
<link href="style.css" type="text/css" rel="stylesheet"></link>

<div style="text-align:right" markdown="1">
![logo](images/logo.png)
</div>
<br/>
<div class="title">
SolidCP Coding Guidelines
</div><br/>
<div class="subTitle">
for C# 3.0, 4.0 and 5.0
</div>
<br/>
<div class="author">
by: Rich Bailo<br/>
December 4, 2016
</div>

# 1. Introduction
## 1.1. What is this?

This document attempts to provide guidelines (or coding standards) for coding within the SolidCP project using C# 3.0, 4.0 or 5.0 that are both useful and pragmatic. So rest assured, these guidelines are representative to what we at [KnowledgeGuard](http://www.Knowledgeguard.com) do in our day-to-day work. Notice that not all guidelines have a clear rationale. Some of them are simply choices we made at Knowledgeguard. In the end, it doesn't matter what choice you made, as long as you make one and apply it consistently.

Visual Studio's [Static Code Analysis](http://msdn.microsoft.com/en-us/library/dd264939.aspx) (which is also known as FxCop) and [StyleCop](https://github.com/StyleCop/StyleCop) can already automatically enforce a lot of coding and design rules by analyzing the compiled assemblies. You can configure it to do that at compile time or as part of a continuous or daily build. This document just provides an additional set of rules and recommendations that should help us achieve a more maintainable code base.

## 1.2. Why would SolidCP use this document?

Although some might see coding guidelines as undesired overhead or something that limits creativity, this approach has already proven its value for many years. 
This is because not every developer or contributer:

- is aware that code is generally read 10 times more than it is changed;
- is aware of the potential pitfalls of **changing the .net framework** in our project. `**Please contact a project leader before you change, or add a project with a different framework other than v4.0**`;
- is aware of the potential pitfalls of certain constructions in C#;
- is up to speed on certain conventions when using the .NET Framework such as `IDisposable` or the deferred execution nature of LINQ;
- is aware of the impact of using (or neglecting to use) particular solutions on aspects like security, performance, multi-language support, etc;
- realizes that not every developer is as capable, skilled or experienced to understand elegant, but potentially very abstract solutions;

## 1.3. Basic principles

There are many unexpected things I run into during my work as a developer over many years, each deserving at least one guideline. Unfortunately, I still need to keep this document within a reasonable size. But unlike what some junior developers believe, that doesn't mean that something is okay just because it is not mentioned in this document.

In general, if if you feel that this document does not cover somthing, please refer back to a set of basic principles that apply to all situations, regardless of context. These include:

- The Principle of Least Surprise (or Astonishment): you should choose a solution that everyone can understand, and that keeps them on the right track.
- Keep It Simple Stupid (a.k.a. KISS): the simplest solution is more than sufficient.
- You Ain't Gonna Need It (a.k.a. YAGNI): create a solution for the problem at hand, not for the ones you think may happen later on. Can you predict the future?
- Don't Repeat Yourself (a.k.a. DRY): avoid duplication within a component, a source control repository or  a [bounded context](http://martinfowler.com/bliki/BoundedContext.html), without forgetting the [Rule of Three](https://en.wikipedia.org/wiki/Rule_of_three_(computer_programming)) heuristic.
- In general, generated code should not need to comply with coding guidelines. However, if it is possible to modify the templates used for generation, try to make them generate code that complies as much as possible.

Regardless of the elegance of someone's solution, if it's too complex for the ordinary developer, exposes unusual behavior, or tries to solve many possible future issues, it is very likely the wrong solution and needs redesign. The worst response a developer can give you to these principles is: "But it works?". 

## 1.4. How do you get started?

- We ask all contributers of the project to carefully read this document at least once. This will give you a sense of the kind of guidelines the document contains. 
- Make sure there are always a few hard copies of this document and the cheet sheet close at hand. 
- Include the most critical coding guidelines on your [Project Checklist]().
- [ReSharper](http://www.jetbrains.com/resharper/) has an intelligent code inspection engine that, with some configuration, already supports many aspects of the Coding Guidelines. It automatically highlights any code that does not match the rules for naming members (e.g. Pascal or Camel casing), detects dead code, and many other things. One click of the mouse button (or the corresponding keyboard shortcut) is usually enough to fix it. 
- ReSharper also has a File Structure window that displays an overview of the members of your class or interface, and allows you to easily rearrange them using a simple drag-and-drop action. 

## 1.5. Why did we create it?

The idea started in October 2016 when Marco, Trevor, Marc and I were talking about the existing code written by some of the original developers of Website Panel. Since then, I've created this, and added rules based on my experiences, We welcome feedback from the SolidCP community and will try and keep this updated with new tooling support offered by a continuous stream of new Visual Studio releases.

I've also decided to include some design guidelines in addition to simple coding guidelines. They are too important to ignore and have a big influence in reaching high quality code.

## 1.6. Is this a coding standard?

The document does not state that SolidSP projects must comply with these guidelines, neither does it say which guidelines are more important than others. However, we encourage SolidCP Contributers to tkae thes guildlines into concideration decide themselves which guidelines are important when contributing to the project, what deviations you will use, and what kind of layout must be used for source code. Contact one of the project leaders in case doubts arise Obviously, you should make these decisions before contributing work.

To help you in this decision, I've assigned a level of importance to each guideline:

![](images/1.png) Guidelines that you should never skip and should be applicable to all situations

![](images/2.png) Strongly recommended guidelines

![](images/3.png) May not be applicable in all situations

## 1.7. Feedback and disclaimer

This document has been created from many years of developing in C#. If you have questions, comments or suggestions, just let me know by sending me an email at [r.bailo@solidcp.com](mailto:r.bailo@soilidcp.com). I will try to revise and republish this document with new insights, experiences and remarks on a regular basis.

# 2. Class Design Guidelines

### <a name="scp1000"></a> A class or interface should have a single purpose (SCP1000) ![](images/1.png)

A class or interface should have a single purpose within the system it functions in. In general, a class either represents a primitive type like an email or ISBN number, an abstraction of some business concept, a plain data structure, or is responsible for orchestrating the interaction between other classes. It is never a combination of those. This rule is widely known as the [Single Responsibility Principle](http://www.objectmentor.com/resources/articles/srp.pdf), one of the S.O.L.I.D. principles.

**Tip:** A class with the word `And` in it is an obvious violation of this rule.

**Tip:** Use [Design Patterns](http://en.wikipedia.org/wiki/Design_pattern_(computer_science)) to communicate the intent of a class. If you can't assign a single design pattern to a class, chances are that it is doing more than one thing.

**Note** If you create a class representing a primitive type you can greatly simplify its use by making it immutable.

### <a name="scp1001"></a> Only create a constructor that returns a useful object (SCP1001) ![](images/3.png)

There should be no need to set additional properties before the object can be used for whatever purpose it was designed. However, if your constructor needs more than three parameters (which violates AV1561), your class might have too much responsibility (and violates AV1000).

### <a name="scp1003"></a> An interface should be small and focused (SCP1003) ![](images/2.png)

Interfaces should have a name that clearly explains their purpose or role in the system. Do not combine many vaguely related members on the same interface just because they were all on the same class. Separate the members based on the responsibility of those members, so that callers only need to call or implement the interface related to a particular task. This rule is more commonly known as the [Interface Segregation Principle](http://www.objectmentor.com/resources/articles/isp.pdf).

### <a name="scp1004"></a> Use an interface rather than a base class to support multiple implementations (SCP1004) ![](images/3.png)

If you want to expose an extension point from your class, expose it as an interface rather than as a base class. You don't want to force users of that extension point to derive their implementations from a base class that might have an undesired behavior. However, for their convenience you may implement a(n abstract) default implementation that can serve as a starting point.

### <a name="scp1005"></a> Use an interface to decouple classes from each other (SCP1005) ![](images/2.png)

Interfaces are a very effective mechanism for decoupling classes from each other:

- They can prevent bidirectional associations; 
- They simplify the replacement of one implementation with another; 
- They allow the replacement of an expensive external service or resource with a temporary stub for use in a non-production environment.
- They allow the replacement of the actual implementation with a dummy implementation or a fake object in a unit test; 
- Using a dependency injection framework you can centralize the choice of which class is used whenever a specific interface is requested.

### <a name="scp1008"></a> Avoid static classes (SCP1008) ![](images/3.png)

With the exception of extension method containers, static classes very often lead to badly designed code. They are also very difficult, if not impossible, to test in isolation, unless you're willing to use some very hacky tools.

**Note:** If you really need that static class, mark it as static so that the compiler can prevent instance members and instantiating your class. This relieves you of creating an explicit private constructor.

### <a name="scp1010"></a> Don't hide inherited members with the new keyword (SCP1010) ![](images/1.png)

Not only does the new keyword break [Polymorphism](http://en.wikipedia.org/wiki/Polymorphism_in_object-oriented_programming), one of the most essential object-orientation principles, it also makes sub-classes more difficult to understand. Consider the following two classes:

	public class Book  
	{
		public virtual void Print()  
		{
			Console.WriteLine("Printing Book");
		}  
	}
	
	public class PocketBook : Book  
	{
		public new void Print()
		{
			Console.WriteLine("Printing PocketBook");
		}  
	}

This will cause behavior that you would not normally expect from class hierarchies:

	PocketBook pocketBook = new PocketBook();
	
	pocketBook.Print(); // Outputs "Printing PocketBook "
	
	((Book)pocketBook).Print(); // Outputs "Printing Book"

It should not make a difference whether you call `Print()` through a reference to the base class or through the derived class.

### <a name="scp1011"></a> It should be possible to treat a derived object as if it were a base class object (SCP1011) ![](images/2.png)

In other words, you should be able to use a reference to an object of a derived class wherever a reference to its base class object is used without knowing the specific derived class. A very notorious example of a violation of this rule is throwing a `NotImplementedException` when overriding some of the base-class methods. A less subtle example is not honoring the behavior expected by the base class.   
  
**Note:** This rule is also known as the Liskov Substitution Principle, one of the [S.O.L.I.D.](http://stackoverflow.com/questions/56860/what-is-the-liskov-substitution-principle) principles.

### <a name="scp1013"></a> Don't refer to derived classes from the base class (SCP1013) ![](images/1.png)

Having dependencies from a base class to its sub-classes goes against proper object-oriented design and might prevent other developers from adding new derived classes.

### <a name="scp1014"></a> Avoid exposing the other objects an object depends on (SCP1014) ![](images/2.png)

If you find yourself writing code like this then you might be violating the [Law of Demeter](http://en.wikipedia.org/wiki/Law_of_Demeter).

	someObject.SomeProperty.GetChild().Foo()

An object should not expose any other classes it depends on because callers may misuse that exposed property or method to access the object behind it. By doing so, you allow calling code to become coupled to the class you are using, and thereby limiting the chance that you can easily replace it in a future stage.

**Note:** Using a class that is designed using the [Fluent Interface](http://en.wikipedia.org/wiki/Fluent_interface) pattern seems to violate this rule, but it is simply returning itself so that method chaining is allowed.

**Exception:** Inversion of Control or Dependency Injection frameworks often require you to expose a dependency as a public property. As long as this property is not used for anything other than dependency injection I would not consider it a violation.

### <a name="scp1020"></a> Avoid bidirectional dependencies (SCP1020) ![](images/1.png)

This means that two classes know about each other's public members or rely on each other's internal behavior. Refactoring or replacing one of those classes requires changes on both parties and may involve a lot of unexpected work. The most obvious way of breaking that dependency is to introduce an interface for one of the classes and using Dependency Injection.

**Exception:** Domain models such as defined in [Domain-Driven Design](http://domaindrivendesign.org/) tend to occasionally involve bidirectional associations that model real-life associations. In those cases, make sure they are really necessary, and if they are, keep them in.

### <a name="scp1025"></a> Classes should have state and behavior (SCP1025) ![](images/1.png)

In general, if you find a lot of data-only classes in your code base, you probably also have a few (static) classes with a lot of behavior (see AV1008). Use the principles of object-orientation explained in this section and move the logic close to the data it applies to.

**Exception:** The only exceptions to this rule are classes that are used to transfer data over a communication channel, also called [Data Transfer Objects](http://martinfowler.com/eaaCatalog/dataTransferObject.html), or a class that wraps several parameters of a method.

# 3. Member Design Guidelines

### <a name="scp1100"></a> Allow properties to be set in any order (SCP1100) ![](images/1.png)

Properties should be stateless with respect to other properties, i.e. there should not be a difference between first setting property `DataSource` and then `DataMember` or vice-versa.

### <a name="scp1105"></a> Use a method instead of a property (SCP1105) ![](images/3.png)

- If the work is more expensive than setting a field value. 
- If it represents a conversion such as the `Object.ToString` method.
- If it returns a different result each time it is called, even if the arguments didn't change. For example, the `NewGuid` method returns a different value each time it is called.
- If the operation causes a side effect such as changing some internal state not directly related to the property (which violates the [Command Query Separation](http://martinfowler.com/bliki/CommandQuerySeparation.html) principle). 

**Exception:** Populating an internal cache or implementing [lazy-loading](http://www.martinfowler.com/eaaCatalog/lazyLoad.html) is a good exception.

### <a name="scp1110"></a> Don't use mutual exclusive properties (SCP1110) ![](images/1.png)

Having properties that cannot be used at the same time typically signals a type that represents two conflicting concepts. Even though those concepts may share some of their behavior and states, they obviously have different rules that do not cooperate.

This violation is often seen in domain models and introduces all kinds of conditional logic related to those conflicting rules, causing a ripple effect that significantly increases the maintenance burden.

### <a name="scp1115"></a> A method or property should do only one thing (SCP1115) ![](images/1.png)

Similarly to rule AV1000, a method should have a single responsibility.

### <a name="scp1125"></a> Don't expose stateful objects through static members (SCP1125) ![](images/2.png)

A stateful object is an object that contains many properties and lots of behavior behind it. If you expose such an object through a static property or method of some other object, it will be very difficult to refactor or unit test a class that relies on such a stateful object. In general, introducing a construction like that is a great example of violating many of the guidelines of this chapter.

A classic example of this is the `HttpContext.Current` property, part of ASP.NET. Many see the `HttpContext` class as a source of a lot of ugly code. In fact, the testing guideline [Isolate the Ugly Stuff](http://codebetter.com/jeremymiller/2005/10/21/haacked-on-tdd-and-jeremys-first-rule-of-tdd/) often refers to this class.

### <a name="scp1130"></a> Return an `IEnumerable<T>` or `ICollection<T>` instead of a concrete collection class (SCP1130) ![](images/2.png)

You generally don't want callers to be able to change an internal collection, so don't return arrays, lists or other collection classes directly. Instead, return an `IEnumerable<T>`, or, if the caller must be able to determine the count, an `ICollection<T>`.

**Note:** If you're using .NET 4.5, you can also use `IReadOnlyCollection<T>`, `IReadOnlyList<T>` or `IReadOnlyDictionary<TKey, TValue>`.

### <a name="scp1135"></a> Properties, methods and arguments representing strings or collections should never be `null` (SCP1135) ![](images/1.png)

Returning `null` can be unexpected by the caller. Always return an empty collection or an empty string instead of a `null` reference. This also prevents cluttering your code base with additional checks for `null`, or even worse, `string.IsNullOrEmpty()`.

### <a name="scp1137"></a> Define parameters as specific as possible (SCP1137) ![](images/2.png)

If your member needs a specific piece of data, define parameters as specific as that and don't take a container object instead. For instance, consider a method that needs a connection string that is exposed through a central `IConfiguration` interface. Rather than taking a dependency on the entire configuration, just define a parameter for the connection string. This not only prevents unnecessary coupling, it also improved maintainability in the long run.

**Note:** An easy trick to remember this guideline is the *Don't ship the truck if you only need a package*.

### <a name="scp1140"></a> Consider using domain-specific value types rather than primitives (SCP1140) ![](images/3.png)

Instead of using strings, integers and decimals for representing domain-specific types such as an ISBN number, an email address or amount of money, consider creating dedicated value objects that wrap both the data and the validation rules that apply to it. By doing this, you prevent ending up having multiple implementations of the same business rules, which both improves maintainability and prevents bugs.

# 4. Miscellaneous Design Guidelines

### <a name="scp1200"></a> Throw exceptions rather than returning some kind of status value (SCP1200) ![](images/2.png)

A code base that uses return values to report success or failure tends to have nested if-statements sprinkled all over the code. Quite often, a caller forgets to check the return value anyway. Structured exception handling has been introduced to allow you to throw exceptions and catch or replace them at a higher layer. In most systems it is quite common to throw exceptions whenever an unexpected situation occurs.

### <a name="scp1202"></a> Provide a rich and meaningful exception message text (SCP1202) ![](images/2.png)

The message should explain the cause of the exception, and clearly describe what needs to be done to avoid the exception.

### <a name="scp1205"></a> Throw the most specific exception that is appropriate (SCP1205) ![](images/3.png)

For example, if a method receives a `null` argument, it should throw `ArgumentNullException` instead of its base type `ArgumentException`.

### <a name="scp1210"></a> Don't swallow errors by catching generic exceptions  (SCP1210) ![](images/1.png)

Avoid swallowing errors by catching non-specific exceptions, such as `Exception`, `SystemException`, and so on, in application code. Only top-level code, such as a last-chance exception handler, should catch a non-specific exception for logging purposes and a graceful shutdown of the application.

### <a name="scp1215"></a> Properly handle exceptions in asynchronous code (SCP1215) ![](images/2.png)
When throwing or handling exceptions in code that uses `async`/`await` or a `Task` remember the following two rules:

- Exceptions that occur within an `async`/`await` block and inside a `Task`'s action are propagated to the awaiter.
- Exceptions that occur in the code preceding the asynchronous block are propagated to the caller.

### <a name="scp1220"></a> Always check an event handler delegate for `null` (SCP1220) ![](images/1.png)

An event that has no subscribers is `null`, so before invoking, always make sure that the delegate list represented by the event variable is not `null`. Furthermore, to prevent conflicting changes from concurrent threads, use a temporary variable to prevent concurrent changes to the delegate.

	event EventHandler Notify;
	
	void RaiseNotifyEvent(NotifyEventArgs args)  
	{
		EventHandler handlers = Notify;  
		if (handlers != null)  
		{  
		    handlers(this, args); 
		
		}
	}

**Tip:** You can prevent the delegate list from being empty altogether. Simply assign an empty delegate like this:

	event EventHandler Notify = delegate {};

### <a name="scp1225"></a> Use a protected virtual method to raise each event (SCP1225) ![](images/2.png)
Complying with this guideline allows derived classes to handle a base class event by overriding the protected method. The name of the protected virtual method should be the same as the event name prefixed with `On`. For example, the protected virtual method for an event named `TimeChanged` is named `OnTimeChanged`.

**Note:** Derived classes that override the protected virtual method are not required to call the base class implementation. The base class must continue to work correctly even if its implementation is not called.

### <a name="scp1230"></a> Consider providing property-changed events (SCP1230) ![](images/3.png)
Consider providing events that are raised when certain properties are changed. Such an event should be named `PropertyChanged`, where `Property` should be replaced with the name of the property with which this event is associated

**Note:** If your class has many properties that require corresponding events, consider implementing the `INotifyPropertyChanged` interface instead. It is often used in the [Presentation Model](http://martinfowler.com/eaaDev/PresentationModel.html) and [Model-View-ViewModel](http://msdn.microsoft.com/en-us/magazine/dd419663.aspx) patterns.

### <a name="scp1235"></a> Don't pass `null` as the `sender` argument when raising an event  (SCP1235) ![](images/1.png)

Often an event handler is used to handle similar events from multiple senders. The sender argument is then used to get to the source of the event. Always pass a reference to the source (typically `this`) when raising the event. Furthermore don't pass `null` as the event data parameter when raising an event. If there is no event data, pass `EventArgs.Empty` instead of `null`.

**Exception:** On static events, the sender argument should be `null`.

### <a name="scp1240"></a> Use generic constraints if applicable (SCP1240) ![](images/2.png)
Instead of casting to and from the object type in generic types or methods, use `where` constraints or the `as` operator to specify the exact characteristics of the generic parameter. For example:

	class SomeClass  
	{}
	
	// Don't  
	class MyClass  
	{
		void SomeMethod(T t)  
		{  
			object temp = t;  
			SomeClass obj = (SomeClass) temp;  
		}  
	}
	
	// Do  
	class MyClass where T : SomeClass  
	{
		void SomeMethod(T t)  
		{  
			SomeClass obj = t;  
		}  
	}

### <a name="scp1250"></a> Evaluate the result of a LINQ expression before returning it  (SCP1250) ![](images/1.png)

Consider the following code snippet
	
	public IEnumerable GetGoldMemberCustomers()
	{
		const decimal GoldMemberThresholdInEuro = 1000000;
		
		var query = 
			from customer in db.Customers
			where customer.Balance > GoldMemberThresholdInEuro
			select new GoldMember(customer.Name, customer.Balance);
		
		return query;  
	}

Since LINQ queries use deferred execution, returning `query` will actually return the expression tree representing the above query. Each time the caller evaluates this result using a `foreach` cycle or similar, the entire query is re-executed resulting in new instances of `GoldMember` every time. Consequently, you cannot use the `==` operator to compare multiple `GoldMember` instances. Instead, always explicitly evaluate the result of a LINQ query using `ToList()`, `ToArray()` or similar methods.

# 5. Maintainability Guidelines

### <a name="scp1500"></a> Methods should not exceed 7 statements (SCP1500) ![](images/1.png)
A method that requires more than 7 statements is simply doing too much or has too many responsibilities. It also requires the human mind to analyze the exact statements to understand what the code is doing. Break it down into multiple small and focused methods with self-explaining names, but make sure the high-level algorithm is still clear.

### <a name="scp1501"></a> Make all members private and types internal by default (SCP1501) ![](images/1.png)
To make a more conscious decision on which members to make available to other classes, first restrict the scope as much as possible. Then carefully decide what to expose as a public member or type.

### <a name="scp1502"></a> Avoid conditions with double negatives (SCP1502) ![](images/2.png)
Although a property like `customer.HasNoOrders` makes sense, avoid using it in a negative condition like this:

	bool hasOrders = !customer.HasNoOrders;

Double negatives are more difficult to grasp than simple expressions, and people tend to read over the double negative easily.

### <a name="scp1505"></a> Name assemblies after their contained namespace (SCP1505) ![](images/3.png)
All DLLs should be named according to the pattern *Company*.*Component*.dll where *Company* refers to your company's name and *Component* contains one or more dot-separated clauses. For example `Sample.Web.Controls.dll`.

As an example, consider a group of classes organized under the namespace `Sample.Web.Binding` exposed by a certain assembly. According to this guideline, that assembly should be called `Sample.Web.Binding.dll`. 

**Exception:** If you decide to combine classes from multiple unrelated namespaces into one assembly, consider suffixing the assembly name with `Core`, but do not use that suffix in the namespaces. For instance, `Sample.Consulting.Core.dll`.

### <a name="scp1506"></a> Name a source file to the type it contains (SCP1506) ![](images/3.png)
Use Pascal casing to name the file and don't use underscores.

### <a name="scp1507"></a> Limit the contents of a source code file to one type (SCP1507) ![](images/3.png)
**Exception:** Nested types should, for obvious reasons, be part of the same file.

### <a name="scp1508"></a> Name a source file to the logical function of the partial type (SCP1508) ![](images/3.png)
When using partial types and allocating a part per file, name each file after the logical part that part plays. For example:

	// In MyClass.cs
	public partial class MyClass
	{...}
	
	// In MyClass.Designer.cs	
	public partial class MyClass
	{...}

### <a name="scp1510"></a> Use using statements instead of fully qualified type names (SCP1510) ![](images/3.png)
Limit usage of fully qualified type names to prevent name clashing. For example, don't do this:

	var list = new System.Collections.Generic.List();

Instead, do this:

	using System.Collections.Generic;
	
	var list = new List();

If you do need to prevent name clashing, use a `using` directive to assign an alias:

	using Label = System.Web.UI.WebControls.Label;

### <a name="scp1515"></a> Don't use "magic" numbers (SCP1515) ![](images/1.png)
Don't use literal values, either numeric or strings, in your code, other than to define symbolic constants. For example:

	public class Whatever  
	{
		public static readonly Color PapayaWhip = new Color(0xFFEFD5);
		public const int MaxNumberOfWheels = 18;  
	}

Strings intended for logging or tracing are exempt from this rule. Literals are allowed when their meaning is clear from the context, and not subject to future changes, For example:

	mean = (a + b) / 2; // okay  
	WaitMilliseconds(waitTimeInSeconds * 1000); // clear enough

If the value of one constant depends on the value of another, attempt to make this explicit in the code.

	public class SomeSpecialContainer  
	{  
		public const int MaxItems = 32;  
		public const int HighWaterMark = 3 * MaxItems / 4; // at 75%  
	}

**Note:** An enumeration can often be used for certain types of symbolic constants.

### <a name="scp1520"></a> Only use var when the type is very obvious (SCP1520) ![](images/1.png)
Only use `var` as the result of a LINQ query, or if the type is very obvious from the same statement and using it would improve readability. So don't

	var i = 3;									// what type? int? uint? float?
	var myfoo = MyFactoryMethod.Create("arg");	// Not obvious what base-class or			
												// interface to expect. Also difficult
												// to refactor if you can't search for
												// the class

Instead, use `var` like this:

	var q = from order in orders where order.Items > 10 and order.TotalValue > 1000;
	var repository = new RepositoryFactory.Get();	
	var list = new ReadOnlyCollection();

In all of three above examples it is clear what type to expect. For a more detailed rationale about the advantages and disadvantages of using `var`, read Eric Lippert's [Uses and misuses of implicit typing](http://blogs.msdn.com/b/ericlippert/archive/2011/04/20/uses-and-misuses-of-implicit-typing.aspx).

### <a name="scp1521"></a> Declare and initialize variables as late as possible (SCP1521) ![](images/2.png)
Avoid the C and Visual Basic styles where all variables have to be defined at the beginning of a block, but rather define and initialize each variable at the point where it is needed.

### <a name="scp1522"></a> Assign each variable in a separate statement (SCP1522) ![](images/1.png)
Don't use confusing constructs like the one below:

	var result = someField = GetSomeMethod();

### <a name="scp1523"></a> Favor Object and Collection Initializers over separate statements (SCP1523) ![](images/2.png)
Instead of:

	var startInfo = new ProcessStartInfo("myapp.exe");	
	startInfo.StandardOutput = Console.Output;
	startInfo.UseShellExecute = true;

Use [Object Initializers](http://msdn.microsoft.com/en-us/library/bb384062.aspx):

	var startInfo = new ProcessStartInfo("myapp.exe")  
	{
		StandardOutput = Console.Output,
		UseShellExecute = true  
	};

Similarly, instead of:

	var countries = new List();
	countries.Add("Netherlands");
	countries.Add("United States");

Use collection or [dictionary initializers](http://msdn.microsoft.com/en-us/library/bb531208.aspx):

	var countries = new List { "Netherlands", "United States" };

### <a name="scp1525"></a> Don't make explicit comparisons to `true` or `false` (SCP1525) ![](images/1.png)

It is usually bad style to compare a `bool`-type expression to `true` or `false`. For example:

	while (condition == false)// wrong; bad style  
	while (condition != true)// also wrong  
	while (((condition == true) == true) == true)// where do you stop?  
	while (condition)// OK

### <a name="scp1530"></a> Don't change a loop variable inside a for loop (SCP1530) ![](images/2.png)
Updating the loop variable within the loop body is generally considered confusing, even more so if the loop variable is modified in more than one place.

	for (int index = 0; index < 10; ++index)  
	{  
		if (_some condition_)
		{
			index = 11; // Wrong! Use 'break' or 'continue' instead.  
		}
	}

### <a name="scp1532"></a> Avoid nested loops (SCP1532) ![](images/2.png)
A method that nests loops is more difficult to understand than one with only a single loop. In fact, in most cases nested loops can be replaced with a much simpler LINQ query that uses the `from` keyword twice or more to *join* the data.

### <a name="scp1535"></a> Always add a block after keywords such as `if`, `else`, `while`, `for`, `foreach` and `case` (SCP1535) ![](images/2.png)
Please note that this also avoids possible confusion in statements of the form:

	if (b1) if (b2) Foo(); else Bar(); // which 'if' goes with the 'else'?
	
	// The right way:  
	if (b1)  
	{  
		if (b2)  
		{  
			Foo();  
		}  
		else  
		{  
			Bar();  
		}  
	}

### <a name="scp1536"></a> Always add a `default` block after the last `case` in a `switch` statement (SCP1536) ![](images/1.png)
Add a descriptive comment if the `default` block is supposed to be empty. Moreover, if that block is not supposed to be reached throw an `InvalidOperationException` to detect future changes that may fall through the existing cases. This ensures better code, because all paths the code can travel have been thought about.

	void Foo(string answer)  
	{  
		switch (answer)  
		{  
			case "no":  
			{
			  Console.WriteLine("You answered with No");  
			  break;
			}  
			  
			case "yes":
			{  
			  Console.WriteLine("You answered with Yes");  
			  break;
			}
			
			default:  
			{
			  // Not supposed to end up here.  
			  throw new InvalidOperationException("Unexpected answer " + answer);
			}  
		}  
	}

### <a name="scp1537"></a> Finish every if-else-if statement with an else-part (SCP1537) ![](images/2.png)
For example:

	void Foo(string answer)  
	{  
		if (answer == "no")  
		{  
			Console.WriteLine("You answered with No");  
		}  
		else if (answer == "yes")  
		{  
			Console.WriteLine("You answered with Yes");  
		}  
		else  
		{  
			// What should happen when this point is reached? Ignored? If not,   
			// throw an InvalidOperationException.  
		}  
	}

### <a name="scp1540"></a> Be reluctant with multiple return statements (SCP1540) ![](images/2.png)
One entry, one exit is a sound principle and keeps control flow readable. However, if the method is very small and complies with guideline AV1500 then multiple return statements may actually improve readability over some central boolean flag that is updated at various points.

### <a name="scp1545"></a> Don't use if-else statements instead of a simple (conditional) assignment (SCP1545) ![](images/2.png)
Express your intentions directly. For example, rather than:

	bool pos;
	
	if (val > 0)  
	{  
		pos = true;  
	}  
	else  
	{  
		pos = false;  
	}

write:

	bool pos = (val > 0); // initialization

Or instead of:

	string result;
	
	if (someString != null)
	{  
		result = someString;  
	}
	else
	{
		result = "Unavailable";
	}

	return result;

write:

	return someString ?? "Unavailable";

### <a name="scp1547"></a> Encapsulate complex expressions in a method or property (SCP1547) ![](images/1.png)
Consider the following example:

	if (member.HidesBaseClassMember && (member.NodeType != NodeType.InstanceInitializer))
	{
		// do something
	}

In order to understand what this expression is about, you need to analyze its exact details and all of its possible outcomes. Obviously, you can add an explanatory comment on top of it, but it is much better to replace this complex expression with a clearly named method:

	if (NonConstructorMemberUsesNewKeyword(member))  
	{  
		// do something
	}  
  
  
	private bool NonConstructorMemberUsesNewKeyword(Member member)  
	{  
		return
			(member.HidesBaseClassMember &&
			(member.NodeType != NodeType.InstanceInitializer)  
	}

You still need to understand the expression if you are modifying it, but the calling code is now much easier to grasp.

### <a name="scp1551"></a> Call the more overloaded method from other overloads (SCP1551) ![](images/2.png)
This guideline only applies to overloads that are intended to provide optional arguments. Consider, for example, the following code snippet:

	public class MyString  
	{
		private string someText;
		
	    	public int IndexOf(string phrase)  
		{  
			return IndexOf(phrase, 0); 
		}
		
		public int IndexOf(string phrase, int startIndex)  
		{  
			return IndexOf(phrase, startIndex, someText.Length - startIndex);
		}
		
		public virtual int IndexOf(string phrase, int startIndex, int count)  
		{  
			return someText.IndexOf(phrase, startIndex, count);
		}  
	}

The class `MyString` provides three overloads for the `IndexOf` method, but two of them simply call the one with one more parameter. Notice that the same rule applies to class constructors; implement the most complete overload and call that one from the other overloads using the `this()` operator. Also notice that the parameters with the same name should appear in the same position in all overloads.

**Important:** If you also want to allow derived classes to override these methods, define the most complete overload as a `protected virtual` method that is called by all overloads.

### <a name="scp1553"></a> Only use optional arguments to replace overloads (SCP1553) ![](images/1.png)
The only valid reason for using C# 4.0's optional arguments is to replace the example from rule AV1551 with a single method like:

	public virtual int IndexOf(string phrase, int startIndex = 0, int count = 0)
	{
		return someText.IndexOf(phrase, startIndex, count);
	}

If the optional parameter is a reference type then it can only have a default value of `null`. But since strings, lists and collections should never be `null` according to rule AV1235, you must use overloaded methods instead.

**Note:** The default values of the optional parameters are stored at the caller side. As such, changing the default value without recompiling the calling code will not apply the new default value properly.

**Note:** When an interface method defines an optional parameter, its default value is not considered during overload resolution unless you call the concrete class through the interface reference. See [this post by Eric Lippert](http://blogs.msdn.com/b/ericlippert/archive/2011/05/09/optional-argument-corner-cases-part-one.aspx) for more details.

### <a name="scp1555"></a> Avoid using named arguments (SCP1555) ![](images/1.png)
C# 4.0's named arguments have been introduced to make it easier to call COM components that are known for offering many optional parameters. If you need named arguments to improve the readability of the call to a method, that method is probably doing too much and should be refactored.

**Exception:** The only exception where named arguments improve readability is when calling a method of some code base you don't control that has a `bool` parameter, like this:  

	object[] myAttributes = type.GetCustomAttributes(typeof(MyAttribute), inherit: false);

### <a name="scp1561"></a> Don't allow methods and constructors with more than three parameters (SCP1561) ![](images/1.png)
If you create a method with more than three parameters, use a structure or class to pass multiple arguments, as explained in the [Specification design pattern](http://en.wikipedia.org/wiki/Specification_pattern). In general, the fewer the parameters, the easier it is to understand the method. Additionally, unit testing a method with many parameters requires many scenarios to test.

### <a name="scp1562"></a> Don't use `ref` or `out` parameters (SCP1562) ![](images/1.png)
They make code less understandable and might cause people to introduce bugs. Instead, return compound objects.

### <a name="scp1564"></a> Avoid methods that take a bool flag (SCP1564) ![](images/2.png)
Consider the following method signature:

	public Customer CreateCustomer(bool platinumLevel) {}

On first sight this signature seems perfectly fine, but when calling this method you will lose this purpose completely:

	Customer customer = CreateCustomer(true);

Often, a method taking such a flag is doing more than one thing and needs to be refactored into two or more methods. An alternative solution is to replace the flag with an enumeration.

### <a name="scp1568"></a> Don't use parameters as temporary variables (SCP1568) ![](images/3.png)
Never use a parameter as a convenient variable for storing temporary state. Even though the type of your temporary variable may be the same, the name usually does not reflect the purpose of the temporary variable.

### <a name="scp1570"></a> Always check the result of an `as` operation (SCP1570) ![](images/1.png)
If you use `as` to obtain a certain interface reference from an object, always ensure that this operation does not return `null`. Failure to do so may cause a `NullReferenceException` at a much later stage if the object did not implement that interface.

### <a name="scp1575"></a> Don't comment out code (SCP1575) ![](images/1.png)
Never check in code that is commented out. Instead, use a work item tracking system to keep track of some work to be done. Nobody knows what to do when they encounter a block of commented-out code. Was it temporarily disabled for testing purposes? Was it copied as an example? Should I delete it?

# 6. Naming Guidelines

### <a name="scp1701"></a> Use US-English (SCP1701) ![](images/1.png)

All type members, parameters and variables should be named using words from the American English language.

- Choose easily readable, preferably grammatically correct names. For example, `HorizontalAlignment` is more readable than `AlignmentHorizontal`.
- Favor readability over brevity. The property name `CanScrollHorizontally` is better than `ScrollableX` (an obscure reference to the X-axis).
- Avoid using names that conflict with keywords of widely used programming languages.

**Exception:** In most projects, you will use words and phrases from your domain and names specific to your company. Visual Studio's Static Code Analysis performs a spelling check on all code, so you may need to add those terms to a [Custom Code Analysis Dictionary](http://blogs.msdn.com/fxcop/archive/2007/08/20/new-for-visual-studio-2008-custom-dictionaries.aspx).

### <a name="scp1702"></a> Use proper casing for language elements (SCP1702) ![](images/1.png) 
Language element&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|Casing&nbsp;&nbsp;&nbsp;&nbsp;|Example
--------------------|----------|:-----------
Class, Struct|Pascal|`AppDomain`
Interface | Pascal | `IBusinessService`
Enumeration type | Pascal | `ErrorLevel` |
Enumeration values | Pascal | `FatalError` |
Event | Pascal | `Click` |
Private field | Camel | `listItem` |
Protected field | Pascal | `MainPanel` |
Constant field | Pascal | `MaximumItems` |
Constant Local variable | Camel | `maximumItems` |
Read-only static field | Pascal | `RedValue` |
Local Variable | Camel | `listOfValues` |
Method | Pascal | `ToString` |
Namespace | Pascal | `System.Drawing` |
Parameter | Camel | `typeName` |
Type Parameter | Pascal | `TView` |
Property | Pascal | `BackColor` |

### <a name="scp1704"></a> Don't include numbers in variables, parameters and type members  (SCP1704) ![](images/3.png)
In most cases they are a lazy excuse for not defining a clear and intention-revealing name.

### <a name="scp1705"></a> Don't prefix fields  (SCP1705) ![](images/1.png)

For example, don't use `g_` or `s_` to distinguish static from non-static fields. A method in which it is difficult to distinguish local variables from member fields is generally too big. Examples of incorrect identifier names are: `_currentUser`, `mUserName`, `m_loginTime`.

### <a name="scp1706"></a> Don't use abbreviations  (SCP1706) ![](images/2.png)
For example, use `OnButtonClick` rather than `OnBtnClick`. Avoid single character variable names, such as `i` or `q`. Use `index` or `query` instead.

**Exceptions:** Use well-known abbreviations that are widely accepted or well-known in your work domain. For instance, use `UI` instead of `UserInterface`.

### <a name="scp1707"></a> Name a member, parameter or variable according to its meaning and not its type  (SCP1707) ![](images/2.png)
- Use functional names. For example, `GetLength` is a better name than `GetInt`.
- Don't use terms like `Enum`, `Class` or `Struct` in a name.
- Identifiers that refer to a collection type should have plural names.

### <a name="scp1708"></a> Name types using nouns, noun phrases or adjective phrases  (SCP1708) ![](images/2.png)
Bad examples include `SearchExamination` (a page to search for examinations), `Common` (does not end with a noun, and does not explain its purpose) and `SiteSecurity` (although the name is technically okay, it does not say anything about its purpose). Good examples include `BusinessBinder`, `SmartTextBox`, or `EditableSingleCustomer`.

Don't include terms like `Utility` or `Helper` in classes. Classes with names like that are usually static classes and are introduced without considering object-oriented principles (see also AV1008).

### <a name="scp1709"></a> Name generic type parameters with descriptive names  (SCP1709) ![](images/2.png)
- Always prefix type parameter names with the letter `T`.
- Always use a descriptive name unless a single-letter name is completely self-explanatory and a longer name would not add value. Use the single letter `T` as the type parameter in that case.
- Consider indicating constraints placed on a type parameter in the name of the parameter. For example, a parameter constrained to `ISession` may be called `TSession`.

### <a name="scp1710"></a> Don't repeat the name of a class or enumeration in its members  (SCP1710) ![](images/1.png)

	class Employee
	{
		// Wrong!
		static GetEmployee() {}
		DeleteEmployee() {}
		
		// Right
		static Get() {...}
		Delete() {...}
		
		// Also correct.
		AddNewJob() {...}
		RegisterForMeeting() {...}
	}

### <a name="scp1711"></a> Name members similarly to members of related .NET Framework classes  (SCP1711) ![](images/3.png)

.NET developers are already accustomed to the naming patterns the framework uses, so following this same pattern helps them find their way in your classes as well. For instance, if you define a class that behaves like a collection, provide members like `Add`, `Remove` and `Count` instead of `AddItem`, `Delete` or `NumberOfItems`.

### <a name="scp1712"></a> Avoid short names or names that can be mistaken for other names  (SCP1712) ![](images/1.png)
Although technically correct, statements like the following can be confusing:

	bool b001 = (lo == l0) ? (I1 == 11) : (lOl != 101);

### <a name="scp1715"></a> Properly name properties  (SCP1715) ![](images/2.png)
- Name properties with nouns, noun phrases, or occasionally adjective phrases. 
- Name boolean properties with an affirmative phrase. E.g. `CanSeek` instead of `CantSeek`.
- Consider prefixing boolean properties with `Is`, `Has`, `Can`, `Allows`, or `Supports`.
- Consider giving a property the same name as its type. When you have a property that is strongly typed to an enumeration, the name of the property can be the same as the name of the enumeration. For example, if you have an enumeration named `CacheLevel`, a property that returns one of its values can also be named `CacheLevel`.

### <a name="scp1720"></a> Name methods using a verb or a verb-object pair  (SCP1720) ![](images/2.png)
Name methods using a verb like `Show` or a verb-object pair such as `ShowDialog`. A good name should give a hint on the *what* of a member, and if possible, the *why*.

Also, don't include `And` in the name of a method. It implies that the method is doing more than one thing, which violates the single responsibility principle explained in AV1115.

### <a name="scp1725"></a> Name namespaces using names, layers, verbs and features  (SCP1725) ![](images/3.png)
For instance, the following namespaces are good examples of that guideline.

	Sample.Commerce.Web
	NHibernate.Extensibility
	Microsoft.ServiceModel.WebApi
	Microsoft.VisualStudio.Debugging
	FluentAssertion.Primitives
	CaliburnMicro.Extensions

**Note:** Never allow namespaces to contain the name of a type, but a noun in its plural form (e.g. `Collections`) is usually OK.

### <a name="scp1735"></a> Use a verb or verb phrase to name an event  (SCP1735) ![](images/2.png)
Name events with a verb or a verb phrase. For example: `Click`, `Deleted`, `Closing`, `Minimizing`, and `Arriving`. For example, the declaration of the `Search` event may look like this:

	public event EventHandler<SearchArgs> Search;

### <a name="scp1737"></a> Use `-ing` and `-ed` to express pre-events and post-events  (SCP1737) ![](images/3.png)
For example, a close event that is raised before a window is closed would be called `Closing`, and one that is raised after the window is closed would be called `Closed`. Don't use `Before` or `After` prefixes or suffixes to indicate pre and post events.

Suppose you want to define events related to the deletion of an object. Avoid defining the `Deleting` and `Deleted` events as `BeginDelete` and `EndDelete`. Define those events as follows:

- `Deleting`: Occurs just before the object is getting deleted
- `Delete`: Occurs when the object needs to be deleted by the event handler.
- `Deleted`: Occurs when the object is already deleted.

### <a name="scp1738"></a> Prefix an event handler with `On` (SCP1738) ![](images/3.png)
It is good practice to prefix the method that handles an event with `On`. For example, a method that handles the `Closing` event can be named `OnClosing`.

### <a name="scp1739"></a> Use an underscore for irrelevant lambda parameters (SCP1739) ![](images/3.png)
If you use a lambda statement, for instance, to subscribe to an event, and the actual parameters of the event are irrelevant, use the following convention to make that more explicit:

	button.Click += (_, __) => HandleClick();

### <a name="scp1745"></a> Group extension methods in a class suffixed with Extensions (SCP1745) ![](images/3.png)
If the name of an extension method conflicts with another member or extension method, you must prefix the call with the class name. Having them in a dedicated class with the `Extensions` suffix improves readability.

### <a name="scp1755"></a> Post-fix asynchronous methods with `Async` or `TaskAsync` (SCP1755) ![](images/2.png)
The general convention for methods that return `Task` or `Task<TResult>` is to post-fix them with `Async`, but if such a method already exists, use `TaskAsync` instead.

# 7. Performance Guidelines

### <a name="scp1800"></a> Consider using `Any()` to determine whether an `IEnumerable<T>` is empty (SCP1800) ![](images/3.png)
When a method or other member returns an `IEnumerable<T>` or other collection class that does not expose a `Count` property, use the `Any()` extension method rather than `Count()` to determine whether the collection contains items. If you do use `Count()`, you risk that iterating over the entire collection might have a significant impact (such as when it really is an `IQueryable<T>` to a persistent store).

**Note:** If you return an `IEnumerable<T>` to prevent editing from outside the owner as explained in AV1130, and you're developing in .NET 4.5 or higher, consider the new read-only classes.

### <a name="scp1820"></a> Only use `async` for low-intensive long-running activities (SCP1820) ![](images/1.png)
The usage of `async` won't automagically run something on a worker thread like `Task.Run` does. It just adds the necessary logic to allow releasing the current thread, and marshal the result back on that same thread if a long-running asynchronous operation has completed. In other words, use `async` only for I/O bound operations.

### <a name="scp1825"></a> Prefer `Task.Run` for CPU-intensive activities (SCP1825) ![](images/1.png)
If you do need to execute a CPU bound operation, use `Task.Run` to offload the work to a thread from the Thread Pool. Remember that you have to marshal the result back to your main thread manually.

### <a name="scp1830"></a> Beware of mixing up `await`/`async` with `Task.Wait` (SCP1830) ![](images/1.png)
`await` does not block the current thread but simply instructs the compiler to generate a state-machine. However, `Task.Wait` blocks the thread and may even cause deadlocks (see AV1835).

### <a name="scp1835"></a> Beware of `async`/`await` deadlocks in single-threaded environments (SCP1835) ![](images/1.png)
Consider the following asynchronous method:

	private async Task GetDataAsync()
	{
		var result = await MyWebService.GetDataAsync();
		return result.ToString();
	}

Now when an ASP.NET MVC controller action does this:

	public ActionResult ActionAsync()
	{
		var data = GetDataAsync().Result;
		
		return View(data);  
	}

You end up with a deadlock. Why? Because the `Result` property getter will block until the `async` operation has completed, but since an `async` method will automatically marshal the result back to the original thread and ASP.NET uses a single-threaded synchronization context, they'll be waiting on each other. A similar problem can also happen on WPF, Silverlight or a Windows Store C#/XAML app. Read more about this [here](http://blogs.msdn.com/b/pfxteam/archive/2011/01/13/10115163.aspx).

# 8. Framework Guidelines

### <a name="scp2201"></a> Use C# type aliases instead of the types from the `System` namespace  (SCP2201) ![](images/1.png)
For instance, use `object` instead of `Object`, `string` instead of `String`, and `int` instead of `Int32`. These aliases have been introduced to make the primitive types first class citizens of the C# language, so use them accordingly.

**Exception:** When referring to static members of those types, it is custom to use the full CLS name, e.g. `Int32.Parse()` instead of `int.Parse()`. The same applies to members that need to specify the type they return, e.g. `ReadInt32`, `GetUInt16`. 

### <a name="scp2205"></a> Properly name properties, variables or fields referring to localized resources  (SCP2205) ![](images/3.png)
The guidelines in this topic apply to localizable resources such as error messages and menu text.

- Use Pascal casing in resource keys.
- Provide descriptive identifiers rather than short ones. Keep them concise where possible, but don't sacrifice readability.
- Use only alphanumeric characters in naming resources.

### <a name="scp2207"></a> Don't hard-code strings that change based on the deployment  (SCP2207) ![](images/3.png)
Examples include connection strings, server addresses, etc. Use `Resources`, the `ConnectionStrings` property of the `ConfigurationManager` class, or the `Settings` class generated by Visual Studio. Maintain the actual values into the `app.config` or `web.config` (and most definitely not in a custom configuration store).

### <a name="scp2210"></a> Build with the highest warning level  (SCP2210) ![](images/1.png)
Configure the development environment to use **Warning Level 4** for the C# compiler, and enable the option **Treat warnings as errors** . This allows the compiler to enforce the highest possible code quality.

### <a name="scp2215"></a> Properly fill the attributes of the `AssemblyInfo.cs` file  (SCP2215) ![](images/3.png)
Ensure that the attributes for the company name, description, copyright statement, version, etc. are filled. One way to ensure that version and other fields that are common to all assemblies have the same values, is to move the corresponding attributes out of the `AssemblyInfo.cs` into a `SolutionInfo.cs` file that is shared by all projects within a solution. 

### <a name="scp2220"></a> Avoid LINQ for simple expressions  (SCP2220) ![](images/3.png)
Rather than:

	var query = from item in items where item.Length > 0 select item;

prefer the use of extension methods from the `System.Linq` namespace:

	var query = items.Where(i => i.Length > 0);

Since LINQ queries should be written out over multiple lines for readability, the second example is a bit more readable.

### <a name="scp2221"></a> Use Lambda expressions instead of delegates  (SCP2221) ![](images/2.png)

Lambda expressions provide a much more elegant alternative for anonymous delegates. So instead of:

	Customer customer = Array.Find(customers, delegate(Customer c)
	{
		return c.Name == "Tom";
	});

use a Lambda expression:

	Customer customer = Array.Find(customers, c => c.Name == "Tom");

Or even better:

	var customer = customers.Where(c => c.Name == "Tom");

### <a name="scp2230"></a> Only use the `dynamic` keyword when talking to a dynamic object  (SCP2230) ![](images/1.png)
The `dynamic` keyword has been introduced for working with dynamic languages. Using it introduces a serious performance bottleneck because the compiler has to generate some complex Reflection code.

Use it only for calling methods or members of a dynamically created instance class (using the `Activator`) as an alternative to `Type.GetProperty()` and `Type.GetMethod()`, or for working with COM Interop types.

### <a name="scp2235"></a> Favor `async`/`await` over the Task (SCP2235) ![](images/1.png)
Using the new C# 5.0 keywords results in code that can still be read sequentially and also improves maintainability a lot, even if you need to chain multiple asynchronous operations. For example, rather than defining your method like this:

	public Task GetDataAsync()
	{
	  return MyWebService.FetchDataAsync()
	    .ContinueWith(t => new Data(t.Result));
	}

define it like this:

	public async Task GetDataAsync()
	{
	  var result = await MyWebService.FetchDataAsync();
	  return new Data (result);
	}

**Tip:** Even if you need to target .NET Framework 4.0 you can use the `async` and `await` keywords. Simply install the [Async Targeting Pack](http://www.microsoft.com/en-us/download/details.aspx?id=29576).

# 9. Documentation Guidelines

### <a name="scp2301"></a> Write comments above your added code to tell other contributers what it is for and add your initials so we know who to ask if we have a question. `**SAMPLE // Created for Let's Encrypt - RB**`  (SCP2301) ![](images/1.png)

### <a name="scp2301"></a> Write comments and documentation in US English  (SCP2301) ![](images/1.png)

### <a name="scp2305"></a> Document all public, protected and internal types and members  (SCP2305) ![](images/2.png)
Documenting your code allows Visual Studio to pop-up the documentation when your class is used somewhere else. Furthermore, by properly documenting your classes, tools can generate professionally looking class documentation.

### <a name="scp2306"></a> Write XML documentation with other developers in mind  (SCP2306) ![](images/2.png)
Write the documentation of your type with other developers in mind. Assume they will not have access to the source code and try to explain how to get the most out of the functionality of your type.

### <a name="scp2307"></a> Write MSDN-style documentation  (SCP2307) ![](images/3.png)
Following the MSDN online help style and word choice helps developers find their way through your documentation more easily.

### <a name="scp2310"></a> Avoid inline comments  (SCP2310) ![](images/2.png)
If you feel the need to explain a block of code using a comment, consider replacing that block with a method with a clear name.

### <a name="scp2316"></a> Only write comments to explain complex algorithms or decisions  (SCP2316) ![](images/1.png)
Try to focus comments on the *why* and *what* of a code block and not the *how*. Avoid explaining the statements in words, but instead help the reader understand why you chose a certain solution or algorithm and what you are trying to achieve. If applicable, also mention that you chose an alternative solution because you ran into a problem with the obvious solution.

### <a name="scp2318"></a> Don't use comments for tracking work to be done later  (SCP2318) ![](images/3.png)
Annotating a block of code or some work to be done using a *TODO* or similar comment may seem a reasonable way of tracking work-to-be-done. But in reality, nobody really searches for comments like that. Use a work item tracking system such as Team Foundation Server to keep track of leftovers.

# 10. Layout Guidelines

### <a name="scp2400"></a> Use a common layout  (SCP2400) ![](images/1.png)

- Keep the length of each line under 130 characters.

- Use an indentation of 4 whitespaces, and don't use tabs

- Keep one whitespace between keywords like `if` and the expression, but don't add whitespaces after `(` and before `)` such as: `if (condition == null)`.

- Add a whitespace around operators like `+`, `-`, `==`, etc.

- Always follow the keywords `if`, `else`, `do`, `while`, `for` and `foreach` with opening and closing braces, even though the language does not require it. 

- Always put opening and closing braces on a new line.
- Don't indent object Initializers and initialize each property on a new line, so use a format like this: 
	
		var dto = new ConsumerDto()
		{  
			Id = 123,  
			Name = "Microsoft",  
			PartnerShip = PartnerShip.Gold
		}

- Don't indent lambda statements and use a format like this:

		methodThatTakesAnAction.Do(x =>
		{ 
			// do something like this 
		}

- Put the entire LINQ statement on one line, or start each keyword at the same indentation, like this:
		
		var query = from product in products where product.Price > 10 select product;

  	or
	
		var query =  
		    from product in products  
		    where product.Price > 10  
		    select product;

- Start the LINQ statement with all the `from` expressions and don't interweave them with restrictions.
- Add parentheses around every comparison condition, but don't add parentheses around a singular condition. For example `if (!string.IsNullOrEmpty(str) && (str != "new"))`

- Add an empty line between multi-line statements, between members, after the closing parentheses, between unrelated code blocks, around the `#region` keyword, and between the `using` statements of different root namespaces.


### <a name="scp2402"></a> Order and group namespaces according to the company  (SCP2402) ![](images/3.png)

	// Microsoft namespaces are first
	using System;
	using System.Collections;
	using System.XML;
	
	// Then any other namespaces in alphabetic order
	using SolidCP.Business;
	using SolidCP.Standard;
	using Telerik.WebControls;
	using Telerik.Ajax;

### <a name="scp2406"></a> Place members in a well-defined order  (SCP2406) ![](images/1.png)
Maintaining a common order allows other team members to find their way in your code more easily. In general, a source file should be readable from top to bottom, as if reading a book, to prevent readers from having to browse up and down through the code file.

1. Private fields and constants (in a region)
2. Public constants
3. Public read-only static fields
4. Factory Methods
5. Constructors and the Finalizer
6. Events 
7. Public Properties
8. Other methods and private properties in calling order

### <a name="scp2407"></a>Use `#region` when you can but be reluctant with `#regions` (SCP2407) ![](images/1.png)
Regions can be helpful, but can also hide the main purpose of a class. Therefore, use `#region` only for:

- Private fields and constants (preferably in a `Private Definitions` region).
- Nested classes
- Interface implementations (only if the interface is not the main purpose of that class)

# A1. Important Resources
Comming Soon!

## The companion website
This document is part of an effort to increase the consciousness with which SolidCP C# contributers provide to the project daily on a professional level.

In addition to the most up to date version of this document, you'll find:

- A companion quick-reference sheet can be found in the SolidCP Documents folder.
- Visual Studio 2010/2012/2015 Rule Sets. COMMING SOON!
- [ReSharper](http://www.jetbrains.com/resharper/download/) layout configurations matching the rules in chapter 10. COMMING SOON!
- A place to discuss SolidCP coding quality. COMMING SOON!

##Useful links
In addition to the many links provided throughout this document, I'd like to recommend the following books, articles and sites for everyone interested in software quality:

* [Code Complete: A Practical Handbook of Software Construction](http://www.amazon.com/Code-Complete-Practical-Handbook-Construction/dp/0735619670) (Steve McConnel)  
One of the best books I've ever read. It deals with all aspects of software development, and even though the book was originally written in 2004 you'll be surprised when you see how accurate it still is. I wrote a [review](http://www.continuousimprover.com/2009/07/book-review-code-complete-2nd-edition.html) in 2009 if you want to get a sense of its contents.

* [The Art of Agile Development](http://www.amazon.com/Art-Agile-Development-James-Shore/dp/0596527675) (James Shore)  
Another great all-encompassing trip through the many practices preached by processes like Scrum and Extreme Programming. If you're looking for a quick introduction with a pragmatic touch, make sure you read James's book.

* [Applying Domain-Driven Design and Patterns: With Examples in C# and .NET](http://www.amazon.com/Applying-Domain-Driven-Design-Patterns-Examples/dp/0321268202) (Jimmy Nilsson)  
The book that started my interest for both Domain-Driven Design and Test-Driven Development. It's one of those books that I wished I had read a few years earlier. It would have spared me from many mistakes.

* [Jeremy D. Miller's Blog](http://codebetter.com/blogs/jeremy.miller/)  
Although he is not that active anymore, in the last couple of years he has written some excellent blog posts on Test-Driven Development, Design Patterns and design principles. I've learned a lot from his real-life and practical insights.

* [LINQ Framework Design Guidelines](http://blogs.msdn.com/b/mirceat/archive/2008/03/13/linq-framework-design-guidelines.aspx)  
A set of rules and recommendations that you should adhere to when creating your own implementations of IQueryable.

* [Best Practices for c# async/await](http://code.jonwagner.com/2012/09/06/best-practices-for-c-asyncawait/)  
The rationale and source of several of the new guidelines in this document, written by [Jon Wagner](https://twitter.com/jonwagnerdotcom).
