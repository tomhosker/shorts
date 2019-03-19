/*
An enum type to hold the various classifications of line and word.
*/

enum Type
{
  QT, PT, REGISTERNAME, OP, NUMBER,
  OPERATION, PRINT, QUIT,
  ADD, SUBTRACT, MULTIPLY,
  BAD;

  // Determines whether a string contains only the characters 0-9 and a-z.
  boolean isAlphaNumeric(String s)
  {
    for(int i = 0; i < s.length(); i++)
    {
      if(s.charAt(i) < '0') return(false);
      else if((s.charAt(i) > '9') && (s.charAt(i) < 'a')) return(false);
      else if(s.charAt(i) > 'z') return(false);
    }

    return(true);
  }

  // Determines whether a string represents a number.
  boolean isNumeric(String s)
  {
    for(int i = 0; i < s.length(); i++)
    {
      if((i == 0) && (s.charAt(i) == '0')) continue;
      else if(s.charAt(i) < '0') return(false);
      else if(s.charAt(i) > '9') return(false);
    }

    return(true);
  }

  // Ronseal.
  Type classifyWord(String word)
  {
    if(word.equals("quit")) return(QT);
    else if(word.equals("print")) return(PT);
    else if(word.equals("add")) return(OP);
    else if(word.equals("subtract")) return(OP);
    else if(word.equals("multiply")) return(OP);
    else if(isNumeric(word)) return(NUMBER);
    else if(isAlphaNumeric(word)) return(REGISTERNAME);
    else
    {
      printBadWord(word);
      return(BAD);
    }
  }

  // Ronseal.
  Type classifyLine(String line)
  {
    String[] words = line.split(" ");

    if(classifyWord(words[0]) == QT) return(QUIT);
    else if(classifyWord(words[0]) == PT) return(PRINT);
    else if((words.length >= 2) && (classifyWord(words[1]) == OP))
    {
      return(OPERATION);
    }
    else
    {
      System.out.println("Unable to classify line: \""+line+"\"");
      return(BAD);
    }
  }

  // Ronseal.
  Type classifyOp(String op)
  {
    if(op.equals("add")) return(ADD);
    else if(op.equals("subtract")) return(SUBTRACT);
    else if(op.equals("multiply")) return(MULTIPLY);
    else
    {
      printBadWord(op);
      return(BAD);
    }
  }

  // Ronseal.
  void printBadWord(String word)
  {
    System.out.println("Unable to classify word: \""+word+"\"");
  }
}
