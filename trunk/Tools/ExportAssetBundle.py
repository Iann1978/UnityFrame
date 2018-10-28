#coding=utf-8

import os
import sys
import Path


def ExportAssetBundle() :
	exe = Path.GetUnityExe();
	commandline = exe + " -quit -batchmode -executeMethod AssetBundleEditor.MakeAssetBundle"
	os.system(commandline);

if sys.argv[0] == __file__ :
	ExportAssetBundle();