/*
This code holds the "Register" class.
*/

import java.util.*;

class Register
{
  // Fields.
  private String name = "";
  private List<String> lazyEvaluationData = new ArrayList<String>();

  // Setters.
  void setName(String s) { name = s; }

  // Getters.
  String getName() { return(name); }
  List<String> getLazyEvaluationData() { return(lazyEvaluationData); }

  /*
  ################
  # CORE METHODS #
  ################
  */

  // Adds more code to the lazy evaluation data.
  void addLazyEvaluationData(String code)
  {
    lazyEvaluationData.add(code);
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
    else
    {
      System.err.println("Please run this class without arguments or, to ");
      System.err.println("test it, run it with a single argument, Test.");
      System.exit(1);
    }
  }

  public static void main(String[] args)
  {
    Register program = new Register();
    program.run(args);
  }
}
