# Strayex Shell Commands

## Strayex Shell for Windows

- _hello_ - Prints welcme message on screen,
No args

- _clear_ - Cleans terminal,
No args

- echo - Prints data in terminal,
This command can write already defined data:
`echo Hi!` will write "Hi!" in terminal,
`echo Hi there!` will write "Hi there!" in terminal,
However, it can be used to print out values from shell's session variables:
When variable named "MyString" has a value "Hi!" use Dolar char first, then type ou variable name:
`echo $MyString` will "Hi!" in terminal,
To serve scripting echo can be turned off:
`echo /off`
To turn it on:
`echo /on`
Help is under:
`echo help`

- _cd_ - Changes active working environment of shell,
Every command interpreted in shell is executed in the active working environment, the given file system path.
Strayex Shell by default takes working environemt as shell's start folder.
For example,to set working environment on C partition, should be used:
`cd C:\`

- _help_ - Prints out all shell's commands,
No args

- _color_ - Changes background  and font colors,
When this command will have one argument, shell will interpret it as background color change:
`color Black`
However, if there will be two arguments, first will be background color, second will be font color:
`color Black Green`
To load back default settings should be used:
`color reset`
Supported colors:
- For background - black, blue, green, white,
- For fonti - black, blue, green, white,

- _set_ - Creates environment variables,
Environment variables are "containers" in memory, to hold given value. They are created by giving name in first argument and value in second.
Value can be string or integer type.
`set MyString Hi!` creates variable of name "MyString" with string type value "Hi!",
To create integer type variable:
`set MyInt .123`
To delete variable:
`set ! MyString`
Prints out list of all variables in active shell session:
`set list`
Help is under:
`set help`

- _exit_ - Closes the shell,
This command don't stop called processes and don't save any work in environment.
No args

Strayex Shell command's interpreter checks input data in given order:
- If this is command,
- If this is script (if it's starting by dor),
- If this is file to open or third-party program,
- If this is program given with extension or not,
If the interpret will not find anything, shell will print out information about not finding command or program.

## Strayex Shell for Linux



## Strayex Shell for Hobby OSes


