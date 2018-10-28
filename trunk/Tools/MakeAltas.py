#coding=utf-8

import os
import sys
import Path

print("脚本名：", sys.argv[0])
for i in range(1, len(sys.argv)):
	print ("参数", i, sys.argv[i])
print("\n\n")

def MakeAltas() :
	print ("MakeAltas")

	trunkPath = Path.GetTrunkPath();
	print ("根路徑：", trunkPath)
	tpsPath = os.path.join(trunkPath, "Art/Altas/Proj")
	print ("tps路徑：", tpsPath)
	exe = Path.GetTexturePackerExe();
	print ("exe：", exe)
	altasPath = Path.GetAltasPath();
	print ("altasPath(输出图集路径): ", altasPath);




	print("\n\n")

	tpslist = os.listdir(tpsPath)
	print(tpslist)

	for tps in tpslist:
		print(tps)
		tpspathname = os.path.join(tpsPath, tps)
		#print("tpspathname:" + tpspathname)
		if(os.path.isfile(tpspathname)): 
			tpsName = tpspathname
			#print(tpsName)	
			dataOption = " --data " + os.path.join(altasPath, tps.replace(".tps", ".tpsheet"));
			sheetOption = " --sheet " + os.path.join(altasPath, tps.replace(".tps", ".png"));
			options = dataOption + sheetOption;
			print ("options(选项): ", options)
			commandline = exe + " " + tpsName + options
			print(commandline)
			os.system(commandline)
	print ("Altas maked.")


if sys.argv[0] == __file__ :
	MakeAltas();