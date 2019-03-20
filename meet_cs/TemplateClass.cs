/*
A template class in C#.
*/

using System;

namespace Templates
{
  class TemplateClass
  {
    /*
    ##############
    # UNIT TESTS #
    ##############
    */

    // A custom version of "Assert".
    void claim(bool b, string errorMessage)
    {
      if(!b) throw new Exception(errorMessage);
    }

    // Runs the unit tests
    void test()
    {
      claim(true, "This test was supposed to pass.");
//      claim(false, "This test was supposed to fail.");

      Console.WriteLine("Tests passed!");
    }

    /*
    ###################
    # RUN AND WRAP UP #
    ###################
    */

    void run(string[] args)
    {
      if(args.Length == 0)
      {

      }
      else if((args.Length == 1) && (args[0].Equals("Test")))
      {
        test();
      }
      else
      {
        Console.WriteLine("Please run this class without arguments, or "+
                          "with a single argument, \"Test\", to run the "+
                          "unit tests.");
      }
    }

    public static void Main(string[] args)
    {
      TemplateClass program = new TemplateClass();

      Console.WriteLine("Running template class...");
      program.run(args);
    }
  }
}
