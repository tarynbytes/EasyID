# project

```mermaid
flowchart TB
subgraph project_name
  subgraph Driver
  direction TB
    subgraph  
      direction TB
        A{User input} =====> B[Extract input\nproperties] --> Z[Stores in standard\ninput object] --> Y[Load applicable module\nvia decision-making logic]
    end
   M[Process another?]
  end
  subgraph Module
    direction TB
    subgraph  
      direction TB
        D[Module inits] --> E((init pass)) --> G[Module Processes] --> H[Ouput results\nand info] --> I[Module de_inits]
    end
    D --> F((init fail))
    F --> J[1. Ouput top\nthree guesses?] & K[2. Percent\nlikelihood?] & L[3. Return to\ntry again?]
  end
I -.- M -.-> A
L --> Y
end
