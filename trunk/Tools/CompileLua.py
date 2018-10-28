#coding=utf-8

import os
import sys

print("脚本名：", sys.argv[0])
for i in range(1, len(sys.argv)):
	print ("参数", i, sys.argv[i])

toolPath = "D:/Demos/NewFrame/Client/Tools/LuaEncoder/luajit/"
luaPath = "D:/Demos/NewFrame/Client/Assets/Lua/"
luabPath = "D:/Demos/NewFrame/Client/Assets/Lua/luab/"
exeName = "luajit.exe"
os.chdir(toolPath);
lualist = os.listdir(luaPath)
print(lualist)

for lua in lualist:
	luapathname = os.path.join(luaPath, lua)
	print(luapathname)
	print(os.path.splitext(lua)[1] == ".lua")

	if(os.path.isfile(luapathname) and os.path.splitext(lua)[1] == ".lua"):
		print(lua)
		commandline = exeName + " -b " + os.path.join(luaPath + lua) + " " + os.path.join(luabPath + lua)
		print(commandline)
		-- os.system(commandline)




