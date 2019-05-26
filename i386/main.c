/*
    Strayex Shell Hobby Operating Systems
    Copyright © 2019 Daniel Strayker Nowak
    All rights reserved
 */

#include <stdio.h>

void Cmd_interpret(char cmd[])
{
	// Command name is the first word, next words are args,
	// If user proviede args for commands, that don't need them, shell will ignore them,

	// Comands:
	if (cmd == "hello") printf("Hello user! :D\n"); // Say hi to user :)

	// TODO: Chech code on Strayex with GCC!
}

int main()
{
	// Standard shell's header:
    printf("Strayex Shell Hobby Operating Systems v1.0.0\n");
	printf("Copyright (c) 2019 Daniel Strayker Nowak\n");
	printf("All rights reserved\n");
	
	// Command routine:
	char temp[255] = "";
	while (temp != "exit")
	{
		printf("> ");
		scanf(temp, stdin);
		Cmd_interpret(temp);

		for (int i = 0; i < 255; i++) temp[i] = '\0';
	}
}