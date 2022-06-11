# -*- coding: utf-8 -*-
"""
Created on Fri Jun 10 17:44:20 2022

@author: Robbie
"""

dict_in = open("common.txt", "r")
dict_out = open("newcommon.txt", "w")

for line in dict_in:
    if len(line) < 6:
        dict_out.write(line)

dict_in.close()
dict_out.close()