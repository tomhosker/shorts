/*
This code holds the "Calculator" class.
*/

import java.util.*;
import java.io.*;

class Calculator
{
  // Constants.
  public final static Type OPERATION = Type.OPERATION, PRINT = Type.PRINT,
                           QUIT = Type.QUIT;
  public final static Type ADD = Type.ADD, SUBTRACT = Type.SUBTRACT,
                           MULTIPLY = Type.MULTIPLY;
  public final static Type REGISTERNAME = Type.REGISTERNAME,
                           NUMBER = Type.NUMBER, OP = Type.OP;
  public final static Type BAD = Type.BAD;
  public static final int maxloops = 1000, operationLineWidth = 3;

  // Fields.
  private String inputCode = "";
  private String[] lines = {};
  private List<Register> registers = new ArrayList<Register>();
  private int lastPrintStatement = 0;

  /*
  ################
  # CORE METHODS #
  ################
  */

  // Ronseal.
  String purgeNonAlphaNumeric(String s)
  {
    Type t = BAD;
    String result = s, cString = "";
    char c = '0';

    for(int i = 0; i < s.length(); i++)
    {
      c = s.charAt(i);
      cString = String.valueOf(c);

      if((t.isAlphaNumeric(cString) == false) && (c != '\n') && (c != ' '))
      {
        result = result.replace(cString, "");
      }
    }

    return(result);
  }

  // Ronseal.
  void setInputCode(String anyString)
  {
    String buffer = "";

    inputCode = anyString;

    // Make code case-insensitive, and suchlike.
    buffer = inputCode.toLowerCase();
    buffer = purgeNonAlphaNumeric(buffer);

    lines = buffer.split("\n");
  }

  // Ronseal.
  void setInputCodeFromFile(String filename)
  {
    File file = new File(filename);
    BufferedReader reader = null;
    String result = "", line = "";

    try{
      reader = new BufferedReader(new FileReader(file));

      while((line = reader.readLine()) != null)
      {
        result = result+line+"\n";
      }
    }
    catch(Exception e){
      System.out.println("Help! File not found.");
      return;
    }

    setInputCode(result);
  }

  // Adds a new register, of a given name, to the list.
  void addRegister(String name)
  {
    Register register = new Register();

    for(int i = 0; i < registers.size(); i++)
    {
      if(registers.get(i).getName().equals(name)) return;
    }

    register.setName(name);
    registers.add(register);
  }

  // Executes the code which has been read into this our Calculator.
  void runLines()
  {
    Type t = BAD;

    for(int i = 0; i < lines.length; i++)
    {
      if(t.classifyLine(lines[i]) == BAD) continue;
      else if(t.classifyLine(lines[i]) == QUIT) return;
      else if(t.classifyLine(lines[i]) == PRINT)
      {
        printRegister(lines[i]);
      }
      else if(t.classifyLine(lines[i]) == OPERATION)
      {
        runALine(lines[i]);
      }
    }
  }

  // Calculates a register's present value based on its "Lazy Evaluation"
  // data.
  int evaluate(String registerName, int loop)
  {
    int result = 0, right = 0;
    String[] words;
    List<String> lazyEvaluationData = new ArrayList<String>();
    Type t = BAD;

    for(int i = 0; i < registers.size(); i++)
    {
      if(registers.get(i).getName().equals(registerName))
      {
        lazyEvaluationData = registers.get(i).getLazyEvaluationData();
        break;
      }
      else if(i == registers.size()-1) throw new Error("No such register.");
    }

    if(loop > maxloops) return(infiniteRecursion(registerName));

    for(int i = 0; i < lazyEvaluationData.size(); i++)
    {
      words = lazyEvaluationData.get(i).split(" ");

      if(t.classifyWord(words[1]) == NUMBER)
      {
        right = Integer.parseInt(words[1]);
      }
      else right = evaluate(words[1], loop+1);

      if(t.classifyOp(words[0]) == ADD) result = result+right;
      else if(t.classifyOp(words[0]) == SUBTRACT) result = result-right;
      else if(t.classifyOp(words[0]) == MULTIPLY) result = result*right;
    }

    return(result);
  }

  int infiniteRecursion(String registerName)
  {
    System.out.println("Help! Infinite recursion!");
    System.out.println("Register "+registerName+" is defined in terms of "+
                       "another register, which itself is defined in "+
                       "terms of "+registerName+".");
    System.out.println("Therefore, I cannot compute the value of "+
                       registerName+".");
    System.out.println("However, I shall set "+registerName+" to zero "+
                       "for the time being.");

    return(0);
  }

  // Prints a register to the screen.
  void printRegister(String line)
  {
    String registerName = "";
    int registerValue = 0;
    String[] words = line.split(" ");

    if(words.length < 2)
    {
      System.out.println("Help! Print command but register specified.");
      return;
    }
    else registerName = words[1];

    registerValue = evaluate(registerName, 1);
    System.out.println(registerValue);
    lastPrintStatement = registerValue;
  }

  // Adds an entry to the register's "lazy evaluation" data.
  void writeToRegister(String registerName, String s)
  {
    String thisRegisterName = "";
    String thatRegisterName = registerName;

    for(int i = 0; i < registers.size(); i++)
    {
      thisRegisterName = registers.get(i).getName();
      if(registerName.equals(thisRegisterName))
      {
        registers.get(i).addLazyEvaluationData(s);
        return;
      }
    }

    addRegister(registerName);
    writeToRegister(registerName, s);
  }

  // Executes a given line.
  void runALine(String line)
  {
    String[] words = line.split(" ");
    Type t = BAD;
    String leftRegister = "a", operation = "add", right = "0";
    int i = 0;

    if(words.length != operationLineWidth)
    {
      System.out.println("Too many, or too few, words in line: \""+
                         line+"\"");
      return;
    }

    if(t.classifyWord(words[i]) == REGISTERNAME)
    {
      leftRegister = words[i];
    }
    else
    {
      printBadLine(line);
      return;
    }
    i++;

    if(t.classifyWord(words[i]) == OP) operation = words[i];
    else
    {
      printBadLine(line);
      return;
    }
    i++;

    if((t.classifyWord(words[i]) == NUMBER) ||
       (t.classifyWord(words[i]) == REGISTERNAME))
    {
      right = words[i];
    }
    else
    {
      printBadLine(line);
      return;
    }

    writeToRegister(leftRegister, operation+" "+right);
  }

  // Prints an error message to the screen.
  void printBadLine(String line)
  {
    System.out.println("Bad syntax in line: \""+line+"\"");
  }

  // Reads in an input file and then runs the result.
  void readAndRun(String filename)
  {
    registers = new ArrayList<Register>();
    setInputCodeFromFile(filename);
    runLines();
  }

  /*
  ##############
  # UNIT TESTS #
  ##############
  */

  // Checks an assertion.
  void claim(boolean b)
  {
    if(!b) throw new Error("Test "+testNumber+" fails");
    testNumber++;
  }
  private int testNumber = 1;

  // Runs the unit tests.
  void test()
  {
    // Tests 1-2.
    Calculator testCalculator = new Calculator();
    testCalculator.setInputCode("Hello, world!");
    claim(testCalculator.inputCode == "Hello, world!");
    testCalculator.setInputCode("Hello,\n world!");
    claim(testCalculator.lines[0].equals("hello"));

    // Tests 3-5.
    testCalculator.addRegister("a");
    claim(testCalculator.registers.size() == 1);
    testCalculator.addRegister("b");
    claim(testCalculator.registers.size() == 2);
    testCalculator.addRegister("a");
    claim(testCalculator.registers.size() == 2);

    // Tests 6-8.
    testCalculator = new Calculator();
    String test1 = "A add 2\n"+
                   "A add 3\n"+
                   "print A\n"+
                   "B add 5\n"+
                   "B subtract 2\n"+
                   "print B\n"+
                   "A add 1\n"+
                   "print A\n"+
                   "quit";
    String test2 = "a add 10\n"+
                   "b add a\n"+
                   "b add 1\n"+
                   "print b\n"+
                   "QUIT";
    String test3 = "result add revenue\n"+
                   "result subtract costs\n"+
                   "revenue add 200\n"+
                   "costs add salaries\n"+
                   "salaries add 20\n"+
                   "salaries multiply 5\n"+
                   "costs add 10\n"+
                   "print result\n"+
                   "QUIT";
    testCalculator.setInputCode(test1);
    testCalculator.runLines();
    claim(testCalculator.lastPrintStatement == 6);
    testCalculator = new Calculator();
    testCalculator.setInputCode(test2);
    testCalculator.runLines();
    claim(testCalculator.lastPrintStatement == 11);
    testCalculator = new Calculator();
    testCalculator.setInputCode(test3);
    testCalculator.runLines();
    claim(testCalculator.lastPrintStatement == 90);
  }

  // Runs the demo.
  void demo()
  {
    Calculator demoCalculator = new Calculator();
    System.out.println("Running demo1...");
    demoCalculator.readAndRun("demo1.txt");
    System.out.println("Running demo2...");
    demoCalculator.readAndRun("demo2.txt");
    System.out.println("Running demo3...");
    demoCalculator.readAndRun("demo3.txt");
  }

  /*
  ###################
  # RUN AND WRAP UP #
  ###################
  */

  void run(String[] args)
  {
    if(args.length == 0)
    {

    }
    else if((args.length == 1) && (args[0].equals("Test")))
    {
      test();
      System.out.println("Tests passed!");
    }
    else if((args.length == 1) && (args[0].equals("Demo")))
    {
      demo();
    }
    else if(args.length == 1)
    {
      readAndRun(args[0]);
    }
    else
    {
      System.err.println("Please run this class without arguments, or "+
                         "run it with a single argument:");
      System.err.println("\"Test\" to run the unit tests, or \"Demo\" to "+
                         "run a demonstration of what this class can do.");
      System.exit(1);
    }
  }

  public static void main(String[] args)
  {
    Calculator program = new Calculator();
    program.run(args);
  }
}
