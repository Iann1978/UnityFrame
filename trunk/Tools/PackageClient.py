
import shutil
import os
import sys

import Path
from CleanClient import CleanClient

from MakeAltas import MakeAltas
from BuildClient import BuildClient



def CopyFolder(olddir, newdir) :
	if not os.path.exists(newdir) or os.path.isfile(newdir) : 
		os.mkdir(newdir);
	copyfiles = os.listdir(olddir)
	print (copyfiles)
	for file in copyfiles :
		oldfile = os.path.join(olddir,file) 
		newfile = os.path.join(newdir,file) 
		oldfile = oldfile.replace("\\", "/")
		newfile = newfile.replace("\\", "/")
		print("oldfile:" + oldfile)
		print("newfile:" + newfile)
		if os.path.isfile(oldfile) :
			shutil.copyfile(oldfile, newfile)
			print(file + " Copied")
		else :
			
			shutil.copytree(oldfile, newfile)
			print(file + " Copied");


def CopyBundles() :
	trunkPath = Path.GetTrunkPath();
	srcBundlePath = os.path.join(trunkPath, "Bundles")
	binBundlePath = os.path.join(trunkPath, "Bin/Bundles");
	shutil.copytree(srcBundlePath, binBundlePath)
	print ("Bundles copied")

def CopyLua() :
	print("\n CopyLua");
	trunkPath = Path.GetTrunkPath();
	binLuaPath = os.path.join(trunkPath, "Bin/Client/Lua");
	#srcLuaPath1 = os.path.join(trunkPath, "Client/Assets/Lua");
	#srcLuaPath0 = os.path.join(trunkPath, "Client/Assets/Thirds/ToLua/ToLua/Lua");
	logicLuaPath = Path.GetLogicLuaPath();
	systemLuaPath = Path.GetSystemLuaPath();
	systemLuaPath = systemLuaPath.strip('/');
	print("binLuaPath: " + binLuaPath);
	print("logicLuaPath:" + logicLuaPath);
	print("systemLuaPath:" + systemLuaPath);	
	shutil.copytree(logicLuaPath, binLuaPath)
	CopyFolder(systemLuaPath, binLuaPath)
	print ("Lua scripts copied")

def PackageClient() :
	CleanClient();
	MakeAltas();
	BuildClient();
	CopyBundles();
	CopyLua();

if sys.argv[0] == __file__ :
	PackageClient();
