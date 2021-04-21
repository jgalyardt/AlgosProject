# AlgosProject

CSE 7350 Final Project 

Full report found here: https://docs.google.com/document/d/1ma0nuc9Mgt1yzgbWw9eaB9Wsdvd7Y8OEJaOKdhPob-A/edit?usp=sharing

Goal of this project was to generate a color a vertex graph, which represents course schedules for a hypothetical school.
All code is in C#.
The project had several parts:

1) Generate four random-distribution functions (I used Inverse Transform Sampling to achieve this) and use them to generate random course schedules for every student.
2) Create two methods for determining course conflicts (I used an AVL Tree and a psuedo-dictionary-thing [it's just a really big array])
3) Export the generated graphs to a text file, one for the Vertices and one for the Edges
4) Order and color the vertices with various implemented algorithms (general problem explained here: https://en.wikipedia.org/wiki/Graph_coloring)
    a) Smallest Last ordering
    b) Welsh-Powell ordering
    c) Random ordering
    d) "Bogo" ordering, a fourth algortihm that was up to us to come up with, just for fun. Random wasn't bad enough, so I made a worse one.
5) Evaluate the run-time efficiency of each of these steps
 
