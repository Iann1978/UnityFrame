
import os
import os.path
import shutil
import time
import sys
import Path

print("脚本名：", sys.argv[0])
for i in range(1, len(sys.argv)):
	print ("参数", i, sys.argv[i])



def BuildClient() :

	print ("Building client")
	trunkPath = Path.GetTrunkPath();
	clientPath = os.path.join(trunkPath, "Client");
	#unity_exe = os.environ["UnityExe"];
	unity_exe = Path.GetUnityExe();
	print(unity_exe);
	cmd = "\"" + unity_exe + "\"" + " -quit -executeMethod AssetBundleEditor.Build D:/Editor.log -projectPath " + clientPath;
	print(cmd);
	os.system(cmd);
	print ("Client builded.")



if sys.argv[0] == __file__ :
	BuildClient();