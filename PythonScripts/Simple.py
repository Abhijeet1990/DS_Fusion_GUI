# test simple
import sys


print('Argument List:' + str(sys.argv))
case = sys.argv[1]
_usecase = case.split('_')[0]
print(_usecase)