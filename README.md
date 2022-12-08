
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
    Data <|-- PhoneNumber
    Search <|--|> PhoneNumber
    
    Data : +init()
    Data : +process()
    Data : +deInit()
    
  class Search {
    +parseInput()
    +storeInput()
    +moduleSelect()
  }
  
  class PhoneNumber {
    +String type
    +String content
    +int length
    +init(userInput)
    +process()
    +countryCode()
    +areaCode()
    +deInit()
  }
```
