"""
Create a text file containing just the names of files in the given folder
"""

import sys
import time
import pathlib

if len(sys.argv) != 2:
    raise Exception("argument required: path to folder")

files = pathlib.Path(sys.argv[1]).glob("*.*")
filename = str(int(time.time()))+".txt"
with open(filename, 'w') as f:
    f.write("\n".join([x.name for x in files]))
print(f"Saved: {filename}")
