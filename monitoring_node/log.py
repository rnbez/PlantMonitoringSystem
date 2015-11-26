import datetime

current_state = ''
log_file = 'sys.log'
def log_error(msg):
    global log_file
    f = open(log_file, 'a')
    date = datetime.datetime.now().isoformat()
    f.write("Error @ {}\n*******\n{}\n\n".format(date, msg))
    f.close()

def log_info(msg):
    global log_file
    f = open(log_file, 'a')
    date = datetime.datetime.now().isoformat()
    f.write("Info @ {}\n*******\n{}\n\n".format(date, msg))
    f.close()
