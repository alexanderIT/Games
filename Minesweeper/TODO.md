TODO:
-----
1. ~~Break up the class Field:~~ Implemented. Also a Factory creates the field. Prints OK to the console.   
	1.1 'Field' should contain only the 2d array, props for rows and cols, ToString implementation    
	1.2 'IGeneratable' interface - property for getting the symbol and method for symbol generation    
	1.3 New class 'Mine' - has a symbol to represent the mine; implements 'IGeneratable' interface    
	1.4 'IGenerator' interface - method for generating 'IGeneratable' objects   
	1.5 New class 'MineGenerator' - implements 'IGenerator'   
	1.6 New class 'AdjacencyMap' - counts the appearance of a 'IGenerable' object around every cell in 2d array   
    
2. ~~Use a creational design pattern to build and populate the field with mines~~ Implemented Factory design pattern   
    
3. The Engine class should implement dependency inversion principle and receive the objects that it will work with   
   
4. ~~The IInputOutputManager interface can be divided into two - rendering interfaces that handles all printing and input
reader interface that handles all input.~~    
    
5. Refactor lifespan of variables and conditional statements ( (variable < interval && interval < variable) OR
(inverval < variable && variable < interval) )
