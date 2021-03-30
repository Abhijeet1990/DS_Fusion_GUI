from datetime import datetime
import sys

dateS = sys.argv[1]
dateE = sys.argv[2]

start = datetime.strptime(dateS, '%Y-%m-%d')
end = datetime.strptime(dateE, '%Y-%m-%d')

days = (end - start).days

print("Start date: ", start)
print("End date: ", end)
print("Days between: ", days)