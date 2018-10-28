
import os
import os.path
import shutil
import time
import sys
import Path


def CleanClient() :
	trunkPath = Path.GetTrunkPath();
	binBundlePath = os.path.join(trunkPath, "Bin/Bundles");
	if os.path.exists(binBundlePath) :
		shutil.rmtree(binBundlePath)
	binClientPath = os.path.join(trunkPath, "Bin/Client");
	if os.path.exists(binClientPath) :
		shutil.rmtree(binClientPath)

	print("Client Cleaned")


if sys.argv[0] == __file__ :
	CleanClient();

