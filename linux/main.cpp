/*
	Strayex Shell for Linux
	Copyright © 2019 Daniel Strayker Nowak
	All rights reserved
 */

#include <cstdio>

void Cmd_interpret(string cmd)
{
	// Command name is the first word, next words are args,
	// If user proviede args for commands, that don't need them, shell will ignore them,

	// Comands:
	if (cmd == "hello") cout << "Hello user! :D" << endl; // Say hi to user :)

	// TODO: Chech code on Linux with GCC!
}

int main()
{
	// Standard shell's header:
    cout << "Strayex Shell for Linux v1.0.0" << endl;
	cout << "Copyright (c) 2019 Daniel Strayker Nowak" << endl;
	cout << "All rights reserved" << endl;

	// Command routine:
	string temp = "";
	while (temp != "exit")
	{
		cout << "> ";
		cin >> temp;
		Cmd_interpret(temp);
	}

    return 0;
}