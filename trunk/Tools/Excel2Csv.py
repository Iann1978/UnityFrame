#coding=utf-8

import xlrd
import os


def ExportOne(excel,csv) :
	data = xlrd.open_workbook(excel)
	table = data.sheets()[0];
	nrows = table.nrows;
	ncols = table.ncols;
	fo = open(csv, "w");
	for r in range(nrows):
		for c in range(ncols):
			print(table.cell(r,c).value);
			fo.write(str(table.cell(r,c).value));
			fo.write(",");
		fo.write("\n");
	fo.close();

def ExportForlder(excelPath, csvPath) :
	excellist = os.listdir(excelPath)
	for excel in excellist:
		excelPathname = os.path.join(excelPath, excel)
		if (os.path.isfile(excelPathname)):
			excelPurename = os.path.splitext(excel)[0]
			csv = excelPurename + ".csv"
			ExportOne(excelPathname, os.path.join(csvPath, csv))

excelPath = "D:/Demos/NewFrame/Excels/"
csvPath = "D:/Demos/NewFrame/Csv/"

ExportForlder(excelPath, csvPath)



