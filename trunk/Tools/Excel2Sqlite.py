#coding=utf-8

import xlrd
import os
import sqlite3
import sys
import Path

def OpenDb(db_file_name) :
	conn = sqlite3.connect(db_file_name);
	cursor = conn.cursor();
	return {'conn': conn, 'cursor': cursor}

def CloseDb(db) :
	db['conn'].close();

def LoadExcel(excel_file_name) :
	data = xlrd.open_workbook(excel_file_name)
	table = data.sheets()[0];
	nrows = table.nrows;
	ncols = table.ncols;
	return {'data': data, 'table': table, 'nrows': nrows, 'ncols': ncols}

def GetColumns(excel) :
	data = excel['data']
	table = data.sheets()[0];
	nrows = table.nrows;
	ncols = table.ncols;

	columns = '';
	for c in range(ncols):
		columns += table.cell(0,c).value + ',';
	columns = columns[0:len(columns)-1];
	print('Columns:  ' + columns);
	return columns;

def GetColumnAndTypes(excel) :
	data = excel['data']
	table = data.sheets()[0];
	nrows = table.nrows;
	ncols = table.ncols;

	columns = '';
	for c in range(ncols):
		columns += table.cell(0,c).value + " " + table.cell(1,c).value + ',';
	columns = columns[0:len(columns)-1];
	print('ColumnAndTypes:   ' + columns);
	return columns;

def GetTableName(excel_file_name) :
	tmp = os.path.splitext(excel_file_name)[0].split('/')[-1]
	print('table_name:  ' + tmp)

	return tmp

def DropTable(db, table_name) :
	print("DropTable " + table_name)
	conn = db['conn']
	cursor = db['cursor']
	cmd = 'drop table ' + table_name;
	
	try:
		cursor.execute(cmd);
	except Exception as err:
		 print("OS error: {0}".format(err))
		 pass
	else:
		pass
	finally:
		pass

def CreateTable(db, table_name, columntypes) :
	print("CreateTable " + table_name)
	cursor = db['cursor']
	cmd = 'create table ' + table_name + '(' + columntypes + ')'
	cursor.execute(cmd)

def MigratingData(db, excel, table_name, columns) :	
	data = excel['data']
	table = data.sheets()[0];
	nrows = table.nrows;
	ncols = table.ncols;

	conn = db['conn'];
	cursor = db['cursor']

	for r in range(2,nrows):
		cmd = 'insert into ' + table_name + '(' + columns + ') values (';
		values = '';
		for c in range(ncols):
			if (table.cell(1,c).value=='string'):
				values += '\'' + str(table.cell(r,c).value) + '\'' + ',';
			else:
				values += str(table.cell(r,c).value) + ',';
		values = values[0:len(values)-1];
		cmd += values + ')'
		print(cmd);
		cursor.execute(cmd);
	conn.commit();


def MigratingOneExcel(db, excel_file_name) :
	print('MigratingOneExcel ' + excel_file_name);
	excel = LoadExcel(excel_file_name)
	table_name = GetTableName(excel_file_name)
	columns = GetColumns(excel);
	columntypes = GetColumnAndTypes(excel);
	DropTable(db, table_name);
	CreateTable(db, table_name, columntypes)
	MigratingData(db, excel, table_name, columns)

def MigratingForlder(db, excel_folder) :
	print('MigratingForlder')
	print('excel_folder:  ' + excel_folder)
	excel_list = os.listdir(excel_folder)
	print('excel_list:  ' + str(excel_list))
	for excel_name in excel_list:
		print('\n')
		excel_file_name = os.path.join(excel_folder, excel_name)
		MigratingOneExcel(db, excel_file_name)



def Excel2Sqlite() :
	excel_folder = Path.GetExcelPath();
	local_db = Path.GetLocalDb();
	db = OpenDb(local_db)
	MigratingForlder(db, excel_folder)
	CloseDb(db);


if sys.argv[0] == __file__ :
	Excel2Sqlite()



