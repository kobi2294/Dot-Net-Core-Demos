# WPF Advanced Course: Feb/2022

# Day 01 - XAML, Trees and Templates
### Projects:
|     |     |
| --- | --- |
| BareBones | The core of WPF - without XAML | 
| FunWithXaml | Deep dive into XAML |
| FunWithTemplates | Introduction to trees and templates in WPF |

### The core of WPF
* We learned that WPF is a rich client framework that works directly through low level graphics drivers (`DirectX`) rather than through the `GDI`
* We learned that WPF uses 4 `DLL` packages:
  * `PresentationCore`
  * `PresentationFramework`
  * `WindowsBase`
  * `System.Xaml`
* We saw that WPF uses a special threading model
  * Single main thread that handles UI
  * Thread Pool to handle tasks in the background
  * Dispatchers schedule work units for each group of threads one dispatcher per group
  * Must use the **Single-Threaded Apartments** model (`STAThread`)
* All WPF objects can be constructed and displayed using "normal" C# code to construct the instances and connect them together.
* We saw an example of how we create a full UI application using C# only:
  * Created several UI elements (`Button`, `Ellispe`, `Window`, `StackPanel`)
  * Connected them together by setting refernces to each other in their respective properties (`Content`, `Children`)
  * Created an `Application` object in order to instrument the application
  * Started the application by calling the `Application.Run(Window)` method

### Introduction to XAML
* We saw that XAML is just an additional language that can perform the same logic that we previously did in C# code
* We prefer using XAML because it is **Declarative** while C# in **Imperative**.
  * Imperative languages describe flow of logic, they tell the computer what to do, step by step. 
  * Declarative languages tell the computer **What** to build, but not **how**. They describe the final state of the UI rather than the steps to get to that state.
  * UI built using declarative languages are easier to read.
* We understood that XAML is an enhancment of XML. It describes a hierarchical structure of data, and adds a few extra features.
* At the core, all XAML does is instantiates C# objects and fills their properties.
* We learned how to read and understand the XAML syntax for **namespaces**
    * We saw how to add a prefix to specific C# namespace, and understood that there is also a way to add prefix to a group of namespaces
    * We saw some of the common prefix and namespaces such as the `x` namespace for XAML language constructs, the `d` namespace for design only attributes, and the `mc` namespace for compatibility issues.
* We saw that we create new instances by adding class elements, for example `<Button>` and `<local:Person>`.
* We saw that we can set their properties using 2 types of syntax
    * The **Property Elements** set the property value explicitly. for example: `<Button.Width>128</Button.Width>` and `<local:Person.FirstName>John</Local:Person.FirstName>`
    * The **Property Attributes** set the property implicitly by "secretly" applying a type converter for the specific type of property
* We saw that some classes have a default property which can be set without specifying the property element. This is called the **Content Property**
* We saw that XAML also helps us to fill collection properties such as `IList<T>` and `IDictionary<T>`. 
    * We saw that we need to make sure the collection itself is instantiated in the object constructor
    * We saw that when using the **Property Element** Syntax, we can simply fill the element with object elements.
    * If we use a dictionary collection, we also need to supply a key for each element using the `x:Key` attribute
* We learned about **Type Converters**
    * We saw how to implement our own type converter for a custom type.
* We learned about **Markup Extensions**
    * We understood that markup extension is a wrapper to a method that nees to be executed while filling the value of a property
    * We saw some built in markup extensions such as `{Binding}`, `{StaticResource}` and `{x:Null}`
    * We saw how to create our own custom markup extension

### Trees and Templates
* We understood that WPF uses 2 type of trees
  * **The Visual Tree** - A single tree that holds all visual elements and represents the hierarchical structure of the UI
    * responsible for: rendering, layout, transforms, enablement, hit testing and more
  * **The logical Tree** - in fact several of them, that represent the logical hierarchy of the UI without specifying the internal implementation detail of each element
    * responsible for: inheriting dependency property values, resolving resource keys, looking up elements by name, bubbling of events
* We learned about the `Shape` elements and how they behave in each tree
  * We also saw tha path markup language
* We learned about the `Decorator` elements and how they behave in each tre
* We defined the term `Control` in WPF and understood that it is an UI element that seperates logic and visual
  * The logic is hard coded in the control class
  * The visual is injected into it using a `ControlTemplate`
* We understood what a template is and that there are 3 types of templates in WPF - the first of them is the `ControlTemplate` which defines the visual implementation of a control.
* We saw that the `ControlTemplate` and the `Control` have a "contract" that allows them to interact
  * The control exposes properties, some of them visual properties, and the template **Binds** to the properties using the `{TemplateBinding}` markup extension, in order to apply them on the visual itself. 
  * The control exposes a set of "visual states" and the template responds to them by defining animations that will be activated in each specific state
* We saw the `ContentControl` class which is the base class of all controls that contain content
  * It has a `Content` property that can hold any object
  * Since data objects can not be displayed in the visual tree, it has a `ContentTemplate` property of type `DataTemplate` in order to define how the data is to be presented.
  * The `ControlTemplate` places a `<ContentPresenter>` element in order to specify where the content will be displayed


# Day 02 - Layout Panels and Resources
### Projects:
|     |     |
| --- | --- |
| FunWithLayout | Understanding WPF Layout and Panels | 
| Ex1Solution) | Solution to exercise 1 on Templates |
| FunWithResourcesAndStyles | Styles, Triggers, Animations and other resources |

### Layout and Layout Properties
* Layout in WPF is a 2-pass algorithm.
  * The **measure** pass where each parent asks child to measure the minimum space required for its presentation
  * The **arrange** pass where eah parent allocates space and position for its child
* The layout passes allocate the **Bounding Box** for its children.
* Once a bounding box is allocated, the layout properties arrange the element inside the box
  * The `Margin` property may allocate some of the bounding box for whitespace and reduce the effective available box
  * The `Width` and `Height` properties may give the element a size that is different than the bounding box. 
  * If the `Width` and `Height` are not set, then the `MinWidth`, `MinHeight`, `MaxWidth` and `MaxHeight` properties may still limit the size
  * The `VerticalAlignment` and `HorizontalAlignment` may set the sizing and position of the element.
    * If the value `Stretch` is used, then the element will be sized according to the available box. (assuming `Width` and `Height` are not set)
    * Otherwise, the element will be sized according to the measure pass.
    * The alignment will set the position of the element within the bounding box
* Custom layout behavior may be programmed into new element classes by overriding the layout methods:
  * `MeasureOverride`
  * `ArrangeOverrride`

  
### The various Panels of WPF
* The `Canvas` panel is the most simple panel because it does not really do any calculations. It simply places each element when it asks to be.
  * Alignment is never relevant because the box allocated for each element is exactly what it requires to be.
  * Position is controlled using the `Canvas.Top` `Canvas.Left`, `Canvas.Bottom` and `Canvas.Right` properties.
* The `StackPanel` is a useful panel for stacking elements in one direction
  * It can either be horizontal or vertical
  * In the stacking direction, the element will always get exactly the desired size
  * In the other dimension, the panel calcualtes the max desired size of all the children, and then allocates that maximum to all of them
*  The `WrapPanel` is a useful panel for stacking and wrapping of elements. It stacks elements in a single direction and then wraps to the next stack if there is not enough space in the container
*  The `DockPanel` is useful for docking elements to the panel boundaries and to fill the ramainder space
   *  `LastChildFill` property decides if the last child fills the entire space that remains after previous children are placed
   *  Each child has a docking direction controlled by the `DockPanel.Dock` property
   *  Multiple elements may be stacked to the same direction
*  The `Grid` panel is the most versitile panel of them all
   *  It is used to fill spaces in 2 dimensions
   *  First you use the `ColumnDefinitions` and `RowDefinitions` properties to divide the space into rows and columns
   *  Then you can place elements in cells using the `Grid.Row` and `Grid.Column` properties
   *  You can span an element on a rectangle of cells using `Grid.RowSpan` and `Grid.ColumnSpan`
   *  Columns and Rows may have various size logics:
      *  `auto` sizing - the row or column will have the minimum size required to fit its content
      *  `pixel` sizing - provides the exact size for the row or column
      *  `star` sizing - divides the remaining space according to proportional weights
   *  We have seen how to use `GridSplitter` along with `Grid` in order to allow to user to resize rows and columns
   *  We have talked about `Size Sharing Group` to allow different grids to share sizing definitions


### Resources and Styles
* We have talked about `ResourceDictionary` and the fact that each element in the visual tree has this property
* We said that `{StaticResource}` and `{DynamicResource}` search for resources by traveling up the **logical tree**
* We saw that each resource in the resource dictionary must have an `x:Key` except for `DataTemplate` and `Style`
  * If the key is ommited, they become default for their target type within the scope of the resource dictionary
* We talked about the fact that "visual" elements can not be resources since they cannot be reused. Each visual element can be placed in the visual tree only at one place.
* We said that resources should usually be either **immutable** (meaning that the classes are readonly and the properties cannot change) or **frozen** (meaning the the classes implement `IFreezble` when you can set the object as **frozen** which turns the properties into read-only).
* In any case - you should not change properties of resource once they are already used as resources.
* We saw some example of objects that are good candidates to be used as resources
  * Brushes
  * Colors
  * Geometries
  * Animations
  * Templates
  * Styles
  * Storyboards
* We saw that a `Style` is an object that defines values for properties. 
  * We saw that a `Style` object contains a list of `Setter` objects, each one defines a specific value to a specific property
  * We saw that each `Style` has `TargetType` which determines which properties are relevant for this style. A style object can be provided to an element only if the element's type is the styles `TargetType`
  * We saw that if we set a property directly on an element, it overrides the value that it receives from the style
  * We saw that a style can set any property of the element (excpet for the `Style` property itself)
  * We saw that a `Style` object can be "Based On" a different style, which means that it inherits all the property setters of the base style
* We talked about animations
  * We said that an `Animation` is like a single style setter, in the sense that it provides a value to a specific property.
  * We said that a `Storyboard` is like a single `Style` since it contains several animations where each sets a single property with a value.
  * There are 3 main differences between styles and storyboards
    1. A style setter is "weaker" than a local value, so if you provide a property with local value it overides the setter value. Animation value, on the other hand, is **stronger** than local value. 
    2. A style setter sets the property value **instantly** while an animation setter sets it over time
    3. You can only provide a single style object to an element. But you can apply many storyboards
* We talked about `Triggers`
  * We saw that each UI element has a `Triggers` property which is a list of `Trigger` objects
  * A trigger object contains 2 parts: The trigger cause, and the trigger action
    * The trigger cause is an object that describes what the triggers responds to, when it is triggered
    * The trigger action is an object that describes the side effect of the trigger
  * We saw that in the `Triggers` property we may only use a specific type of trigger called `EventTrigger` which is triggered by a specific event
  * We saw that we may use the `StartStoryboard` action so that the trigger causes a storyboard to play
  * We saw an example of how to respond to a `Loaded` event of an element in order to play a storyboard on it
* We saw how to set the storyboard "repeat" behavior to make it loop forever.
* We saw how to create an animation with several keyframes
* We saw how to set easing functions to each keyframe


# Day 03 - Dependency Properties and Data Binding
### Projects:
|     |     |
| --- | --- |
| FunWithDependencyProperties | WPF Dependency Properties | 
| FunWithBinding | Data Binding, sources, and converters |

### Dependency Properties
* We have talked about the **Property Service** as a storage for property values for objects
* We talked about the `DependencyObject` class and the fact that it provides access to dependency properties.
* We saw how to define a new dependency property token using the `DependencyProperty.Register` static method.
* We saw how to define default value to the property
* We saw how to define a style that changes the property
* We saw how to query the source of the data using `DependencyPropertyHelper`.
* We saw how to **Coerce** the value of the dependency property
* We saw how to respond to value changes of the property using the `OnChange` callback.
* We saw how to use `Style Trigger` inside style in order to conditionaly set a property value according to the value of another property (for example `IsMouseOver`)
* We saw how to define property behaviors such as inheritance, default binding and more, using the `FrameworkPropertyMetadata`

### Attached Properties
* We understood that at the core - **All dependency properties may be attached to every dependency object**. 
* Still, mostly for XAML purposes, we can specifically define a property as **Attached Property** using the `RegisterAttached` method. This specifically indicates that the property isd designated to be attached to **other** objects.
* We saw how to implement **Attached Behavior** By responding to the property changes and implementing some logic as a result.
* We demonstrated how to create a template that binds to attached properties.
* We saw that actually, we almost never need to inherit from a control in order to extend it
  * We can create an alternate look using the template
  * We can add data properties using attached properties
  * We can add custom behaviors controlled by properties using **Attached Behaviors**


### Data Binding
* We understood that a binding is an external object that synchronizes the value of properties
* We saw that the object is defined by 4 things:
  * The source object - which can be any object
  * The source property path - which can be any property on the source object, or a path from it through other objects to a final source property.
  * The target object - which must be a `DependencyObject`
  * The target property - which must be a `DependencyProperty`
* We saw that we can also add
  * Mode: One way, Two way, One time, and more
  * Converter: A value converter to modify the data that is synchronized
  * And other binding properties which we did not talk about:
    * `UpdateSourceTrigger` - controls when to update the source property on two way bindings
    * `FallbackValue` - a value to use when the binding is not legal
    * `NullValue` - a value to use when the source is null
* We saw how to use Value converters, and how to write them
* We saw how to define the binding source
  * Data Context - by default
  * Element by name
  * Self
  * Templated Parent
* We saw that implementing `INotifyPropertyChanged` on the source means that the binding knows to refresh the target value whenever the source changes
* We saw how to create binding "Programatically" in C#
* Finally we saw a cool example of how to implement a sophisticated template for `ProgressBar` using `MultiBinding` cobined with `TemplatedParent` source

# Day 04 - Items Controls and TPL
### Projects:
|     |     |
| --- | --- |
| Fun With Items Control | Cool demo of `ListBox` templateing | 
| FunWithTpl | Introduction to Task Parallel Library |

### Items Controls
* We talked about customization of items controls
* We saw how to create a `ControlTemplate` to the items control
  * We saw that we are required to place a `ItemsPresenter` somewhere in the template in order to present the items themselves
* We saw how to customize the layout
  * We learned about the `ItemsPanelTemplate` type
  * We saw how to set the panel using the `ItemsPanel` property. 
* We learned about the item containers
  * `ListBoxItem` for list boxes
  * `ComboBoxItem` for combo boxes
  * `MenuItem` for menus
  * `TreeViewItem` for tree views
* We understood that each items control generates a single "item container" per item it needs to present
* We understood that each item container is:
  * A control, so it may be styled and templated
  * A content control, so we may also template the content
* We saw how to set the style per item container using the `ItemContainerStyle` property
* We saw how to create a custom template for the item container using the style
* In our example, for `ListBoxItem` we saw how to use visual states to determines how an item should look when it is selected
* We saw how to use the item container style to set the panel attached properties
  * In our example we set the `Canvas.Top` and `Canvas.Left` properties
  * We even used Binding inside the style to set these properties
* Finally, we saw how to use the `ItemTemplate` property to set the `DataTemplate` for each item.
  * The item container uses these templates as `ContentTemplate`

### Introduction to TPL
* We talked about the evolution of asynchronous programming though the first versions of the .NET framework
* We defined the concept of `Process` and understood that it
  * Mostly defines a separation of memory
  * Also defines a set of threads
* We understood the concept of `Thread`
  * The only entity that actually runs code
  * May be shared and reused in an application
* We understood that .net framework comes with a `Thread Pool` to make better use of threads
  * avoid creating and destroying threads - that are expensive to allocate
* We understood that the `TPL` model attempts to minimize the number of threads used by the application by reusing them
* We understood what a `Task` is
  * An object describing "something that needs to be completed with result"
  * Contains 2 fields
    * The status (in progress, completed, or failed)
    * The result (or error if failed)
  * We understood that tasks are based on "Push", so you cannot call them to get the result synchronously. Instead, they call you back with the result.
  * We understood that tasks do not run code. Threads do. Tasks are only there to tell you when the code completes.
  * We saw how to create a new task using `Task.Factory.StartNew`
  * We saw how to respond to completion using `Task.ContinueWith`
  * We saw how to set the thread context that the continuation will run on using the `TaskScheduler`
  * We saw that in order to set properties on the ui when tasks complete, we need to set the continuation to run on the main thread.
  * We learned about the `async` and `await` keywords and understood that they are
    * Compilation directives
    * `async` means that the method will be compiled in a different way
    * `await` actually ends the method. Anything after it is in a new method
    * `await` must come before a task object
    * Whatever comes after the `await` is registered as continuation of the Task.
    * `async` and `await` gives us better code readbility


# Day 05 - More TPL and Mvvm
### Projects:
|     |     |
| --- | --- |
| FunWithTpl | Introduction to Task Parallel Library |
| FunWithMvvm | Introduction to DI and techniques in MVVM |

# TPL
* We talked in more depth about `async-await`
  * What does `async void` mean
  * What does `async Task<T>` mean
  * What does an async method return
* We demonstrated how to do task cancallation
  * We understood that it is a mutual process where the running task needs to "agree" to cancel
  * We saw that canceling is done by throwing a `OperationCancelledException`
  * We saw that the way to request cancellation is by passing `CancellationToken` to the task
    * The task itself should check the token from time to time to see if it was triggered
    * The token is generated by a `CancellationTokenSource` instance that can be used to trigger the token
* We saw how to convert sequential algorithm to "functional" one and then run it in parallel using `PLINQ`
* We saw how to report progress using `IProgress<T>`
  * The task receives an `IProgress` and uses the `Report` method to report progress every once in a while
  * The caller creates an instance of `Progress<T>` that wraps a delegate that will be executed **in the calling thread**
* We saw how to create atomic completed tasks from scrarch
  * `Task.FromResult`
  * `Task.FromException`
  * `Task.FromCanceled`
  * `Task.CompletedTask`
* We saw how to create a task that delays asynchronously
  * `Task.Delay`
* We saw how to combine tasks together
  * `Task.WhenAll`
  * `Task.WhenAny`
* Finally we saw an example of how to use `TaskCompletionSource` to control our own `Task`
  * We also used `CancellationToken.Register` to register a callback that will cancel the task if required.

# Blendable MVVM
* We saw how to create a view model base in .net core
* We saw how to inject design time view model into the view using `d:DataContext` and `{d:DesignData}`

# Depndency Injection in .net Core
* We understood why it is important to use .net core dependency injection
* We understood the meaning of `IServiceCollection` and `IServiceProvider`
* We talked about the 3 possible lifecycles
  * Singletons
  * Transient
  * Scoped
* We saw how to create services and register them in the service collection
* We saw how to create a view model using the container
* We created different ctors for Design time and Runtime



  







  


  
