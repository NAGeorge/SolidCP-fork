
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
