# project

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
