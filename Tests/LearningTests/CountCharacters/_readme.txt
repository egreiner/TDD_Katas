Count Characters
Function Kata “Count Characters”

Write a function to count the number of occurrences of characters in a string. 
The input to the function is a string, its output a Dictionary (map) of char/int pairs.
IDictionary<char, int> CountCharacters(string input)

Processing the string “HelLo wOrld” would result in:
H:1, e:1, l:2, L:1, o:1, w:1, O:1, r:1, d:1, _:1

(The character “_” is representing a space character.)


Variations #1

Handle upper and lower case the same. The string “HelLo wOrld” would then result in:
H:1, e:1, l:3, o:2, w:1, r:1, d:1, _:1


