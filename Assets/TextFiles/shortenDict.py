# -*- coding: utf-8 -*-
"""
Created on Fri Jun 10 17:44:20 2022

@author: Robbie
"""

dict_in = open("common1000Raw.txt", "r")
dict_out = open("common6Dict.txt", "w")

for line in dict_in:
    # Cleaning up dictionary entries
    #split_line = line.split()
    #for word in split_line:
    #    dict_out.write(word + '\n')
    
    # Verify word length
    if len(line) < 8 and len(line) > 2:
        dict_out.write(line)

dict_in.close()
dict_out.close()