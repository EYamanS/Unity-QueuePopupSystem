

2 implementations of the task queue system came to my mind.

The first one is the *Action Queue Implementation*

    It uses callable System.Actions to Enqueue and Dequeue + Execute tasks.
    It should work for basic use cases but for a complex system such as Popup Systems, we'll need more control and abstraction.
    
    PROS:
        - Quick and easier to call, execute.
    CONS:
        - Uses Actions. will create hard to understand/write code while calling system components.
        - Not Abstract enough to be used seperately for each system 
   
   
This is why I come up with the interface implementation.
    Using ITask and ITaskQueue, any system can create a custom queue and work on it.
    I'll designed this by keeping in mind that I'll also create a popup system.
        It will use this implementation.
        
    PROS:
        - Each system that uses ITaskQueue is fully decoupled from other 
          TaskQueues since they implement their own.
        - this "ITask" could be anything or any object in game. Abstracted
        - 