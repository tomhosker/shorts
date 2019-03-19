This is my solution to the attached "Simple Calculator" problem sheet.

My interpretation of "lazy evaluation" allows for registers to be defined in
a circular fashion. However, when infinite recursion seems to be underway,
my program sets the register in question to zero and continues to the next
line.

#######

I built this program on my Ubuntu laptop. I would compile using:

  javac Calculator.java

To run the unit tests, I would use:

  java Calculator Test

Whereas to run a demonstration I would use:

  java Calculator Demo

To run a particular input file I would use:

  java Calculator filename

I hope some of that is helpful for running my program on a Windows machine!
