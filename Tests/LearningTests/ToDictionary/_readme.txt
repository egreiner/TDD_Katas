﻿-> https://ccd-school.de/en/coding-dojo/function-katas/todictionary/

Function Kata „ToDictionary“

Implement a function that maps a special formatted string into a dictionary. 
The function should have the following signature:
IDictionary<string, string> ToDictionary(string input);

The following table shows some examples for input strings and the resulting dictionary.
“a=1;b=2;c=3”	{{“a”, “1”}, {“b”, “2”},{“c”, “3”}}
“a=1;a=2”	{{“a”, “2”}}
“a=1;;b=2”	{{“a”, “1”}, {“b”, “2”}}
“a=”	{{“a”, “”}}
“=1”	Exception
“”	{}
“a==1”	{{“a”, “=1”}}

