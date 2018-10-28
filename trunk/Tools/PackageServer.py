

import os;
import sys;
import shutil;
import Path;


def CleanServer() :
	trunkPath = Path.GetTrunkPath();
	binServerPath = os.path.join(trunkPath, "Bin/Server");
	if (os.path.exists(binServerPath)) :
		shutil.rmtree(binServerPath);
	print ("Server Cleaned");

def CopyServerToBin() :
	trunkPath = Path.GetTrunkPath();
	srcServerPath = os.path.join(trunkPath,"Server/HallAndRoomServer/bin/Debug");
	binServerPath = os.path.join(trunkPath, "Bin/Server");
	shutil.copytree(srcServerPath, binServerPath);
	print ("Server Copied");

def CompileServer() :
	truckPath = Path.GetTrunkPath();
	serverSlnPath = os.path.join(truckPath, "Server/HallAndRoomServer.sln");
	exe = Path.GetMsvsExe();
	print ("Begin compiling server");
	cmd = exe + " " +  serverSlnPath + " /rebuild";
	print (cmd);
	os.system(cmd);
	print ("Server Compiled");


def PackageServer() :
	CleanServer();
	CopyServerToBin();

def Main() :
	if (len(sys.argv) == 1) :
		PackageServer();
		return;

	fun = sys.argv[1];
	if (fun == "CleanServer") :
		CleanServer();
		return;

	if (fun == "CompileServer") : 
		CompileServer();
		return;


if sys.argv[0] == __file__ :
	Main()

	