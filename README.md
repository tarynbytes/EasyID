
# EasyID
EasyID is a tool that launches a local web server. The server hosts a search bar that accepts user input and attempts to identify what a piece of data is. It accepts either a singular data string, or a file full of potential data strings as input. If there is a match, it outputs to the screen what it has identified on, and any extra details about the data. Example: 630-111-2222 matches on phone number and extrapolates an Illinois area code.


### Program Flow
```mermaid
flowchart LR
subgraph EasyID
  subgraph Search.razor
    A{User input} =====> B[Extract input\nproperties] --> Z["Stores in standard\ninput (Driver) object"] --> Y[Load applicable module\nvia decision-making logic]
    M[Process another?]
    Q[Clear or append\nresults]
  end
  subgraph Module.cs
    D[Module inits] --> E["Module properties initialize\nbased on driver object properties"] --> G[Module Processes] --> H[Ouput results\nand info] --> I[Module de_inits]
    R[Match]
    S[No Match]
    D --> F["Module properties fail to initialize\n(A property returns null)"]
  end
I -.- R -.- M -.-> Q --> A
F -.- S -.- M 
Y --> D
end
```


### Class Diagram
```mermaid
classDiagram
    DataTemplate <|-- UnitedStatesPhoneNumber
    Search <|--|> UnitedStatesPhoneNumber
    
    
  class Search {
    +public Driver d;
    +ClearResults()
    +HandleValidSubmit()
    +ParseFile(string Input)
    +ParseData(string Input)
    +ModuleSelect(Driver d)
  }
      
  class DataTemplate {
    +private static Driver _d;
    +public static void SetDriver()
    +public abstract string Process()
  }
  class Driver {
    +private int _length;
    +private string _content;
    +private string _results;
    +private string _letterList;
    +private string _numList;
    +private string _symList;
    +private string _indexList;
    +private string _noWhiteSpace;
    +private string _noWhiteSpaceContent;
    +private string _noWhiteSpaceLetterList;
    +private string _noWhiteSpaceNumList;
    +private string _noWhiteSpaceSymList;
    +private string _noWhiteSpaceIndexList;
    +private Dictionary<string, string> _hex;
    +private int _atSymbolCount;
    +private int _youTubeCount;
    +private string _btcStart;
    +public string? Input
    +public string? Type;
    +public bool IsValidatedDesign;
    +public int Length;
    +public string NoWhiteSpace;
    +public string Content;
    +public string NoWhiteSpaceContent;
    +public string LetterList;
    +public string NumList;
    +public string SymList;
    +public string IndexList;
    +public string NoWhiteSpaceLetterList;
    +public string NoWhiteSpaceNumList;
    +public string NoWhiteSpaceSymList;
    +public string NoWhiteSpaceIndexList;
    +public Dictionary<string, string> Hex;
    +public int AtSymbolCount;
    +public int YouTubeCount;
    +public string BtCStart;
    +public string Results;
    +public void Reset()
  }
  class UnitedStatesPhoneNumber {
    +private Driver _driver;
    +private List<int> _lengthList;
    +private List<string> _contentList;
    +private List<string> _indexList
    +private List<string> _symList;
    +private string _letterList;
    +private string _areaCode;
    +private string _areaCodeCallerID;
    +private string _phoneInput;
    +private string _phoneInputIndexList;
    +private string _phoneInputSymList;
    +public int? Length;
    +public string? Content;
    +public string? IndexList;
    +public string? SymList;
    +public string? LetterList;
    +private void ParseDriver()
    +private void GetPhoneInputIndexList()
    +private void GetPhoneInputSymList()
    +private void GetAreaCode()
    +private void GetCallerID()
    +public override string Process()
    +public override string ToString()
  }
  class URL {
    +private Driver _driver;
    +private int _length;
    +private List<string> _contentList;
    +private string _uriHostNameType;
    +private string _urlDetails;
    +private string _proto;
    +private string _url;
    +private string _tld;
    +public int? Length;
    +public string? Content;
    +public string? UriHostNameType;
    +public string? URLDetails;
    +public string? TopLevelDomain;
    +private void ParseUrl()
    +public override string Process()
    +public override string ToString()
  }
  class USStateAbbreviation {
    +private Driver _driver;
    +private List<int> _lengthList;
    +private List<string> _contentList;
    +private string? _symList;
    +private string? _numList;
    +private Dictionary<string, string> _abbpreviations;
    +public string? Abbreviations;
    +public int? Length;
    +public string? Content;
    +public string? IndexList;
    +public string? SymList;
    +public string? NumList;
    +public override string Process()
    +public override string ToString()
  }
  class SocialSecurityNumber {
    +private Driver _driver;
    +private List<int> _lengthList;
    +private List<string> _contentList;
    +private List<string> _indexList
    +private List<string> _symList;
    +private string? _letterList;
    +private string _ssnState;
    +private List<int, int, string> _states;
    +public int? Length;
    +public string? Content;
    +public string? IndexList;
    +public string? SymList;
    +public string? LetterList;
    +public int? SSNFirstThree;
    +public override string Process()
    +public override string ToString()
  }
  class IPv4 {
    +private Driver _driver;
    +private List<int> _lengthList;
    +private string _content;
    +private List<string> _symList;
    +private string? _letterList;
    +private int[] _octets;
    +public int[] Octets;
    +public int? Length;
    +public string? Content;
    +public string? SymList;
    +public string? LetterList;
    +public override string Process()
    +private string PrivateIP(int[] octets)
    +private string IPClass(int[] octets)
    +public override string ToString()
  }
```
