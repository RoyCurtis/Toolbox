The **Toolbox** is a .NET 4 Framework utility library that provides:

* A collection of extensions to the standard framework namespaces and classes
* A basic logging framework with tag-based console logging available

# Usage

Just search for **Toolbox** in the NuGet repository or [visit the page here](https://www.nuget.org/packages/Toolbox/). Alternatively, [visit the Releases section](https://github.com/RoyCurtis/Toolbox/releases).

# Framework
## Extensions

* **System.Collections.Generic.*TCollections*** : Safe methods for getting values from dictionaries without exceptions, adding ranges to sets and case-insensitive string contains
* **System.*TConsole*** : Thread-safe extra writing functions for the Console, mainly in color
* **System.*TDateTime*** : Extensions for quickly determining seconds-until-now and unix timestamps
* **System.*TEnums*** : Syntactic sugar for enum parsing
* **System.*TMath*** : Methods for clamping numbers
* **System.*TRandom*** : Methods for quickly accessing psuedo-random data from a static instance of Random
* **System.Text.RegularExpressions.*TRegex*** : Shortcut methods for case-insensitive regex matching/replacements and try methods
* **System.*TString*** : Methods for cleaner string splits, mapping, case-insensitive equality checking and syntactic sugar for formatting

## Logging

### Top-level

* **System.*Log*** : Global logging channel methods, including one for each log level and a QuickSetup for console logging. Also has extensions for quick-logging exceptions.
* **System.*LogChannel*** : Represents a "channel" where logging messages can be sent to and loggers attached for handling those messages
* **System.*LogLevels*** : Flag enum of logging levels supported by this framework, including combination flags

### Loggers
* **System.Loggers.*ILogger*** : Interface for implementing a log handler
* **System.Loggers.*ConsoleLogger*** : Basic handler for outputting log messages to console, with varying colors based on log level. Can be paused, with automatic printout of backlog when unpaused
* **System.Loggers.*FileLogger*** : Basic handler for outputting log messages to files in UTF-8 encoding which can be paused 

# Examples
## Logging
```cs
/// Quick start example with console-logger and all logging levels
Log.QuickSetup();
Log.Info("Application", "Hello, world!");
```

```cs
/// Quick start for debugging/production code
#if DEBUG
Log.QuickSetup(LogLevels.Debugging);
#else
Log.QuickSetup(LogLevels.Production);
#endif

Log.Info("Application", "Hello, world!");
```

```cs
/// Multiple loggers
Log.Attach( new ConsoleLogger() );
Log.Attach( new FileLogger("Log.txt") );
Log.Info("Application", "Hello, world!");
```